using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ActividadCaso1.Models;

namespace ActividadCaso1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClsDatos obj = new ClsDatos();

        string idCLiente;
        Int32 idPedido;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {}

        private void cboAnioPedido_Loaded(object sender, RoutedEventArgs e)
        {
            List<Pedido> pedidos = obj.ListarPedidoFecha();
            //cbo show
            List<string> dataAnio = new List<string>();
            foreach (var pedido in pedidos)
            {
                dataAnio.Add(pedido.Anio+"");
            }
            cboAnioPedido.ItemsSource = dataAnio;
        }

        private void cboAnioPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            List<Pedido> pedidos = obj.ListarPedidoFecha(text);
            // cbo show
            List<string> dataMes = new List<string>();
            foreach (var pedido in pedidos)
            {
                dataMes.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(pedido.Mes));
            }
            cboMesPedido.ItemsSource = dataMes;

        }

        private void cboMesPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int anio = int.Parse(cboAnioPedido.Text);

            string stringMes = cboMesPedido.Text;
            int mes = DateTime.Parse("1." + stringMes + " 2008").Month;

            List<Empleado> empleados = obj.ListarEmpleados(mes, anio);
            // cbo show
            List<string> dataEmpleado = new List<string>();
            foreach (var empleado in empleados)
            {
                dataEmpleado.Add(empleado.Nombre+" "+empleado.Apellidos);
            }
            cboEmpleado.ItemsSource = dataEmpleado;

        }

        private void cboEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btnMostrarLista(object sender, RoutedEventArgs e)
        {
            int anio = int.Parse(cboAnioPedido.Text);
            string stringMes = cboMesPedido.Text;
            int mes = DateTime.Parse("1." + stringMes + " 2008").Month;
            string nombreEmpleado = cboEmpleado.Text;

            System.Console.WriteLine(anio);
            System.Console.WriteLine(mes);
            System.Console.WriteLine(nombreEmpleado);

            //DataTable dt = new DataTable();
            dgvClientes.ItemsSource = obj.ListarCliente(mes, anio, nombreEmpleado).DefaultView;
        }

        private void dgvClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgvClientes.SelectedItem as DataRowView;
            if (null == item) return;
            this.idCLiente = Convert.ToString(item.Row["Codigo"]);
            System.Console.WriteLine(this.idCLiente);
            dgvPedido.ItemsSource = obj.ListarPedido(this.idCLiente).DefaultView;
            //txtTotal.Text = Convert.ToString(obj.PedidoTotal(idPedido));
            txtNroPedidos.Text = Convert.ToString(obj.ListarPedido(this.idCLiente).Rows.Count);
        }

        private void dgvPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgvPedido.SelectedItem as DataRowView;
            if (null == item) return;
            this.idPedido = Convert.ToInt32(item.Row["Nro Pedido"]);
            System.Console.WriteLine(this.idPedido);
            dgvDetallePedido.ItemsSource = obj.ListarDetallePedido(this.idPedido).DefaultView;

            // show count of products in the textbox
            txtTotalProductos.Text = Convert.ToString(obj.ListarDetallePedido(this.idPedido).Rows.Count);

            //show total price in the textbox
            txtMontoTotal.Text = Convert.ToString(obj.PedidoTotal(this.idPedido));

        }
    }
}
