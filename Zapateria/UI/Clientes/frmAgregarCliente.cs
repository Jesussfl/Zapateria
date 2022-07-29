using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.UI.Clientes
{
    public partial class frmAgregarCliente : Form
    {
        #region Instanciaciones
        Clases.Cliente coleccionClientes = new Clases.Cliente();
        Clases.Cliente nuevoCliente;
        #endregion

        //Constructor
        public frmAgregarCliente()
        {
            InitializeComponent();
            
        }

        //Metodos
        private void CargarDatos() //Metodo para cargar todos los datos necesarios
        {
            cbTipo.DataSource = coleccionClientes.TiposCedulas;
        }
        
        
        #region Eventos
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            nuevoCliente = new Clases.Cliente()
            {
                TipoCedulaCliente = cbTipo.Text,
                Cedula = txtCedulaRif.Texts,
                Nombre = txtNombre.Texts.ToUpper(),
                Apellido = txtApellido.Texts.ToUpper(),
                Telefono = txtTelefono.Texts,
                Direccion = txtDireccion.Texts.ToUpper(),
                FechaRegistro = DateTime.Now
            };
            nuevoCliente.CargarAtributos();
            this.Close();
        }

        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        } 
        #endregion
    }
}
