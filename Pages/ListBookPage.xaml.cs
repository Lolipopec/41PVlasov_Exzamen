using _41PVlasov_Exzamen.Support_class;
using System;
using System.Collections.Generic;
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

namespace _41PVlasov_Exzamen.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListBookPage.xaml
    /// </summary>
    public partial class ListBookPage : Page
    {
        List<Book> Busket = new List<Book>();
        List<Book> Books = Book.getListBook();
        public ListBookPage()
        {
            InitializeComponent();
            try
            {
                ListBookListBox.ItemsSource = Books;
            }
            catch
            {

            }
        }

        public ListBookPage(List<Book> Busket)
        {
            InitializeComponent();
            try
            {
                ListBookListBox.ItemsSource = Books;
                this.Busket = Busket;
                double cost = 0;
                int count = 0;
                foreach (Book book1 in Busket)
                {
                    count += Convert.ToInt32(book1.CountBook);
                    cost = cost + Convert.ToDouble(book1.Cost) * Convert.ToDouble(book1.CountBook);
                }
                CountSelectBook.Text = count.ToString();
                double Sale = DLL.DLL.SaleCost(Convert.ToInt32(count), cost);
                СostSaleSelectBook.Text = " " + (cost - (cost * Sale)).ToString();
                СostSelectBook.Text = cost.ToString();
                СostSelectBook.Visibility = Visibility.Visible;
                SaleProcentBook.Text = (Sale * 100).ToString();
                if (Busket.Count == 0)
                {
                    BaseConnect.BaseModel = new Entities1();
                    Books = Book.getListBook();
                    ListBookListBox.ItemsSource = Books;
                    ListBookListBox.Items.Refresh();
                }
            }
            catch
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button senderButton = (Button)sender;
                int id = Convert.ToInt32(senderButton.Uid);
                Book book = Books.FirstOrDefault(x => x.id == id);
                if (book != null)
                {
                    if (Convert.ToInt32(book.CountBook) + 1 <= book.CountInStock + book.CountInStore)
                    {
                        book.CountBook = (Convert.ToInt32(book.CountBook) + 1).ToString();
                        if (Busket.Where(x => x.id == id).Count() > 0)
                        {
                        }
                        else
                        {
                            Busket.Add(Books.FirstOrDefault(x => x.id == id));
                        }
                        ListBookListBox.ItemsSource = Books;
                        double cost = 0;
                        int count = 0;
                        foreach (Book book1 in Busket)
                        {
                            count += Convert.ToInt32(book1.CountBook);
                            cost = cost + Convert.ToDouble(book1.Cost) * Convert.ToDouble(book1.CountBook);
                        }
                        CountSelectBook.Text = count.ToString();
                        double Sale = DLL.DLL.SaleCost(Convert.ToInt32(count), cost);
                        СostSaleSelectBook.Text = " " + (cost - (cost * Sale)).ToString();
                        СostSelectBook.Text = cost.ToString();
                        СostSelectBook.Visibility = Visibility.Visible;
                        ListBookListBox.Items.Refresh();
                        SaleProcentBook.Text = (Sale * 100).ToString();
                    }
                    else MessageBox.Show("Нет в наличии!");
                }
            }
            catch
            {

            }
        }

        private void GoBucket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadPage.MainFrame.Navigate(new BusketPage(Busket));
            }
            catch
            {

            }
        }
    }
}
