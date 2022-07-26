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
using Zapateria.Clases;
using Zapateria.Secciones.Inventario;
using Zapateria.UI.Inventario;

namespace Zapateria.Inventario
{
    public partial class frmInventario : Form
    {
        #region Instanciaciones
        public Calzado inventarioDB { get; set; }

        Clases.Controles controles = new Clases.Controles();
        #endregion


        public frmInventario() //Constructor
        {
            InitializeComponent();

            inventarioDB = new Calzado();
            inventarioDB.Grid = dataGridView1; //Definición de atributos para la clase inventario
            cargarDatos();

            //Asignacion de color de bordes a botones de paginacion
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;

        }

        //Métodos
        public void cargarDatos() //Función dedicada a rellenar las filas del datagridview
        {


            inventarioDB.CargarBuscar(inventarioDB.Cargar);
            inventarioDB.AsignarNombreColumnas();
            inventarioDB.AsignarBotones("editar", "Editar", "Editar");
        }


        #region Eventos

        #region Busqueda
        private void clearTb_Click(object sender, EventArgs e) //Botón de limpiar busqueda
        {
            busProducto.Clear();
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto"; }
        }
        private void busCliente_TextChanged(object sender, EventArgs e) //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar
        {


            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == true || busProducto.Text == "Buscar Producto") { clearTb.Visible = false; }
            else { clearTb.Visible = true; }

        }
        private void busCliente_Leave(object sender, EventArgs e) //Añade o quita el texto de ayuda al buscador
        {

            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        }
        private void busCliente_Enter(object sender, EventArgs e) //Añade el texto de ayuda al buscador
        {

            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        }

        private void busProducto_KeyDown(object sender, KeyEventArgs e) //Buscar mediante codigo del producto, id de la categoria, tipo de calzado, la talla, el color, categoria, marca o modelo
        {

            inventarioDB.Buscar = $@"Select inv.*, ctg.nombreCategoria, ctg.marca, mdl.nombreModelo
                                        from inventario inv 
                                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.id) 
                                        INNER JOIN modelos mdl ON (inv.idModelo = mdl.indexer) 
                                        where concat_ws(idProducto,inv.idCategoria,tipoCalzado,talla,color,nombreCategoria,marca,nombreModelo) 
                                        like '%{busProducto.Text}%'";

            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; inventarioDB.CargarBuscar(inventarioDB.Buscar); }
        }

        #endregion

        public void Inventario_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["idCategoria"].Visible = false;
            dataGridView1.Columns["idModelo"].Visible = false;
        }
        private void btnAgregar_Click(object sender, EventArgs e) //Llamado del formulario para agregar productos
        {
            frmAgregarProductos popup = new frmAgregarProductos(this);
            controles.mostrarPopup(popup);
        }
        private void btnCategoriasModelos_Click(object sender, EventArgs e) //Llamado del formulario para agregar productos
        {

            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            controles.mostrarPopup(popup);
        }
        #endregion

    }
}
