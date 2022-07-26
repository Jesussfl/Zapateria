using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Ventas
{

    public partial class Ventas : Form
    {
        Secciones.Ventas.Clase_Ventas ventas = new Secciones.Ventas.Clase_Ventas();

        public Ventas()
        {
            InitializeComponent();

            //Asignacion del grid a clase inventario
            ventas.Grid = dataGridView1;
            ventas.Cargar = "Select * From ventas";
            ventas.Columnas = new string[] { "ID Factura", "Fecha de Venta", "Cédula Cliente", "Producto", "Cantidad", "Empleado", "Total Pagado", "Ganancia", "Método de Pago" };

            //Asignacion de color de bordes a botones de paginacion
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            //Llamados para cargar el grid y sus columnas
            ventas.CargarBuscar(ventas.Cargar);
            ventas.AsignarNombreColumnas();

        }
    }
}
