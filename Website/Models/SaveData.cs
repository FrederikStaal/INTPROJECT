/*
 * Group 6
 * Rasmus, Joseph, Tony and Frederik
 * Class type: Model
 * - We wanted to add this to the cookie, but it gave us problems
 */

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
