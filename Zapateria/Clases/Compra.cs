using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    public class Compra : Database //Herencia
    {
        #region Compra
        private int idProducto;
        private string nombreProducto;
        private string cantidad;
        private double precioCalculado;
        private string detalle;
        #endregion

        #region Encapsulamiento
        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioCalculado { get => precioCalculado; set => precioCalculado = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; } 
        #endregion

        public Compra() //Constructor
        {
            InsertarSQL = "Insert into aux_ventas (idProducto, cantidad, precioCalculado, detalle) values (@idProducto, @cantidad, @subtotal, @detalle)";

        }

        public override MySqlParameter[] ParametrizarAtributos() //Polimorfismo - Se modifica el método declarado en la clase base de datos
        {
            //Método para cargar atributos a la base de datos
            Parametros = new MySqlParameter[]
             {
                new MySqlParameter("@idProducto", idProducto),
                new MySqlParameter("@cantidad", Convert.ToInt32(cantidad)),
                new MySqlParameter("@subtotal", precioCalculado),
                new MySqlParameter("@detalle", detalle),

             };
            return Parametros;
        }

    }
}
