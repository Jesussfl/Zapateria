using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    public class Venta : Database
    {
        #region Atributos

        private string idProductos;
        private string cedulaCliente;
        private string cedulaEmpleado;
        private string detalle;
        private string metodoPago;
        private double subTotal;
        private double montoTotal;
        private DateTime fechaVenta;
        private string referencia;
        private const int iva = 16;


        private int[] productos;
        private string cantidad;
        private double precioCalculado; 

        #endregion

        #region Encapsulamiento
        public int[] Productos { get => productos; set => productos = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioCalculado { get => precioCalculado; set => precioCalculado = value; }
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }
        public string IdProductos { get => idProductos; set => idProductos = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string CedulaEmpleado { get => cedulaEmpleado; set => cedulaEmpleado = value; }
        public double Subtotal { get => subTotal; set => subTotal = value; }
        public DateTime FechaVenta { get => fechaVenta; set => fechaVenta = value; }
        public string Referencia { get => referencia; set => referencia = value; } 
        #endregion


        //Constructor
        public Venta()
        {
            CargarSQL = @"select idFactura, ciCliente, idProductos, detalle, 
                        concat('$', FORMAT(subTotal, 2, 'de_DE')) as subTotal, 
                        concat('$', FORMAT(montoTotal, 2, 'de_DE')) as montoTotal,  metodoPago,fechaVenta, referencias 
                        from ventas";

            Columnas = new string[] 
            {
                "N° Factura",
                "Cliente",
                "Productos",
                "Detalle",
                "SubTotal",
                "Monto Total",
                "Método de Pago",
                "Fecha de venta",
                "Referencia"
            };

            InsertarSQL = @"Insert into ventas (ciCliente, idProductos,detalle,montoTotal,metodoPago,subtotal,fechaVenta,referencias) 
                            values (@ciCliente, @idProductos,@detalle,@montoTotal,@metodoPago,@subtotal,@fechaVenta,@referencias)";

            BuscarSQL = $"{CargarSQL} where concat_ws(idFactura,ciCliente, idProductos, metodoPago, referencias) like";

        }

        #region Métodos

        public void AjustarColumnas()
        {
            Grid.Columns["idFactura"].FillWeight = 35;
            Grid.Columns["detalle"].FillWeight = 155;
            Grid.Columns["ciCliente"].FillWeight = 45;
            Grid.Columns["montoTotal"].FillWeight = 43;
            Grid.Columns["montoTotal"].DefaultCellStyle.ForeColor = Color.Green;
            Grid.Columns["montoTotal"].DefaultCellStyle.SelectionForeColor = Color.Green;
            Grid.Columns["subTotal"].FillWeight = 45;

            Grid.Columns["metodoPago"].FillWeight = 55;
            Grid.Columns["fechaVenta"].FillWeight = 65;
            Grid.Columns["referencias"].FillWeight = 50;
        }
        public void CargarAtributosAuxiliar() //Método para parametrizar los atributos y cargarlos en mysql en la tabla auxiliar de ventas
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idProducto", productos[0]),
                new MySqlParameter("@cantidad", Convert.ToInt32(cantidad)),
                new MySqlParameter("@subtotal", subTotal),
                new MySqlParameter("@detalle", IdProductos),

            };

            InsertarActualizarEliminar(InsertarSQL, false);

        }
        public void CargarAtributosVentas()//Método para parametrizar los atributos y cargarlos en mysql en la tabla de ventas
        {
            Parametros = new MySqlParameter[]
       {
            new MySqlParameter("@ciCliente", cedulaCliente),
            new MySqlParameter("@idProductos", idProductos),
            new MySqlParameter("@detalle", detalle),
            new MySqlParameter("@subtotal", subTotal),
            new MySqlParameter("@montoTotal", montoTotal),
            new MySqlParameter("@metodoPago", metodoPago),
            new MySqlParameter("@fechaVenta", fechaVenta),
            new MySqlParameter("@referencias", referencia)

       };

            InsertarActualizarEliminar(InsertarSQL, false);
        }
        public int ExtraerCantidadProducto(int idProducto) //Método para extraer la cantidad de los productos
        {
            Conexion.Open();

            MySqlCommand cm = new MySqlCommand($"select cantidad from inventario where idProducto = {idProducto}", Conexion);
            MySqlDataReader sdr = cm.ExecuteReader();

            if (sdr.Read())
            {
                return sdr.GetInt32("cantidad");
            }
            else
            {
                return 0;
            }


        }
        public void ContarInventario(int idProducto, int cantidad) //Metodo para descontar productos del inventario al realizar una venta
        {
            Calzado objInventario = new Calzado();
            objInventario.InsertarSQL = $"update inventario set cantidad = @cantidad where idProducto = {idProducto}";

            int nuevaCantidad = ExtraerCantidadProducto(idProducto) - cantidad; Conexion.Close();

            objInventario.Parametros = new MySqlParameter[] { new MySqlParameter("@cantidad", nuevaCantidad) };

            objInventario.InsertarActualizarEliminar(objInventario.InsertarSQL, false);
            
        }
        public void CalcularMontos() //Método para el cálculo de los montos
        {
            subTotal = Grid.Rows.Cast<DataGridViewRow>() //Sumar columnas del datagrid
            .Sum(t => Convert.ToDouble(t.Cells["precioCalculado"].Value));

            montoTotal = ((subTotal * iva) / 100) + subTotal;

        }
        public string JuntarFilas(string columna) //Metodo para juntar las filas de una columna de un datagrid;
        {
            DataTable dt = (DataTable)Grid.DataSource;
            List<string> lista = new List<string>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows) //Añadir ids de los productos
            {
                lista.Add((string)row[columna].ToString());

            }

            return string.Join(", ", lista);
        }

        #endregion

    }
}
