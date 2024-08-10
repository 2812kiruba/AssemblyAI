using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssemblyAITranscriber.Model
{
   public class Word
    {
        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
