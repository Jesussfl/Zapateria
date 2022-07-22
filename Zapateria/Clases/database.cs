using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria
{
    public class Database
    {
        public string server { get; set; }
        public string uid { get; set; }
        public string password { get; set; }
        public string dbname { get; set; }
        public string port { get; set; }

        private MySqlConnection conexion = new MySqlConnection("server=localhost; uid=root; password=13122002b; database=zapateria; port=3306");

        public void cargarGrid(DataGridView grid, string consulta, string [] nombres)
        {
            //Este metodo sirve para cargar y buscar en la base de datos

            conexion.Open();
            MySqlCommand cm = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();

            da.Fill(dt);

            grid.DataSource = dt;
            conexion.Close();
            columnNombres(grid, nombres);
        }
        private void columnNombres(DataGridView grid, string[] nombres)
        {
            int i = 0;
            foreach (string column in nombres) {
                grid.Columns[i].HeaderText = column;
                i += 1;
            }
        }
        private void manipularBD(string consulta)
        {
            //Este metodo sirve para insertar, actualizar y eliminar en la base de datos
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

    }
}
