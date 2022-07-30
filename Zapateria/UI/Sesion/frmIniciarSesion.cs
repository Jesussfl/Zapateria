using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.UI.Sesion
{
    public partial class frmIniciarSesion : Form
    {

        Clases.Controles controles = new Clases.Controles();
        public frmIniciarSesion()
        {
            InitializeComponent();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmIniciarSesion_Load(object sender, EventArgs e)
        {
            this.ActiveControl = btnFocus;
    

        }
   
            private void txtContraseña_Enter(object sender, EventArgs e)
        {
            controles.añadirPlaceholderInputs(txtContraseña, "Contraseña");

        }

        private void txtCorreo_Enter(object sender, EventArgs e)
        {
            controles.añadirPlaceholderInputs(txtCorreo, "Correo Electrónico");

        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            controles.añadirPlaceholderInputs(txtCorreo, "Correo Electrónico");

        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            controles.añadirPlaceholderInputs(txtContraseña, "Contraseña");

        }
    }
}
