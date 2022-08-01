using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.UI.Clientes;

namespace Zapateria.Secciones.Clientes
{
    public partial class Clientes : Form
    {
        #region Instanciaciones
        Clases.Cliente clientes = new Clases.Cliente();
        Clases.Controles controles = new Clases.Controles(); 
        #endregion

        bool existeColumna = false;

        //Constructor
        public Clientes()
        {
            InitializeComponent();
            //Asignacion del grid a clase inventario
            clientes.Grid = dataGridView1;

            //Asignacion de color de bordes a botones de paginacion
         
        }


        #region Métodos

        private void CargarDatos()
        {

            clientes.Cargar(clientes.CargarSQL);
            if (existeColumna == false)
            {
                clientes.AsignarNombreColumnas();
                clientes.AsignarBotones("editar", "Editar", "Editar");
                existeColumna = true;
            }
            clientes.AutoCompletar(busCliente);
        } 
        #endregion

        #region Eventos Principales
        private void Clientes_Load(object sender, EventArgs e)
        {
            //Llamados para cargar el grid y sus columnas
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarCliente popup = new frmAgregarCliente();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {
            CargarDatos();
        } 
        #endregion

        #region Busqueda
        private void clearTb_Click(object sender, EventArgs e)
        {
            busCliente.Clear();
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == false) { busCliente.Text = "Buscar Cliente"; }
        }

        private void busCliente_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == true || busCliente.Text == "Buscar Cliente")
            {
                clearTb.Visible = false;
                clientes.Cargar(clientes.CargarSQL);
            }
            else
            {
                clearTb.Visible = true;
                clientes.Cargar($"{clientes.BuscarSQL} '%{busCliente.Text}%'");
            }
        }

        private void busCliente_Leave(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busCliente, "Buscar Cliente");
        }

        private void busCliente_Enter(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busCliente, "Buscar Cliente");
        }

        private void busCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                clientes.Cargar($"{clientes.BuscarSQL} '%{busCliente.Text}%'"); //Se llama el atributo con el query y se le asigna el valor del buscador}
            }
        } 
        #endregion
    }
}
