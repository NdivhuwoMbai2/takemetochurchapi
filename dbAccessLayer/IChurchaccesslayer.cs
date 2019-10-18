using System.Collections.Generic;
using TakeMeToChurchAPI.Models;

namespace TakeMeToChurchAPI.dbAccessLayer
{
    public interface Ichurchaccess
    {
        List<Church> GetAllChurches();
    }
}