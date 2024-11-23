using juwel.kolychevaDataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace juwel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        kolychevaDataSet1 db = new kolychevaDataSet1();
        public MainWindow()
        {
            InitializeComponent();
            RequestGrid.ItemsSource = db.Orders.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Zayavka mainPage = new Zayavka();
            mainPage.Show();
            this.Close();
        }

        private void AddButtons(object sender, RoutedEventArgs e)
        {
            try 
            {
                Orders _orders = new Orders;
                { 
                    OrderID = 
                    Client_Id = 
                    Craftsperson_id = 
                    OrderDate = 
                    OrderDesription = 
                    OrderStatus_id = 
                }
                db.Orders.Add(_orders);
                db.SaveChanges();
                DGRoute.ItemsSource = kolychevaDataSet1.GetContext().Orders.ToList();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        private void DeleteButtons(object sender, RoutedEventArgs e)
        {
            var propDel = RequestGrid.SelectedItems.Cast<Orders>().ToList();
            try
            {
                if (MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    db.Orders.RemoveRange(propDel);
                    db.SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    RequestGrid.ItemsSource = kolychevaDataSet1.GetContext().Orders.ToList();
                }
                RequestGrid.ItemsSource = db.Orders.ToList();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }

        }

        private void EditRoute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int routID = Convert.ToInt32(RouteIdTB.Text);
                var selectedId = db.routes_buss.Where(w => w.route_id == routID).FirstOrDefault();
                selectedId.route_id = int.TryParse(RouteIdTB.Text, out int route) ? route : 0;
                selectedId.route_name = RouteNameTB.Text;
                selectedId.start_point = StartPointTb.Text;
                selectedId.end_point = EndPointTb.Text;
                selectedId.bus_id = int.TryParse(BusIdTb.Text, out int bus_id) ? bus_id : 0;
                selectedId.parking_id = int.TryParse(parkingIdTb.Text, out int parking_id) ? parking_id : 0;
                db.SaveChanges();
                DGRoute.ItemsSource = BusPPEntities.GetContext().routes_buss.ToList();
                MessageBox.Show("Данные успешно обновлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }
    }
}
