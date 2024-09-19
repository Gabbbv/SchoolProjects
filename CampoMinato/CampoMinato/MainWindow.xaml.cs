using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CampoMinato
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Refresh();
        }

        private Gioco gioco = new Gioco();

        public void Refresh()
        {
            this.Griglia.Children.Clear();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // Creo un bottone per ogni cella
                    Button b = new Button();
                    b.Tag = new int[] { i, j };
                    b.Click += B_Click;
                    b.Width = 25;
                    b.Height = 25;
                    b.Margin = new Thickness(5);
                    if (gioco.Tabella[i, j].Scoperta && !gioco.Tabella[i,j].Mina && gioco.Tabella[i, j].Numero != 0)
                    {
                        b.Content = gioco.Tabella[i, j].Numero;
                    }
                    else 
                    { 
                        b.Content = ""; 
                    }
                    if (gioco.Tabella[i, j].Scoperta)
                    {
                        b.Background = Brushes.White;
                    }
                    else
                    {
                        b.Background = Brushes.Blue;
                    }
                    b.VerticalAlignment = VerticalAlignment.Top;
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.Margin = new Thickness(j * 30, i * 30, 0, 0);
                    this.Griglia.Children.Add(b);
                }
            }
        }

        public void B_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int[] pos = (int[])b.Tag;
            if (gioco.Scoppia(pos[0], pos[1]))
            {
                MessageBox.Show("Hai perso!");
            }
            Refresh();
        }
    }
}