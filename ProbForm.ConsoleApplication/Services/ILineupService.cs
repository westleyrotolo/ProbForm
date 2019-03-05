using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProbForm.ConsoleApplication.Services
{
    public interface ILineupsService
    {
        Task<string> FetchData();

        Task<List<Match>> Matches();
    }
}
