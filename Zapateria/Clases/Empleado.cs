using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    public class Empleado : Database //Herencia
    {

        #region Atributos

        private string cedula;
        private string tipoCedulaEmpleado;
        private string nombres;
        private string apellidos;
        private string telefono;
        private string correo;
        private string contraseña;

        private string[] tiposCedulas = new string[] { "V", "E", "J" };

        #endregion


        #region Encapsulamiento

        public string Cedula { get => cedula; set => cedula = value; }
        public string TipoCedulaEmpleado { get => tipoCedulaEmpleado; set => tipoCedulaEmpleado = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string[] TiposCedulas { get => tiposCedulas; set => tiposCedulas = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; } 

        #endregion

        //Constructor
        public Empleado()
        {
            CargarSQL = "Select concat_ws('. ',tipoCedula,ciEmpleado) as cedula,  concat_ws(' ',nombres, apellidos) as empleado, telefono, correo From empleados";

            Columnas = new string[] { "Cédula", "Empleado", "Teléfono", "Correo" };

            InsertarSQL = @"insert into empleados (ciEmpleado, tipoCedula, nombres, apellidos, telefono, correo, contraseña) 
                                values (@ciEmpleado, @tipoCedula, @nombres, @apellidos, @telefono, @correo, @contraseña)";

            BuscarSQL = $"{CargarSQL} where concat(ciEmpleado, tipoCedula, nombres, apellidos) like";

        }

        #region Métodos
        public void AutoCompletar(TextBox textbox)
        {
            Conexion.Open();
            MySqlCommand cm = new MySqlCommand("select ciEmpleado, concat(' ',nombres,apellidos) as empleado from empleados", Conexion);
            MySqlDataReader sdr = cm.ExecuteReader();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            while (sdr.Read())
            {
                lista.Add(sdr.GetString("ciEmpleado"));
                lista.Add(sdr.GetString("empleado"));
            }
            textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textbox.AutoCompleteCustomSource = lista;
            Conexion.Close();

        }

        public string ExtraerEmpleado(string correo) //Método es para extraer el nombre del cliente
        {
            Conexion.Open();
            MySqlCommand cm = new MySqlCommand($"select concat_ws(' ',nombres,apellidos) as empleado from empleados where correo = '{correo}'", Conexion);
            MySqlDataReader sdr = cm.ExecuteReader();

            while (sdr.Read())
            {
                return sdr.GetValue(0).ToString();
            }

            return "No existe";

        }

        public override MySqlParameter[] ParametrizarAtributos()
        {
            Parametros = new MySqlParameter[]
             {
                new MySqlParameter("@ciEmpleado", cedula),
                new MySqlParameter("@tipoCedula", tipoCedulaEmpleado),
                new MySqlParameter("@nombres", nombres),
                new MySqlParameter("@apellidos", apellidos),
                new MySqlParameter("@telefono", telefono),
                new MySqlParameter("@correo", correo),
                new MySqlParameter("@contraseña", contraseña)
             };
            return Parametros;
        }
        #endregion
    }
}
