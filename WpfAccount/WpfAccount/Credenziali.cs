/*
 * Gabriele Ventura    3H    30/04/2024
 * Classe per la gestione di account all'interno di un database, collegato al file xaml
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfAccount
{
    internal class Credenziali
    {
        private string file;
        private Dictionary<string, (string password, string nome, string cognome)> accounts = new Dictionary<string, (string, string, string)>();
        private enum Data
        {
            Username,
            Password,
            Nome,
            Cognome,
            Length
        }
        
        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="file">Percorso file database</param>
        public Credenziali(string file)
        {
            this.file = file;

            using (StreamReader sr = new StreamReader(this.file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(' ');

                    accounts.Add(parts[(int)Data.Username], (parts[(int)Data.Password], parts[(int)Data.Nome], parts[(int)Data.Cognome]));
                }
            }
        }

        /// <summary>
        /// Autentica credenziali
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Restituisce true se autenticato, altrimenti false (+ messaggio)</returns>
        public (string, bool) AutenticaUtente(string username, string password)
        {
            if (accounts.ContainsKey(username))
                if (accounts[username].password == Encrypt(password))
                    return ("Utente autenticato con successo", true);
                
            return ("Username o password errati", false);
        }

        /// <summary>
        /// Registra nuovo account
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="cognome">Cognome</param>
        /// <param name="username">Username</param>
        /// <param name="password1">Password</param>
        /// <param name="password2">Password ripetuta</param>
        /// <returns>Restituisce true se registra il nuovo utente, altrimenti false (+ messaggio)</returns>
        public (string, bool) RegistraAccount(string username, string nome, string cognome, string password1, string password2)
        {
            if (password1 != password2)
                return ("Le password non combaciano", false);

            if (!ControlloUsername(username))
                return ("Username non valido", false);

            if (!ControlloPassword(password1))
                return ("Password non valida", false);

            if (!ControlloNome(nome))
                return ("Nome non valido", false);

            if (!ControlloCognome(cognome))
                return ("Cognome non valido", false);

            if (accounts.ContainsKey(username))
                return ("Esiste già un account con questo username", false);

            accounts.Add(username, (Encrypt(password1), nome, cognome));
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine($"{username} {Encrypt(password1)} {nome} {cognome}");
            }
            return ("Registrazione avvenuta con successo", true);
        }

        /// <summary>
        /// Elimina un account
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Restituisce true se elimina l'account, altrimenti false (+ messaggio)</returns>
        public (string, bool) EliminaAccount(string username, string password)
        {
            if (accounts.ContainsKey(username))
                if (accounts[username].password == Encrypt(password))
                {
                    accounts.Remove(username);
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        foreach (string username_ in accounts.Keys)
                        {
                            sw.WriteLine($"{username_} {accounts[username_].password} {accounts[username_].nome} {accounts[username_].cognome}");
                        }
                    }
                    return ("Utente eliminato con successo", true);
                }

            return ("Username o password errati", false);
        }

        /// <summary>
        /// Encrypt della password
        /// </summary>
        /// <param name="password">Password in chiaro</param>
        /// <returns>Restituisce la password cryptata in base64</returns>
        private string Encrypt(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            string encodedString = Convert.ToBase64String(bytes);
            return encodedString;
        }

        /// <summary>
        /// Esegue controlli sulla password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Restituisce true se la password rispetta i requisiti</returns>
        private bool ControlloPassword(string password)
        {
            if (password.Length < 8) return false;

            if (!Regex.IsMatch(password, "[A-Z]")) return false;

            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            bool flag = false;
            foreach (char item in specialChar)
            {
                if (password.Contains(item))
                    flag = true;
            }
            if (!flag) return false;

            if (password.Contains(' ')) return false;

            return true;
        }

        /// <summary>
        /// Esegue controlli sullo username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Restituisce true se lo username rispetta i requisiti</returns>
        private bool ControlloUsername(string username)
        {
            if (username.Length > 25)
                return false;

            if (username.Contains(' '))
                return false;

            return true;
        }

        /// <summary>
        /// Esegue controlli sul nome
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <returns>Restituisce true se il nome rispetta i requisiti</returns>
        private bool ControlloNome(string nome)
        {
            if (nome.Length > 25)
                return false;

            if (nome.Contains(' '))
                return false;

            return true;
        }

        /// <summary>
        /// Esegue controlli sul cognome
        /// </summary>
        /// <param name="cognome">Cognome</param>
        /// <returns>Restituisce true se il cognome rispetta i requisiti</returns>
        private bool ControlloCognome(string cognome)
        {
            if (cognome.Length > 25)
                return false;

            if (cognome.Contains(' '))
                return false;

            return true;
        }
    }
}
