using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    public class Calzado : Database //Herencia
    {
        #region Atributos

        private int codigo;
        private string marca;
        private string nombreCategoria;
        private string codigoCategoria;
        private string nombreModelo;
        private string codigoModelo;
        private string descripcion;
        private string tipoCalzado;
        private int talla;
        private string color;
        private int cantidad;
        private double precioProducto;

        private string[] colores = new string[] { "NEGRO", "GRIS", "BLANCO", "MARRON", "AMARILLO", "VERDE", "BEIGE", "AZUL", "ROSADO", "MORADO", "ROJO", "VERDE", "AMARILLO", "NARANJA" };

        #endregion

        #region Encapsulamiento
        //Encapsulamiento
        public int Codigo { get => codigo; set => codigo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Talla { get => talla; set => talla = value; }
        public string Color { get => color; set => color = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioProducto { get => precioProducto; set => precioProducto = value; }
        public string CodigoModelo { get => codigoModelo; set => codigoModelo = value; }
        public string CodigoCategoria { get => codigoCategoria; set => codigoCategoria = value; }
        public string TipoCalzado { get => tipoCalzado; set => tipoCalzado = value; }
        public string NombreModelo { get => nombreModelo; set => nombreModelo = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        public string[] Colores { get => colores; set => colores = value; }
        #endregion

        //Constructor
        public Calzado()
        {
            CargarSQL = @"Select inv.idProducto, concat_ws(' ',ctg.nombreCategoria, ctg.marca, mdl.nombreModelo) as producto, inv.descripcion, inv.tipoCalzado, inv.talla, inv.color,inv.cantidad, concat('$', FORMAT(inv.precioVenta, 2, 'de_DE')) as precioVenta
                        from inventario inv 
                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.idCategoria) 
                        LEFT JOIN modelos mdl ON (inv.idModelo = mdl.id and inv.idCategoria = mdl.idCategoria)";

            Columnas = new string[]
            {
                "Código",
                "Producto",
                "Descripcion",
                "Sexo",
                "Talla",
                "Color",
                "Cantidad",
                "Precio",

            };

            InsertarSQL = @"INSERT INTO inventario (idProducto, idCategoria, idModelo, descripcion, tipoCalzado, talla, color, cantidad, precioVenta) 
                             VALUES (@idProducto, @idCategoria, @idModelo, @descripcion,@tipoCalzado, @talla, @color, @cantidad, @precioVenta)";
            ActualizarSQL = @"UPDATE inventario SET idProducto = @idProducto, idCategoria = @idCategoria, idModelo = @idModelo, descripcion = @descripcion, tipoCalzado = @tipoCalzado, talla = @talla, color = @color, cantidad = @cantidad, precioVenta = @precioVenta";

            BuscarSQL = $@"{CargarSQL} where concat_ws(idProducto,ctg.nombreCategoria, ctg.marca, mdl.nombreModelo,tipoCalzado,talla,color) like";
        }

        #region Métodos
        public int AsignarCódigo() //Funcion para generar un código a cada producto según ciertas características
        {

            int indexColor = Array.IndexOf(Colores, color);

            return int.Parse(CodigoCategoria + codigoModelo + talla.ToString() + indexColor.ToString());


        }
        public string ObtenerCategoria(string texto) //Metodo para obtener el string (categoria) del texto que se envie
        {
            string[] extractor = texto.Split('-');

            return extractor[0];
        } 

        public override MySqlParameter[] ParametrizarAtributos() //Polimorfismo - Se modifica el método declarado en la clase base de datos
        {
            //Metodo para hacer los atributos compatibles con mysql
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@idProducto", codigo),
                new MySqlParameter("@idCategoria", int.Parse(codigoCategoria)),
                new MySqlParameter("@idModelo", int.Parse(codigoModelo)),
                new MySqlParameter("@descripcion", descripcion),
                new MySqlParameter("@tipoCalzado", tipoCalzado),
                new MySqlParameter("@talla", talla),
                new MySqlParameter("@color", color),
                new MySqlParameter("@cantidad", cantidad),
                new MySqlParameter("@precioVenta", precioProducto)

            };
            return Parametros;
        }

        #endregion
    }
 
}
