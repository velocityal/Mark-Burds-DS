using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.io;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using Console = System.Console;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace Stanford.NLP.POSTagger.CSharp
{
    public partial class Form2 : Form
    {
        //private String myString;
        public Form2()
        {
            InitializeComponent();



        }



        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = getQuestion(NewChunk(getTable(textBox1.Text)));
           // NewChunk(getTable("The Pasig River  is a river in the Philippines that connects Laguna de Bay to Manila Bay."));
           
           // getChunk(getWhatSentence(getData(),textBox1.Text));
            
        }



        public List<DataClass> getTable(String ask)
        {
            String directories;
            directories = System.IO.Directory.GetCurrentDirectory();


            List<object> NP = new List<object>();
            var jarRoot = directories + @"\stanford-postagger-2018-02-27";
            var modelsDirectory = jarRoot + @"\models";
            List<DataClass> newdata = new List<DataClass>();
            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");
            var data = new List<DataClass>();

            // Text for tagging
            var text = ask;
            string[] arr = new string[10];
            var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();
            string[] getType = new string[10];
            foreach (ArrayList sentence in sentences)
            {

                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine(SentenceUtils.listToString(taggedSentence, false));


                for (int i = 0; i <= taggedSentence.size() - 1; i++)
                {
                     String myString = taggedSentence.get(i).ToString();

                    data.Add(new DataClass
                    {
                        SWord = sentence.get(i).ToString(),
                        WType = myString.Substring(myString.IndexOf("/") + 1)
                    });
                    //getType[i] = myString.Substring(myString.IndexOf("/") + 1);
                }
                
            }
            String[] locations = { "Pasig", "Manila", "Laguna de Bay", "Philippines", "Marikina" };
            for (int k = 0; k <= data.Count - 1; k++)
            {
                if (locations.Contains(data[k].SWord))
                {
                    data[k].WType = "LC";
                }
            }
            for (int k = 0; k <= data.Count - 1; k++)
            {
                if (k < 0)
                {
                    if (data[k].WType == "IN")
                    {
                        if (data[k - 1].WType == "NNS")
                        {
                            data[k - 1].WType = "VBS";
                        }
                    }
                }
                // k++;
            }
            return data;
        }

        public String getParse(String ask)
        {
            String directories;
            directories = System.IO.Directory.GetCurrentDirectory();


            List<object> NP = new List<object>();
            var jarRoot = directories + @"\stanford-postagger-2018-02-27";
            var modelsDirectory = jarRoot + @"\models";
            List<DataClass> newdata = new List<DataClass>();
            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");
            var data = new List<DataClass>();

            // Text for tagging
            var text = ask;
            string[] arr = new string[10];
            var sentences = MaxentTagger.tokenizeText(new StringReader(text)).toArray();
            string[] getType = new string[10];
            String tagString = "";
            foreach (ArrayList sentence in sentences)
            {

                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine(SentenceUtils.listToString(taggedSentence, false));
                tagString = string.Join(" ",taggedSentence);
                
                for (int i = 0; i <= taggedSentence.size() - 1; i++)
                {
                     String myString = taggedSentence.get(i).ToString();

                    data.Add(new DataClass
                    {
                        SWord = sentence.get(i).ToString(),
                        WType = myString.Substring(myString.IndexOf("/") + 1)
                    });
                    //getType[i] = myString.Substring(myString.IndexOf("/") + 1);
                }
                
            }
            //int k = 0;
            String outStr = "";
            String[] locations = { "Pasig", "Manila", "Laguna de Bay", "Philippines", "Marikina" };
            for (int k = 0; k <= data.Count - 1; k++)
            {
                if (locations.Contains(data[k].SWord) && data[k + 1].WType != "NNP")
                {
                    data[k].WType = "LC";
                }
                outStr = outStr + data[k].SWord + "/" + data[k].WType;
               // k++;
            }
            //String[] locations = { "Pasig", "Manila", "Laguna de Bay", "Philippines", "Marikina" };
           
            String outPut = "[" + outStr + "]";
           // return s;
            return tagString;
        }

        public String getWhatSentence(String[] data, String ask)
        {
            String askSub = "";
           var question = new List<DataClass>();
            question = getTable(ask);
           
                if (question[0].SWord == "What")
                {
                for (int i = 0; i <= question.Count - 1; i++)
                {
                    if (question[i].WType == "NN" && question[i + 1].WType == "IN" && question[i + 2].WType == "DT" && question[i+3].WType == "NN")
                    {
                        askSub += question[i + 3].SWord + " ";
                        if(question[i].SWord == "location")
                        {
                            return getLocationSentence(askSub, getData());
                           // goto end_of_loop;
                           
                        }
                    }
                    else if(question[i].WType == "NN" && question[i+1].WType == "POS" && question[i + 2].WType == "NN")
                    {
                        askSub += question[i].SWord + " ";
                        if (question[i+2].SWord == "location")
                        {
                          return getLocationSentence(askSub, getData());
                           // goto end_of_loop;
                        }
                    }
                        Regex word = new Regex(@"(NN)");
                        if (word.Match(question[i].WType).Success)
                        {
                            askSub += question[i].SWord + " ";
                        }
                        else if (question[i].SWord == "?")
                        {
                            break;
                        }
                    
                }
                }
                else if (question[0].SWord == "Where")
                {
                    for (int i = 0; i <= question.Count - 1; i++)
                    {
                        Regex word = new Regex(@"(NN)");
                        if (word.Match(question[i].WType).Success)
                        {
                            askSub += question[i].SWord + " ";
                        }
                        else if (question[i].SWord == "?")
                        {
                            break;
                        }
                    }
                }
            

            for(int i = 0; i <= data.Length - 1; i++)
            {
                Regex word = new Regex(@"("+ askSub +")");
                if(word.Match(data[i]).Success)
                {
                    return data[i];
                }

            }



            return "Not Found";
           // end_of_loop:;
            
        }

       

        public String getChunk(String ans)
        {
            String parsed = getParse(ans);


            var rules = new List<Rule>
{
    new Rule { ChunkName = "NP", Pattern = @"{*/(DT|JJ|NNPS|NNP|NNS|NN|PRP)}+" }, // noun phrase
    new Rule { ChunkName = "PP", Pattern = @"{*/IN} {NP}" },                      // prepositional phrase
    new Rule { ChunkName = "VP", Pattern = @"{*/VB.*?} {NP} {PP}*" },             // verb phrase
    new Rule { ChunkName = "DC", Pattern = @"{NP} {VP}" }                         // declarative clause
};

            string chunkedText = new Chunker().Chunk(parsed, rules);
            
            splitChunk(chunkedText);
            return chunkedText;
        }

        public void splitChunk(String parsedAns)
        {
            String regex = @"\[NP(.*?)\]\]\,";
            var matches = Regex.Matches(parsedAns, regex);

            string[] phrases = Regex.Split(parsedAns, @"");

            textBox2.Text = matches[1].ToString();
        }

        public String getLocationSentence(String subject, String[] data)
        {
            String[] locations = { "Pasig", "Manila", "Laguna de Bay", "Philippines", "Marikina" };

            for(int i = 0; i<= data.Length - 1; i++)
            {
               // String regex = @"(?<=["+ subject +"])\s+";
               // var matches = Regex.Matches(parsedAns, regex);
               for(int j = 0; j <= locations.Length; j++)
                { 
                if (data[i].Contains(subject) && data[i].Contains(locations[j]))
                {
                        return data[i];
                }

                }
               
            }

            return "";
        }

        public List<ChunkPhrases> NewChunk(List<DataClass> tagSen)
        {
            var listData = new List<ChunkPhrases>();
            String phraseName = "None";
            List<DataClass> newData = new List<DataClass>();
            for (int i = 0; i <= tagSen.Count - 1; i++)
            {
                //Regex prep = new Regex(@"{*/IN} {NP}");
                Regex sub = new Regex("(VB.*)|(IN)");
                Regex word = new Regex("(^DT)|(JJ)|(NNPS)|(NNP)|(NNS)|(NN)|(PRP)|(LC)");
                if (word.Match(tagSen[i].WType).Success)
                {
                    newData.Add(tagSen[i]);
                    if(word.Match(tagSen[i+1].WType).Success == false)
                    {
                        phraseName = "NP";
                        listData.Add(new ChunkPhrases
                        {
                            Chunk = newData,
                            Name = phraseName
                        });
                        newData = new List<DataClass>();
                    }
                }
                
                else if (sub.Match(tagSen[i].WType).Success)
                {
                    newData.Add(tagSen[i]);
                    if (sub.Match(tagSen[i + 1].WType).Success == false)
                    {
                        phraseName = "S";
                        listData.Add(new ChunkPhrases
                        {
                            Chunk = newData,
                            Name = phraseName
                        });
                        newData = new List<DataClass>();
                    }
                }
                
            }


            return listData;
        }

        public String getQuestion(List<ChunkPhrases> question)
        {
            List<String> themeSearch = new List<String>();
            List<String> questSearch = new List<String>();
            String quest = "";
            String theme = "";
            int x = 0;
            int y = 0;
            for(int i = 0;i <= question.Count-1; i++)
            {
                if(question[i].Name == "S")
                {
                    for(int j = 0; j<= question[i].Chunk.Count-1; j++)
                    {
                        quest += "(?=.*";
                        quest = quest + question[i].Chunk[j].SWord ;
                        quest += ".*)";
                        questSearch.Add("(" + question[i].Chunk[j].SWord + ".*)");
                       // if (j != question[i].Chunk.Count - 1)
                       // {
                       //  quest += "|";
                       //}
                    }
                    
                }
                if (question[i].Name == "NP")
                {

                    for (int j = 0; j <= question[i].Chunk.Count - 1; j++)
                    {
                        if (question[i].Chunk[j].WType != "DT")
                        {
                            theme += "(?=.*";
                            theme = theme + question[i].Chunk[j].SWord;
                            theme += ".*)";
                            themeSearch.Add("(" + question[i].Chunk[j].SWord + ".*)");
                            //if (j != question[i].Chunk.Count - 1)
                            //{
                            //theme += "&";
                            //}
                        }
                    }
                }
            }
            String[] sentences = getData();
           
            List<String> phrases = new List<String>();
            for(int i = 0; i<= sentences.Length - 1; i++)
            {
                Regex qst = new Regex(@"" + quest.ToUpper() + "");
                Regex thm = new Regex(@"" + theme.ToUpper() + "");
                
                if (qst.Match(sentences[i].ToUpper()).Success && thm.Match(sentences[i].ToUpper()).Success)
                {
                    String qstNew = String.Join("|",questSearch);
                    //for(int qstcounter = 0; qstcounter <= questSearch.Count - 1; qstcounter++)
                    //{
                    //    qstNew += "(";
                    //    qstNew += questSearch[qstcounter];
                    //    qstNew += ")";
                    //}
                    String thmNew = String.Join("|", themeSearch);
                    //for (int qstcounter = 0; qstcounter <= themeSearch.Count - 1; qstcounter++)
                    //{
                    //    thmNew += "(";
                    //    thmNew += themeSearch[qstcounter];
                    //    thmNew += ")";
                    //}
                    Regex qstReg = new Regex(@"" + qstNew.ToUpper() + "");
                    Regex thmReg = new Regex(@"" + thmNew.ToUpper() + "");
                    List<ChunkPhrases> phraseSearch = NewChunk(getTable(sentences[i]));
                    for(int j = 0; j <= phraseSearch.Count - 1; j++)
                    {

                        String phrase = "";
                        int z = 0;
                        if(phraseSearch[j].Name == "NP")
                        {
                            if(j > 0)
                            {
                                if(phraseSearch[j-1].Name == "NP")
                                {
                                    
                                    for(int a =0; a <= phraseSearch[j-1].Chunk.Count - 1; a++)
                                    {
                                        if(thmReg.Match(phraseSearch[j - 1].Chunk[a].SWord.ToUpper()).Success)
                                        {
                                            phrase = "";
                                            break;
                                        }
                                        phrase += phraseSearch[j-1].Chunk[a].SWord + " ";
                                    }
                                    for (int a = 0; a <= phraseSearch[j].Chunk.Count - 1; a++)
                                    {
                                        if (thmReg.Match(phraseSearch[j].Chunk[a].SWord.ToUpper()).Success)
                                        {
                                            phrase = "";
                                            break;
                                        }
                                        phrase += phraseSearch[j].Chunk[a].SWord + " ";
                                    }

                                    phrases.Add(phrase);
                                }
                                if (phraseSearch[j - 1].Name == "S")
                                {
                                    for (int a = 0; a <= phraseSearch[j - 1].Chunk.Count - 1; a++)
                                    {

                                        phrase += phraseSearch[j - 1].Chunk[a].SWord + " ";
                                    }
                                    for (int a = 0; a <= phraseSearch[j].Chunk.Count - 1; a++)
                                    {
                                        if (thmReg.Match(phraseSearch[j].Chunk[a].SWord.ToUpper()).Success)
                                        {
                                            phrase = "";
                                            break;
                                        }
                                        phrase += phraseSearch[j].Chunk[a].SWord + " ";
                                    }

                                    phrases.Add(phrase);
                                }
                                z++;
                            }

                            
                            //for(int k = 0; k <= phraseSearch[j].Chunk.Count - 1; k++)
                            //{
                            //    if(phraseSearch[j].Chunk[k].SWord)
                            //}
                        }
                       
                    }
                    return String.Join(" ", phrases);
                   // return sentences[i];
                }
            }

            return quest;
        }

        public String[] getData()
        {
            String data;
            data = " A smartphone is a class of mobile phone and mobile computing device. They are distinguished from feature phones by their stronger hardware capabilities and extensive mobile operating systems, which facilitate wider software, internet (including web browsing over mobile broadband), and multimedia functionality(including music, video, cameras, and gaming), alongside core phone functions such as voice calls and text messaging. Smartphones typically include various sensors that can be leveraged by their software, such as a magnetometer, proximity sensors, barometer, gyroscope and accelerometer, and support wireless communications protocols such as Bluetooth, Wi-Fi, and satellite navigation." +
                " Smartphones have central processing units (CPUs), similar to those in computers, but optimised to operate in low power environments. Mobile CPU performance depends not only on the clock rate(generally given in multiples of hertz)[67] but also the memory hierarchy also greatly affects overall performance.Because of these problems, the performance of mobile phone CPUs is often more appropriately given by scores derived from various standardized tests to measure the real effective performance in commonly used applications.";


               string[] sentences = Regex.Split(data, @"(?<=[\.!\?])\s+");

            return sentences;

        }




        static void Main()
        {
            Application.Run(new Form2());
            
        }

       

    }
    
}
