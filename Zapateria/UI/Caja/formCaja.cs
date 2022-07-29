using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Zapateria.UI.Caja;
using Zapateria.UI.Clientes;

namespace Zapateria.UI.Caja
{
    public partial class formCaja : Form
    {

        #region Instanciaciones
        Clases.Controles controles = new Clases.Controles();
        Clases.Cliente clientes = new Clases.Cliente();
        Clases.Venta auxiliarVentas = new Clases.Venta();
        Clases.Venta historialVentas = new Clases.Venta();
        Clases.Venta nuevaVenta;
        #endregion


        #region Atributos
        public string cedulaCliente;
        private const double iva = 16;
        private double subTotal;
        private double total;
        private bool columnaExiste = false; 
        #endregion

        public formCaja() //Constructor
        {
            InitializeComponent();

            #region Constructor auxiliar de ventas
            auxiliarVentas.Grid = dataGridView1;
            auxiliarVentas.CargarSQL = @"select aux.detalle, aux.precioCalculado,aux.idProducto,aux.cantidad,concat_ws('-',ctg.nombreCategoria,ctg.marca,mdl.id) as producto, inv.color, inv.talla, inv.precioVenta
                                      from aux_ventas aux 
                                      inner join inventario inv on(aux.idProducto = inv.idProducto) inner join categorias ctg on (inv.idCategoria = ctg.id) inner join modelos mdl on (inv.idModelo = mdl.indexer)";

            auxiliarVentas.Columnas = new string[]
            {
                "detalle",
                "precio Calculado",
                "Código",
                "Cantidad",
                "Producto",
                "Color",
                "Talla",
                "Precio",

            };

            auxiliarVentas.CargarEditarSQL = "Insert into aux_ventas (idProducto) values (@idProducto)";

            auxiliarVentas.EliminarSQL = "delete from aux_ventas";
            auxiliarVentas.InsertarActualizarEliminar(auxiliarVentas.EliminarSQL, false, false, false);

            #endregion

            #region Constructor historial de ventas
            historialVentas.Grid = dataGridView2;
            historialVentas.CargarSQL = "select vnt.idFactura, ciCliente, idProductos, montoTotal, ganancia from ventas vnt";
            historialVentas.Columnas = new string[] { "Factura", "Cliente", "Productos", "Monto", "Ganancia" }; 
            #endregion

        }

        #region Métodos
        private void cargarDatos() //Método para cargar todos los datos en el grid
        {

            auxiliarVentas.Cargar(auxiliarVentas.CargarSQL);

            historialVentas.Cargar(historialVentas.CargarSQL);

            clientes.AutoCompletar(busCliente);

            if (columnaExiste == false)
            {

                auxiliarVentas.AsignarBotones("eliminar", "Quitar", "Quitar");                
                auxiliarVentas.AsignarNombreColumnas();

                historialVentas.AsignarNombreColumnas();
                columnaExiste = true;
            }
            modificarColumnas();

            CalcularMontos();
        }
        private void modificarColumnas() //Metodo para modificar caracteristicas de las columnas
        {
            dataGridView1.Columns["precioCalculado"].Visible = false;
            dataGridView1.Columns["detalle"].Visible = false;
            dataGridView1.Columns["idProducto"].FillWeight = 30;
            dataGridView1.Columns["color"].FillWeight = 50;
            dataGridView1.Columns["eliminar"].FillWeight = 30;
            dataGridView1.Columns["talla"].FillWeight = 20;
            dataGridView1.Columns["precioVenta"].FillWeight = 30;
            dataGridView1.Columns["cantidad"].FillWeight = 30;

        }
        private void CalcularMontos() //Método para el cálculo de los montos
        {
            subTotal = dataGridView1.Rows.Cast<DataGridViewRow>() //Sumar columnas del datagrid
            .Sum(t => Convert.ToInt32(t.Cells["precioCalculado"].Value));

            total = ((subTotal * iva) / 100) + subTotal;
            lblSubTotal.Text = subTotal.ToString();
            lblTotal.Text = total.ToString();

        } 
        private string JuntarFilas(string columna) //Metodo para juntar las filas de una columna de un datagrid;
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            List<string> lista = new List<string>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows) //Añadir ids de los productos
            {
                lista.Add((string)row[columna].ToString());

            }
            
            return string.Join(", ", lista);
        }
        private void CargarValores(string metodoPago) //Metodo para cargar los atributos de la nueva clase
        {
            nuevaVenta = new Clases.Venta();
            nuevaVenta.MetodoPago = metodoPago;
            nuevaVenta.IdProductos = JuntarFilas("idProducto");
            nuevaVenta.Detalle = JuntarFilas("detalle");
            nuevaVenta.CedulaCliente = cedulaCliente;
            nuevaVenta.MontoTotal = Convert.ToDouble(lblTotal.Text);
            nuevaVenta.CedulaEmpleado = "123";
            nuevaVenta.Ganancia = Convert.ToDouble(lblTotal.Text);
            nuevaVenta.FechaVenta = DateTime.Now;

            nuevaVenta.cargarAtributos();
            cargarDatos();
        }
        #endregion


        #region Búsqueda de clientes
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

            //Botón de limpiar busqueda
            busCliente.Clear();
            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == false) { busCliente.Text = "Buscar Cliente"; }
        }

        private void busCliente_Leave_1(object sender, EventArgs e)
        {
            //Añade el texto de ayuda al buscador

            controles.añadirPlaceholder(busCliente, "Buscar Cliente");

        }

        private void busCliente_Enter_1(object sender, EventArgs e)
        {
            //Añade o quita el texto de ayuda al buscador

            controles.añadirPlaceholder(busCliente, "Buscar Cliente");
        }

        private void busCliente_TextChanged(object sender, EventArgs e)
        {
            //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar

            if (string.IsNullOrWhiteSpace(busCliente.Text) && busCliente.Focused == true || busCliente.Text == "Buscar Cliente")
            {
                clearTb.Visible = false;
              
            }
            else
            {
                clearTb.Visible = true;
               
            }

        }
        private void busCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (MessageBox.Show("¿Seguro que quieres asignar esta factura a este usuario?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    
                    lblCliente.Text = clientes.ExtraerCliente(busCliente.Text);
                    cedulaCliente = busCliente.Text;
                    clientes.Conexion.Close();
                    busCliente.Text = "Buscar Cliente";


                }
            
            }
        }
        #endregion


        #region Eventos
        private void Caja_Load(object sender, EventArgs e)
        {

            cargarDatos();

        }

        private void btnAgregarPro_Click(object sender, EventArgs e) //Llamada a formulario de agregar Productos
        {
            Secciones.Caja.CrearFactura popup = new Secciones.Caja.CrearFactura();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {
            cargarDatos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //Acciones al dar click en los botones del datagrid
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
            {

                auxiliarVentas.EliminarSQL = $"delete from aux_ventas where idProducto = {dataGridView1.CurrentRow.Cells["idProducto"].Value.ToString()}";
                auxiliarVentas.InsertarActualizarEliminar(auxiliarVentas.EliminarSQL, true, false, false);
                cargarDatos();
            }
        }

        private void btnReiniciar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea una nueva factura?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                auxiliarVentas.InsertarActualizarEliminar(auxiliarVentas.EliminarSQL, false, false, false);
                lblCliente.Text = "Cliente de la Factura";
                busCliente.Text = "Buscar Cliente";
                cargarDatos();
            }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            frmAgregarCliente popup = new frmAgregarCliente();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEfectivo popup = new frmEfectivo();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed1);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed1(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {
            cargarDatos();
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            using (frmTarjeta frm = new frmTarjeta(this))
            {
                controles.mostrarPopup(frm);

                if(frm.resultado == true)
                {
                    CargarValores("TARJETA");
                }
            }
   

        }
        private void btnPagoMovil_Click(object sender, EventArgs e)
        {
            frmPagoMovil popup = new frmPagoMovil();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed3);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed3(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {

            cargarDatos();
        }
        #endregion

    }
}
