using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.UI.Caja
{
    public partial class frmTarjeta : Form
    {
        formCaja main;

        public bool resultado = false;
        public string metodoPago = "TARJETA";
        public frmTarjeta(formCaja frm)
        {
            InitializeComponent();
            main = frm;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            resultado = false;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            resultado = false;
            this.Close();
        }

        private void frmPunto_Load(object sender, EventArgs e)
        {
            lblSubTotal.Text = main.lblSubTotal.Text;
            lblTotal.Text = main.lblTotal.Text;
            lblCliente.Text = main.lblCliente.Text;
            txtCedula.Texts = main.cedulaCliente;
        }

        private void lblCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quiere registrar la venta?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                resultado = true;
                this.Close();
            }

        }
    }
}
