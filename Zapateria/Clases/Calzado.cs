using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Clases
{
    public class Calzado : Database
    {
        #region Atributos
        private int codigo;
        private string marca;

        private string codigoCategoria;

        private string codigoModelo;

        private string descripcion;
        private int talla;
        private string color;
        private int cantidad;
        private double precioProducto;
        private double costePorProducto;

        private string[] colores = new string[] { "NEGRO", "GRIS", "BLANCO", "MARRON", "Amarillo", "Verde", "Beige", "Azul", "Rosado", "Morado", "Rojo", "Verde", "Amarillo", "Naranja" };

        #region Encapsulamiento
        //Encapsulamiento
        public int Codigo { get => codigo; set => codigo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Talla { get => talla; set => talla = value; }
        public string Color { get => color; set => color = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioProducto { get => precioProducto; set => precioProducto = value; }
        public double CostePorProducto { get => costePorProducto; set => costePorProducto = value; }
        public string CodigoModelo { get => codigoModelo; set => codigoModelo = value; }
        public string CodigoCategoria { get => codigoCategoria; set => codigoCategoria = value; }
        #endregion 
        #endregion

        //Constructor
        public Calzado()
        {
            Cargar = @"Select inv.*, ctg.nombreCategoria, ctg.marca, mdl.nombreModelo 
                        from inventario inv 
                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.id) 
                        INNER JOIN modelos mdl ON (inv.idModelo = mdl.indexer)";

            Columnas = new string[] 
            { 
                "Código", 
                "Codigo Categoría", 
                "Código Modelo", 
                "Descripcion", 
                "Sexo", "Talla", 
                "Color", "Cantidad", 
                "Precio de Venta", 
                "Coste Mercancía", 
                "Categoría", 
                "Marca", 
                "Modelo" 
            };

            CargarEditar = @"INSERT INTO inventario (idProducto, idCategoria, idModelo, descripcion, talla, color, cantidad, precioVenta, costeTotal) 
                             VALUES (@idProducto, @idCategoria, @idModelo, @descripcion, @talla, @color, @cantidad, @precioVenta, @costeTotal)";
        }

        #region Métodos
        public void cargarAtributos()
        {
            //Función dedicada de generar los parametros de MYSQL para poder insertarlos en la base de datos
            Parametros = new MySqlParameter[]
                {
                new MySqlParameter("@idProducto", codigo),
                new MySqlParameter("@idCategoria", codigoCategoria),
                new MySqlParameter("@idModelo", codigoModelo),
                new MySqlParameter("@descripcion", descripcion),
                new MySqlParameter("@talla", talla),
                new MySqlParameter("@color", color),
                new MySqlParameter("@cantidad", cantidad),
                new MySqlParameter("@precioVenta", precioProducto),
                new MySqlParameter("@costeTotal", costePorProducto)
                };
            InsertarActualizarEliminar(CargarEditar, true, false);
        }
        public int GenerarCodigo()
        {
            //Funcion para generar un código a cada producto según ciertas características
            int indexColor;

            for (indexColor = 0; indexColor <= colores.Length; indexColor++)
            {
                if (color == colores[indexColor]) {

                    return int.Parse(CodigoCategoria + codigoModelo + talla.ToString() + indexColor.ToString());

                };

            }
            return 1;

        }
        #endregion
    }
 
}
