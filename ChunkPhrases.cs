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

    public class TagChunkPhrases
    {
        private List<ChunkPhrases> chunk;
        private String name;

        public List<ChunkPhrases> Chunk { get => chunk; set => chunk = value; }
        public string Name { get => name; set => name = value; }
    }
    public class StringChunkPhrases
    {
        private String chunk;
        private String name;

        public String Chunk { get => chunk; set => chunk = value; }
        public string Name { get => name; set => name = value; }
    }
}
