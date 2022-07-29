using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.UI.Empleados;

namespace Zapateria.Secciones.Empleados
{
    public partial class Empleados : Form
    {
        Clases.Empleado empleados = new Clases.Empleado();
        Clases.Controles controles = new Clases.Controles();

        public Empleados()
        {
            InitializeComponent();
            empleados.Grid = dataGridView1;

            //Asignacion de color de bordes a botones de paginacion
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            empleados.Cargar(empleados.CargarSQL);
            empleados.AsignarNombreColumnas();
            empleados.AutoCompletar(busEmpleado);
        }
        private void busEmpleado_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(busEmpleado.Text) && busEmpleado.Focused == true || busEmpleado.Text == "Buscar Cliente")
            {
                clearTb.Visible = false;
                empleados.Cargar(empleados.CargarSQL);
            }
            else
            {
                clearTb.Visible = true;
                empleados.Cargar($"{empleados.BuscarSQL} '%{busEmpleado.Text}%'");
            }
        }

        private void busEmpleado_Leave(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busEmpleado, "Buscar Empleado");
        }

        private void busEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                empleados.Cargar($"{empleados.BuscarSQL} '%{busEmpleado.Text}%'"); //Se llama el atributo con el query y se le asigna el valor del buscador}
            }
        }

        private void busEmpleado_Enter(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busEmpleado, "Buscar Empleado");
        }

        private void clearTb_Click(object sender, EventArgs e)
        {
            busEmpleado.Clear();
            if (string.IsNullOrWhiteSpace(busEmpleado.Text) && busEmpleado.Focused == false) { busEmpleado.Text = "Buscar Cliente"; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarEmpleado popup = new frmAgregarEmpleado();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {
            CargarDatos();
        }
    }
}
