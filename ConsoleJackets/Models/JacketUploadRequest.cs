using System;
namespace ConsoleJackets.Models
{
    public class JacketUploadRequest
    {
        public JacketUploadRequest()
        {
        }

        public int IndexId { get; set; }
        public string Owner { get; set; }
        public string ID { get; set; }
        public string Secret { get; set; }
        public string Country { get; set; }
    }
}
