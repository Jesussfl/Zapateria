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
        #region Instanciaciones

        Clases.Venta ventas = new Clases.Venta();
        Clases.Controles controles = new Clases.Controles();

        #endregion

        //Constructor
        public Ventas()
        {
            InitializeComponent();

            //Asignacion del grid a clase inventario
            ventas.Grid = dataGridView1;

        }

        #region Eventos Principales

        private void Ventas_Load(object sender, EventArgs e)
        {
            //Llamados para cargar el grid y sus columnas

            ventas.Cargar(ventas.CargarSQL);
            ventas.AsignarNombreColumnas();
            ventas.AjustarColumnas();

        }

        private void cbFiltrarFecha_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cbFiltrarFecha.SelectedItem.ToString())
            {
                case "Esta semana":
                    ventas.BuscarSQL = $"{ventas.CargarSQL} WHERE  YEARWEEK(`fechaVenta`, 1) = YEARWEEK(CURDATE(), 1)";
                    ventas.Cargar(ventas.BuscarSQL);
                    break;
                case "Hoy":
                    ventas.BuscarSQL = $"{ventas.CargarSQL} WHERE DATE(`fechaVenta`) = CURDATE()";
                    ventas.Cargar(ventas.BuscarSQL);
                    break;
                case "Este mes":
                    ventas.BuscarSQL = $"{ventas.CargarSQL} WHERE  MONTH(`fechaVenta`) = MONTH(CURDATE())";
                    ventas.Cargar(ventas.BuscarSQL);
                    break;
                case "Este año":
                    ventas.BuscarSQL = $"{ventas.CargarSQL} WHERE  YEAR(`fechaVenta`) = YEAR(CURDATE())";
                    ventas.Cargar(ventas.BuscarSQL);
                    break;
                case "Todos":
                    ventas.Cargar(ventas.CargarSQL);
                    break;
                default:
                    break;
            }

        }

        private void cbTallas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        { 
            string cantidad = "321";
            string periodo;
            if (cbFiltrarFecha.SelectedItem == null || cbFiltrarFecha.SelectedItem == "Todos")
            {
               periodo = "Desde los inicios";
            }
            else
            {
                periodo = cbFiltrarFecha.Text;
            }
            string[,] datos = { { DateTime.Now.ToString(), "date"},
                                { periodo, "lapse"},
                                { ventas.BuscarProductoMasVendido(), "bestProduct"},
                                { cantidad, "amount"}};

            string ruta = Environment.CurrentDirectory.Replace(@"\bin\Debug", @"\Resources\Reportes\Reporte-Sencillo (Template).docx");
            ventas.GenerarReporteSencillo(ruta, datos);
        } 

        #endregion

        #region Busqueda

        private void busVenta_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(busVenta.Text) && busVenta.Focused == true || busVenta.Text == "Buscar Venta")
            {
                clearTb.Visible = false;
                if (dataGridView1.RowCount > 0)
                {
                ventas.Cargar(ventas.CargarSQL);

                }
            }
            else
            {
                clearTb.Visible = true;
                if (dataGridView1.RowCount > 0)
                {
                    ventas.BuscarSQL = $"{ventas.CargarSQL} where concat_ws(idFactura,ciCliente, idProductos, metodoPago, referencias) like";
                    ventas.Cargar($"{ventas.BuscarSQL} '%{busVenta.Text}%'");
                }
            }
        }

        private void busVenta_Leave(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busVenta, "Buscar Venta");
        }

        private void busVenta_Enter(object sender, EventArgs e)
        {
            controles.añadirPlaceholder(busVenta, "Buscar Venta");
        }

        private void busVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ventas.Cargar($"{ventas.BuscarSQL} '%{busVenta.Text}%'"); //Se llama el atributo con el query y se le asigna el valor del buscador}
            }
        }

        private void clearTb_Click(object sender, EventArgs e)
        {
            busVenta.Clear();
            if (string.IsNullOrWhiteSpace(busVenta.Text) && busVenta.Focused == false) { busVenta.Text = "Buscar Venta"; }
        }

        #endregion

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ventas.Grid = dataGridView1;
            ventas.GenerarReporteExcel("ventas");
        }
    }
}
