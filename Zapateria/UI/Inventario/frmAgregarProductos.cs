using MySql.Data.MySqlClient;
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
using Zapateria.UI.Inventario;

namespace Zapateria.Secciones.Inventario
{
    public partial class frmAgregarProductos : Form
    {

        #region Instanciaciones

        Clases.Calzado objCalzado;

        Clases.Calzado coleccionCalzados = new Clases.Calzado();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo coleccionModelo = new Clases.Modelo();

        Clases.Controles controles = new Clases.Controles();

        #endregion
        
        private frmInventario frm;


        public frmAgregarProductos(frmInventario frm) //Constructor
        {
            InitializeComponent();
            this.frm = frm;   //Asignación a la variable con el formulario padre
        }


        #region Métodos
        private string obtenerCategoria()
        {
            string[] extractor = cbCategoria.Text.Split('-');

            return extractor[0];
        }

        private void cargarDatos()
        {

            cbColor.DataSource = coleccionCalzados.Colores;
            coleccionCategorias.LlenarComboBox(cbCategoria, "Select id, concat_ws('-',idCategoria,nombreCategoria, marca) as categoria, nombreCategoria from categorias order by(idCategoria)", "id", "categoria");
            
            coleccionModelo.LlenarComboBox(cbModelo, $"Select id, nombreModelo from modelos where idCategoria = '%{obtenerCategoria()}%'", "id", "nombreModelo");
        } 
        #endregion



        #region Eventos Principales

        private void frmAgregarProductos_Load(object sender, EventArgs e)
        {
            cargarDatos();

        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {

            objCalzado = new Clases.Calzado()
            {
                CodigoCategoria = obtenerCategoria(),
                CodigoModelo = cbModelo.SelectedValue.ToString(),
                Descripcion = txtDescripcion.Texts,
                TipoCalzado = cbSexo.Text,
                Talla = int.Parse(cbTalla.Text),
                Color = cbColor.Text,
                Cantidad = int.Parse(txtStock.Texts),
                PrecioProducto = double.Parse(txtPrecioVenta.Texts)
            };
            objCalzado.Codigo = objCalzado.GenerarCodigo();

            objCalzado.CargarAtributos();

            frm.CargarDatos();

           this.Close();
        }
        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Close();

            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            controles.mostrarPopup(popup);

        }
        private void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            controles.mostrarPopup(popup);
            Close();

        }
        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] extractor = cbCategoria.Text.Split('-');
            coleccionModelo.LlenarComboBox(cbModelo, $"Select id, nombreModelo from modelos where idCategoria like '%{extractor[0]}%'", "id", "nombreModelo");

        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
