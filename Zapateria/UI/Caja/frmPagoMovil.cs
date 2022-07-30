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
    public partial class frmPagoMovil : Form
    {
        formCaja main;
      
        public bool resultado = false;
        public string metodoPago = "PAGO MOVIL";
        public frmPagoMovil(formCaja frm)
        {
            InitializeComponent();
            main = frm;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void frmPagoMovil_Load(object sender, EventArgs e)
        {
            lblSubTotal.Text = main.lblSubTotal.Text;
            lblTotal.Text = main.lblTotal.Text;
            lblCliente.Text = main.lblCliente.Text;
            txtCedula.Texts = main.cedulaCliente;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
        //    if (string.IsNullOrEmpty(txtReferencia.Texts) || string.IsNullOrEmpty(txtTelefono.Texts))
        //    {
        //        MessageBox.Show("Llene los campos necesarios", "Faltan Datos");

        //    }
             if (MessageBox.Show("¿Seguro que quiere registrar la venta?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                resultado = true;
                this.Close();
            }
        }
    }
}
