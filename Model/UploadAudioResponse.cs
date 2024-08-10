using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace AssemblyAITranscriber.Model
{
    public class UploadAudioResponse
    {
        [JsonPropertyName("upload_url")]
        public string UploadUrl { get; set; }
    }
}
