//Credit: music: Music by Eric Matyas - www.soundimage.org
//Home image: Erin Linnnn
public static class Languages {

    #region Variables
    public static string[] lang = new string[1000];
    public static string[] ItemName = new string[600];
    public static string[] ItemInfor = new string[600];
    public static string[] ItemNameType0 = new string[100];
    public static string[] ItemInforType0 = new string[100];
    public static string[] ItemNameType1 = new string[10];
    public static string[] ItemNameType2 = new string[10];
    public static string[] ItemNameType3 = new string[10];
    public static string[] ItemNameType4 = new string[10];
    public static string[] ItemNameType5 = new string[10];
    public static string[] ItemNameType6 = new string[10];
    public static string[] ItemNameType7 = new string[10];
    public static string[] ItemNameType8 = new string[10];
    public static string[] ItemNameType10 = new string[50];
    public static string[] ItemInforType10 = new string[50];
    public static string[] ItemNameType11 = new string[50];
    public static string[] ItemInforType11 = new string[50];
    public static string[] ItemNameType12 = new string[34];
    public static string[] ItemInforType12 = new string[34];
    public static string[] ItemInforType1 = new string[10];
    public static string[] HeroSkillName = new string[20]; //Tên skill của các hero
    public static string[] HeroSkillDescription = new string[20]; //Giới thiệu skill của các hero
    public static string[] HeroIntrinsic = new string[HeroSkillDescription.Length]; //Giới thiệu nội tại của các hero
    public static string[] IntroductionTitle = new string[10];
    public static string[] IntroductionDescriptions = new string[10];
    #endregion

    // Use this for initialization
    //public void Start () {
    //       lang = new string[2000];
    //       Language_VN();
    //}
    public static void SetupLanguage (int _idlanguage) {
        switch (_idlanguage) {
            case 0:
                Language_EN ();
                break;
            case 1:
                Language_VN ();
                break;
            default:
                Language_EN ();
                break;
        }

    }
    public static void SetupDefaultLanguage () {
        if (string.IsNullOrEmpty (lang[11]))
            SetupLanguage (1);
    }
    private static void Language_VN () {
        //0-99: information game
        //100-199: player name near
        //200-299: player near descript
        //300-399: player name magic
        //400-499: player magic descript
        //500-599: player name far
        //600-699: player far descript

        #region Phần ngôn ngữ tổng quan 

        lang[0] = "";
        lang[1] = "";
        lang[2] = "Sắp ra mắt...!";
        lang[11] = "Thông tin";
        lang[12] = "Trang bị";
        lang[13] = "Kỹ năng";
        lang[14] = "Đặc biệt";
        lang[15] = "Tiểu sử";
        lang[16] = "Cận chiến";
        lang[17] = "Sát thủ";
        lang[18] = "Hỗ trợ";
        lang[19] = "Đỡ đòn";
        lang[20] = "Xạ thủ";
        lang[21] = "Pháp sư";
        lang[22] = "Chỉ số";
        lang[23] = "Lv: ";
        lang[24] = "Máu";
        lang[25] = "Thể lực";
        lang[26] = "S.thương vật lý";
        lang[27] = "S.thương phép";
        lang[28] = "Giáp";
        lang[29] = "Kháng phép";
        lang[30] = "Hút máu";
        lang[31] = "Hút máu phép";
        lang[32] = "Xuyên giáp (%)";
        lang[33] = "Xuyên phép (%)";
        //lang[34] = "Hồi máu mỗi " + ItemCoreSetting.SecondHeathRegen.ToString () + " giây";
        lang[35] = "Hồi thể lực mỗi giây";
        lang[36] = "Chí mạng (%)";
        lang[37] = "+ h.ứng gây ra";
        lang[38] = "Kháng hiệu ứng (%)";
        lang[39] = "Tốc độ đánh";
        lang[40] = "%";
        lang[41] = "/s";
        lang[42] = "/cấp";
        lang[43] = "Giảm t.gian hồi chiêu";
        lang[45] = "T.gian hồi chiêu";
        lang[46] = "Thể lực kỹ năng";
        lang[47] = "+ ";
        lang[48] = "Gỡ bỏ";

        //Setting --Window setting
        lang[50] = "Cài đặt";
        lang[51] = "Đồ họa";
        lang[52] = "Cực thấp";
        lang[53] = "Thấp";
        lang[54] = "Trung bình";
        lang[55] = "Cao";
        lang[56] = "Audio";
        lang[57] = "Nhạc nền";
        lang[58] = "Âm thanh";
        lang[59] = "Ngôn ngữ";
        //--------Button
        lang[60] = "OK";
        lang[61] = "Chọn NV";
        lang[62] = "Phản hồi";
        lang[63] = "Thoát game";
        lang[64] = "Tiêu đề...";
        lang[65] = "Nội dung...";
        //--------Window feedback
        lang[66] = "Gửi";
        lang[67] = "Hủy bỏ";
        lang[68] = "Email của bạn...";
        lang[69] = "";
        lang[70] = "Bạn chưa nhập tiêu đề";
        lang[71] = "Bạn chưa nhập nội dung";
        lang[72] = "<color=green>Cám ơn phản hồi của bạn!</color>";
        lang[73] = "Phản hồi của bạn chưa được gửi đi, kiểm tra lại mạng xem sao";
        //---------Inventory
        lang[74] = "Sử dụng: ";
        lang[75] = "Số lượng: ";
        lang[76] = "Giá bán: ";
        lang[77] = "Không thao tác được khi đang ở chế độ lựa chọn vật phẩm";
        lang[78] = "Trang bị nhanh";
        lang[79] = "Gỡ nhanh";
        lang[80] = "Đã gỡ trang bị";

        //Scene Room
        lang[100] = "Vào trận";
        lang[101] = "Phát hiện thấy phiên bản {0} trên máy chủ, bạn có muốn tải và cập nhật phiên bản mới ?";
        lang[102] = "Bạn muốn thoát trò chơi?";
        lang[103] = "<color=green>Chuyển đổi ngôn ngữ thành công, vui lòng tải lại màn hình.</color>";
        lang[104] = "Nhập mã phòng";
        lang[110] = " tạo thành công phòng ";
        lang[111] = "Đã vào phòng ";
        lang[112] = " đã kết nối";
        lang[113] = "Bạn cùng phòng đã thoát";
        lang[114] = "Chờ đồng đội sẵn sàng";
        lang[115] = "Sẵn sàng";
        lang[116] = "Không thực hiện được";
        lang[117] = "Không kết nối được tới máy chủ";
        lang[118] = "Đội hình";
        lang[119] = "Cá nhân";

        //Phần kết nối
        lang[120] = "Không kết nối được máy chủ...";
        lang[121] = "Vui lòng nhập tên";
        lang[122] = "Nhập tên của bạn";
        lang[123] = "Đội hình của bạn chưa đủ nhân vật";
        lang[124] = "Tiết kiệm pin";
        lang[125] = " - Mở";
        lang[126] = " - Tắt";
        lang[127] = "Đang thiết lập hệ thống trò chuyện...!";
        lang[128] = "Không thể trang bị thêm";
        lang[129] = "Trang bị thành công";
        //Scene Inventory
        lang[130] = "Xác nhận bán {0} vật phẩm với {1} vàng";
        lang[131] = "Xác nhận bán vật phẩm này với giá {0} vàng";
        lang[132] = "Xem";
        lang[133] = "tất cả";
        lang[134] = "Trang bị";
        lang[135] = "Vật phẩm";
        lang[136] = "Nhiệm vụ";
        lang[137] = "Lựa chọn";
        lang[138] = "Bán";
        lang[139] = "Sắp xếp";
        lang[140] = "Không đủ chỗ trống trong thùng đồ";
        lang[141] = "<color=green>Chế tạo thành công</color>";
        lang[142] = "<color=red>Lỗi - Không thể chế tạo đồ</color>";
        lang[143] = "<color=red>Không đủ nguyên liệu để chế tạo</color>";
        lang[144] = "Túi đồ";
        lang[145] = "Chế tạo";
        lang[146] = "Nhân vật";
        lang[147] = "Chế tạo hết";
        lang[148] = "Yêu cầu";
        lang[149] = "<color=red>Không đủ tài nguyên</color>";

        lang[150] = "Đất";
        lang[151] = "Nước";
        lang[152] = "Lửa";
        lang[153] = "Gió";

        lang[154] = "Nâng cấp";
        lang[155] = "Nâng phẩm";
        lang[156] = "Phân giải";
        lang[157] = "<color=green>Nâng cấp thành công</color>";
        lang[158] = "<color=red>Trang bị đã được nâng cấp tối đa</color>";
        lang[159] = "<color=green>Nâng phẩm thành công</color>";
        lang[160] = "<color=red>Trang bị đã được nâng phẩm tối đa</color>";
        lang[161] = "Vị trí nút bấm";
        lang[162] = " - Trái";
        lang[163] = " - Phải";
        lang[164] = "Slow motion";
        lang[165] = "Nội tại và kỹ năng";
        lang[166] = "<color=green>Nội tại</color>";
        lang[167] = "<color=orange>Kỹ năng</color>";
        lang[168] = "<color=#454523>Rừng rậm</color>";
        lang[169] = "<color=#BDAD62>Đồng bằng</color>";
        lang[170] = "<color=#FF5410>Núi lửa</color>";
        lang[171] = "Núi tuyết";
        lang[172] = "<color=red>Địa ngục</color>";
        lang[173] = "<color=green>Hang độc</color>";
        lang[174] = "<color=#3F42AF>Hang ma</color>";
        lang[175] = "<color=red>Vượt quá giới hạn rương đồ</color>";
        lang[176] = "Chiến thắng";
        lang[177] = "Thất bại";
        lang[178] = "EXP";
        lang[179] = "Tạm dừng\nBạn muốn thoát khỏi trận đấu?";
        lang[180] = "Bạn muốn video để mở rộng thùng đồ?";
        lang[181] = "Hãy chờ video tiếp theo được tải nhé";
        lang[182] = "Xem video để nhận thưởng";
        lang[183] = "Xem";
        lang[184] = "Chinh chiến";
        lang[185] = "Trang bị vật lý";
        lang[186] = "Trang bị phép thuật";
        lang[187] = "Trang bị phòng thủ";
        lang[188] = "Vật dụng tiêu hao";
        lang[189] = "Trang bị đặc biệt";
        lang[190] = "Bạn nhận được <color=#FF5410>{0}</color>";
        lang[191] = "<color=red>Không đủ Lông phượng để thực hiện</color>";
        lang[192] = "Tự quay";
        lang[193] = "Dừng quay";
        lang[194] = "Hướng dẫn";
        //lang[195] = "- Yêu cầu tối thiểu cho 1 lần quay thưởng là " + Module.SpinItemQuantity.ToString () + " <color=red>Lông phượng</color>";
        lang[195] += "\n\n- Lông phượng có thể tìm ở bất kỳ đâu trong lục địa";
        lang[195] += "\n\n- Phần thưởng quay được có thể là bất kỳ vật phẩm nào, từ giá trị thấp tới cực kỳ quý hiếm.";
        lang[195] += "\n\n- Chú ý: Sắp xếp thùng đồ của bạn trước khi quay";
        lang[196] = "Chỉ số của trang bị sẽ ngẫu nhiên khi chế tạo";
        lang[197] = "Xác nhận";
        lang[198] = "Thuê sát thủ";
        lang[199] = "Xác nhận thuê sát thủ với {0} đá quý";
        lang[200] = "Máu";
        lang[201] = "Năng lượng";
        lang[202] = "Sát thương vật lý";
        lang[203] = "Sát thương phép thuật";
        lang[204] = "Giáp";
        lang[205] = "kháng phép";
        //lang[206] = "Hồi máu mỗi " + ItemCoreSetting.SecondHeathRegen + " giây";
        lang[207] = "Toàn bộ";
        lang[208] = "Quốc gia";
        lang[209] = "Sát thương hệ nước";
        lang[210] = "Sát thương hệ lửa";
        lang[211] = "Kháng hệ đất";
        lang[212] = "Kháng hệ nước";
        lang[213] = "Kháng hệ hỏa";
        lang[214] = "Tốc độ đánh";
        lang[215] = "Thời gian hồi chiêu";
        lang[216] = "Hút máu (%)";
        lang[217] = "Hút máu phép(%)";
        lang[218] = "Xuyên giáp (%)";
        lang[219] = "Xuyên phép (%)";
        lang[220] = "Chí mạng (%)";
        lang[221] = "Kháng hiệu ứng (%)";
        lang[222] = "Giảm thời gian hồi chiêu (%)";
        lang[223] = "Sát thương hoàn hảo (%)";
        lang[224] = "Phòng thủ hoàn hảo  (%)";
        lang[225] = "Tỉ lệ x2 đòn đánh (%)";
        lang[226] = "Tỉ lệ x3 đòn đánh (%)";
        lang[227] = "Phản sát thương (%)";
        lang[228] = "Tăng lượng vàng rơi ra (%)";
        lang[229] = "Năng lượng đòn đánh thường";
        lang[230] = "Năng lượng dùng kỹ năng";
        lang[231] = "Máu mỗi cấp độ";
        lang[232] = "Sát thương vật lý/cấp";
        lang[233] = "Sức mạnh phép thuật/cấp";
        lang[234] = "Giáp/cấp";
        lang[235] = "Kháng phép/cấp";
        lang[236] = "Hồi máu/cấp";
        lang[237] = "Hồi năng lượng/cấp";
        lang[238] = "Giảm thời gian hồi chiêu/cấp";
        lang[250] = "Bạn không đủ đá quý";
        lang[251] = "Thuê sát thủ thành công!";
        lang[252] = "Xem trước";
        lang[253] = "Ngẫu nhiên";
        lang[254] = "Sinh tử";
        lang[255] = "Chế độ này cho phép bạn biết trước đội hình và trang bị của đối phương, để có thể lựa chọn những chiến thuật và trang bị phù hợp nhất cho mình";
        lang[256] = "Bạn sẽ không biết trước được mình sẽ chạm trán với ai ở chế độ này, tất nhiên phần thưởng bạn nhận được nếu vượt qua cũng sẽ cao hơn";
        lang[257] = "Hãy chiến đấu đến khi gục ngã, bảng đếm số sẽ luôn chờ đợi bạn.";
        lang[258] = "Vàng: ";
        lang[259] = "Đá quý: ";
        lang[260] = "Điểm: ";
        lang[261] = "Xóa";
        lang[262] = "Thông tin";
        lang[263] = "Trang bị";
        lang[264] = "Đổi trang bị";
        lang[265] = "Lựa chọn trang bị";
        lang[266] = "Không thể đổi trang bị cho chính mình";
        lang[267] = "Nhân vật chưa được trang bị";
        lang[268] = "Đổi trang bị thành công";
        lang[269] = "Đập bóng";
        lang[270] = "Búa: ";
        lang[271] = "Quay thưởng";
        lang[272] = "Đập bóng là một tính năng cần kết nối mạng. Mỗi ngày, bạn sẽ có 5 chiếc búa để đập bóng"; //Hướng dẫn break ball, lấy từ server
        lang[273] = "Không thể phân giải vật phẩm này";
        lang[274] = "Túi đồ";
        lang[275] = "Nhận ";
        lang[276] = " vàng";
        lang[277] = " đá quý";
        lang[278] = "Xác nhận phân giải trang bị này?";
        lang[279] = "Thưởng Online";
        lang[280] = "Nhận";
        lang[281] = "Đội hình đã đủ";
        lang[282] = "Lựa chọn loại vật phẩm chế tạo";
        lang[283] = "Lựa chọn vật phẩm chế tạo";
        lang[284] = "Đây là những nguyên liệu cần thiết để chế tạo";
        lang[285] = "Với các vật phẩm sử dụng, bạn có thể chế tạo toàn bộ nếu đủ nguyên liệu";
        lang[286] = "Lựa chọn để lọc tướng theo hệ";
        lang[287] = "Danh sách tướng đã lọc theo hệ";
        lang[288] = "Lựa chọn các chức năng để thao tác";
        lang[289] = "Phần thông tin chứa các chi tiết thông tin chỉ số về nhân vật";
        lang[290] = "Bạn có thể trang bị cho nhân vật ở phần này";
        lang[291] = "Thông tin về kỹ năng và nội tại của nhân vật";
        lang[292] = "Tiểu sử, quá khứ và cốt truyện của nhân vật";
        lang[293] = "Các hiệu ứng đặc biệt mà nhân vật này sở hữu";
        lang[294] = "Bắt đầu";
        lang[295] = "Không thể khảm thêm ngọc";
        lang[296] = "Không thể khảm ngọc";
        lang[297] = "Khảm ngọc";
        lang[298] = "Gỡ khảm";
        lang[299] = "Đục lỗ";
        lang[300] = "<color=red>Bạn muốn phá hủy ngọc này?</color>";
        lang[301] = "Ngọc đã bị phá hủy";
        lang[302] = "Lỗ";
        lang[303] = "Không thể đục lỗ";
        lang[304] = "Không đủ vàng";
        lang[305] = "Tháp địa ngục";
        lang[306] = "Xác nhận rời khỏi màn chơi?";
        lang[307] = "Bạn cần chọn 1 lá bài hoặc bỏ lượt";
        lang[308] = "Lá bài không phù hợp";
        lang[309] = "Tùy chọn cách chơi";
        lang[310] = "Chọn bài đánh nhanh";
        lang[311] = "Chọn màu muốn đổi";
        lang[312] = "Đánh";
        lang[313] = "Bỏ lượt";
        lang[314] = "Thoát";
        lang[315] = "Chơi";
        lang[316] = "Xếp hạng";
        lang[317] = "Chế độ";
        lang[318] = "Đối thủ";
        lang[319] = "Tùy chọn";
        lang[320] = "Màu nền";
        lang[321] = "Người chơi ";
        lang[322] = "Đánh bạc";
        lang[323] = "Bỏ lượt nhanh";
        lang[324] = "Hỗ trợ hiển thị lá bài được đánh";
        lang[325] = "Lá bài số";
        lang[326] = "Các lá bài này được chia đều ra 4 màu: đỏ, vàng, lục, lam. Chúng được đánh số lớn dần từ 0 đến 9";
        lang[327] = "Lá bài chức năng";
        lang[328] = "Lá bài mất lượt: Khi bạn đánh lá bài này, người chơi tiếp theo sẽ bị mất lượt";
        lang[329] = "Lá bài đảo chiều: Khi quân bài này được đánh xuống sẽ tiến hành Đảo ngược chiều đánh bài của cuộc chơi. Mặc định khi khai cuộc là vòng theo chiều kim đồng hồ";
        lang[330] = "Lá bài +2: Khi đánh quân bài này xuống, người kế tiếp bắt buộc bốc 2 lá bài và mất lượt";
        lang[331] = "Lá bài đổi màu: Khi đánh quân bài này, bạn có quyền lựa chọn màu sắc cho lượt đánh tiếp theo";
        lang[332] = "Các lá bài mở rộng";
        lang[333] = "Đây là những lá bài mở rộng so với bộ bài cơ bản để tăng thêm phần thú vị trong quá trình chơi";
        lang[334] = "Lá bài mất lượt x2: Bỏ qua liên tiếp 2 lượt chơi, nếu chỉ có 2 người chơi trong bàn, nó sẽ chỉ có tác dụng như lá bài mất lượt bình thường";
        lang[335] = "Lá bài bão tố: Khi đánh lá bài này, đối phương tiếp theo buộc phải bốc bài cho tới khi bốc được lá bài trùng màu với lá bão tố đã đánh hoặc bốc được lá bài đổi màu, và mất lượt";
        lang[336] = "Lá bài xả màu: Khi đánh lá bài này, bạn được phép xả hết tất cả các lá bài cùng màu";
        lang[337] = "Lá bài tấn công bất ngờ: Đổi màu ván bài tùy ý, buộc đối phương bốc thêm 4 lá, tuy nhiên được phép chọn đối phương bất kỳ để tấn công.";
        lang[338] = "Lá bài ngẫu nhiên: Buộc đối phương phải bốc ngẫu nhiên từ 1-9 lá bài và mất lượt";
        lang[339] = "Khi bắt đầu, ván bài sẽ được chơi theo chiều kim đồng hồ, người chơi đầu tiên đánh quân bài trùng màu với yêu cầu xuống bàn, Người chơi tiếp theo cần tiền hành đánh một quân bài tiếp nối với quân bài của người chơi đầu tiên. Mỗi lần người chơi chỉ được đánh một quân bài trong mỗi lượt của mình dựa theo nguyên tắc sau: Người chơi đánh quân bài xuống bằng cách sử dụng một quân bài cùng số hoặc cùng màu với quân bài của người chơi trước đó ( bao gồm cả lá số và lá chức năng có một màu đều được). Hoặc người chơi có thể đánh các quân bài có chức năng đổi màu.\n\n" +
            "- Nếu không đánh được bài xuống thì người chơi phải bốc 1 lá bài. Nếu lá bài đó đúng với nguyên tắc đánh bài, người chơi có thể đánh luôn lá bài đó. Còn nếu không, người chơi sẽ bị mất lượt.\n" +
            "- Người chơi sẽ thay phiên nhau đánh các là bài theo lượt cho đến khi người chơi đánh hết lá bài trên tay của mình xuống.\n\n" +
            "Một ván bài kết thúc khi có người bỏ được hết bài xuống. Khi đó tiến hành đếm điểm của những người chơi còn lại. Người thắng cuộc sẽ được có toàn bộ số điểm này. Số điểm này có thể quy ra phần thưởng sau này.\n\n" +
            "Cách tính điểm trong trò chơi như sau:\n" +
            "- Cộng tổng điểm của các lá bài trên tay người thua cuộc." +
            "- Các lá bài số (0-9) được tính điểm bằng với số ghi trên lá bài." +
            "- Các lá bài chức năng như +2, Cấm lượt, Đổi chiều... được tính 20 điểm." +
            "- Các lá bài Đổi màu, Đổi màu +4... được tính 50 điểm";
        lang[340] = "Cơ bản";
        lang[341] = "Mở rộng";
        lang[342] = "Cách chơi";
        lang[343] = "Lá bài đổi màu +4: Bạn có quyền lựa chọn màu sắc cho lượt tiếp theo, đồng thời buộc người chơi tiếp theo bốc 4 lá bài và mất lượt";
        lang[344] = "Thông tin cách chơi";
        lang[345] = "Tự bốc bài khi không có lá phù hợp";
        lang[346] = "Vui lòng nhập tên";
        lang[347] = "Tên không được chứa ký tự đặc biệt";

        //Vote setup
        lang[348] = "Bạn muốn chức năng gì sẽ xuất hiện trong phiên bản tiếp theo";
        lang[349] = "Bầu chọn";
        lang[350] = "Thêm 1 lá bài";
        lang[351] = "Thêm 1 lá bài mở rộng vào bộ bài, lá bài này chỉ xuất hiện trong chế độ chơi mở rộng, trong bản cập nhật tới, bạn sẽ biết lá bài này là gì";
        lang[352] = "Thêm 1 máy";
        lang[353] = "Nâng tổng số người chơi trong bàn lên 7, bạn có thể kiếm được nhiều điểm hơn từ đây";
        lang[354] = "Cám ơn bình chọn của bạn";
        //=============================================
        lang[355] = "Tự động chơi (không khuyến cáo)";
        lang[356] = "Chế độ cơ bản: ";
        lang[357] = "Chế độ mở rộng: ";
        lang[358] = "Tổng điểm: ";
        lang[359] = "Thắng chế độ cơ bản: ";
        lang[360] = "Thua chế độ cơ bản: ";
        lang[361] = "Tỉ lệ thắng: ";
        lang[362] = "Thắng chế độ mở rộng: ";
        lang[363] = "Thua chế độ mở rộng: ";

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
    private static void Language_EN () {
        #region Phần ngôn ngữ tổng quan 

        lang[0] = "";
        lang[1] = "";
        lang[2] = "Comming Soon...!";
        lang[11] = "Infor";
        lang[12] = "Equip";
        lang[13] = "Skill";
        lang[14] = "Special";
        lang[15] = "Story";
        lang[16] = "Fighter";
        lang[17] = "Assassin";
        lang[18] = "Support";
        lang[19] = "Tanker";
        lang[20] = "Archer";
        lang[21] = "Magician";
        lang[22] = "Values";
        lang[23] = "Level: ";
        lang[24] = "Health";
        lang[25] = "Mana";
        lang[26] = "Atk Physic";
        lang[27] = "Atk Magic";
        lang[28] = "Armor";
        lang[29] = "Magic Resist";
        lang[30] = "Life steal physic";
        lang[31] = "Life steal magic";
        lang[32] = "Lethality";
        lang[33] = "Magic penetration";
        //lang[34] = "Health regen per " + ItemCoreSetting.SecondHeathRegen.ToString () + " second";
        lang[35] = "Mana regen";
        lang[36] = "Critical";
        lang[37] = "+ h.ứng gây ra";
        lang[38] = "Tenacity (%)";
        lang[39] = "Attack speed";
        lang[40] = "%";
        lang[41] = "/s";
        lang[42] = "/lvl)";
        lang[43] = "Cooldown reduction";
        lang[45] = "Skill cooldown";
        lang[46] = "Mana skill";
        lang[47] = "+ ";
        lang[48] = "Remove";

        //Setting --Window setting
        lang[50] = "Settings";
        lang[51] = "Graphics";
        lang[52] = "Very low";
        lang[53] = "Low";
        lang[54] = "Normal";
        lang[55] = "Hight";
        lang[56] = "Audio";
        lang[57] = "Music";
        lang[58] = "Sound";
        lang[59] = "Languages";
        //--------Button
        lang[60] = "OK";
        lang[61] = "Chọn NV";
        lang[62] = "Feedback";
        lang[63] = "Exit game";
        lang[64] = "Title...";
        lang[65] = "Content...";
        //--------Window feedback
        lang[66] = "Send";
        lang[67] = "Cancel";
        lang[68] = "Email...";
        lang[69] = "";
        lang[70] = "Please input title";
        lang[71] = "Please input content";
        lang[72] = "<color=green>Thanks your feedback!</color>";
        lang[73] = "Phản hồi của bạn chưa được gửi đi, kiểm tra lại mạng xem sao";
        //---------Inventory
        lang[74] = "Use: ";
        lang[75] = "Quantity: ";
        lang[76] = "Price: ";
        lang[77] = "Không thao tác được khi đang ở chế độ lựa chọn vật phẩm";
        lang[78] = "Fast Equip";
        lang[79] = "Fast Remove";
        lang[80] = "Remove success";

        //Scene Room
        lang[100] = "Combat";
        lang[101] = "Phát hiện thấy phiên bản {0} trên máy chủ, bạn có muốn tải và cập nhật phiên bản mới ?";
        lang[102] = "Are you want exit game?";
        lang[103] = "<color=green>Applied language, please change scene!</color>";
        lang[104] = "Nhập mã phòng";
        lang[110] = " tạo thành công phòng ";
        lang[111] = "Đã vào phòng ";
        lang[112] = " đã kết nối";
        lang[113] = "Bạn cùng phòng đã thoát";
        lang[114] = "Chờ đồng đội sẵn sàng";
        lang[115] = "Sẵn sàng";
        lang[116] = "Do not action";
        lang[117] = "Can not connected to server";
        lang[118] = "Team";
        lang[119] = "Personal";

        lang[120] = "Can not connect to server...";
        lang[121] = "Please input username";
        lang[122] = "Your name";
        lang[123] = "Your squad needs at least 3 hero";
        lang[124] = "Save battery";
        lang[125] = " - ON";
        lang[126] = " - OFF";
        lang[127] = "Chat box system is loading...!";
        lang[128] = "Can not add equip item, slot is full";
        lang[129] = "Equiped";

        //Scene Inventory
        lang[130] = "Confirm sell {0} item with {1} gold";
        lang[131] = "Confirm sell this item with {0} gold";
        lang[132] = "View";
        lang[133] = "all";
        lang[134] = "Equip";
        lang[135] = "Use";
        lang[136] = "Quest";
        lang[137] = "Select";
        lang[138] = "Sell";
        lang[139] = "Sort";
        lang[140] = "Inventory not enough slots";
        lang[141] = "<color=green>Craft success!</color>";
        lang[142] = "<color=red>Eror - Do not craft item</color>";
        lang[143] = "<color=red>Not enough resources to craft</color>";
        lang[144] = "Bag";
        lang[145] = "Craft";
        lang[146] = "Heroes";
        lang[147] = "Craft all";
        lang[148] = "Required";
        lang[149] = "<color=red>Not enough resources</color>";

        lang[150] = "Earth";
        lang[151] = "Water";
        lang[152] = "Fire";
        lang[153] = "Wind";

        lang[154] = "Upgrade";
        lang[155] = "Up quality";
        lang[156] = "Disassemble";
        lang[157] = "<color=green>Upgrade successfully</color>";
        lang[158] = "<color=red>This item is max level</color>";
        lang[159] = "<color=green>Up quality successfully</color>";
        lang[160] = "<color=red>This item is max quality</color>";
        lang[161] = "Battle button";
        lang[162] = " - Left";
        lang[163] = " - Right";
        lang[164] = "Slow motion";
        lang[165] = "Intrinsic and skill";
        lang[166] = "<color=green>Intrinsic</color>";
        lang[167] = "<color=orange>Skill</color>";
        lang[168] = "<color=#454523>The Forest</color>";
        lang[169] = "<color=#BDAD62>Meadow</color>";
        lang[170] = "<color=#FF5410>Volcano</color>";
        lang[171] = "Snow Mountain";
        lang[172] = "<color=red>Hell</color>";
        lang[173] = "<color=green>Poisonous Cave</color>";
        lang[174] = "<color=#3F42AF>Ghost Cave</color>";
        lang[175] = "<color=red>Inventory slot is limited</color>";
        lang[176] = "Win";
        lang[177] = "Lose";
        lang[178] = "EXP";
        lang[179] = "Pause\nYou want leave battle?";
        lang[180] = "You want watch video to expand inventory?";
        lang[181] = "Wait for next video to load";
        lang[182] = "Watch video for reward";
        lang[183] = "Watch";
        lang[184] = "Let's go";
        lang[185] = "Equip physic";
        lang[186] = "Equip magic";
        lang[187] = "Equip defense";
        lang[188] = "Item use";
        lang[189] = "Equip special";
        lang[190] = "You received <color=#FF5410>{0}</color>";
        lang[191] = "<color=red>Do not enough Phoenix feather</color>";
        lang[192] = "Auto spin";
        lang[193] = "Stop";
        lang[194] = "Introduction";
        //lang[195] = "- Required for 1 lucky spin is " + Module.SpinItemQuantity.ToString () + " <color=red>Phoenix Feather</color>";
        lang[195] += "\n\n- Phoenix Feather can be found in every region";
        lang[195] += "\n\n- Item reward can is any item, from low value to extremely rare.";
        lang[195] += "\n\n- Caution: Sort your inventory before spin";
        lang[196] += "Values of item is random when crafting";
        lang[197] = "Enter";
        lang[198] = "Hire Assassins";
        lang[199] = "Hire assassins with {0} gems";

        lang[200] = "Health";
        lang[201] = "Mana";
        lang[202] = "Atk";
        lang[203] = "Magic";
        lang[204] = "Armor";
        lang[205] = "Magic resist";
        //lang[206] = "Health regen per " + ItemCoreSetting.SecondHeathRegen.ToString () + " second";
        lang[207] = "Global";
        lang[208] = "Your country";
        lang[209] = "Damage water";
        lang[210] = "Damage fire";
        lang[211] = "Earth Defense";
        lang[212] = "Water Defense";
        lang[213] = "Fire Defense";
        lang[214] = "Attack speed";
        lang[215] = "Skill cooldown";
        lang[216] = "Life steal physic";
        lang[217] = "Life steal magic";
        lang[218] = "Lethality";
        lang[219] = "Magic penetration";
        lang[220] = "Critical";
        lang[221] = "Tenacity";
        lang[222] = "Cooldown reduction";
        lang[223] = "Damage excellent";
        lang[224] = "Excellent Defense";
        lang[225] = "Double damage ratio";
        lang[226] = "Triple damage ratio";
        lang[227] = "Reflect Damage";
        lang[228] = "Reward plus";
        lang[229] = "Mana for atk";
        lang[230] = "Mana for skill";
        lang[231] = "Health per level";
        lang[232] = "Atk per level";
        lang[233] = "Magic per level";
        lang[234] = "Armor per level";
        lang[235] = "Magic resist per level";
        lang[236] = "Health regen per level";
        lang[237] = "Mana regen per level";
        lang[238] = "Cooldown reduction per level";
        lang[250] = "You not enough gems";
        lang[251] = "Hire assassins successfully!";
        lang[252] = "Preview";
        lang[253] = "Random";
        lang[254] = "Survival";
        lang[255] = "This mode allows you to know in advance the enemy squad, so you can choose the tactics and equipment that best suit you.";
        lang[256] = "You will not know who you will encounter in this mode, of course, the reward you get will be higher.";
        lang[257] = "Fight until you fall, the leaderboard will always be waiting for you.";
        lang[258] = "Gold: ";
        lang[259] = "Gem: ";
        lang[260] = "Point: ";
        lang[261] = "Remove";
        lang[262] = "Information";
        lang[263] = "Equip";
        lang[264] = "Change equip";
        lang[265] = "Select your equip";
        lang[266] = "Can't change the item for yourself";
        lang[267] = "Characters are not yet equipped";
        lang[268] = "Fast change equip success!";
        lang[269] = "Break ball";
        lang[270] = "Hammer: ";
        lang[271] = "Spin";
        lang[272] = "Break ball is a feature that requires a network connection. Every day, you will have 5 hammers to break the ball"; //Hướng dẫn break ball, lấy từ server
        lang[273] = "Can not break this item";
        lang[274] = "Inventory";
        lang[275] = "Reward ";
        lang[276] = " golds";
        lang[277] = " gems";
        lang[278] = "Confirm break this item?";
        lang[279] = "Online Reward";
        lang[280] = "Claim";
        lang[281] = "Team is full";
        lang[282] = "Choose item type to craft";
        lang[283] = "Choose item to craft";
        lang[284] = "Resources need to craft";
        lang[285] = "With items use, you can craft many";
        lang[286] = "Filter class hero";
        lang[287] = "Hero list";
        lang[288] = "Choose functions";
        lang[289] = "Hero detail informations";
        lang[290] = "You can equip for hero in this";
        lang[291] = "Skill and Intrinsic information";
        lang[292] = "Hero's story";
        lang[293] = "Special effects that this hero possesses";
        lang[294] = "Start";
        lang[295] = "Limit jewel socket";
        lang[296] = "Please select item";
        lang[297] = "Insert jewel";
        lang[298] = "Break jewel";
        lang[299] = "Create socket";
        lang[300] = "<color=red>Confirm break this jewel?</color>";
        lang[301] = "Jewel destroyed";
        lang[302] = "Socket";
        lang[303] = "Can not create socket";
        lang[304] = "Do not enough gold";
        lang[305] = "Hell Tower";
        lang[306] = "Confirm leave stage?";
        lang[307] = "You need select a card or choose pass";
        lang[308] = "This card not match";
        lang[309] = "Style Options";
        lang[310] = "Fast select card";
        lang[311] = "Select color";
        lang[312] = "OK";
        lang[313] = "Pass";
        lang[314] = "Exit";
        lang[315] = "Play";
        lang[316] = "Ranking";
        lang[317] = "Choose type";
        lang[318] = "Players";
        lang[319] = "Options";
        lang[320] = "Background color";
        lang[321] = "Player ";
        lang[322] = "Card game";
        lang[323] = "Fast pass round";
        lang[324] = "Show card available";
        lang[325] = "Card number";
        lang[326] = "These cards are divided equally into 4 colors: red, yellow, green, blue. They are numbered from 0 to 9";
        lang[327] = "Functional card";
        lang[328] = "Skip card: skip round of next player";
        lang[329] = "Reverse card: When this card is throw, it will be reversed. The default when the game is open is clockwise";
        lang[330] = "Card +2: forced next player get 2 card";
        lang[331] = "Change color card: allow you choose a color for next round";
        lang[332] = "Extension cards";
        lang[333] = "These are cards that extend from the basic cards to add some fun to the game";
        lang[334] = "Skip card x2: Ignore 2 consecutive turns, if there are only 2 players in the table, it will only work as a skip card";
        lang[335] = "Tornado card: When playing this card, the next opponent is forced to draw until it has picked a card of the same color as the tornado card, or the color changed card, and lost its turn";
        lang[336] = "Discard card: When playing this card, you are allowed to discharge all cards of the same color";
        lang[337] = "Unexpected attack card: Change the color of round at will, forcing the opponent to get another 4 cards, but it is allowed to choose an opponent to attack.";
        lang[338] = "Random card: Force the opponent to draw randomly from 1-9 cards and lose their turn";
        lang[339] = "At the start of the game, the hand will be played clockwise, the first player to play a card of the same color as the required, The next player needs to play a card that continues with the first player's card. Each player is allowed to play only one card at a time in his or her turn based on the following principle: Players play their cards down using a card of the same number or color of the previous player's card (including Both number and function cards have one color.) Or players can play cards with the function of changing colors.\n\n" +
            "- If the card cannot be dealt then the player must draw a card. If the card is in accordance with the rules of the card, the player may play that card as well. If not, the player will lose a turn.\n" +
            "- Players take turns to play their cards in turn until the player has dealt all the cards in list.\n\n" +
            "A game ends when someone throw all the cards. Then proceed to count the points of the remaining players. The winner will get all these points. These points can be converted into rewards later.\n\n" +
            "The method of calculating the score in the game is as follows:\n" +
            "- Add up the points in the cards to the loser." +
            "- Cards with numbers (0-9) are counted equal to the number on the card." +
            "- Functional cards such as +2, Skip card, Reverse card, etc. are counted 20 points." +
            "- Cards that Change Color, Change Color +4 ... are counted 50 points";
        lang[340] = "Basic";
        lang[341] = "Extension";
        lang[342] = "Introduction";
        lang[343] = "Change color +4: allow you choose a color for next round, forced next player get 4 card and skip their round";
        lang[344] = "Informations";
        lang[345] = "Fast get card";
        lang[346] = "Please input your name";
        lang[347] = "Your name has special character";

        //Vote setup
        lang[348] = "What function you want see in the next version?";
        lang[349] = "Vote";
        lang[350] = "New a card";
        lang[351] = "Add a new card extension to list card";
        lang[352] = "Add 1 AI bot";
        lang[353] = "Add 1 AI bot, maximum player up to 7";
        lang[354] = "Thanks for your vote";
        //=============================================
        lang[355] = "Auto play (not recommend)";
        lang[356] = "Basic mode point";
        lang[357] = "Extension mode point";
        lang[358] = "Total point";
        lang[359] = "Basic mode win: ";
        lang[360] = "Basic mode lose: ";
        lang[361] = "Ratio: ";
        lang[362] = "Extension mode win: ";
        lang[363] = "Extension mode lose: ";

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