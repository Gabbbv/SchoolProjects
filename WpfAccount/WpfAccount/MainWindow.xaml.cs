/*
 * Gabriele Ventura    3H    30/04/2024
 * Gestione eventi del file xaml
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAccount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Credenziali database_manager = new Credenziali(@"..\..\..\database.txt");

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (txt_username_login.Text != "" && txt_password_login.Password != "")
            {
                (string messaggio, bool esito) risposta = database_manager.AutenticaUtente(txt_username_login.Text, txt_password_login.Password);
                if (risposta.esito)
                {
                    lbl_errore_login.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_errore_login.Foreground = Brushes.Red;
                }
                lbl_errore_login.Content = risposta.messaggio;
            }
            else
            {
                lbl_errore_login.Foreground = Brushes.Red;
                lbl_errore_login.Content = "Inserire username e password";
            }
        }

        private void btn_elimina_account_Click(object sender, RoutedEventArgs e)
        {
            if (txt_username_elimina_account.Text != "" && txt_password_elimina_account.Password != "")
            {
                (string messaggio, bool esito) risposta = database_manager.EliminaAccount(txt_username_elimina_account.Text, txt_password_elimina_account.Password);
                if (risposta.esito)
                {
                    lbl_errore_elimina_account.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_errore_elimina_account.Foreground = Brushes.Red;
                }
                lbl_errore_elimina_account.Content = risposta.messaggio;
            }
            else
            {
                lbl_errore_login.Foreground = Brushes.Red;
                lbl_errore_login.Content = "Inserire username e password";
            }
        }

        private void btn_registra_account_Click(object sender, RoutedEventArgs e)
        {
            if (txt_username_registra_account.Text != "" && txt_nome_registra_account.Text != "" && txt_cognome_registra_account.Text != "" && txt_password_registra_account.Password != "" && txt_ripeti_password_registra_account.Password != "")
            {
                (string messaggio, bool esito) risposta = database_manager.RegistraAccount(txt_username_registra_account.Text, txt_nome_registra_account.Text, txt_cognome_registra_account.Text, txt_password_registra_account.Password, txt_ripeti_password_registra_account.Password);
                if (risposta.esito)
                {
                    lbl_errore_registra_account.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_errore_registra_account.Foreground = Brushes.Red;
                }
                lbl_errore_registra_account.Content = risposta.messaggio;
            }
            else
            {
                lbl_cognome_registra_account.Foreground = Brushes.Red;
                lbl_cognome_registra_account.Content = "Inserire tutti i campi";
            }
        }
    }
}
