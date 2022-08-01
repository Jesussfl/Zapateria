using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zapateria.Controles;

namespace Zapateria.UI.Sesion
{
    public partial class frmIniciarSesion : Form
    {

        #region Instanciaciones

        Clases.Controles controles = new Clases.Controles();
        Clases.Sesion sesiones = new Clases.Sesion();
        Clases.Empleado nuevoEmpleado = new Clases.Empleado();

        #endregion

        #region Atributos

        private bool iniciar = true;
        private int codigoError; 

        #endregion

        //Constructor
        public frmIniciarSesion()
        {
            InitializeComponent();

        }

        #region Métodos

        private void ValidarIngreso() //Método para validar el inicio de sesion
        {
            sesiones.Correo = txtCorreo.Text;
            sesiones.Contraseña = txtContraseña.Text;
            if (sesiones.ValidarEmail(txtCorreo.Text) == true && HayCamposVacios(false) == false && sesiones.ValidarUsuario() == true)
            {
        
                sesiones.IniciarSesion(this);
                
            }
            else { txtContraseña.ResetText(); }

        }
        private bool HayCamposVacios(bool esRegistro = true)
        {
            if (esRegistro == true) //La validacion es para registrarse
            {
                if (string.IsNullOrEmpty(txtCedula.Text)
                || string.IsNullOrEmpty(txtNombres.Text)
                || string.IsNullOrEmpty(txtApellidos.Text)
                || string.IsNullOrEmpty(txtTelefono.Text)
                || string.IsNullOrEmpty(txtRegistroCorreo.Text)
                || string.IsNullOrEmpty(txtContraseñaConfirmar.Text))
                {
                    MessageBox.Show("Hay campos vacios");
                    return true; // Hay Campos Vacios
                }
                else { return false; } // No hay campos vacios
            }
            else //La validacion es para iniciar sesion
            {
                if(string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtContraseña.Text))
                {
                    return true; //Hay campos vacios
                }
                else { return false; }//No hay campos vacios
                }

        }
        private void CargarDatosRegistro() //Metodo para Cargar Datos al MySQL
        {
            nuevoEmpleado = new Clases.Empleado()
            {
                TipoCedulaEmpleado = "V",
                Cedula = txtCedula.Text,
                Nombres = txtNombres.Text.ToUpper(),
                Apellidos = txtApellidos.Text.ToUpper(),
                Telefono = txtTelefono.Text.ToString(),
                Correo = txtRegistroCorreo.Text,
                Contraseña = txtRegistrarContraseña.Text

            };
            nuevoEmpleado.CargarAtributos();


            if (nuevoEmpleado.HayError) //En caso de haber un error
            {
                iniciar = false;
                codigoError = nuevoEmpleado.NumeroError;
            }
        }
        private void validarRegistro() //Método para validar que se hayan ingresado datos en todos los campos
        {
            if( HayCamposVacios() == false 
                && sesiones.ValidarEmail(txtRegistroCorreo.Text) == true 
                && sesiones.ValidarContraseña(txtRegistrarContraseña.Text) == true)
            { 
                    CargarDatosRegistro();

                //Validar el inicio de sesion
                if (iniciar == false)
                {
                    if (codigoError == 1062)
                    {

                    MessageBox.Show("Ya existe alguien registrado con esta cedula");
                    }
                }
                else
                {
                    sesiones.IniciarSesion(this);

                }
            }
        }


        #endregion

        #region Eventos Principales

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIniciarSesion_Load(object sender, EventArgs e)
        {
            contenedorRegistro.Hide();
            this.ActiveControl = btnFocus;

        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (lblLink.Text == "Crearme una cuenta") 
            {
                ValidarIngreso();
            }
            else
            {
                validarRegistro();
            }

        }


        private void lblLink_Click(object sender, EventArgs e)
        {
            if (lblLink.Text == "Crearme una cuenta")
            {
                contenedorRegistro.Show();
                lblLink.Text = "Iniciar Sesión";
                lblPregunta.Text = "¿Ya tiene una cuenta?";
            }
            else
            {
                contenedorRegistro.Hide();
                lblLink.Text = "Crearme una cuenta";
                lblPregunta.Text = "¿No tienes cuenta?";
            }
        }

        #endregion

        #region Textboxes

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);
        }


        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnIniciar_Click(this, new EventArgs());
            }
        }

        private void txtContraseñaConfirmar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnIniciar_Click(this, new EventArgs());
            }
        }

        private void txtTelefono_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);
        }

        private void txtCedula_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            controles.AceptarSoloNumeros(e);

        } 

        #endregion
    }
}
