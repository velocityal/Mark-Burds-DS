using System.Text.RegularExpressions;

namespace Stanford.NLP.POSTagger.CSharp
{

    public class Rule
    {

        public string Pattern { get; set; }


        public string ChunkName { get; set; }


        public RegexOptions RegexOptions { get; set; } = RegexOptions.None;
    }
}
