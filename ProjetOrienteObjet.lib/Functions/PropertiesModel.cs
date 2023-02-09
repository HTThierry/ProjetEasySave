using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetEasySave.lib.Functions
{
    public class PropertiesModel
    {
        public string? PathFrom { get; set; }
        public string PathTo { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public float fichierNum = 0;
        public bool SaveState = false;

        private Stopwatch TimerA = new Stopwatch();

        //Fonction qui calcule le temps entre chaque copie de fichier
        public long Timer()
        {
            long time = TimerA.ElapsedMilliseconds;
            return time;
        }

    }
}
