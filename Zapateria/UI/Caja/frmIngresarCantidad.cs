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
    public partial class frmIngresarCantidad : Form
    {
        public bool resultado = false;
        public frmIngresarCantidad()
        {
            InitializeComponent();
        }
        public string GetMyResult()
        {
            return txtCantidad.Text;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            resultado = false;
            this.Close();
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            resultado = true;
            GetMyResult();
            this.Close();
        }

        private void btnMas_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = (Convert.ToInt32(txtCantidad.Text) + 1).ToString();
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCantidad.Text) > 1)
            {
                txtCantidad.Text = (Convert.ToInt32(txtCantidad.Text) - 1).ToString();
            }

        }
    }
}
