﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zapateria
{
    public partial class Inicio : Form
    {

        public Inicio()
        {
            InitializeComponent();
            quitarBordeBtns();
            //Definición de bordes UI
            
   

        }

        //Definicion de variables con formularios indexados en pagina de inicio
        Caja.Caja caj = new Caja.Caja();
        Ventas.Ventas ven = new Ventas.Ventas();
        Inventario.Inventario inv = new Inventario.Inventario();
        Reportes.Reportes rep = new Reportes.Reportes();
        Secciones.Clientes.Clientes cli = new Secciones.Clientes.Clientes();
        Secciones.Empleados.Empleados emp = new Secciones.Empleados.Empleados();


        //Funcion para abrir formularios desde el sidebar
        private void abrirForms(Form frm)
        {
            frm.TopLevel = false;
            frm.TopMost = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.content.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }
        #region Botones

        //Funcion para quitar borde de los botones al cargar el programa
        private void quitarBordeBtns()
        {
            //Función para fines estéticos de botones
            //Lee los botones del sidebar y quita los bordes de los botones
            foreach (Control c in sideBar.Controls)
            {
                if(c is Panel) { foreach (Button boton in c.Controls.OfType<Button>()) { boton.FlatAppearance.BorderColor = boton.Parent.BackColor; } }
            }
        }

        //Funcion para dar apariencia a los botones al interactuar con ellos en el sidebar
        private void misBotonesApariencia (object sender, EventArgs e, string img)
        {

            //Funcion para fines esteticos de los botones del sidebar
            //Lee los botones del sidebar y establece cuando se está en una sección o se ha cambiado a otra

            foreach (Control c in sideBar.Controls)
            {
                if(c is Panel)
                {
                    foreach (Button p in c.Controls.OfType<Button>()) {

                        p.BackColor = Clases.Colores.white;
                        p.ForeColor = Clases.Colores.secondary;
                    }
                  
                }
              
            }

            btnCaja.Image = Properties.Resources.bag_2;
            btnInventario.Image = Properties.Resources.box;
            btnVentas.Image = Properties.Resources.chart;
            btnSalir.Image = Properties.Resources.logout;
            btnReportes.Image = Properties.Resources.document;
            btnInicio.Image = Properties.Resources.home;
            btnClientes.Image = Properties.Resources.profile_2user;
            btnEmpleados.Image = Properties.Resources.user_octagon;

            Button click = (Button)sender;
            click.BackColor = Clases.Colores.primary;
            click.ForeColor = Color.White;
            click.Image = Zapateria.Properties.Resources.homeWhite;
            click.Image = Image.FromFile(Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\Resources\\" + img));

        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de inventario
            abrirForms(inv);
            misBotonesApariencia(btnInventario, null, "boxWhite.png");
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de reportes
            abrirForms(rep);
            misBotonesApariencia(btnReportes, null, "documentWhite.png");

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de ventas
            abrirForms(ven);

            misBotonesApariencia(btnVentas, null, "chartWhite.png");

        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de caja
            abrirForms(caj);

            misBotonesApariencia(btnCaja, null, "bag-2White.png");

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            misBotonesApariencia(btnSalir, null, "logoutWhite.png");

            //Cerrar Programa
            this.Close();

        }

        #endregion

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Mensaje de confirmación para cerrar programa

            if (MessageBox.Show("¿Estás seguro que quieres salir?", "Confirmar Salida", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            misBotonesApariencia(btnInicio, null,"homeWhite.png");

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de empleados
            abrirForms(emp);
            misBotonesApariencia(btnEmpleados, null, "user-octagonWhite.png");

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

            //Llamado a pagina de clientes
            abrirForms(cli);

            misBotonesApariencia(btnClientes, null, "profile-2userWhite.png") ;

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            sideBar.Show();
            btnMostrar.Visible = false;
            btnOcultar.Visible = true;
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            sideBar.Hide();
            btnMostrar.Visible = true;
            btnOcultar.Visible = false;
        }
    }
}
