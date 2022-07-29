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

        public Venta()
        {
            CargarSQL = "select * from ventas";
            Columnas = new string[] 
            {
                "N° Factura",
                "Cliente",
                "Productos",
                "Detalle",
                "Monto Total",
                "Método de Pago",
                "Empleado",
                "Ganancia",
                "Fecha de venta",
                "Referencia"
            };
            CargarEditarSQL = "Insert into ventas (ciCliente, idProductos,detalle,montoTotal,metodoPago,ganancia,fechaVenta,referencias) values (@ciCliente, @idProductos,@detalle,@montoTotal,@metodoPago,@ganancia,@fechaVenta,@referencias)";

        }
        public void cargarAtributosAUXILIAR()

        //Método para parametrizar los atributos y cargarlos en mysql
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idProducto", productos[0]),
                new MySqlParameter("@cantidad", Convert.ToInt32(cantidad)),
                new MySqlParameter("@precioCalculado", precioCalculado),
                new MySqlParameter("@detalle", IdProductos),

            };

            InsertarActualizarEliminar(CargarEditarSQL, false);
        }
        public void cargarAtributos()
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

            InsertarActualizarEliminar(CargarEditarSQL, true);
        }
        public void generarCodigoFactura(string producto)
        {

        }
    }
}
