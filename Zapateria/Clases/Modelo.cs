using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    class Modelo : Database
    {
        #region Atributos
        private int idCategoria;
        private string nombreModelo;

        #region Encapsulamiento
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string NombreModelo { get => nombreModelo; set => nombreModelo = value; }
        #endregion 
        #endregion

        public Modelo()
        {
            CargarSQL = "Select concat_ws('-',m.idCategoria,c.nombreCategoria) as categoria, concat_ws(m.id, m.nombreModelo) as modelo from modelos m LEFT JOIN categorias c ON (m.idCategoria = c.id)";
            Columnas = new string[] {"Categoria", "Modelo" };
            CargarEditarSQL = "INSERT INTO modelos (idCategoria, nombreModelo) VALUES (@idCategoria, @nombreModelo)";

        }


        #region Métodos
        public void cargarAtributos()
        {

            //Método para cargar atributos a la base de datos
            Parametros = new MySqlParameter[]
             {
                new MySqlParameter("@idCategoria", idCategoria),
                new MySqlParameter("@nombreModelo", nombreModelo)
             };

            InsertarActualizarEliminar(CargarEditarSQL, false);
            InsertarActualizarEliminar("idgrupal", false, true);

        }

        #endregion
    }
}
