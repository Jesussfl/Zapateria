using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace Zapateria.Clases
{
    public class Controles
    {
        
        public void añadirPlaceholder(TextBox tb, string text)
        {
            //Funcion para añadir texto de ayuda en los textbox
            if (tb.Text == text) { tb.Text = ""; }
            else if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = text; }
        }
        
        public Point Location { get; private set; }

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
