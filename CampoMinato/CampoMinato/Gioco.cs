using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinato
{
    internal class Gioco
    {
        private Random rnd = new Random();

        private Cella[,] _tabella = new Cella[14, 18];
        private int mine = 40;

        public Cella[,] Tabella { get => _tabella; }

        public Gioco()
        {
            InizializzaTabella();
        }

        public void InizializzaTabella()
        {
            for (int i = 0; i < _tabella.GetLength(0); i++)
            {
                for (int j = 0; j < _tabella.GetLength(1); j++)
                {
                    _tabella[i, j] = new Cella();
                }
            }
        }

        public void GeneraTabella(int n_i, int n_j)
        {
            int mineMesse = 0;

            InizializzaTabella();

            while (mineMesse < mine)
            {
                int i = rnd.Next(_tabella.GetLength(0));
                int j = rnd.Next(_tabella.GetLength(1));
                if (!_tabella[i, j].Mina && !(i >= n_i - 1 && i <= n_i + 1 && j >= n_j - 1 && j <= n_j + 1))
                {
                    _tabella[i, j].MettiMina();
                    mineMesse++;
                }
            }

            for (int i = 0; i < _tabella.GetLength(0); i++)
            {
                for (int j = 0; j < _tabella.GetLength(1); j++)
                {
                    if (!_tabella[i, j].Mina)
                    {
                        int mineVicine = 0;
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (k >= 0 && k < _tabella.GetLength(0) && l >= 0 && l < _tabella.GetLength(1) && _tabella[k, l].Mina)
                                {
                                    mineVicine++;
                                }
                            }
                        }
                        _tabella[i, j].Numero = mineVicine;
                    }
                }
            }
        }

        public bool Scoppia(int i, int j)
        {
            if (_tabella[i, j].Scoperta || _tabella[i, j].Bandiera)
            {
                return false;
            }
            else if (_tabella[i, j].Mina)
            {
                InizializzaTabella();
                return true;
            }
            else
            {
                _tabella[i, j].Scopri();
                if (_tabella[i, j].Numero == 0)
                {
                    // Scopri tutte le celle vicine
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (k >= 0 && k < _tabella.GetLength(0) && l >= 0 && l < _tabella.GetLength(1) && !_tabella[k, l].Scoperta)
                            {
                                Scoppia(k, l);
                            }
                        }
                    }
                }
                return false;
            }
        }

        public bool Vittoria()
        {
            int celleScoperte = 0;
            for (int i = 0; i < _tabella.GetLength(0); i++)
            {
                for (int j = 0; j < _tabella.GetLength(1); j++)
                {
                    if (_tabella[i, j].Scoperta)
                    {
                        celleScoperte++;
                    }
                }
            }
            bool vittoria = celleScoperte == Tabella.Length - mine;
            if (vittoria)
            {
                InizializzaTabella();
            }
            return vittoria;
        }
    }
}
