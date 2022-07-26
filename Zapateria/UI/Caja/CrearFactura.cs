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
        Clases.Controles controles = new Clases.Controles();
        Clases.Calzado coleccionCalzado = new Clases.Calzado();
        public CrearFactura()
        {
            InitializeComponent();
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

        #endregion

        #region Busqueda
        private void clearTb_Click(object sender, EventArgs e)
        {
            //Botón de limpiar busqueda
            busProducto.Clear();
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto"; }
        }

        private void busProducto_TextChanged(object sender, EventArgs e)
        {
            //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == true || busProducto.Text == "Buscar Producto") { clearTb.Visible = false; }
            else { clearTb.Visible = true; }
        }

        private void busProducto_Leave_1(object sender, EventArgs e)
        {
            //Añade o quita el texto de ayuda al buscador
            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        }

        private void busProducto_KeyDown(object sender, KeyEventArgs e)
        {
            //coleccionCalzado.SqlBuscar = $"Select * from inventario where concat_ws(idProducto,nombreProducto,marca,talla,color,cantidad,precioCompra,precioVenta) like '%{busProducto.Text}%'";
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; }
        }

        private void busProducto_Enter_1(object sender, EventArgs e)
        {
            //Añade el texto de ayuda al buscador
            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        } 
        #endregion
    }
}
