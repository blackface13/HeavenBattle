using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCore
{
    /// <summary>
    /// Gây sát thương
    /// </summary>
    /// <param name="Hero"></param>
    /// <param name="Enemy"></param>
    /// <param name="vec">Vị trí để showdame</param>
    /// <param name="Dameper">Phần trăm dame</param>
    /// <param name="type">Kiểu dame, physic hay magic</param>
    /// <param name="damefrom">Bên nào tấn công</param>
    public static float Damage(ChampModel hero1, ChampModel hero2, int Dameper, int type)
    {
        var defExellent = hero2.vDefenseExcellent > 0 && UnityEngine.Random.Range(0, 100) <= hero2.vDefenseExcellent ? true : false; //Phòng thủ hoàn hảo
        var dgmExellent = !defExellent && hero1.vDamageExcellent > 0 && UnityEngine.Random.Range(0, 100) <= hero1.vDamageExcellent ? true : false; //Sát thương hoàn hảo
        var dmgEarth = DamageCaculator(hero1.vDamageEarth, hero1.vDamageEarth, hero2.vDefenseEarth, hero2.vDefenseEarth, 0, 0, 100, 0, 0); //Sát thương hệ đất
        var dmgWater = DamageCaculator(hero1.vDamageWater, hero1.vDamageWater, hero2.vDefenseWater, hero2.vDefenseWater, 0, 0, 100, 0, 0); //Sát thương hệ nước
        var dmgFire = DamageCaculator(hero1.vDamageFire, hero1.vDamageFire, hero2.vDefenseFire, hero2.vDefenseFire, 0, 0, 100, 0, 0); //Sát thương hệ lửa
        return (dmgEarth + dmgWater + dmgFire) + DamageCaculator(defExellent ? 0 : hero1.vAtk, defExellent ? 0 : hero1.vMagic, dgmExellent ? 0 : hero2.vArmor, dgmExellent ? 0 : hero2.vMagicResist, hero1.vLethality, hero1.vMagicPenetration, Dameper, type, hero1.vCritical);
    }

    //Tính toán sát thương
    /// <summary>
    /// Caculator damage
    /// </summary>
    /// <param name="dame_physic">Dame gốc vật lý</param>
    /// <param name="dame_magic">Dame phép</param>
    /// <param name="def_physic">Thủ vật lý của đối phương</param>
    /// <param name="def_magic">Thủ phép của đối phương</param>
    /// <param name="pass_def_physic">Xuyên giáp</param>
    /// <param name="pass_def_magic">Xuyên phép</param>
    /// <param name="dameper">Số phần trăm tính toán</param>
    /// <param name="type">Kiểu sát thương. 0: Vật lý, 1: phép</param>
    /// <returns></returns>
    public static float DamageCaculator(float dame_physic, float dame_magic, float def_physic, float def_magic, float pass_def_physic, float pass_def_magic, int dameper, int type, float crit)
    {
        float dame_end = 0f;
        if (type == 0) //Sát thương vật lý
        {
            dame_end = def_physic >= 0 ? dame_physic * (100 / (100 + (def_physic - (def_physic * pass_def_physic / 100)))) : dame_end = dame_physic * (2 - 100 / (100 - def_physic));
            dame_end = UnityEngine.Random.Range(0, 100) <= crit ? dame_end * 2 : dame_end; //Gấp đôi sát thương với đòn chí mạng
        }
        if (type == 1) //Sát thương phép
            dame_end = def_magic >= 0 ? dame_end = dame_magic * (100 / (100 + (def_magic - (def_magic * pass_def_magic / 100)))) : dame_end = dame_magic * (2 - 100 / (100 - def_magic));
        dame_end = dame_end * dameper / 100f; //Tính toán số phần trăm sát thương
        return UnityEngine.Random.Range(dame_end - (dame_end * 10 / 100f), dame_end + (dame_end * 10 / 100f)); //Dame sẽ chênh lệch 10%
    }
}
