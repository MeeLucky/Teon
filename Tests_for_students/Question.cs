using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_for_students
{
    [Serializable]
    public class Question
    {
        private string question;
        private string[] answers;
        private int[] maskAnswers;

        public Question(string question, string[] answers, int[] maskAnswers)
        {
            this.question = question;
            this.answers = answers;
            this.maskAnswers = maskAnswers;
        }

        public string GetQuestion()
        {
            return this.question;
        }

        public string[] GetAnswers()
        {
            return this.answers;
        }

        public int[] GetMask ()
        {
            return this.maskAnswers;
        }
    }
}
