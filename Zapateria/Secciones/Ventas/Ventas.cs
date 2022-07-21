using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Ventas
{

    public partial class Ventas : Form
    {
        Database ventasDB = new Database();

        public Ventas()
        {
            InitializeComponent();
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            ventasDB.cargarGrid(dataGridView1, "SELECT * FROM ventas;");

        }
    }
}
