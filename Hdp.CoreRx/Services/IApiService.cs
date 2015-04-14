using System;
using Refit;
using System.Threading.Tasks;
using Hdp.CoreRx.Models;
using System.Collections.Generic;

namespace Hdp.CoreRx.Services
{
    public interface IApiService
    {
        IHDPApiService Background    {get;}
        IHDPApiService UserInitiated {get;}
        IHDPApiService Speculative   {get;}
    }
}

