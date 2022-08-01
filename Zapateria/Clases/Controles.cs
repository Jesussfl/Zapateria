using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using Zapateria.Controles;

namespace Zapateria.Clases
{
    public class Controles
    {
        //Clase dedicada exclusivamente a almacenar métodos sencillos para los controles de la interfáz gráfica

        public Point Location { get; private set; }
        public void añadirPlaceholder(TextBox tb, string text)
        {
            //Funcion para añadir texto de ayuda en los textbox
            if (tb.Text == text) 
            {
             
                tb.Text = "";  
            }
            else if (string.IsNullOrWhiteSpace(tb.Text)) { tb.PasswordChar = '\0'; tb.Text = text; }
        }
        public void añadirPlaceholderInputs(InputText tb, string text)
        {
            //Funcion para añadir texto de ayuda en los textbox
            if (tb.Texts == text) 
            {
                if (tb.Texts == "Contraseña" || tb.Texts == "Confirmar Contraseña")
                {
                    tb.Texts = "";
                    tb.PasswordChar = true;

                } 
                    tb.Texts = "";
            }
            else if (string.IsNullOrWhiteSpace(tb.Texts)) { tb.PasswordChar = false; tb.Texts = text; }
        }

        public void AceptarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public void mostrarPopup(Form frm)
        {

            //Funcion para mostrar los popup con un fondo oscuro
            Form mainform = new Form();
            try 
            { 

            mainform.StartPosition = FormStartPosition.CenterScreen;
            mainform.FormBorderStyle = FormBorderStyle.None;
            mainform.Opacity = .50d;
            mainform.BackColor = Color.Black;
            mainform.WindowState = FormWindowState.Maximized;
           // mainform.TopMost = true;
            mainform.Location = this.Location;
            mainform.ShowInTaskbar = false;
            mainform.Show();
            frm.Owner = mainform;
            frm.ShowDialog();
            frm.BringToFront();
            mainform.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               mainform.Dispose();
            }
        }

    }
}
