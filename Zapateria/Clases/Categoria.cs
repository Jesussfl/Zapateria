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
            Cargar = "Select * from categorias";
            CargarEditar = "insertar_categorias";
            SqlCombo = "Select idCategoria, nombreCategoria from categorias";

        }

        #region Métodos
        public void cargarAtributos()

            //Método para parametrizar los atributos y cargarlos en mysql
        {
            Parametros = new MySqlParameter[]
            {
                new MySqlParameter("@nombreCategoria", nombreCategoria),
                new MySqlParameter("@marca", marca)
            };

            InsertarActualizarEliminar(CargarEditar, true, true);
        }

        #endregion
    }
}
