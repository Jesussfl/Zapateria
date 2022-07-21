using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Secciones.Clientes
{
    public partial class Clientes : Form
    {
        Database clientesDB = new Database();

        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            clientesDB.cargarGrid(dataGridView1, "SELECT * FROM clientes;");

        }
    }
}
