using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    
    public class Cliente : Database//Herencia
    {
        #region Atributos

        private string cedula;
        private string tipoCedulaCliente;
        private string nombre;
        private string apellido;
        private string telefono;
        private string direccion;
        private DateTime fechaRegistro;

        private string[] tiposCedulas = new string[] { "V", "E", "J" };

        #endregion

        #region Encapsulamiento
        public string Cedula { get => cedula; set => cedula = value; }
        public string TipoCedulaCliente { get => tipoCedulaCliente; set => tipoCedulaCliente = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string[] TiposCedulas { get => tiposCedulas; set => tiposCedulas = value; }
        #endregion

        //Constructor
        public Cliente()
        {
            CargarSQL = "Select ciCliente, concat_ws('. ',tipoCedula,ciCliente) as cedula,  concat_ws(' ',nombre, apellido) as cliente, telefono, direccion, fechaRegistro From clientes";

            Columnas = new string[] { "cedula","Cédula", "Cliente", "Teléfono", "Dirección", "Fecha de Registro" };

            InsertarSQL = @"insert into clientes (ciCliente, tipoCedula, nombre, apellido, telefono, direccion, fechaRegistro) 
                                values (@ciCliente, @tipoCedula, @nombre, @apellido, @telefono, @direccion, @fechaRegistro)";

            BuscarSQL = $"{CargarSQL} where concat(ciCliente, tipoCedula, nombre, apellido) like";

            ActualizarSQL = "update clientes set ciCliente = @ciCliente, tipoCedula = @tipoCedula, nombre = @nombre, apellido = @apellido, telefono = @telefono, direccion = @direccion, fechaRegistro = @fechaRegistro";
        }


        #region Métodos
        public void AutoCompletar(System.Windows.Forms.TextBox textbox) //Metodo para autocompletar valores en textboxes
        {
            Conexion.Open();
            MySqlCommand cm = new MySqlCommand("select ciCliente, concat(' ',nombre,apellido) as cliente from clientes", Conexion);
            MySqlDataReader sdr = cm.ExecuteReader();
            AutoCompleteStringCollection lista = new AutoCompleteStringCollection();

            while (sdr.Read())
            {
                lista.Add(sdr.GetString("ciCliente"));
                lista.Add(sdr.GetString("cliente"));
            }
            textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textbox.AutoCompleteCustomSource = lista;
            Conexion.Close();

        }
        public override MySqlParameter[] ParametrizarAtributos()
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@ciCliente", cedula),
                new MySqlParameter("@tipoCedula", tipoCedulaCliente),
                new MySqlParameter("@nombre", nombre),
                new MySqlParameter("@Apellido", apellido),
                new MySqlParameter("@telefono", telefono),
                new MySqlParameter("@direccion", direccion),
                new MySqlParameter("@fechaRegistro", fechaRegistro)
            };
            return Parametros;
        }
        #endregion
    }
}
