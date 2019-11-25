using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleJackets.Models;
using ConsoleJackets.Services;

namespace ConsoleJackets.ViewModels
{
    public class LaunchViewModel
    {
        public List<Jacket> RecentJacketList = new List<Jacket>();

        public LaunchViewModel()
        {
        }

        //Mock Start
        public async Task GetRecentJacketsAsync()
        {
            RecentJacketList = await MockAPIService.GetRecentJackets();
            return;
        }

        public async Task<int> GetRemaingJacketCount()
        {
            var remainingCount = await MockAPIService.GetRemainingJacketCount();
            return remainingCount;
        }
        //Mock End

    }
}
