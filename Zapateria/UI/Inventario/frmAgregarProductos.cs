﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.Inventario;
using Zapateria.UI.Inventario;

namespace Zapateria.Secciones.Inventario
{
    public partial class frmAgregarProductos : Form
    {

        #region Instanciaciones

        Clases.Calzado objCalzado;

        Clases.Calzado coleccionCalzados = new Clases.Calzado();
        Clases.Categoria coleccionCategorias = new Clases.Categoria();
        Clases.Modelo coleccionModelo = new Clases.Modelo();

        Clases.Controles controles = new Clases.Controles();

        #endregion
        
        private frmInventario frm;


        public frmAgregarProductos(frmInventario frm)
        {
            InitializeComponent();
            this.frm = frm;   //Asignación a la variable con el formulario padre
        }


        #region Métodos
        private void CargarDatos()
        {

            cbColor.DataSource = coleccionCalzados.Colores;

            coleccionCategorias.LlenarComboBox(cbCategoria, "Select id, concat_ws('-',idCategoria,nombreCategoria, marca) as categoria, nombreCategoria from categorias order by(idCategoria)", "id", "categoria");

            string categoria = coleccionCalzados.ObtenerCategoria(cbCategoria.Text);
            coleccionModelo.LlenarComboBox(cbModelo, $"Select id, nombreModelo from modelos where idCategoria = '%{categoria}%'", "id", "nombreModelo");
        } 
        #endregion


        #region Eventos Principales

        private void frmAgregarProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();

        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {

            objCalzado = new Clases.Calzado() //Nueva instancia del calzado
            {
                CodigoModelo = cbModelo.SelectedValue.ToString(),
                Descripcion = txtDescripcion.Texts.ToUpper(),
                TipoCalzado = cbSexo.Text,
                Talla = int.Parse(cbTalla.Text),
                Color = cbColor.Text,
                Cantidad = int.Parse(txtStock.Text),
                PrecioProducto = double.Parse(txtPrecioVenta.Text)
            };
            objCalzado.CodigoCategoria = objCalzado.ObtenerCategoria(cbCategoria.Text);
            
            objCalzado.Codigo = objCalzado.AsignarCódigo();

            objCalzado.Insertar(objCalzado.InsertarSQL);

 //---------------------------------------------------------------------------------------------
            //Validación en caso de algún error
            if(objCalzado.HayError == true)
            {
                if(objCalzado.NumeroError == 1062)
                {
                    MessageBox.Show("Ya existe este calzado el inventario");

                }
                else
                {
                    MessageBox.Show("Uy, Parece que hubo un problema, revise los datos que ingresó");

                }

            }
            else
            {
            frm.CargarDatos();
            this.Close();

            }
//-----------------------------------------------------------------------------------------------------
        }
        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            Close();

            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            controles.mostrarPopup(popup);

        }
        private void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            controles.mostrarPopup(popup);
            Close();

        }
        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
  
        }

        #endregion

        #region Eventos Normales

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtContraseñaConfirmar_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);

        }
        #endregion

        private void btnNuevaCategoria_Click_1(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            popup.FormClosed += new FormClosedEventHandler(categorias_FormClosed);
   
            controles.mostrarPopup(popup);
        }
        private void categorias_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {

            CargarDatos();

        }


        private void btnNuevoModelo_Click_1(object sender, EventArgs e)
        {
            frmCategoriasYModelos popup = new frmCategoriasYModelos();
            popup.FormClosed += new FormClosedEventHandler(modelos_FormClosed);

            controles.mostrarPopup(popup);
        }
        private void modelos_FormClosed(object sender, FormClosedEventArgs e) //Al cerrar el formulario de agregar productos se cargan los datos nuevamente
        {

            CargarDatos();

        }

        private void cbCategoria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string[] extractor = cbCategoria.Text.Split('-');
            coleccionModelo.LlenarComboBox(cbModelo, $"Select id, nombreModelo from modelos where idCategoria like '%{extractor[0]}%'", "id", "nombreModelo");

        }
    }
}
