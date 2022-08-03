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
    public partial class frmEditarProductos : Form
    {

        #region Instanciaciones

        Clases.Calzado objCalzado;

        Clases.Calzado coleccionCalzados = new Clases.Calzado();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo coleccionModelo = new Clases.Modelo();

        Clases.Controles controles = new Clases.Controles();

        #endregion

        #region Atributos del formulario
        private frmInventario frm;

        public string id; //Atributo que recoge el id del producto seleccionado 
        #endregion

        public frmEditarProductos(frmInventario frm)
        {
            InitializeComponent();
            this.frm = frm;   //Asignación a la variable con el formulario padre
        }


        #region Métodos
        private void CargarDatos() //Este metodo carga todos los datos necesarios del formulario
        {

            cbColor.DataSource = coleccionCalzados.Colores;

           coleccionCategorias.LlenarComboBox(cbCategoria, "Select id, concat_ws('-',idCategoria,nombreCategoria, marca) as categoria, nombreCategoria from categorias order by(idCategoria)", "id", "categoria");

            string categoria = coleccionCalzados.ObtenerCategoria(cbCategoria.Text);
            coleccionModelo.LlenarComboBox(cbModelo, $"Select id, nombreModelo from modelos where idCategoria = '%{categoria}%'", "id", "nombreModelo");

            LlenarCampos();
        } 
        private void LlenarCampos() //Este método extrae todos los datos del producto al cual se va a editar y carga con datos los controles de la interfaz
        {
            string consulta = $@"select inv.idProducto, concat_ws('-',ctg.idCategoria,ctg.nombreCategoria,marca) as categoria , mdl.nombreModelo as modelo,descripcion, tipoCalzado, talla, color, cantidad, precioVenta
                                from inventario inv
                                INNER JOIN categorias ctg ON (inv.idCategoria = ctg.idCategoria) 
                                LEFT JOIN modelos mdl ON (inv.idModelo = mdl.id and inv.idCategoria = mdl.idCategoria) 
                                where idProducto = {id}";

            cbCategoria.SelectedIndex = cbCategoria.FindStringExact(coleccionCalzados.ExtraerDato($"{consulta}", "categoria"));

            cbModelo.SelectedIndex = cbModelo.FindStringExact(coleccionCalzados.ExtraerDato($"{consulta}", "modelo"));

            txtDescripcion.Texts = coleccionCalzados.ExtraerDato($"{consulta}", "descripcion");

            cbSexo.SelectedIndex = cbSexo.FindStringExact(coleccionCalzados.ExtraerDato($"{consulta}", "tipoCalzado"));

            cbTalla.SelectedIndex = cbTalla.FindStringExact(coleccionCalzados.ExtraerDato($"{consulta}", "talla"));

            cbColor.SelectedIndex = cbColor.FindStringExact(coleccionCalzados.ExtraerDato($"{consulta}", "color"));

            txtStock.Text = coleccionCalzados.ExtraerDato($"{consulta}", "cantidad");

            txtPrecioVenta.Text = coleccionCalzados.ExtraerDato($"{consulta}", "precioVenta");
        }
        #endregion

        #region Eventos Principales

        private void frmAgregarProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {

            objCalzado = new Clases.Calzado() //Nueva instancia del calzado
            {
                CodigoModelo = cbModelo.SelectedValue.ToString(),
                Descripcion = txtDescripcion.Texts.ToUpper(),
                TipoCalzado = cbSexo.Text,
                Talla = int.Parse(cbTalla.Text),
                Color = cbColor.Text,
                Cantidad = int.Parse(txtStock.Text),
                PrecioProducto = double.Parse(txtPrecioVenta.Text)
            };

            objCalzado.CodigoCategoria = objCalzado.ObtenerCategoria(cbCategoria.Text);
            objCalzado.Codigo = objCalzado.AsignarCódigo();

            objCalzado.Actualizar($"{objCalzado.ActualizarSQL} where idProducto = {id}");

 //---------------------------------------------------------------------------------------------
            //Validación en caso de algún error
            if(objCalzado.HayError == true)
            {
                if(objCalzado.NumeroError == 1062)
                {
                    MessageBox.Show("Ya existe este calzado el inventario");

                }
                else
                {
                    MessageBox.Show("Uy, Parece que hubo un problema, revise los datos que ingresó");

                }

            }
            else
            {
            frm.CargarDatos();
            this.Close();

            }
//-----------------------------------------------------------------------------------------------------
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar este producto del inventario?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                coleccionCalzados.Eliminar(coleccionCalzados.EliminarSQL + id);
                frm.CargarDatos();
                this.Close();
            }
        }
        #endregion

        #region Eventos Normales

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

        private void txtContraseñaConfirmar_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);

        }
        private void btnNuevaCategoria_Click_1(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            popup.FormClosed += new FormClosedEventHandler(categorias_FormClosed);
   
            controles.mostrarPopup(popup);
        }
        private void categorias_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {

            CargarDatos();

        }


        private void btnNuevoModelo_Click_1(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            popup.FormClosed += new FormClosedEventHandler(modelos_FormClosed);

            controles.mostrarPopup(popup);
        }
        private void modelos_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {

            CargarDatos();

        }
        #endregion

    }
}
