using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.Inventario;

namespace Zapateria.UI.Inventario
{
    public partial class frmCategoriasYModelos : Form
    {
        #region Instanciaciones

        Clases.Modelo coleccionModelos = new Clases.Modelo();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo nuevoModelo;
        Clases.Categoria nuevaCategoria; 
        #endregion

        bool existeColumna = false;
        //Constructor
        public frmCategoriasYModelos()
        {
            InitializeComponent();
            coleccionModelos.Grid = dataGridView1;
        }


        #region Métodos

        private void cargarDatosCategorias()
        {
            //Cargar ComboBoxes
            coleccionCategorias.LlenarComboBox(cbCategoria, "Select id, nombreCategoria from categorias group by(nombreCategoria)", "id", "nombreCategoria");
            coleccionCategorias.LlenarComboBox(cbMarca, "Select marca, idcategoria from categorias group by marca", "idCategoria", "marca");

            cbCategoria.ResetText();
            cbMarca.ResetText();


        }

        private void cargarDatosModelos()
        {
            coleccionCategorias.LlenarComboBox(cbCategoriaModelo, "Select id, idCategoria,nombreCategoria, concat_ws('-',idCategoria, nombreCategoria, marca) as categoria from categorias order by idCategoria", "idCategoria", "categoria");
            coleccionModelos.BuscarSQL = $"{coleccionModelos.CargarSQL} where c.idCategoria = {cbCategoriaModelo.SelectedValue}";
            coleccionModelos.Cargar(coleccionModelos.CargarSQL);


            if(existeColumna == false)
            {
                coleccionModelos.AsignarNombreColumnas();
                coleccionModelos.AsignarBotones("Editar", "Editar", "Editar");
                existeColumna = true;
            }
            ModificarColumnas();

        } 

        private void ModificarColumnas()
        {
            dataGridView1.Columns["Editar"].FillWeight = 30;
            dataGridView1.Columns["categoria"].FillWeight = 40;
        }

        #endregion


        #region Eventos

        private void frmCategoriasYModelos_Load(object sender, EventArgs e)
        {
            cargarDatosCategorias();
            cargarDatosModelos();

        }
        private void btnRegistrarModelo_Click(object sender, EventArgs e)
        {
            //Inserción de valores a los atributos de la nueva clase modelo
            nuevoModelo = new Clases.Modelo()
            {
                IdCategoria = (int)cbCategoriaModelo.SelectedValue,
                NombreModelo = txtModelo.Texts.ToUpper()
            };
            nuevoModelo.Insertar(nuevoModelo.InsertarSQL, true, false);
            nuevoModelo.Insertar("idgrupal", false, true);

            //Limpiar y Actualizar Campos
            cargarDatosCategorias();
            cargarDatosModelos();
            cbCategoriaModelo.ResetText();
            txtModelo.Texts = "";
            coleccionModelos.Cargar(coleccionModelos.CargarSQL);
        }
        private void btnRegistrarCategoria_Click(object sender, EventArgs e)
        {
            //Inserción de valores a los atributos de la nueva clase categoría
            nuevaCategoria = new Clases.Categoria()
            {
                NombreCategoria = cbCategoria.Text.ToUpper(),
                Marca = cbMarca.Text.ToUpper()
            };

            nuevaCategoria.Insertar(nuevaCategoria.InsertarSQL, true, true);
            nuevaCategoria.Insertar("asignar_ids", false, true);

            //Actualización de campos
            cargarDatosCategorias();
            cargarDatosModelos();

            cbCategoria.ResetText();
            cbMarca.ResetText();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbCategoriaModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            coleccionModelos.BuscarSQL = $"{coleccionModelos.CargarSQL} where c.idCategoria = {cbCategoriaModelo.SelectedValue}";
            coleccionModelos.Cargar(coleccionModelos.BuscarSQL);
        }

        #endregion


    }
}
