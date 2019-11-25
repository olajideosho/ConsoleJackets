using System;
using System.Threading.Tasks;
using ConsoleJackets.Models;
using ConsoleJackets.Services;

namespace ConsoleJackets.ViewModels
{
    public class UploadJacketViewModel
    {
        public JacketUploadRequest payload;
        public Response response;
        public UploadJacketViewModel()
        {
        }

        public async Task<Response> UploadJacket()
        {
            if(payload != null)
            {
                response = await MockAPIService.PostJacket(payload);
                return response;
            }
            return new Response { Error = true, Message = "Something went wrong" };
        }
    }
}
