using EasySave.lib.Services;

namespace EasySave.lib.Models
{
    public class Model
    {
        private static Model _Model;
        public List<SaveWorkService> ArrayOfSaveWork = new List<SaveWorkService>();

        private Model()
        { }

        public static Model GetInstance()
        {
            if (_Model == null)
            {
                _Model = new Model();
            }
            return _Model;
        }
    }
}