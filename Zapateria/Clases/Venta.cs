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
        private string nombreEmpleado;
        private string detalle;
        private string metodoPago;
        private double subTotal;
        private double montoTotal;
        private DateTime fechaVenta;
        private string referencia;
        private const int iva = 16;

        #endregion

        #region Encapsulamiento
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }
        public string IdProductos { get => idProductos; set => idProductos = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public double Subtotal { get => subTotal; set => subTotal = value; }
        public DateTime FechaVenta { get => fechaVenta; set => fechaVenta = value; }
        public string Referencia { get => referencia; set => referencia = value; } 
        #endregion


        //Constructor
        public Venta()
        {
            CargarSQL = @"select idFactura, ciCliente, idProductos, detalle, 
                        concat('$', FORMAT(subTotal, 2, 'de_DE')) as subTotal, 
                        concat('$', FORMAT(montoTotal, 2, 'de_DE')) as montoTotal, empleado,  metodoPago,fechaVenta, referencias 
                        from ventas";

            Columnas = new string[] 
            {
                "N° Factura",
                "Cliente",
                "Productos",
                "Detalle",
                "SubTotal",
                "Monto Total",
                "Empleado",
                "Método de Pago",
                "Fecha de venta",
                "Referencia"
            };

            InsertarSQL = @"Insert into ventas (ciCliente, idProductos,detalle,montoTotal,metodoPago,empleado,subtotal,fechaVenta,referencias) 
                            values (@ciCliente, @idProductos,@detalle,@montoTotal,@metodoPago,@empleado,@subtotal,@fechaVenta,@referencias)";

            BuscarSQL = $"{CargarSQL} where concat_ws(idFactura,ciCliente, idProductos, metodoPago, referencias) like";

        }

        #region Métodos

        public void AjustarColumnas() //Metodo para modifcar la interfaz del datagridview en el formulario
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
            Grid.Columns["empleado"].FillWeight = 50;
        }
        public override MySqlParameter[] ParametrizarAtributos() //Polimorfismo - Se modifica el método declarado en la clase base de datos
        {
            //Método para cargar atributos a la base de datos
            Parametros = new MySqlParameter[]
             {
            new MySqlParameter("@ciCliente", cedulaCliente),
            new MySqlParameter("@idProductos", idProductos),
            new MySqlParameter("@detalle", detalle),
            new MySqlParameter("@subtotal", subTotal),
            new MySqlParameter("@montoTotal", montoTotal),
            new MySqlParameter("@empleado", nombreEmpleado),
            new MySqlParameter("@metodoPago", metodoPago),
            new MySqlParameter("@fechaVenta", fechaVenta),
            new MySqlParameter("@referencias", referencia)
             };
            return Parametros;
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
        public string BuscarProductoMasVendido(string cb) //Metodo para encontrar el producto que se ha vendido mas
        {

            string consulta = @"select substring_index(idProductos, ',', 1) as ids, sum(v.montoTotal) from ventas v
                                group by ids order by sum(v.montoTotal) desc limit 1" + FiltrarFecha(cb);
            string productoMasVendido = ExtraerDato(consulta, "ids");
            
            consulta = $@"Select inv.idProducto, concat_ws(' ',ctg.nombreCategoria, ctg.marca, mdl.nombreModelo) as producto, inv.descripcion, inv.tipoCalzado, inv.talla, inv.color,inv.cantidad, concat('$', FORMAT(inv.precioVenta, 2, 'de_DE')) as precioVenta
                        from inventario inv 
                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.idCategoria) 
                        LEFT JOIN modelos mdl ON (inv.idModelo = mdl.id and inv.idCategoria = mdl.idCategoria) where idProducto = {productoMasVendido}";

            productoMasVendido = ExtraerDato(consulta, "producto");
            return productoMasVendido;

        }
        public string BuscarVentaMasAlta(string cb)
        {
            string consulta = @"SELECT concat_ws('-',idFactura,FORMAT(montoTotal, 2)) as factura,fechaVenta,ciCliente , MAX(montoTotal) AS alto
                                FROM ventas
                                GROUP BY ciCliente
                                ORDER BY MAX(montoTotal) DESC" + FiltrarFecha(cb);
            string ventaMasAlta = ExtraerDato(consulta, "factura");
      
            return ventaMasAlta;
        }
        public string FiltrarFecha(string cb)
        {
            string where = "";
            switch (cb)
            {
                case "Esta semana":
                    where = "WHERE  YEARWEEK(`fechaVenta`, 1) = YEARWEEK(CURDATE(), 1)";
                    return where;
                   
                case "Hoy":
                    where = $" WHERE DATE(`fechaVenta`) = CURDATE()";
                    return where;

                    
                case "Este mes":
                    where = $" WHERE  MONTH(`fechaVenta`) = MONTH(CURDATE())";
                    return where;

                    
                case "Este año":
                    where = $"WHERE  YEAR(`fechaVenta`) = YEAR(CURDATE())";
                    return where;

                case "Todos":
                    return "";

                   
                default:
                    return "";
            }
        }


        //Métodos para actualizar el inventario
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
        public void ContarInventario() //Metodo para actualizar los datos del inventario una vez realizada una venta
        {
            DataTable dt = (DataTable)Grid.DataSource;

            List<int> ids = new List<int>(dt.Rows.Count);
            List<int> cantidades = new List<int>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows) //Añadir ids de los productos
            {
                ids.Add((int)row["idProducto"]);
                cantidades.Add((int)row["cantidad"]);
            }

            for (int i = 0; i < ids.Count; i++)
            {
                ActualizarInventario(ids[i], cantidades[i]);
            }
        }
        private void ActualizarInventario(int idProducto, int cantidad) //Metodo para descontar productos del inventario al realizar una venta
        {
            Calzado objInventario = new Calzado();
            string consulta = $"update inventario set cantidad = @cantidad where idProducto = {idProducto}";

            int nuevaCantidad = ExtraerCantidadProducto(idProducto) - cantidad; Conexion.Close();

            objInventario.Parametros = new MySqlParameter[] { new MySqlParameter("@cantidad", nuevaCantidad) };



            //Ejecutar comando sql
            Conexion.Open();
            MySqlCommand cmd = new MySqlCommand(consulta, Conexion);
            cmd.Parameters.AddRange(objInventario.Parametros); 
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }
        #endregion

    }
}
