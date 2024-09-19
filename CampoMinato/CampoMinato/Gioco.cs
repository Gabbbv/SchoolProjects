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
        private Cella[,] _tabella = new Cella[10, 10];

        public Cella[,] Tabella { get => _tabella; }

        public Gioco()
        {
            int mineMesse = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bool mina = rnd.Next(0, 100) < 10;
                    _tabella[i, j] = new Cella();
                    if (mina && mineMesse < 10)
                    {
                        _tabella[i, j].MettiMina();
                        mineMesse++;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!_tabella[i, j].Mina)
                    {
                        int mineVicine = 0;
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (k >= 0 && k < 10 && l >= 0 && l < 10 && _tabella[k, l].Mina)
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
            if (_tabella[i, j].Mina)
            {
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
                            if (k >= 0 && k < 10 && l >= 0 && l < 10 && !_tabella[k, l].Scoperta)
                            {
                                Scoppia(k, l);
                            }
                        }
                    }
                }
                return false;
            }
        }
    }
}
