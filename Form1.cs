using java.io;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using Console = System.Console;
using System.Collections.Generic;

namespace Stanford.NLP.POSTagger.CSharp
{
    class Program
    {



        static void Main()
        {

            var jarRoot = @"C:\Users\Burds\Downloads\Stanford.NLP.NET-master (1)\Stanford.NLP.NET-master\samples\Stanford.NLP.POSTagger.CSharp\bin\Debug\stanford-postagger-2018-02-27";
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");

            // Text for tagging
            var text = "This is a test sentence.";
            string[] arr = new string[10];
            var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();
            string[] getType = new string[10];
            foreach (ArrayList sentence in sentences)
            {

                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine(SentenceUtils.listToString(taggedSentence, false));
                var data = new List<DataClass>();

                for (int i = 0; i < taggedSentence.size() - 1; i++)
                {
                    string myString = taggedSentence.get(i).ToString();

                    data.Add(new DataClass
                    {
                        SWord = sentence.get(i).ToString(),
                        WType = myString.Substring(myString.IndexOf("/") + 1)
                    });
                    //getType[i] = myString.Substring(myString.IndexOf("/") + 1);
                }




            }



        }

        /*   public List<DataClass> test1(string test1)
           {
               string test2 = "asdasd";

               return test2;
           }*/


    }
}
