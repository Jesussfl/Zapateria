using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Zapateria
{
    public class Database
    {
        #region Atributos
        //Atributos
        private string[] columnas;
        private string cargar;
        private string cargarEditar;
        private string eliminar;
        private string buscar;
        private string sqlCombo;
        private MySqlParameter[] parametros;

        private MySqlConnection conexion = new MySqlConnection("server=localhost; uid=root; password=13122002b; database=zapateria; port=3306");
        #endregion

        #region Encapsulamiento
        public string[] Columnas { get => columnas; set => columnas = value; }
        public string Cargar { get => cargar; set => cargar = value; }
        public string CargarEditar { get => cargarEditar; set => cargarEditar = value; }
        public string Eliminar { get => eliminar; set => eliminar = value; }
        public string Buscar { get => buscar; set => buscar = value; }
        public string SqlCombo { get => sqlCombo; set => sqlCombo = value; }
        public DataGridView Grid { get; set; }
        public MySqlParameter[] Parametros { get => parametros; set => parametros = value; }
        #endregion

        #region Métodos
        public void CargarBuscar(string consulta) //Método para cargar y buscar en la base de datos
        {

            conexion.Open();
            MySqlCommand cm = new MySqlCommand(consulta, conexion);

            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();

            da.Fill(dt);
            Grid.DataSource = dt;
            conexion.Close();

        }
        public void AsignarNombreColumnas() //Método para cambiar el nombre de las columnas
        {

            
            int i = 0;
            foreach (string column in columnas)
            {
                Grid.Columns[i].HeaderText = column;
                i += 1;
            }
        }
        public void InsertarActualizarEliminar(string consulta, bool message = true, bool procedure = false) //Método para insertar, actualizar y eliminar en la base de datos
        {
            
            try
            {

                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                if (procedure == true) { cmd.CommandType = CommandType.StoredProcedure; }

                cmd.Parameters.AddRange(parametros);
                cmd.ExecuteNonQuery();
                conexion.Close();

                if (message == true) { MessageBox.Show("Se han actualizado los cambios"); }
  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AsignarBotones(string nombreBase, string nombreHeader, string textoBotón) //Método para agregar una columna con botones
        {
            DataGridViewButtonColumn columnaBoton = new DataGridViewButtonColumn();
            columnaBoton.Name = nombreBase;
            columnaBoton.HeaderText = nombreHeader;
            columnaBoton.Text = textoBotón;
            columnaBoton.UseColumnTextForButtonValue = true;
            columnaBoton.FlatStyle = FlatStyle.Flat;
            columnaBoton.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnaBoton.Width = 50;
            columnaBoton.DefaultCellStyle.ForeColor = Clases.Colores.primary;
            Grid.Columns.Add(columnaBoton);


        }
        public void LlenarComboBox(ComboBox comboBox, string consulta, string value, string display) //Metodo para cargar items a combobox
        {
            
            MySqlCommand cm = new MySqlCommand(consulta, conexion);

            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();

            da.Fill(dt);

            comboBox.ValueMember = value;
            comboBox.DisplayMember = display;
            comboBox.DataSource = dt;

        } 
        #endregion

    }
}
