using System;
namespace ConsoleJackets.Models
{
    public class JacketUploadResponse
    {
        public JacketUploadResponse()
        {
        }

        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
