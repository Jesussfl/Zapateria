using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Zapateria.Caja;
using Zapateria.Inventario;
using Microsoft.VisualBasic;
using Zapateria.UI.Caja;

namespace Zapateria.Secciones.Caja
{
    public partial class CrearFactura : Form
    {
        #region Instanciaciones
        Clases.Controles controles = new Clases.Controles();
        Clases.Calzado coleccionCalzado = new Clases.Calzado();
        Clases.Compra compra; 
        #endregion

        #region Atributos
        private bool existeColumna = false;
        private List<int> codigos = new List<int>();
        private List<string> productos = new List<string>();
        private List<double> precioMultiplicado = new List<double>(); 
        #endregion

        //Constructor
        public CrearFactura()
        {
            InitializeComponent();
            coleccionCalzado.Grid = dataGridView1;
            coleccionCalzado.CargarSQL = $@"Select inv.idProducto, concat_ws('-',ctg.nombreCategoria,ctg.marca,mdl.nombreModelo) as Producto, inv.tipoCalzado, inv.talla, inv.color, concat('$', FORMAT(precioVenta, 2, 'de_DE')) as precioVenta
                                        from inventario inv 
                                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.idCategoria) 
                                        INNER JOIN modelos mdl ON (inv.idModelo = mdl.id and inv.idCategoria = mdl.idCategoria)";
            coleccionCalzado.BuscarSQL = $@"Select inv.idProducto, concat_ws('-',ctg.nombreCategoria,ctg.marca,mdl.nombreModelo) as Producto, inv.tipoCalzado, inv.talla, inv.color, concat('$', FORMAT(precioVenta, 2, 'de_DE')) as precioVenta
                                        from inventario inv 
                                        INNER JOIN categorias ctg ON (inv.idCategoria = ctg.id) 
                                        INNER JOIN modelos mdl ON (inv.idModelo = mdl.indexer) 
                                        where concat_ws(idProducto,nombreCategoria,marca, nombreModelo,tipoCalzado,talla,color,precioVenta) 
                                        like";

            coleccionCalzado.Columnas = new string[] {"Codigo", "Producto", "Tipo", "Talla", "Color", "Precio"};

        }

        #region Métodos
        private void CargarBusqueda() //Método para cargar lo que se escriba en el buscador
        {
            coleccionCalzado.Cargar($"{coleccionCalzado.BuscarSQL} '%{busProducto.Text}%'");

            if (existeColumna == false)
            {
                coleccionCalzado.AsignarNombreColumnas();

                coleccionCalzado.AsignarBotones("acciones", "Acciones", "Añadir");

                modificarColumnas();

                existeColumna = true;
            }


        }
        private void modificarColumnas() //Metodo para asignar tamaños a las columnas
        {
            dataGridView1.Columns["idProducto"].FillWeight = 30;
            dataGridView1.Columns["tipoCalzado"].FillWeight = 35;
            dataGridView1.Columns["color"].FillWeight = 50;
            dataGridView1.Columns["talla"].FillWeight = 20;
            dataGridView1.Columns["acciones"].FillWeight = 30;
            dataGridView1.Columns["precioVenta"].FillWeight = 30;
        }
        private string obtenerCantidad(string texto) //Metodo para obtener la cantidad que el usuario escogió al añadir producto
        {
            string extractor = new string(texto.TakeWhile(Char.IsDigit).ToArray());

            return extractor;
        }
        #endregion


        #region Eventos Principales
        private void CrearFactura_Load(object sender, EventArgs e)
        {
            coleccionCalzado.Cargar(coleccionCalzado.CargarSQL);
            if (existeColumna == false)
            {
                coleccionCalzado.AsignarNombreColumnas();

                coleccionCalzado.AsignarBotones("acciones", "Acciones", "Añadir");

                modificarColumnas();

                existeColumna = true;
            }
        }


        private void btnAñadir_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Seguro que quieres añadir los elementos seleccionados a la factura?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                compra = new Clases.Compra(); //Creacion de nueva venta auxiliar

                for(int i = 0; i < codigos.Count;i++) //Ingreso de valores a los atributos de la clase venta
                {
                    compra.NombreProducto = productos[i];
                    compra.IdProducto = codigos[i];
                    compra.Cantidad = obtenerCantidad(productos[i]);
                    compra.PrecioCalculado = precioMultiplicado[i];
                    compra.Detalle = listProductos.Items[i].ToString();
                    compra.Insertar(compra.InsertarSQL, false);

                }
            }

            
           this.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) //Evento para dar click al botón del datagrid y añadir elementos a la lista
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Acciones")
            {
                using (frmIngresarCantidad frm = new frmIngresarCantidad()) //Se utiliza este metodo para extraer resultados del form hijo llamado (Ingresar Cantidad)
                {
                    frm.ShowDialog();

                    if (frm.resultado == true)
                    {
                        string result = frm.GetMyResult();

                        //Ingreso de valores a los atributos del formulario
                        codigos.Add(Convert.ToInt32(dataGridView1.CurrentRow.Cells["idProducto"].Value));
                        productos.Add(result + "X " + dataGridView1.CurrentRow.Cells["Producto"].Value + " de color " + dataGridView1.CurrentRow.Cells["color"].Value);

                        precioMultiplicado.Add(Convert.ToDouble(dataGridView1.CurrentRow.Cells["precioVenta"].Value.ToString().Substring(1)) * Convert.ToDouble(result)); //Calculo del monto total por producto

                        listProductos.Items.Clear(); //Se limpia el listbox en caso de agregar nuevos elementos
                        listProductos.Items.AddRange(productos.ToArray());
                    }
                    
                }

            }
        }


        #endregion

        #region Busqueda
        private void clearTb_Click(object sender, EventArgs e)
        {
            //Botón de limpiar busqueda
            busProducto.Clear();
               
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == false) { busProducto.Text = "Buscar Producto";  }
        }

        private void busProducto_TextChanged(object sender, EventArgs e)
        {
            //Valida que el texto de ayuda esté colocado o no para hacer visible el botón de limpiar
            if (string.IsNullOrWhiteSpace(busProducto.Text) && busProducto.Focused == true || busProducto.Text == "Buscar Producto")
            { 
                clearTb.Visible = false;
                coleccionCalzado.Cargar(coleccionCalzado.CargarSQL);


            }
            else if (string.IsNullOrWhiteSpace(busProducto.Text))
            {
                coleccionCalzado.Cargar(coleccionCalzado.CargarSQL);


            }
            else 
            { 
                clearTb.Visible = true;
                CargarBusqueda();
            }
        }

        private void busProducto_Leave_1(object sender, EventArgs e)
        {
            //Añade o quita el texto de ayuda al buscador
            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        }

        private void busProducto_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; }
        }

        private void busProducto_Enter_1(object sender, EventArgs e)
        {
            //Añade el texto de ayuda al buscador
            controles.añadirPlaceholder(busProducto, "Buscar Producto");
        }


        #endregion

        #region Botones básicos
        private void button1_Click(object sender, EventArgs e)
        {
            listProductos.Items.Clear();
            codigos.Clear();
            productos.Clear();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {

            codigos.RemoveAt(listProductos.SelectedIndex);
            productos.RemoveAt(listProductos.SelectedIndex);
            listProductos.Items.RemoveAt(listProductos.SelectedIndex);
        }
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            datagridContenedor.Show();
            btnMostrar.Visible = false;
            btnOcultar.Visible = true;
        }
        private void btnOcultar_Click(object sender, EventArgs e)
        {
            datagridContenedor.Hide();
            btnMostrar.Visible = true;
            btnOcultar.Visible = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}
