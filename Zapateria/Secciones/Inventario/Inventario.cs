using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Inventario
{
    public partial class Inventario : Form
    {
        database inventarioDB = new database();
        public Inventario()
        {
            InitializeComponent();
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;

        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            
            inventarioDB.cargarGrid(dataGridView1, "SELECT * FROM alumnos;");

        }


    }
}
