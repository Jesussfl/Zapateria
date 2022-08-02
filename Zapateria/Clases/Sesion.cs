using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Zapateria.Controles;
using Zapateria.UI.Sesion;

namespace Zapateria.Clases
{
    class Sesion : Database //Herencia
    {

        Empleado empleados = new Empleado();

        #region Atributos

        private string correo;
        private string contraseña;
        public string Correo { get => correo; set => correo = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; } 

        #endregion


        //Constructor
        public Sesion()
        {
            correo = Correo;
            contraseña = Contraseña;
        }



        #region Métodos
        public override MySqlParameter[] ParametrizarAtributos()
        {
            return null;
        }
        public bool ValidarEmail(string email) //Método para verificar que el correo esté correcto
        {
            var correoCortado = email.Trim();

            if (correoCortado.EndsWith("."))
            {
                MessageBox.Show("El correo no es válido");
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == correoCortado;
            }
            catch
            {
                MessageBox.Show("El correo no es válido");
                return false;
            }
        }
        public bool ValidarContraseña(string contraseña)
        {
            var input = contraseña;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("La contraseña no puede estar vacía");
            }

            var tieneNumero = new Regex(@"[0-9]+");
            var tieneCaracterMayusculas = new Regex(@"[A-Z]+");
            var tieneCaracterMINyMAX = new Regex(@".{8,15}");
            var tieneCaracterMinusculas = new Regex(@"[a-z]+");
            var tieneCaracterEspecial = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!tieneCaracterMinusculas.IsMatch(input))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra minúscula.");
                return false;
            }
            else if (!tieneCaracterMayusculas.IsMatch(input))
            {
                MessageBox.Show("La contraseña debe contener al menos una contraseña mayúscula.");
                return false;
            }
            else if (!tieneCaracterMINyMAX.IsMatch(input))
            {
                MessageBox.Show("La contraseña no puede ser menor a 8 o mayor a 15 carácteres.");
                return false;
            }
            else if (!tieneNumero.IsMatch(input))
            {
                MessageBox.Show("La contraseña debe contener al menos un valor númerico.");
                return false;
            }

            else if (!tieneCaracterEspecial.IsMatch(input))
            {
                MessageBox.Show("La contraseña debe contener un carácter especial.");
                return false;
            }
            else
            {
                return true;
            }
        } //Método para validar que la contraseña esté con buen formato
        public bool ValidarUsuario()
        {
            try
            {
                Conexion.Open();
                BuscarSQL = $"select * from empleados where correo = '{correo}' and contraseña = '{contraseña}'";
                MySqlDataAdapter sda = new MySqlDataAdapter(BuscarSQL, Conexion);
                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("Hay Datos Incorrectos", "Datos Incorrectos", MessageBoxButton.OK);
                    return false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Conexion.Close();
            }
        }
        public void IniciarSesion(frmIniciarSesion frm) //Método para Iniciar sesion
        {

            Inicio frmInicio = new Inicio();
            frmInicio.nombreUsuario.Text = empleados.ExtraerEmpleado(correo);
            frmInicio.Show();
            frm.Hide();
        } 

        #endregion
    }
}
