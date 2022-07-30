using DocumentFormat.OpenXml.Packaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Zapateria
{
    public class Database
    {
        #region Atributos
        //Atributos
        private string[] columnas;
        private string cargarSQL;
        private string insertarSQL;
        private string eliminarSQL;
        private string buscarSQL;
        private string sqlCombo;
        private MySqlParameter[] parametros;

        private MySqlConnection conexion = new MySqlConnection("server=localhost; uid=root; password=13122002b; database=zapateria; port=3306");
        #endregion
        //pscale_pw_0lQdoePNHCUe1bJlJ6USh9PxMCAwLY6sGwXR_hSiITw
        #region Encapsulamiento
        public string[] Columnas { get => columnas; set => columnas = value; }
        public string CargarSQL { get => cargarSQL; set => cargarSQL = value; }
        public string InsertarSQL { get => insertarSQL; set => insertarSQL = value; }
        public string EliminarSQL { get => eliminarSQL; set => eliminarSQL = value; }
        public string BuscarSQL { get => buscarSQL; set => buscarSQL = value; }
        public string SqlCombo { get => sqlCombo; set => sqlCombo = value; }
        public DataGridView Grid { get; set; }
        public MySqlParameter[] Parametros { get => parametros; set => parametros = value; }
        public MySqlConnection Conexion { get => conexion; set => conexion = value; }
        #endregion

        #region Métodos
        public void Cargar(string consulta) //Método para cargar y buscar en la base de datos
        {

            conexion.Open();
            MySqlCommand cm = new MySqlCommand(consulta, conexion);

            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();

            da.Fill(dt);
            Grid.DataSource = dt;
            conexion.Close();

        }

        //Método para insertar, actualizar y eliminar en la base de datos
        public void InsertarActualizarEliminar(string consulta, bool IncluyeMensaje = true, bool EsProcedimiento = false, bool IncluyeParametros = true) 
        {
            
            try
            {

                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);

                if (EsProcedimiento == true) { cmd.CommandType = CommandType.StoredProcedure; }

                if (IncluyeParametros)
                {
                    cmd.Parameters.AddRange(parametros);

                }
                cmd.ExecuteNonQuery();
                conexion.Close();

                if (IncluyeMensaje == true) { MessageBox.Show("Se han actualizado los cambios"); }
  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        public void AsignarNombreColumnas() //Método para cambiar el nombre de las columnas
        {

            
            int i = 0;
            foreach (string column in columnas)
            {
                Grid.Columns[i].HeaderText = column;
                i += 1;
            }
        }
        public void AsignarBotones(string nombreBase, string nombreHeader, string textoBotón, bool tipo = false) //Método para agregar una columna con botones
        {
            if (tipo == false)
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
                columnaBoton.ReadOnly = false;

                Grid.Columns.Add(columnaBoton);
            }
            else
            {
                DataGridViewCheckBoxColumn columnaCheck = new DataGridViewCheckBoxColumn();
                columnaCheck.Name = nombreBase;
                columnaCheck.HeaderText = nombreHeader;
                columnaCheck.ReadOnly = false;

                columnaCheck.FillWeight = 40;
                columnaCheck.Width = 50;
                columnaCheck.DefaultCellStyle.ForeColor = Clases.Colores.primary;

                Grid.Columns.Add(columnaCheck);

            }



        }
        public void GenerarReporteSencillo(string document, string[,] datos)
        {
            // To search and replace content in a document part.
            using (SaveFileDialog sfd = new SaveFileDialog() {Filter = "Word |*.docx"})
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    using (WordprocessingDocument wordDoc = WordprocessingDocument.CreateFromTemplate(document, false))
                    {
                        string docText = null;
                        string buscar, reemplazar;
                        int j = 0;
                        for (int i = 0; i <= datos.GetUpperBound(1) + 1; i++)
                        {
                            while(j == 0)
                            {
                                buscar = datos[i, j + 1];
                                reemplazar = datos[i, j];

                                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                                {
                                    docText = sr.ReadToEnd();
                                }

                                Regex regexText = new Regex(buscar);
                                docText = regexText.Replace(docText, reemplazar);

                                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                                {
                                    sw.Write(docText);
                                }
                                j = 1;
                            }
                            j = 0;

                        }
                        WordprocessingDocument word = (WordprocessingDocument)wordDoc.SaveAs(sfd.FileName);
                        word.Close();
                        wordDoc.Close();

                    }
                    if (System.IO.File.Exists(sfd.FileName) == true)
                    {
                        Process.Start(sfd.FileName);
                    }
                    else
                    {
                        MessageBox.Show("El archivo no existe");
                    }
                }
            }
            
        }

        #endregion

    }
}
