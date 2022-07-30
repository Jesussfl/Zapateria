using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    public class Venta : Database
    {
        #region Atributos
        private string cedulaCliente;
        private string idProductos;
        private string detalle;
        private double montoTotal;
        private string metodoPago;
        private string cedulaEmpleado;
        private double ganancia;
        private DateTime fechaVenta;
        private string referencia;

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
        public double Ganancia { get => ganancia; set => ganancia = value; }
        public DateTime FechaVenta { get => fechaVenta; set => fechaVenta = value; }
        public string Referencia { get => referencia; set => referencia = value; } 
        #endregion


        //Constructor
        public Venta()
        {
            CargarSQL = "select idFactura, ciCliente, idProductos, detalle, concat('$', FORMAT(montoTotal, 2, 'de_DE')) as montoTotal, metodoPago, concat('$', FORMAT(ganancia, 2, 'de_DE')) as ganancia, fechaVenta, referencias from ventas";

            Columnas = new string[] 
            {
                "N° Factura",
                "Cliente",
                "Productos",
                "Detalle",
                "Monto Total",
                "Método de Pago",
                "Ganancia",
                "Fecha de venta",
                "Referencia"
            };

            InsertarSQL = "Insert into ventas (ciCliente, idProductos,detalle,montoTotal,metodoPago,ganancia,fechaVenta,referencias) values (@ciCliente, @idProductos,@detalle,@montoTotal,@metodoPago,@ganancia,@fechaVenta,@referencias)";
            BuscarSQL = "select * from ventas where concat_ws(ciCliente, idProductos, metodoPago, referencias) like";
        }

        #region Métodos
        public void CargarAtributosAuxiliar() //Método para parametrizar los atributos y cargarlos en mysql en la tabla auxiliar de ventas
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idProducto", productos[0]),
                new MySqlParameter("@cantidad", Convert.ToInt32(cantidad)),
                new MySqlParameter("@precioCalculado", precioCalculado),
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
            new MySqlParameter("@montoTotal", montoTotal),
            new MySqlParameter("@metodoPago", metodoPago),
            new MySqlParameter("@ganancia", ganancia),
            new MySqlParameter("@fechaVenta", fechaVenta),
            new MySqlParameter("@referencias", referencia)

       };

            InsertarActualizarEliminar(InsertarSQL, false);
        }
        public int ExtraerCantidadProducto(int idProducto) //Método para extraer el nombre del cliente
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
        #endregion

    }
}
