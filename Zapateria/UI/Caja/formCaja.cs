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
        Clases.Venta venta = new Clases.Venta();
        Clases.Venta historial = new Clases.Venta();

        Clases.Venta nuevaVenta;

        #endregion


        #region Atributos
        public string cedulaCliente;

 
        private bool columnaExiste = false;
        private string referencia;
        #endregion

        public formCaja() //Constructor
        {
            InitializeComponent();

            #region Constructor auxiliar de ventas
            venta.Grid = dataGridView1;
            venta.CargarSQL = @"select aux.detalle, aux.precioCalculado,aux.idProducto,aux.cantidad,concat_ws('-',ctg.nombreCategoria,ctg.marca,mdl.id) as producto, inv.color, inv.talla, concat('$', FORMAT(precioVenta, 2, 'de_DE')) as precioVenta
                                      from aux_ventas aux 
                                      inner join inventario inv on(aux.idProducto = inv.idProducto) inner join categorias ctg on (inv.idCategoria = ctg.id) inner join modelos mdl on (inv.idModelo = mdl.indexer)";

            venta.Columnas = new string[]
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

            venta.InsertarSQL = "Insert into aux_ventas (idProducto) values (@idProducto)";

            venta.EliminarSQL = "delete from aux_ventas";
            venta.InsertarActualizarEliminar(venta.EliminarSQL, false, false, false);

            #endregion

            #region Constructor historial de ventas
            historial.Grid = dataGridView2;
            historial.CargarSQL = "select vnt.idFactura, ciCliente, idProductos, concat('$', FORMAT(montoTotal, 2, 'de_DE')) as montoTotal, concat('$', FORMAT(subtotal, 2, 'de_DE')) as subtotal from ventas vnt WHERE DATE(`fechaVenta`) = CURDATE()";
            historial.Columnas = new string[] { "Factura", "Cliente", "Productos", "Monto", "Subtotal" }; 
            #endregion

        }

        #region Métodos
        private void CargarDatos() //Método para cargar todos los datos en el grid
        {
            venta.Grid = dataGridView1;

            venta.Cargar(venta.CargarSQL);

            historial.Cargar(historial.CargarSQL);

            clientes.AutoCompletar(busCliente);

            if (columnaExiste == false)
            {

                venta.AsignarBotones("eliminar", "", "Quitar");                
                venta.AsignarNombreColumnas();

                historial.AsignarNombreColumnas();
                columnaExiste = true;
            }
            ModificarColumnas();


            venta.CalcularMontos();

            lblSubTotal.Text = "$" + venta.Subtotal.ToString();
            lblTotal.Text = "$" + venta.MontoTotal.ToString();

        }
        private void ModificarColumnas() //Metodo para modificar caracteristicas de las columnas
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
        
        private void CargarValores(string metodoPago) //Metodo para cargar los atributos de la nueva clase
        {
            nuevaVenta = new Clases.Venta();

            nuevaVenta.MetodoPago = metodoPago;
            nuevaVenta.IdProductos = venta.JuntarFilas("idProducto");
            nuevaVenta.Detalle = venta.JuntarFilas("detalle");
            MessageBox.Show(nuevaVenta.Detalle);
            nuevaVenta.CedulaCliente = cedulaCliente;
            nuevaVenta.MontoTotal = venta.MontoTotal;
            nuevaVenta.CedulaEmpleado = "123";
            nuevaVenta.Subtotal = venta.Subtotal;
            nuevaVenta.FechaVenta = DateTime.Now;
            nuevaVenta.Referencia = referencia;

            nuevaVenta.CargarAtributosVentas();
            CargarDatos();
        }
        private bool Validacion() //Método para validar si se han añadido elementos a la factura
        {
            if (lblCliente.Text == "Cliente de la Factura" || string.IsNullOrEmpty(venta.JuntarFilas("idProducto")))
            {
                return false;

            }
            else { return true; }

        }
        private void LimpiarFactura()
        {
            venta.InsertarActualizarEliminar(venta.EliminarSQL, false, false, false);
            lblCliente.Text = "Cliente de la Factura";
            busCliente.Text = "Buscar Cliente";
            CargarDatos();
        }
        private void ActualizarInventario()
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            List<int> ids = new List<int>(dt.Rows.Count);
            List<int> cantidades = new List<int>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows) //Añadir ids de los productos
            {
                ids.Add((int)row["idProducto"]);
                cantidades.Add((int)row["cantidad"]);
            }

            for (int i = 0; i < ids.Count; i++)
            {
                historial.ContarInventario(ids[i], cantidades[i]);
            }
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


        #region Eventos Principales
        private void Caja_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnAgregarPro_Click(object sender, EventArgs e) //Llamada a formulario de agregar Productos
        {
            Secciones.Caja.CrearFactura popup = new Secciones.Caja.CrearFactura();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }
        private void popup_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {
            CargarDatos();
   
        }



        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            frmAgregarCliente popup = new frmAgregarCliente();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validacion() == false)
            {
                MessageBox.Show("No hay un cliente o producto asociado a la factura", "Asocie un cliente o producto");

            }
            else
            {
                using (frmEfectivo frm = new frmEfectivo(this))
                {
                    controles.mostrarPopup(frm);

                    if (frm.resultado == true)
                    {
                        CargarValores(frm.metodoPago);
                        LimpiarFactura();
                    }
                }

            }
      
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if(Validacion() == false)
            {
                MessageBox.Show("No hay un cliente o producto asociado a la factura", "Asocie un cliente o producto");

            }
            else
            {
                using (frmTarjeta frm = new frmTarjeta(this))
                {
                    controles.mostrarPopup(frm);

                    if (frm.resultado == true)
                    {
                        ActualizarInventario();
                        CargarValores(frm.metodoPago);
                        LimpiarFactura();

                    }
                }

            }


        }
        private void btnPagoMovil_Click(object sender, EventArgs e)
        {
            if (Validacion() == false)
            {
                MessageBox.Show("No hay un cliente o producto asociado a la factura", "Asocie un cliente o producto");

            }
            else
            {
                using (frmPagoMovil frm = new frmPagoMovil(this))
                {
                    controles.mostrarPopup(frm);

                    if (frm.resultado == true)
                    {
                        referencia = frm.txtReferencia.Texts;
                        CargarValores(frm.metodoPago);
                        LimpiarFactura();

                    }
                }

            }

        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "")
            {

                venta.EliminarSQL = $"delete from aux_ventas where idProducto = {dataGridView1.CurrentRow.Cells["idProducto"].Value.ToString()}";
                venta.InsertarActualizarEliminar(venta.EliminarSQL, true, false, false);
                CargarDatos();
            }
        }

        #endregion

        #region Botones 
        private void btnReiniciar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea una nueva factura?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                venta.InsertarActualizarEliminar(venta.EliminarSQL, false, false, false);
                lblCliente.Text = "Cliente de la Factura";
                busCliente.Text = "Buscar Cliente";
                CargarDatos();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            frmAgregarCliente popup = new frmAgregarCliente();
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            controles.mostrarPopup(popup);
        }
        #endregion

    }
}
