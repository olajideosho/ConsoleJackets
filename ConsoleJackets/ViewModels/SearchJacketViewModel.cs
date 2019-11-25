using System;
using System.Threading.Tasks;
using ConsoleJackets.Models;
using ConsoleJackets.Services;

namespace ConsoleJackets.ViewModels
{
    public class SearchJacketViewModel
    {
        public Jacket searchedJacket = new Jacket();

        public SearchJacketViewModel()
        {
        }

        public async Task<Jacket> GetJacketWithId(string JacketId)
        {
            searchedJacket = await MockAPIService.GetJacketById(JacketId);
            if(searchedJacket != null)
            {
                return searchedJacket;
            }
            return new Jacket { Id = 0, JacketID = null, JacketOwner = null, Location = null };
        }
    }
}
