using System.Diagnostics;

namespace ProjetEasySave.lib.Functions
{
    public class LogFileModel
    {
        public string Name { get; set; }

        private Stopwatch TimerA = new Stopwatch();

        //Fonction qui calcule le temps entre chaque copie de fichier
        public long Timer()
        {
            long time = TimerA.ElapsedMilliseconds;
            return time;
        }
    }
}