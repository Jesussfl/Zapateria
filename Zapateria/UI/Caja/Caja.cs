using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Caja
{
    public partial class Caja : Form
    {
       Clases.Controles misControles = new Clases.Controles();
        Clases.Calzado inventarioCaja = new Clases.Calzado();
        public Caja()
        {
            InitializeComponent();
            //inventarioCaja.SqlCargarGrid = "SELECT idProducto, marca, talla, color, precioVenta FROM inventario;";
            //inventarioCaja.Grid = dataGridView1;
        }
        private void Caja_Load(object sender, EventArgs e)
        {

            //Llamadas de métodos para modificar o agregar contenido de los datagrid
            //string[] nombres = { "Código","Marca","Talla","Color","Precio de Venta" };
            //inventarioCaja.cargarGrid(dataGridView1, inventarioCaja.SqlCargarGrid);
            //cajaDB.columnNombres(dataGridView1, nombres);

            //string[] nombresGrid2 = { "Cliente", "Total", "Ganancia", "Metodo" };
            //cajaDB.cargarGrid(dataGridView2, "SELECT ciCliente, montoTotal, ganancia, metodoPago FROM ventas;");
            //cajaDB.columnNombres(dataGridView2, nombresGrid2);

        }

        private void busProducto_Enter(object sender, EventArgs e)
        {
            //misControles.AñadirPlaceholder(busProducto, "Buscar Producto");

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //busProducto.Clear();
            //if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto"; }
        }

        private void btnAgregarPro_Click(object sender, EventArgs e)
        {
            Secciones.Caja.CrearFactura popup = new Secciones.Caja.CrearFactura();
            misControles.mostrarPopup(popup);
            //prueba.showForm(popup);
        }

        #region Control para Buscar Cliente
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

            //Botón de limpiar busqueda
            busCliente.Clear();
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == false) { busCliente.Text = "Buscar Cliente";  }
        }

        private void busCliente_Leave_1(object sender, EventArgs e)
        {
            //Añade el texto de ayuda al buscador

            misControles.añadirPlaceholder(busCliente, "Buscar Cliente");

        }

        private void busCliente_Enter_1(object sender, EventArgs e)
        {
            //Añade o quita el texto de ayuda al buscador

            misControles.añadirPlaceholder(busCliente, "Buscar Cliente");
        }

        private void busCliente_TextChanged(object sender, EventArgs e)
        {
            //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar

            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == true || busCliente.Text == "Buscar Cliente") { clearTb.Visible = false; }
            else { clearTb.Visible = true; }

        }
        #endregion

    }
}
