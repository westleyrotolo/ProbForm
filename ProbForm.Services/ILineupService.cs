using ProbForm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProbForm.Services
{
    public interface ILineupsService
    {
        Task<string> FetchData();

        Task<List<Match>> Matches();
    }
}
