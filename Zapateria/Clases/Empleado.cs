using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    public class Empleado : Database
    {
        #region Atributos
        private string cedula;
        private string tipoCedulaEmpleado;
        private string nombres;
        private string apellidos;
        private string direccion;
        private string telefono;
        private string horario;
        private int ventasRealizadas;

        private string[] tiposCedulas = new string[] { "V", "E", "J" };

        public string Cedula { get => cedula; set => cedula = value; }
        public string TipoCedulaEmpleado { get => tipoCedulaEmpleado; set => tipoCedulaEmpleado = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Horario { get => horario; set => horario = value; }
        public int VentasRealizadas { get => ventasRealizadas; set => ventasRealizadas = value; }
        public string[] TiposCedulas { get => tiposCedulas; set => tiposCedulas = value; }
        #endregion
        public Empleado()
        {
            CargarSQL = "Select concat_ws('. ',tipoCedula,ciEmpleado) as cedula,  concat_ws(' ',nombres, apellidos) as empleado, direccion, telefono, horario, ventasRealizadas From empleados";
            Columnas = new string[] { "Cédula", "Empleado", "Dirección","Teléfono", "Horario", "Ventas Realizadas" };
            InsertarSQL = @"insert into empleados (ciEmpleado, tipoCedula, nombres, apellidos, direccion, telefono, horario, ventasRealizadas) 
                                values (@ciEmpleado, @tipoCedula, @nombres, @apellidos, @telefono, @direccion, @horario, @ventasRealizadas)";
            BuscarSQL = $"{CargarSQL} where concat(ciEmpleado, tipoCedula, nombres, apellidos) like";

        }
        public void AutoCompletar(System.Windows.Forms.TextBox textbox)
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
        public string ExtraerCliente()
        {
            Conexion.Open();
            MySqlCommand cm = new MySqlCommand("select ciEmpleado, concat(' ',nombres,apellidos) as empleado from empleados", Conexion);
            MySqlDataReader sdr = cm.ExecuteReader();

            if (sdr.Read())
            {

                return sdr["ciEmpleado"].ToString();

            }
            else
            {

                return "Null";
            }

        }

        public void CargarAtributos()
        {
            Parametros = new MySqlParameter[]
                {
                new MySqlParameter("@ciEmpleado", cedula),
                new MySqlParameter("@tipoCedula", tipoCedulaEmpleado),
                new MySqlParameter("@nombres", nombres),
                new MySqlParameter("@apellidos", apellidos),
                new MySqlParameter("@telefono", telefono),
                new MySqlParameter("@direccion", direccion),
                new MySqlParameter("@horario", horario),
                new MySqlParameter("@ventasRealizadas", ventasRealizadas)
                };

            InsertarActualizarEliminar(InsertarSQL, true, false);
        }
    }
}
