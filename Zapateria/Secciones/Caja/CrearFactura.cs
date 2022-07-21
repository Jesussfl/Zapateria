using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Secciones.Caja
{
    public partial class CrearFactura : Form
    {
        Clases.Controles misControles = new Clases.Controles();

        public CrearFactura()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void busProducto_Enter(object sender, EventArgs e)
        {
            misControles.añadirPlaceholder(busProducto, "Buscar Producto");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            busProducto.Clear();
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto"; }
        }

        private void busProducto_Leave(object sender, EventArgs e)
        {
            misControles.añadirPlaceholder(busProducto, "Buscar Producto");

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
