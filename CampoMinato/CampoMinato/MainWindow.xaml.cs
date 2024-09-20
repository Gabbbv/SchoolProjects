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
        private Gioco _gioco = new Gioco();
        private bool _primoClick = true;

        public MainWindow()
        {
            InitializeComponent();
            Griglia.Height = _gioco.Tabella.GetLength(0) * 30 - 5;
            Griglia.Width = _gioco.Tabella.GetLength(1) * 30 - 5;
            Container.Height = Griglia.Height + 100;
            Container.Width = Griglia.Width + 200;
            Refresh();
        }

        public void Refresh()
        {
            this.Griglia.Children.Clear();

            for (int i = 0; i < _gioco.Tabella.GetLength(0); i++)
            {
                for (int j = 0; j < _gioco.Tabella.GetLength(1); j++)
                {
                    // Creo un bottone per ogni cella
                    Button b = new Button();
                    b.Tag = new int[] { i, j };
                    b.Click += B_Click;
                    b.MouseRightButtonDown += B_MouseRightButtonDown;
                    b.Width = 25;
                    b.Height = 25;
                    b.Margin = new Thickness(5);
                    if (_gioco.Tabella[i, j].Scoperta && !_gioco.Tabella[i, j].Mina && _gioco.Tabella[i, j].Numero != 0)
                    {
                        b.Content = _gioco.Tabella[i, j].Numero;
                    }
                    else 
                    { 
                        b.Content = ""; 
                    }
                    if (_gioco.Tabella[i, j].Scoperta)
                    {
                        b.Background = Brushes.White;
                    }
                    else if (_gioco.Tabella[i, j].Bandiera)
                    {
                        b.Background = Brushes.Red;
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
            if (_primoClick)
            {
                _gioco.GeneraTabella(pos[0], pos[1]);
                _primoClick = false;
            }
            if (_gioco.Scoppia(pos[0], pos[1]))
            {
                MessageBox.Show("Hai perso!");
                _primoClick = true;
            }
            Refresh();
            if (_gioco.Vittoria())
            {
                MessageBox.Show("Hai vinto!");
                _primoClick = true;
                Refresh();
            }
        }

        public void B_MouseRightButtonDown(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int[] pos = (int[])b.Tag;
            _gioco.Tabella[pos[0], pos[1]].TogliMettiBandiera();
            Refresh();
        }
    }
}