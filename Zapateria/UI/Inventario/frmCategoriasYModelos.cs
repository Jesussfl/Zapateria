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
        #region Instanciación

        Clases.Modelo coleccionModelos = new Clases.Modelo();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo nuevoModelo;
        Clases.Categoria nuevaCategoria; 
        #endregion

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
            //Categorias
            coleccionCategorias.LlenarComboBox(cbCategoria, "Select distinct idCategoria, nombreCategoria from categorias group by(nombreCategoria)", "idCategoria", "nombreCategoria");
            coleccionCategorias.LlenarComboBox(cbMarca, "Select distinct marca, idcategoria from categorias group by(marca)", "idCategoria", "marca");
            cbCategoria.ResetText();
            cbMarca.ResetText();

            //Cargar ComboBoxes
            //Modelos
            coleccionModelos.Buscar = $"Select * from modelos where idCategoria like '%{cbCategoriaModelo.SelectedValue}%'";

        }
        private void cargarDatosModelos()
        {
            coleccionModelos.CargarBuscar(coleccionModelos.Cargar);
           // coleccionModelos.nombreColumnas();
            coleccionModelos.AsignarBotones("Editar", "Editar", "Editar");

            //Ocultar columnas
            dataGridView1.Columns["indexer"].Visible = false;
            dataGridView1.Columns["idCategoria"].Visible = false;

        } 
        #endregion


        #region Eventos
        private void btnRegistrarModelo_Click(object sender, EventArgs e)
        {
            //Inserción de valores a los atributos de la nueva clase modelo
            nuevoModelo = new Clases.Modelo()
            {
                IdCategoria = (int)cbCategoriaModelo.SelectedValue,
                NombreModelo = txtModelo.Texts.ToUpper()
            };
            nuevoModelo.cargarAtributos();

            //Limpiar y Actualizar Campos
            cbCategoriaModelo.ResetText();
            coleccionModelos.CargarBuscar(coleccionModelos.Cargar);
        }
        private void btnRegistrarCategoria_Click(object sender, EventArgs e)
        {
            //Inserción de valores a los atributos de la nueva clase categoría
            nuevaCategoria = new Clases.Categoria()
            {
                NombreCategoria = cbCategoria.Text.ToUpper(),
                Marca = cbMarca.Text.ToUpper()
            };

            nuevaCategoria.cargarAtributos();
            nuevaCategoria.InsertarActualizarEliminar("asignar_ids", false, true);

            //Actualización de campos
            cargarDatosCategorias();
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
            coleccionModelos.Buscar = $"Select * from modelos where idCategoria like '%{cbCategoriaModelo.SelectedValue}%'";
            coleccionModelos.CargarBuscar(coleccionModelos.Buscar);
        }
        private void frmCategoriasYModelos_Load(object sender, EventArgs e)
        {
            cargarDatosCategorias();
            cargarDatosModelos();
            coleccionCategorias.LlenarComboBox(cbCategoriaModelo, "Select distinct idCategoria, nombreCategoria from categorias group by(nombreCategoria)", "idCategoria", "nombreCategoria");

        }
        #endregion


    }
}
