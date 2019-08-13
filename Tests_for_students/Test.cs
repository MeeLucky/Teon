using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Tests_for_students
{
    [Serializable]
    public class Test
    {
        public string testName = "Название теста";
        public string author = "";
        public string desckription = "";
        public string userName = "Аноним";
        public string password = "";

        public List<Question> qList = new List<Question>() { new Question("Пример вопроса",
                                                            new string[] { "Пример ответа 1", "Пример ответа 2", "Пример ответа 3 (правильный)" },
                                                            new int[] { 0, 0, 1 } ) };

        private string path;

        public Test(string path)
        {
            this.path = path;
        }

        public void SetPath (string path)
        {
            this.path = path;
        }

        public static Test OpenTest(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Test deserilizetestlist = deserilizetestlist = (Test)formatter.Deserialize(fs);
                
                    return deserilizetestlist;
                }
            }
            catch (Exception) { return null; };
        }

        public void SaveTest(List<Question> qList, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, qList);
            }
        }

        public void SaveTest()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(this.path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
    }
}