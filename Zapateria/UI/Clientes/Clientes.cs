using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Secciones.Clientes
{
    public partial class Clientes : Form
    {
        Clase_Clientes clientes = new Clase_Clientes();

        public Clientes()
        {
            InitializeComponent();
            //Asignacion del grid a clase inventario
            clientes.Grid = dataGridView1;
            clientes.Cargar = "Select * From clientes";
            clientes.Columnas = new string[] { "Cédula", "Tipo De Cédula", "Nombre", "Apellido", "Teléfono", "Dirección", "Fecha de Registro", "Cantidad de Ventas" };

            //Asignacion de color de bordes a botones de paginacion
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            //Llamados para cargar el grid y sus columnas
            clientes.CargarBuscar(clientes.Cargar);
            clientes.AsignarNombreColumnas();
        }
    }
}
