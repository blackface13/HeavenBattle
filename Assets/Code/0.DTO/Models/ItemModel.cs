using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemModel
{
    public TypeMode ItemTypeMode;
    public string ItemGuidID; //Mã item
    public sbyte ItemType; //Loại item (giới hạn -128 -> 127)
    public byte ItemID; //ID item (giới hạn 0 -> 255)
    public sbyte ItemLevel; //Cấp độ item
    public sbyte ItemColor; //Màu sắc item
    public int Quantity;
    public float ItemPrice;
    public float vAtk; //Sát thương vật lý
    public float vMagic; //Sát thương phép thuật
    public float vHealth; //Máu
    public float vMana; //Mana
    public float vArmor; //Giáp
    public float vMagicResist; //Kháng phép
    public float vHealthRegen; //Chỉ số hồi máu mỗi giây
    public float vManaRegen; //Chỉ số hồi mana mỗi giây
    public float vDamageEarth; //Sát thương hệ đất
    public float vDamageWater; //Sát thương hệ nước
    public float vDamageFire; //Sát thương hệ lửa
    public float vDefenseEarth; //Kháng hệ đất
    public float vDefenseWater; //Kháng hệ nước
    public float vDefenseFire; //Kháng hệ hỏa
    public float vAtkSpeed; //% Tốc độ tấn công cơ bản tăng thêm
    public float vLifeStealPhysic; //% hút máu
    public float vLifeStealMagic; //% hút máu phép
    public float vLethality; //% Xuyên giáp
    public float vMagicPenetration; //% Xuyên phép
    public float vCritical; //% chí mạng
    public float vTenacity; //% kháng hiệu ứng
    public float vCooldownReduction; //% Giảm tgian hồi chiêu
    public float vAtkPlus; //Gia tăng % sát thương vật lý (max = 32767)
    public float vMagicPlus; //Gia tăng % sát thương phép (max = 32767)
    public float vHealthPlus; //Gia tăng % máu (max = 32767)
    public float vManaPlus; //Gia tăng % mana (max = 32767)
    public float vArmorPlus; //Gia tăng % Giáp
    public float vMagicResistPlus; //Gia tăng % Kháng phép
    public float vDamageExcellent; //% Sát thương hoàn hảo (bỏ qua giáp hoặc kháng phép). max = 10%
    public float vDefenseExcellent; //% phong thu hoàn hảo (ko bị đánh trúng). max = 10%
    public float vDoubleDamage; //Tỉ lệ x2 đòn đánh max = 10%
    public float vTripleDamage; //Tỉ lệ x3 đòn đánh max = 10%
    public float vDamageReflect; //Phản hồi % sát thương. max = 5%
    public float vRewardPlus; //Tăng lượng vàng rơi ra vào cuối trận. max = 100%
    public enum TypeMode
    {
        Equip, //Item trang bị
        Use, //Item sử dụng
        Quest, //Item nhiệm vụ
        Inschar //Mảnh ghép tướng
    }
    public ItemModel() { }
    public ItemModel Clone()
    {
        return (ItemModel)this.MemberwiseClone();
    }
    public ItemModel(TypeMode itemtypemode,
        sbyte itemtype,
        byte itemid,
        sbyte itemlevel,
        sbyte itemcolor,
        int quantity,
        float vatk,
        float vmagic,
        float vhealth,
        float vmana,
        float varmor,
        float vmagicresist,
        float vhealthregen,
        float vmanaregen,
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
        float vatkplus,
        float vmagicplus,
        float vhealthplus,
        float vmanaplus,
        float varmorplus,
        float vmagicresistplus,
        float vdamageexcellent,
        float vdefenseexcellent,
        float vdoubledamage,
        float vtripledamage,
        float vdamagereflect,
        float vrewardplus)
    {
        ItemTypeMode = itemtypemode;
        ItemType = itemtype;
        ItemID = itemid;
        ItemLevel = itemlevel;
        ItemColor = itemcolor;
        Quantity = quantity;
        vAtk = vatk;
        vMagic = vmagic;
        vAtkSpeed = vatkspeed;
        vHealth = vhealth;
        vMana = vmana;
        vArmor = varmor;
        vMagicResist = vmagicresist;
        vHealthRegen = vhealthregen;
        vManaRegen = vmanaregen;
        vDamageEarth = vdamageearth;
        vDamageWater = vdamagewater;
        vDamageFire = vdamagefire;
        vDefenseEarth = vdefenseearth;
        vDefenseWater = vdefensewater;
        vDefenseFire = vdefensefire;
        vLifeStealPhysic = vlifestealphysic;
        vLifeStealMagic = vlifestealmagic;
        vLethality = vlethality;
        vMagicPenetration = vmagicpenetration;
        vCritical = vcritical;
        vTenacity = vtenacity;
        vCooldownReduction = vcooldownreduction;
        vAtkPlus = vatkplus;
        vMagicPlus = vmagicplus;
        vHealthPlus = vhealthplus;
        vManaPlus = vmanaplus;
        vArmorPlus = varmorplus;
        vMagicResistPlus = vmagicresistplus;
        vDamageExcellent = vdamageexcellent;
        vDefenseExcellent = vdefenseexcellent;
        vDoubleDamage = vdoubledamage;
        vTripleDamage = vtripledamage;
        vDamageReflect = vdamagereflect;
        vRewardPlus = vrewardplus;
    }
}