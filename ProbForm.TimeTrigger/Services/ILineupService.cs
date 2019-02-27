using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace probform.Service
{
    public interface ILineupsService
    {
        Task<string> FetchData();

        Task<List<Match>> Matches();
    }
}
