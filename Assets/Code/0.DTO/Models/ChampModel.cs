using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ChampModel 
{
    public int ID;
    public string Name;
    public string Description;
    public string Intrinsic; //Nội tại
    public string SkillDescription; //
    public int Level;
    public float EXP;
    public float vHealth; //Máu
    public float vHealthCurrent;
    public float vAtk; //Sát thương vật lý
    public float vMagic; //Sát thương phép thuật
    public float vArmor; //Giáp
    public float vMagicResist; //kháng phép
    public float vHealthRegen; //Hồi máu mỗi giây
    public float vDamageEarth; //Sát thương hệ đất
    public float vDamageWater; //Sát thương hệ nước
    public float vDamageFire; //Sát thương hệ lửa
    public float vDefenseEarth; //Kháng hệ đất
    public float vDefenseWater; //Kháng hệ nước
    public float vDefenseFire; //Kháng hệ hỏa
    public float vAtkSpeed; //This is speed anim. Default = 1.0f
    public float vLifeStealPhysic; //% hút máu
    public float vLifeStealMagic; //% hút máu phép
    public float vLethality; //% Xuyên giáp
    public float vMagicPenetration; //% Xuyên phép
    public float vCritical; //% chí mạng
    public float vTenacity; //% kháng hiệu ứng
    public float vCooldownReduction; //Thời gian hồi chiêu
    public float vDamageExcellent; //Sát thương hoàn hảo
    public float vDefenseExcellent; //% phong thu hoàn hảo (ko bị đánh trúng). max = 10%
    public float vDoubleDamage; //Tỉ lệ x2 đòn đánh max = 10%
    public float vTripleDamage; //Tỉ lệ x3 đòn đánh max = 10%
    public float vDamageReflect; //Phản hồi % sát thương. max = 5%
    public float vRewardPlus; //Tăng lượng vàng rơi ra vào cuối trận. max = 100%
    public float vSkillCooldown;//Thời gian hồi chiêu
    public float vHealthPerLevel;//Máu cộng thêm mỗi cấp
    public float vAtkPerLevel;//Atk vật lý cộng thêm mỗi cấp
    public float vMagicPerLevel;//Atk phép cộng thêm mỗi cấp
    public float vArmorPerLevel;//Giáp cộng thêm mỗi cấp
    public float vMagicResistPerLevel;//Kháng phép cộng thêm mỗi cấp
    public float vHealthRegenPerLevel;//Hồi máu cộng thêm mỗi cấp
    public float vCooldownReductionPerLevel;//Giảm thời gian hồi chiêu cộng thêm mỗi cấp
    public float vMoveSpeed;//Tốc độ di chuyển
    //public List<ItemModel> ItemsEquip;
    public enum HeroType
    {
        near, //Cận chiến
        far //Tầm xa
    }
    public HeroType HType;
    public player_type Type;
    public enum player_type
    {
        canchien = 1,
        satthu = 2,
        hotro = 3,
        dodon = 4,
        xathu = 5,
        phapsu = 6
    }
    public ChampModel(int id,
        string name,
        string description,
        string intrinsic,
        string skilldescription,
        int level,
        float exp,
        float vhealth,
        float vatk,
        float vmagic,
        float varmor,
        float vmagicresist,
        float vhealthregen,
        float vdamageearth,
        float vdamagewater,
        float vdamagefire,
        float vdefenseearth,
        float vdefensewater,
        float vdefensefire,
        float vatkspeed,
        float vlifestealphysic,
        float vlifestealmagic,
        float vlethality,
        float vmagicpenetration,
        float vcritical,
        float vtenacity,
        float vcooldownreduction,
        sbyte vdamageexcellent,
        sbyte vdefenseexcellent,
        sbyte vdoubledamage,
        sbyte vtripledamage,
        sbyte vdamagereflect,
        sbyte vrewardplus,
        float vskillcooldown,
        float vhealthperlevel,
        float vatkperlevel,
        float vmagicperlevel,
        float varmorperlevel,
        float vmagicresistperlevel,
        float vhealthregenperlevel,
        float vcooldownreductionperlevel,
        float vmovespeed,
        player_type type,
        HeroType htype)
    {
        ID = id;
        Name = name;
        Description = description;
        Intrinsic = intrinsic;
        SkillDescription = skilldescription;
        Level = level;
        EXP = exp;
        vHealth = vhealth;
        vAtk = vatk;
        vMagic = vmagic;
        vArmor = varmor;
        vMagicResist = vmagicresist;
        vHealthRegen = vhealthregen;
        vDamageEarth = vdamageearth;
        vDamageWater = vdamagewater;
        vDamageFire = vdamagefire;
        vDefenseEarth = vdefenseearth;
        vDefenseWater = vdefensewater;
        vDefenseFire = vdefensefire;
        vAtkSpeed = vatkspeed;
        vLifeStealPhysic = vlifestealphysic;
        vLifeStealMagic = vlifestealmagic;
        vLethality = vlethality;
        vMagicPenetration = vmagicpenetration;
        vCritical = vcritical;
        vTenacity = vtenacity;
        vCooldownReduction = vcooldownreduction;
        vDamageExcellent = vdamageexcellent;
        vDefenseExcellent = vdefenseexcellent;
        vDoubleDamage = vdoubledamage;
        vTripleDamage = vtripledamage;
        vDamageReflect = vdamagereflect;
        vRewardPlus = vrewardplus;
        vSkillCooldown = vskillcooldown;
        vHealthPerLevel = vhealthperlevel;
        vAtkPerLevel = vatkperlevel;
        vMagicPerLevel = vmagicperlevel;
        vArmorPerLevel = varmorperlevel;
        vMagicResistPerLevel = vmagicresistperlevel;
        vHealthRegenPerLevel = vhealthregenperlevel;
        vCooldownReductionPerLevel = vcooldownreductionperlevel;
        vMoveSpeed = vmovespeed;
        Type = type;
        HType = htype;
    }
    public ChampModel() { }
    public ChampModel Clone()
    {
        return (ChampModel)this.MemberwiseClone();
    }
}
