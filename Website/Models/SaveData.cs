using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class SaveData
    {
        int Turn;
        int Cid;
        int Mil;
        int Hap;
        int Eco;
        int Rel;

        public SaveData(int turn, int cid, int mil, int hap, int eco, int rel)
        {
            Turn = turn;
            Cid = cid;
            Mil = mil;
            Hap = hap;
            Eco = eco;
            Rel = rel;
        }
    }
}
