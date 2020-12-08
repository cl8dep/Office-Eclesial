using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeEcclesial.App.Database.Enums
{
    public enum Level
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Joven


    }


    public static class LevelExtensions
    {
        public static string ToString(this Level level)
        {
            switch (level)
            {
                case Level.Level1:
                    return "1er nivel";
                case Level.Level2:
                    return "2do nivel";
                case Level.Level3:
                    return "3er nivel";
                case Level.Level4:
                    return "4to nivel";
                case Level.Level5:
                    return "rto nivel";
                case Level.Level6:
                    return "6to nivel";
                case Level.Joven:
                    return "PJ";
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
