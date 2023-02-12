namespace EasySave.lib.Services
{
    public class Presenter
    {
        public string[][] GetSaveWorkInfos(List<SaveWork> ArrayOfSaveWork)
        {
            string[][] SaveWorkInfos = new string[ArrayOfSaveWork.Count][];

            for (int i = 0; i < ArrayOfSaveWork.Count; ++i)
            {
                if (ArrayOfSaveWork[i] != null)
                    SaveWorkInfos[i] = ArrayOfSaveWork[i].GetInstanceInfo();
            }
            return SaveWorkInfos;
        }

        public string[] GetSaveWorkNames(List<SaveWork> ArrayOfSaveWork)
        {
            string[] NameOfSaveWorks = new string[ArrayOfSaveWork.Count];

            for (int i = 0; i < ArrayOfSaveWork.Count; i++)
            {
                NameOfSaveWorks[i] = ArrayOfSaveWork[i].GetInstanceInfo()[0];
            }
            return NameOfSaveWorks;
        }
    }
}