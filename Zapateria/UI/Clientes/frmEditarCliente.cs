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
    public partial class frmEditarCliente : Form
    {
        #region Instanciaciones
        Clases.Cliente coleccionClientes = new Clases.Cliente();
        Clases.Cliente nuevoCliente;
        #endregion

        public string cedulaCliente; //Atributo que guarda la cedula del cliente seleccionado a editar
        //Constructor
        public frmEditarCliente()
        {
            InitializeComponent();
            
        }


        #region Métodos
        private void CargarDatos() //Metodo para cargar todos los datos necesarios
        {
            cbTipo.DataSource = coleccionClientes.TiposCedulas;
        
            string consulta = "Select ciCliente, concat_ws('. ',tipoCedula,ciCliente) as cedula,  concat_ws(' ',nombre, apellido) as cliente, nombre,apellido, telefono, direccion, fechaRegistro From clientes";
            txtCedulaRif.Texts = cedulaCliente;
            txtCedulaRif.Enabled = false;
            txtNombre.Texts = coleccionClientes.ExtraerDato(consulta, "nombre");
            txtApellido.Texts = coleccionClientes.ExtraerDato(consulta, "apellido");
            txtDireccion.Texts = coleccionClientes.ExtraerDato(consulta, "direccion");
            txtTelefono.Texts = coleccionClientes.ExtraerDato(consulta, "telefono");
        } 
        #endregion


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
            nuevoCliente.Actualizar($"{nuevoCliente.ActualizarSQL} where ciCliente = {cedulaCliente}");
            this.Close();
        }

        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        } 
        #endregion
    }
}
