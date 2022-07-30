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
        bool existeColumna = false;
        public void cargarDatos() //Función dedicada a rellenar las filas del datagridview
        {


            inventarioDB.Cargar(inventarioDB.CargarSQL);
            if (existeColumna == false)
            {
                inventarioDB.AsignarNombreColumnas();
                inventarioDB.AsignarBotones("editar", "Editar", "Editar");
                existeColumna = true;
            }
           
            dataGridView1.Columns["idProducto"].FillWeight = 35;
            dataGridView1.Columns["talla"].FillWeight = 20;
            dataGridView1.Columns["color"].FillWeight = 50;
            dataGridView1.Columns["precioVenta"].FillWeight = 30;
            dataGridView1.Columns["cantidad"].FillWeight = 30;
            dataGridView1.Columns["tipoCalzado"].FillWeight = 45;
            dataGridView1.Columns["costeTotal"].FillWeight = 30;
            dataGridView1.Columns["editar"].FillWeight = 30;



        }


        #region Busqueda
        private void clearTb_Click(object sender, EventArgs e) //Botón de limpiar busqueda
        {
            busProducto.Clear();
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto"; }
        }
        private void busCliente_TextChanged(object sender, EventArgs e) //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar
        {


            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == true || busProducto.Text == "Buscar Producto") 
            { 
                clearTb.Visible = false;
                inventarioDB.Cargar(inventarioDB.CargarSQL);
            }
            else 
            { 
                clearTb.Visible = true; 
                inventarioDB.Cargar($"{inventarioDB.BuscarSQL} '%{busProducto.Text}%'"); 
            }

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

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                inventarioDB.Cargar($"{inventarioDB.BuscarSQL} '%{busProducto.Text}%'"); //Se llama el atributo con el query y se le asigna el valor del buscador}
            }
        }

        #endregion

        #region Eventos


        public void Inventario_Load(object sender, EventArgs e)
        {
          
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }
        private void cbTallas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbTallas.SelectedItem.ToString() == "Todas")
            {
                inventarioDB.Cargar(inventarioDB.CargarSQL);

            }
            else
            {
                inventarioDB.BuscarSQL = $"{inventarioDB.CargarSQL} WHERE  talla = {cbTallas.SelectedItem}";
                inventarioDB.Cargar(inventarioDB.BuscarSQL);
            }
            
        }
        #endregion

    }
}
