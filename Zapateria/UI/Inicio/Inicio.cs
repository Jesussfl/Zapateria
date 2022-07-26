﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.Inventario;
using Zapateria.UI.Caja;

namespace Zapateria
{
    public partial class Inicio : Form
    {
        //Definicion de variables con formularios indexados en pagina de inicio
        formCaja caj = new formCaja();
        Ventas.Ventas ven = new Ventas.Ventas();
        Secciones.Empleados.Empleados emp = new Secciones.Empleados.Empleados();

        public string correoEmpleado;
        public Inicio()
        {
            InitializeComponent();
            quitarBordeBtns();
   
          
        }






        
        
        private void abrirForms(Form frm)//Funcion para abrir formularios desde el sidebar
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
        private void misBotonesApariencia (object sender, EventArgs e)
        {

            //Funcion para fines esteticos de los botones del sidebar
            //Lee los botones del sidebar y establece cuando se está en una sección o se ha cambiado a otra

            foreach (Control c in sideBar.Controls)
            {
                if(c is Panel)
                {
                    foreach (Button p in c.Controls.OfType<Button>()) {

                        p.BackColor = Clases.Colores.BG;
                        p.ForeColor = Color.White;
                    }
                  
                }
              
            }


            Button click = (Button)sender;
            click.BackColor = Clases.Colores.Primary;
            click.ForeColor = Color.White;
 

        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de inventario
            abrirForms(new frmInventario());
            misBotonesApariencia(btnInventario, null);
        }

 

        private void btnVentas_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de ventas
            abrirForms(new Ventas.Ventas());

            misBotonesApariencia(btnVentas, null);

        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de caja
            caj.nombreEmpleado = nombreUsuario.Text;
            abrirForms(caj);

            misBotonesApariencia(btnCaja, null);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            misBotonesApariencia(btnSalir, null);

            //Cerrar Programa
            
            this.Close();
            Application.Exit();
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
            misBotonesApariencia(btnInicio, null);

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            correoEmpleado = nombreUsuario.Text;
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            //Llamado a pagina de empleados
            abrirForms(emp);
            misBotonesApariencia(btnEmpleados, null);
            
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

            //Llamado a pagina de clientes
            abrirForms(new Secciones.Clientes.Clientes());

            misBotonesApariencia(btnClientes, null) ;

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
