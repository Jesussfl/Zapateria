using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Secciones.Empleados
{
    public partial class Empleados : Form
    {
        Clase_Empleados empleados = new Clase_Empleados();


        public Empleados()
        {
            InitializeComponent();
            empleados.Grid = dataGridView1;
            empleados.Columnas = new string[] { "Cédula", "Tipo De Cédula", "Nombres", "Apellidos", "Dirección", "Teléfono", "Horario" };
            empleados.Cargar = "Select * From empleados";
            //Asignacion de color de bordes a botones de paginacion
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            empleados.CargarBuscar(empleados.Cargar);
            empleados.AsignarNombreColumnas();

        }
    }
}
