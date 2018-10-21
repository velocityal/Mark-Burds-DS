using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanford.NLP.POSTagger.CSharp
{
    public class DataClass
    {
        private String sWord;
        private string wType;
        private string Phrase;
        private string tagSentence;

        public string SWord { get => sWord; set => sWord = value; }
        public string WType { get => wType; set => wType = value; }
        public string Phrase1 { get => Phrase; set => Phrase = value; }
        public string TagSentence { get => tagSentence; set => tagSentence = value; }
    }
}
