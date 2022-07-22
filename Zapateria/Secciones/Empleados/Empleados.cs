using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Secciones.Empleados
{
    public partial class Empleados : Form
    {
        Database empleadosDB = new Database();

        public Empleados()
        {
            InitializeComponent();
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            //empleadosDB.cargarGrid(dataGridView1, "SELECT * FROM empleados;");

        }
    }
}
