using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Caja
{
    public partial class Caja : Form
    {
        database cajaDB = new database();

        public Caja()
        {
            InitializeComponent();
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            cajaDB.cargarGrid(dataGridView1, "SELECT * FROM alumnos;");
        }
    }
}
