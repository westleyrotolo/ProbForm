using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProbForm.Models;

namespace ProbForm.ConsoleApplication.Services
{
    public interface ILineupsService
    {
        Task<string> FetchData();

        Task<List<Match>> Matches();
    }
}
