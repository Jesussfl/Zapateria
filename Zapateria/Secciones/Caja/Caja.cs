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
        Database productosDB = new Database();
        public Caja()
        {
            InitializeComponent();

        }
        private void Caja_Load(object sender, EventArgs e)
        {
            string[] nombres = { "Código","Marca","Talla","Color","Precio de Venta" };
            productosDB.cargarGrid(dataGridView1, "SELECT idProducto, marca, talla, color, precioVenta FROM inventario;", nombres);
            string[] nombresGrid2 = { "Cliente", "Total", "Ganancia", "Metodo" };
            productosDB.cargarGrid(dataGridView2, "SELECT ciCliente, montoTotal, ganancia, metodoPago FROM ventas;", nombresGrid2);
            
        }

        private void busCliente_Enter(object sender, EventArgs e)
        {
            misControles.añadirPlaceholder(busCliente, "Buscar Cliente");
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

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            busCliente.Clear();
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == false) { busCliente.Text = "Buscar Cliente";  }
        }

        private void busCliente_Leave_1(object sender, EventArgs e)
        {
            misControles.añadirPlaceholder(busCliente, "Buscar Cliente");

        }

        private void busCliente_Enter_1(object sender, EventArgs e)
        {
            misControles.añadirPlaceholder(busCliente, "Buscar Cliente");
        }

        private void busCliente_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == true || busCliente.Text == "Buscar Cliente") { clearTb.Visible = false; }
            else { clearTb.Visible = true; }

        }
    }
}
