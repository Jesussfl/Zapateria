using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapateria.Clases
{
    class Modelo : Database //Herencia
    {
        #region Atributos

        private int idCategoria;
        private string nombreModelo;


        #endregion

        #region Encapsulamiento

        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string NombreModelo { get => nombreModelo; set => nombreModelo = value; }

        #endregion 

        //Constructor
        public Modelo()
        {
            CargarSQL = @"Select concat_ws('-',m.idCategoria,c.nombreCategoria) as categoria, 
                            concat_ws(m.id, m.nombreModelo) as modelo 
                            from modelos m LEFT JOIN categorias c ON (m.idCategoria = c.idCategoria)";

            Columnas = new string[] {"Categoria", "Modelo" };

            InsertarSQL = "INSERT INTO modelos (idCategoria, nombreModelo) VALUES (@idCategoria, @nombreModelo)";

        }


        #region Métodos
        public override MySqlParameter[] ParametrizarAtributos()
        {

            //Método para cargar atributos a la base de datos
            Parametros = new MySqlParameter[]
             {
                new MySqlParameter("@idCategoria", idCategoria),
                new MySqlParameter("@nombreModelo", nombreModelo)
             };
            return Parametros;
        }
        #endregion
    }
}
