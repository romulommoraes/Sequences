using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequences
{
    class Concat
    {
        public Concat(string head, string seq, string seq1, string seq2)
        {
            this.head = head;
            this.seq = seq;
            this.seq1 = seq1;
            this.seq2 = seq2;
        }

        public string head { get; set; }
        public string seq { get; set; }
        public string seq1 { get; set; }
        public string seq2 { get; set; }

        public string ToCSV()
        {
            return $"{head}\n{seq}";
        }
        public string RecIds()
        {
            return $"{seq1}, {seq2}";
        }
    }


}
