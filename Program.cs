using AssemblyAITranscriber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssemblyAITranscriber
{
    internal class Program
    {
        private const string API_KEY = "75b5f6fe188e42d28a8d0d70f0e5b4fb";
        private const string API_URL = "https://api.assemblyai.com/";
        //private const string AUDIO_FILE_PATH = "./audio/CallRecording4.mp3";
        private const string AUDIO_FILE_PATH = "C:\\Users\\rndbcp\\source\\repos\\AssemblyAITranscriber\\audio\\CallRecording4.mp3";
        static void Main(string[] args)
        {
            var client = new AssemblyAIApiClient(API_KEY, API_URL);

            // Upload file
            var uploadResult = client.UploadFileAsync(AUDIO_FILE_PATH).GetAwaiter().GetResult();

            // Submit file for transcription
            var submissionResult = client.SubmitAudioFileAsync(uploadResult.UploadUrl).GetAwaiter().GetResult();
            Console.WriteLine($"File {submissionResult.Id} in status {submissionResult.Status}");

            // Query status of transcription until it's `completed`
            TranscriptionResponse result = client.GetTranscriptionAsync(submissionResult.Id).GetAwaiter().GetResult();
            while (!result.Status.Equals("completed"))
            {
                Console.WriteLine($"File {result.Id} in status {result.Status}");
                Thread.Sleep(15000);
                result = client.GetTranscriptionAsync(submissionResult.Id).GetAwaiter().GetResult();
            }

            // Perform post-procesing with the result of the transcription
            Console.WriteLine($"File {result.Id} in status {result.Status}");
            Console.WriteLine($"{result.Words?.Count} words transcribed.");

            foreach (var word in result.Words)
            {
                Console.Write(word.Text+" ");
              //  Console.WriteLine($"Word: '{word.Text}' at {word.Start} with {word.Confidence * 100}% confidence.");
            }

            Console.ReadLine();
        }
    }
}
