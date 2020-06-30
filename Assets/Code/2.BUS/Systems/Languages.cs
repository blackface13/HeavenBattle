//Credit: music: Music by Eric Matyas - www.soundimage.org
//Home image: Erin Linnnn
public static class Languages
{

    #region Variables
    public static string[] lang = new string[1000];
    #endregion

    // Use this for initialization
    //public void Start () {
    //       lang = new string[2000];
    //       Language_VN();
    //}
    public static void SetupLanguage(int _idlanguage)
    {
        switch (_idlanguage)
        {
            case 0:
                Language_EN();
                break;
            case 1:
                Language_VN();
                break;
            default:
                Language_EN();
                break;
        }

    }
    public static void SetupDefaultLanguage()
    {
        if (string.IsNullOrEmpty(lang[11]))
            SetupLanguage(1);
    }
    private static void Language_VN()
    {
        //0-99: information game
        //100-199: player name near
        //200-299: player near descript
        //300-399: player name magic
        //400-499: player magic descript
        //500-599: player name far
        //600-699: player far descript

        #region Phần ngôn ngữ tổng quan 

        lang[0] = "";

        //Battle
        lang[11] = "Câm lặng";
        lang[12] = "Choáng";
        lang[13] = "Giữ chân";
        lang[14] = "Đóng băng";
        lang[15] = "Mù";
        lang[16] = "Làm chậm";
        lang[17] = "Thiêu đốt";
        lang[18] = "Trúng độc";

        lang[20] = "Tướng đã chọn";
        lang[21] = "Đường trên";
        lang[22] = "Đường giữa";
        lang[23] = "Đường dưới";
        lang[24] = "Setting";
        lang[25] = "Setting";
        lang[26] = "Setting";
        lang[27] = "Tốc độ: X";
        lang[28] = "Điều khiển trận đấu";

        //Thông tin chỉ số
        lang[700] = "+{0} Sát thương vật lý";
        lang[701] = "+{0} Sức mạnh phép thuật";
        lang[702] = "+{0} Máu";
        lang[703] = "+{0} Năng lượng";
        lang[704] = "+{0} Giáp";
        lang[705] = "+{0} Kháng phép";
        //lang[706] = "<color=darkblue>+{0} Hồi máu mỗi " + ItemCoreSetting.SecondHeathRegen + " giây</color>";
        lang[707] = "<color=darkblue>+{0} Hồi năng lượng mỗi giây</color>";
        lang[708] = "<color=darkblue>+{0} Sát thương hệ thổ</color>";
        lang[709] = "<color=darkblue>+{0} Sát thương hệ nước</color>";
        lang[710] = "<color=darkblue>+{0} Sát thương hệ lửa</color>";
        lang[711] = "<color=darkblue>+{0} Kháng sát thương hệ thổ</color>";
        lang[712] = "<color=darkblue>+{0} Kháng sát thương hệ nước</color>";
        lang[713] = "<color=darkblue>+{0} Kháng sát thương hệ lửa</color>";
        lang[714] = "<color=darkblue>+{0}% Tốc độ tấn công</color>";
        lang[715] = "<color=darkblue>+{0}% Hút máu</color>";
        lang[716] = "<color=darkblue>+{0}% Hút máu phép</color>";
        lang[717] = "<color=darkblue>+{0}% Xuyên giáp</color>";
        lang[718] = "<color=darkblue>+{0}% Xuyên phép</color>";
        lang[719] = "<color=darkblue>+{0}% Chí mạng</color>";
        lang[720] = "<color=darkblue>+{0}% Kháng hiệu ứng</color>";
        lang[721] = "<color=darkblue>+{0}% Giảm t.gian hồi chiêu</color>";
        lang[722] = "<color=darkblue>+{0}% Sát thương vật lý</color>";
        lang[723] = "<color=darkblue>+{0}% Sức mạnh phép thuật</color>";
        lang[724] = "<color=darkblue>+{0}% Máu</color>";
        lang[725] = "<color=darkblue>+{0}% Năng lượng</color>";
        lang[726] = "<color=red>+{0}% Sát thương hoàn hảo</color>";
        lang[727] = "<color=red>+{0}% Phòng thủ hoàn hảo</color>";
        lang[728] = "<color=red>+{0}% Tỉ lệ x2 đòn đánh</color>";
        lang[729] = "<color=red>+{0}% Tỉ lệ x3 đòn đánh</color>";
        lang[730] = "<color=red>+{0}% Phản sát thương</color>";
        lang[731] = "<color=red>+{0}% Vàng nhận được sau trận đấu</color>";
        lang[732] = "<color=purple>+{0} Lỗ khảm ngọc</color>";
        lang[733] = "<color=purple>- (Trống)</color>";
        lang[734] = "<color=darkblue>+{0}% Giáp</color>";
        lang[735] = "<color=darkblue>+{0}% Kháng phép</color>";

        #endregion

    }
    // Update is called once per frame
    private static void Language_EN()
    {
        #region Phần ngôn ngữ tổng quan 

        lang[0] = "";

        //Battle
        lang[11] = "Silent"; //Câm lặng
        lang[12] = "Stun"; //Choáng
        lang[13] = "Root"; //Giữ chân
        lang[14] = "Ice"; //Đóng băng
        lang[15] = "Blind"; //Mù
        lang[16] = "Slow";  //Làm chậm
        lang[17] = "Fire";  //Thiêu đốt
        lang[18] = "Poison";//Trúng độc

        lang[20] = "Champ selected";
        lang[21] = "Top lane";
        lang[22] = "Mid lane";
        lang[23] = "Bottom lane";
        lang[24] = "Setting";
        lang[25] = "Setting";
        lang[26] = "Setting";
        lang[27] = "Speed: X";
        lang[28] = "Battle controller";



        //Thông tin chỉ số
        lang[700] = "+{0} Attack physic";
        lang[701] = "+{0} Magic";
        lang[702] = "+{0} Health";
        lang[703] = "+{0} Mana";
        lang[704] = "+{0} Armor";
        lang[705] = "+{0} Magic Resist";
        //lang[706] = "<color=darkblue>+{0} Health regen per " + ItemCoreSetting.SecondHeathRegen.ToString () + " second</color>";
        lang[707] = "<color=darkblue>+{0} Mana regen</color>";
        lang[708] = "<color=darkblue>+{0} Damage earth</color>";
        lang[709] = "<color=darkblue>+{0} Damage water</color>";
        lang[710] = "<color=darkblue>+{0} Damage fire</color>";
        lang[711] = "<color=darkblue>+{0} Against eather damage</color>";
        lang[712] = "<color=darkblue>+{0} Against water damage</color>";
        lang[713] = "<color=darkblue>+{0} Against fire damage</color>";
        lang[714] = "<color=darkblue>+{0}% Attack Speed</color>";
        lang[715] = "<color=darkblue>+{0}% Life Steal Physic</color>";
        lang[716] = "<color=darkblue>+{0}% Life Steal Magic</color>";
        lang[717] = "<color=darkblue>+{0}% Lethality</color>";
        lang[718] = "<color=darkblue>+{0}% MagicPenetration</color>";
        lang[719] = "<color=darkblue>+{0}% Critical</color>";
        lang[720] = "<color=darkblue>+{0}% Tenacity</color>";
        lang[721] = "<color=darkblue>+{0}% Cooldown Reduction</color>";
        lang[722] = "<color=darkblue>+{0}% Attack Plus</color>";
        lang[723] = "<color=darkblue>+{0}% Magic Plus</color>";
        lang[724] = "<color=darkblue>+{0}% Health Plus</color>";
        lang[725] = "<color=darkblue>+{0}% Mana Plus</color>";
        lang[726] = "<color=red>+{0}% Damage Excellent</color>";
        lang[727] = "<color=red>+{0}% Excellent Defense</color>";
        lang[728] = "<color=red>+{0}% Double Damage</color>";
        lang[729] = "<color=red>+{0}% Triple Damage</color>";
        lang[730] = "<color=red>+{0}% Reflect Damage</color>";
        lang[731] = "<color=red>+{0}% Reward plus in battle</color>";
        lang[732] = "<color=purple>+{0} Jewel Socket</color>";
        lang[733] = "<color=purple>- (Empty)</color>";
        lang[734] = "<color=darkblue>+{0}% Armor</color>";
        lang[735] = "<color=darkblue>+{0}% Magic Resist</color>";

        #endregion

    }
}