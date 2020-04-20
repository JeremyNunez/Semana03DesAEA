
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Laboratorio03
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ClsDatos obj = new ClsDatos();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("This method was executed when the grid was loaded");
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            dgvPedido.ItemsSource = obj.ListaPedidoFechas(Convert.ToDateTime(txtFechaInicio.Text)
                , Convert.ToDateTime(txtFechaFin.Text)).DefaultView;

        }   

        private void DgvPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idPedido;
            var item = dgvPedido.SelectedItem as DataRowView;
            if (null == item) return;
            idPedido = Convert.ToInt32(item.Row["idPedido"]);
            dgvDetallePedido.ItemsSource = obj.ListarDetalle(idPedido).DefaultView;
            txtTotal.Text = Convert.ToString(obj.PedidoTotal(idPedido));

        }
    }
}
