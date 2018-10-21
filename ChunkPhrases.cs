using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanford.NLP.POSTagger.CSharp
{
    public class ChunkPhrases
    {
        private List<DataClass> chunk;
        private String name;

        public List<DataClass> Chunk { get => chunk; set => chunk = value; }
        public string Name { get => name; set => name = value; }
    }
}
