﻿using System;
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
        Database cajaDB = new Database();

        public Caja()
        {
            InitializeComponent();
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            
        }
    }
}
