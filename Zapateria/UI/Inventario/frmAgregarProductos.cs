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
        //Clases.Calzado coleccionCalzados = new Clases.Calzado();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo coleccionModelo = new Clases.Modelo();

        Clases.Controles controles = new Clases.Controles();
        //Formulario Padre
        private frmInventario frm;

        #endregion

        public frmAgregarProductos(frmInventario frm) //Constructor
        {
            InitializeComponent();
            this.frm = frm;   //Asignación a la variable con el formulario padre
        }

        #region Eventos
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            objCalzado = new Clases.Calzado()
            {
                Marca = cbMarca.Text,
                CodigoCategoria = cbCategoria.SelectedValue.ToString(),
                CodigoModelo = cbModelo.SelectedValue.ToString(),
                Descripcion = txtDescripcion.Texts,
                Talla = int.Parse(cbTalla.Text),
                Color = cbColor.Text,
                Cantidad = int.Parse(txtStock.Texts),
                PrecioProducto = double.Parse(txtPrecioVenta.Texts),
                CostePorProducto = double.Parse(txtCosteMercancia.Texts),
                Codigo = 324
            };
            objCalzado.cargarAtributos();
           frm.cargarDatos();

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
        private void frmAgregarProductos_Load(object sender, EventArgs e)
        {
            coleccionCategorias.LlenarComboBox(cbCategoria, "Select distinct idCategoria, nombreCategoria from categorias group by(nombreCategoria)", "idCategoria", "nombreCategoria");
            coleccionCategorias.LlenarComboBox(cbMarca, $"Select distinct idCategoria, marca from categorias where idCategoria like '%{cbCategoria.SelectedValue}%'", "idCategoria", "marca");
            coleccionModelo.LlenarComboBox(cbModelo, $"Select distinct id, nombreModelo from modelos where idCategoria like '%{cbCategoria.SelectedValue}%'", "id", "nombreModelo");

        }



        #endregion

    }
}
