using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.UI.Empleados
{
    public partial class frmAgregarEmpleado : Form
    {
        #region Instanciaciones
        Clases.Empleado coleccionEmpleados = new Clases.Empleado();
        Clases.Empleado nuevoEmpleado;
        #endregion
        public frmAgregarEmpleado()
        {
            InitializeComponent();
        }
        private void CargarDatos()
        {
            cbTipo.DataSource = coleccionEmpleados.TiposCedulas;

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            nuevoEmpleado = new Clases.Empleado()
            {
                TipoCedulaEmpleado = cbTipo.Text,
                Cedula = txtCedulaRif.Texts,
                Nombres = txtNombre.Texts.ToUpper(),
                Apellidos = txtApellido.Texts.ToUpper(),
                Telefono = txtTelefono.Texts,
                Direccion = txtDireccion.Texts.ToUpper(),
                Horario = txtHorario.Texts.ToUpper()
            };
            nuevoEmpleado.CargarAtributos();
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAgregarEmpleado_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
