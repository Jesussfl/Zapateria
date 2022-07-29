using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    class Categoria : Database
    {

        #region Atributos

        private string nombreCategoria;
        private string marca;

        #region Encapsulamiento
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        public string Marca { get => marca; set => marca = value; }
        #endregion 

        #endregion

        //Constructor
        public Categoria()
        {
            CargarSQL = "Select * from categorias";
            CargarEditarSQL = "insertar_categorias";
            SqlCombo = "Select idCategoria, nombreCategoria from categorias";

        }

        #region Métodos
            //Método para parametrizar los atributos y cargarlos en mysql
        public void cargarAtributos()
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@nombreCategoria", nombreCategoria),
                new MySqlParameter("@marca", marca)
            };

            InsertarActualizarEliminar(CargarEditarSQL, true, true);
        }

        #endregion
    }
}
