using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace CampoMinato
{
    internal class Cella
    {
        private bool _mina = false;
        private bool _scoperta = false;
        private bool _bandiera = false;
        private int _numero;

        public bool Mina { get => _mina; }
        public bool Scoperta { get => _scoperta; }
        public bool Bandiera { get => _bandiera; }
        public int Numero { get => _numero; set => _numero = value; }
        
        public void MettiMina()
        {
            if (!_mina)
            {
                _mina = true;
            }
        }

        public void Scopri()
        {
            if (!_scoperta)
            {
                _scoperta = true;
            }
        }

        public void TogliMettiBandiera()
        {
            _bandiera = !_bandiera;
        }
    }
}
