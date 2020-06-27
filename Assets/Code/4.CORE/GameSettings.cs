using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
            SafeRegionTeam2 = 18,
            SoldierTeam1 = 19,
            SoldierTeam2 = 20,
            SoldierDetectTeam1 = 21,
            SoldierDetectTeam2 = 22,
            HomeTeam1 = 23, 
            HomeTeam2 = 24
        }

        public static Vector3 PositionShowEffectFix = new Vector3(0, 2f, 0);//Fix tọa độ trung tâm của tướng

        public static List<ChampModel> ChampDefault;
        public static List<ChampModel> SoldierDefault;

        //Battle
        public static readonly float[] BattleSpeed = new float[] { 1f, 1.5f, 2f, 3f };//Tốc độ trận đấu
        public static float SpeedFillImgWaitingHoldTap = 2f;//Thời gian nhấn giữ nhân vật để show popup
        public static float TimeMoveImgTempInBattle = 0f;
        public static float ObjectSizeScollViewTotalInBattle = 215f;
        public static float ObjectSizeScollViewLaneInBattle = 130f;
        public static readonly float StartPositionXTeam1 = -20f;//Tọa độ X xuất phát của team 1
        public static readonly float StartPositionXTeam2 = 120f;//Tọa độ X xuất phát của team 2
        #endregion

        #region Functions

        /// <summary>
        /// Khởi tạo dữ liệu các nhân vật mặc định
        /// </summary>
        public static void CreateChampDefault()
        {
            if (ChampDefault == null)
            {
                ChampDefault = new List<ChampModel>();

                #region Champ1
                ChampDefault.Add(new ChampModel
                {
                    ID = 0,
                    Name = "ChampTest",
                    Description = "",
                    Intrinsic = "",
                    SkillDescription = "",
                    Level = 0,
                    EXP = 0,
                    vHealth = 4500,
                    vAtk = 450,
                    vMagic = 0,
                    vArmor = 35,
                    vMagicResist = 25,
                    vHealthRegen = 0.5f,
                    vDamageEarth = 0,
                    vDamageWater = 0,
                    vDamageFire = 0,
                    vDefenseEarth = 0,
                    vDefenseWater = 0,
                    vDefenseFire = 0,
                    vAtkSpeed = 1f,
                    vLifeStealPhysic = 0,
                    vLifeStealMagic = 0,
                    vLethality = 0,
                    vMagicPenetration = 0,
                    vCritical = 0,
                    vTenacity = 0,
                    vCooldownReduction = 0,
                    vDamageExcellent = 0,
                    vDefenseExcellent = 0,
                    vDoubleDamage = 0,
                    vTripleDamage = 0,
                    vDamageReflect = 0,
                    vRewardPlus = 0,
                    vSkillCooldown = 10,
                    vHealthPerLevel = 35,
                    vAtkPerLevel = 7,
                    vMagicPerLevel = 0,
                    vArmorPerLevel = 6,
                    vMagicResistPerLevel = 5,
                    vHealthRegenPerLevel = .5f,
                    vCooldownReductionPerLevel = 2,
                    vMoveSpeed = 3,
                    Type = ChampModel.player_type.canchien,
                    HType = ChampModel.HeroType.near
                });
                #endregion

                #region Champ2
                ChampDefault.Add(new ChampModel
                {
                    ID = 1,
                    Name = "ChampTest",
                    Description = "",
                    Intrinsic = "",
                    SkillDescription = "",
                    Level = 0,
                    EXP = 0,
                    vHealth = 3500,
                    vAtk = 0,
                    vMagic = 370,
                    vArmor = 35,
                    vMagicResist = 25,
                    vHealthRegen = 0.5f,
                    vDamageEarth = 0,
                    vDamageWater = 0,
                    vDamageFire = 0,
                    vDefenseEarth = 0,
                    vDefenseWater = 0,
                    vDefenseFire = 0,
                    vAtkSpeed = 0.7f,
                    vLifeStealPhysic = 0,
                    vLifeStealMagic = 0,
                    vLethality = 0,
                    vMagicPenetration = 0,
                    vCritical = 0,
                    vTenacity = 0,
                    vCooldownReduction = 0,
                    vDamageExcellent = 0,
                    vDefenseExcellent = 0,
                    vDoubleDamage = 0,
                    vTripleDamage = 0,
                    vDamageReflect = 0,
                    vRewardPlus = 0,
                    vSkillCooldown = 10,
                    vHealthPerLevel = 35,
                    vAtkPerLevel = 7,
                    vMagicPerLevel = 0,
                    vArmorPerLevel = 6,
                    vMagicResistPerLevel = 5,
                    vHealthRegenPerLevel = .5f,
                    vCooldownReductionPerLevel = 2,
                    vMoveSpeed = 2,
                    Type = ChampModel.player_type.phapsu,
                    HType = ChampModel.HeroType.far
                });
                #endregion

                #region Champ4
                ChampDefault.Add(new ChampModel
                {
                    ID = 3,
                    Name = "ChampTest",
                    Description = "",
                    Intrinsic = "",
                    SkillDescription = "",
                    Level = 0,
                    EXP = 0,
                    vHealth = 4500,
                    vAtk = 450,
                    vMagic = 0,
                    vArmor = 35,
                    vMagicResist = 25,
                    vHealthRegen = 0.5f,
                    vDamageEarth = 0,
                    vDamageWater = 0,
                    vDamageFire = 0,
                    vDefenseEarth = 0,
                    vDefenseWater = 0,
                    vDefenseFire = 0,
                    vAtkSpeed = 1f,
                    vLifeStealPhysic = 0,
                    vLifeStealMagic = 0,
                    vLethality = 0,
                    vMagicPenetration = 0,
                    vCritical = 0,
                    vTenacity = 0,
                    vCooldownReduction = 0,
                    vDamageExcellent = 0,
                    vDefenseExcellent = 0,
                    vDoubleDamage = 0,
                    vTripleDamage = 0,
                    vDamageReflect = 0,
                    vRewardPlus = 0,
                    vSkillCooldown = 10,
                    vHealthPerLevel = 35,
                    vAtkPerLevel = 7,
                    vMagicPerLevel = 0,
                    vArmorPerLevel = 6,
                    vMagicResistPerLevel = 5,
                    vHealthRegenPerLevel = .5f,
                    vCooldownReductionPerLevel = 2,
                    vMoveSpeed = 3,
                    Type = ChampModel.player_type.canchien,
                    HType = ChampModel.HeroType.near
                });
                #endregion
            }
        }

        /// <summary>
        /// Khởi tạo dữ liệu các lính mặc định
        /// </summary>
        public static void CreateSoldierDefault()
        {
            if (SoldierDefault == null)
            {
                SoldierDefault = new List<ChampModel>();

                #region Sol1
                SoldierDefault.Add(new ChampModel
                {
                    ID = 0,
                    Name = "Soldier 1",
                    Description = "",
                    Intrinsic = "",
                    SkillDescription = "",
                    Level = 0,
                    EXP = 0,
                    vHealth = 1000,
                    vAtk = 15,
                    vMagic = 0,
                    vArmor = 35,
                    vMagicResist = 25,
                    vHealthRegen = 0.5f,
                    vDamageEarth = 0,
                    vDamageWater = 0,
                    vDamageFire = 0,
                    vDefenseEarth = 0,
                    vDefenseWater = 0,
                    vDefenseFire = 0,
                    vAtkSpeed = 1f,
                    vLifeStealPhysic = 0,
                    vLifeStealMagic = 0,
                    vLethality = 0,
                    vMagicPenetration = 0,
                    vCritical = 0,
                    vTenacity = 0,
                    vCooldownReduction = 0,
                    vDamageExcellent = 0,
                    vDefenseExcellent = 0,
                    vDoubleDamage = 0,
                    vTripleDamage = 0,
                    vDamageReflect = 0,
                    vRewardPlus = 0,
                    vSkillCooldown = 10,
                    vHealthPerLevel = 35,
                    vAtkPerLevel = 7,
                    vMagicPerLevel = 0,
                    vArmorPerLevel = 6,
                    vMagicResistPerLevel = 5,
                    vHealthRegenPerLevel = .5f,
                    vCooldownReductionPerLevel = 2,
                    vMoveSpeed = 2,
                    Type = ChampModel.player_type.canchien,
                    HType = ChampModel.HeroType.near
                });
                #endregion

                #region Sol2
                SoldierDefault.Add(new ChampModel
                {
                    ID = 1,
                    Name = "Soldier 2",
                    Description = "",
                    Intrinsic = "",
                    SkillDescription = "",
                    Level = 0,
                    EXP = 0,
                    vHealth = 1000,
                    vAtk = 0,
                    vMagic = 17,
                    vArmor = 35,
                    vMagicResist = 25,
                    vHealthRegen = 0.5f,
                    vDamageEarth = 0,
                    vDamageWater = 0,
                    vDamageFire = 0,
                    vDefenseEarth = 0,
                    vDefenseWater = 0,
                    vDefenseFire = 0,
                    vAtkSpeed = 0.7f,
                    vLifeStealPhysic = 0,
                    vLifeStealMagic = 0,
                    vLethality = 0,
                    vMagicPenetration = 0,
                    vCritical = 0,
                    vTenacity = 0,
                    vCooldownReduction = 0,
                    vDamageExcellent = 0,
                    vDefenseExcellent = 0,
                    vDoubleDamage = 0,
                    vTripleDamage = 0,
                    vDamageReflect = 0,
                    vRewardPlus = 0,
                    vSkillCooldown = 10,
                    vHealthPerLevel = 35,
                    vAtkPerLevel = 7,
                    vMagicPerLevel = 0,
                    vArmorPerLevel = 6,
                    vMagicResistPerLevel = 5,
                    vHealthRegenPerLevel = .5f,
                    vCooldownReductionPerLevel = 2,
                    vMoveSpeed = 2,
                    Type = ChampModel.player_type.phapsu,
                    HType = ChampModel.HeroType.far
                });
                #endregion
            }
        }

        #endregion
    }
}
