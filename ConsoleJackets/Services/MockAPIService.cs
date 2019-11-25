using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleJackets.Models;

namespace ConsoleJackets.Services
{
    public static class MockAPIService
    {
        public static List<Jacket> Jackets = new List<Jacket>()
        {
            new Jacket
            {
                Id = 3,
                JacketOwner = "Wei Xiang",
                JacketID = "ZRXE",
                Location = "China"
            },
            new Jacket
            {
                Id = 2,
                JacketOwner = "Jumoke Alison",
                JacketID = "UMLX",
                Location = "Nigeria"
            },
            new Jacket
            {
                Id = 1,
                JacketOwner = "Simon Arslan",
                JacketID = "MOIP",
                Location = "United States of America"
            }
        };

        public static async Task<List<Jacket>> GetRecentJackets()
        {
            await Task.Delay(3000);
            var recentJackets = Jackets;
            return recentJackets;
        }

        public static async Task<int> GetRemainingJacketCount()
        {
            await Task.Delay(3000);
            var remainingJacketCount = 5000 - Jackets.Count;
            return remainingJacketCount;
        }

        public static async Task<Response> PostJacket(JacketUploadRequest jacketRequest)
        {
            await Task.Delay(3000);
            var response = new Response
            {
                Error = false,
                Message = "Jacket Upload Successful"
            };
            return response;
        }

        public static async Task<Jacket> GetJacketById(string jacketId)
        {
            await Task.Delay(3000);
            var jacket = Jackets.Where(j => j.JacketID == jacketId).FirstOrDefault();
            if(jacket != null)
            {
                return jacket;
            }
            return new Jacket { Id = 0, JacketID = null, JacketOwner = null, Location = null };
        }
    }
}
