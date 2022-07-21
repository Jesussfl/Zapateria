using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria.Reportes
{
    public partial class Reportes : Form
    {
        database reportesDB = new database();

        public Reportes()
        {
            InitializeComponent();
            btnSiguiente.FlatAppearance.BorderColor = btnSiguiente.Parent.BackColor;
            btnAnterior.FlatAppearance.BorderColor = btnAnterior.Parent.BackColor;
            btnIrFinal.FlatAppearance.BorderColor = btnIrFinal.Parent.BackColor;
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            reportesDB.cargarGrid(dataGridView1, "SELECT * FROM alumnos;");

        }
    }
}
