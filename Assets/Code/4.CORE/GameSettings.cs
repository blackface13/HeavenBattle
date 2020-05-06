using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code._4.CORE
{
    public static class GameSettings
    {
        #region Variables
        public enum LayerSettings
        {
            HeroTeam1 = 9,
            HeroTeam2 = 10,
            DetectEnemyTeam1 = 11,
            DetectEnemyTeam2 = 12,
            SkillTeam1ToVictim = 13,
            SkillTeam2ToVictim = 14,
            SkillTeam1ToMyTeam = 15,
            SkillTeam2ToMyTeam = 16,
            SafeRegionTeam1 = 17,
            SafeRegionTeam2 = 18
        }
        #endregion
    }
}
