using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Code._0.DTO.Models;
using Assets.Code._4.CORE;
using System.Linq;

public static class GameSystem
{
    public static SettingModel Settings;//Setting game
    public static Canvas MessageCanvas; //Canvas UI dành cho confirm box, message error...
    public static AnimationCurve AnimCurve; //Đường vẽ tọa độ di chuyển object
    public static bool ControlActive = true; //Cho phép thao tác mọi thứ trong scene hay không
    public static AudioSource BGM;
    public static AudioSource Sound; //Control Âm thanh của skill
                                     //public static SettingModel Settings;//Setting game
    public static ControlFunctions ControlFunctions;
    public static GameObject[] ObjectUI = new GameObject[4];
    //public static Player UserPlayer;
    public static string FBID;
    private static readonly string StrData = "Data";
    private static readonly string StrSetting = "Settings";

    //public static List<RankingModel> GlobalRankingList;
    //public static List<RankingModel> CountryRankingList;

    public static int TotalRoundPlay = 0;

    public static string AppVersion;

    #region Khởi tạo variable Confirm Box 
    private static GameObject ConfirmBox; //Confirm box
    private static Button ConfirmBoxOK; //Button OK của confirm box
    private static Button ConfirmBoxCancel; //Button Cancel của confirm box
    public static Text ConfirmBoxText; //Text thông báo của confirm box
    public static int ConfirmBoxResult; //0 = wait, 1 = OK, 2 = Cancel
    #endregion

    #region Khởi tạo variable Message Box 
    private static readonly int MessageMax = 20; //Tối đa số object message
    private static List<GameObject> Message;
    private static Text[] MessageText;
    private static Image[] MessageImage;
    private static float[] MessageOpacity;
    private static RectTransform[] MessageRect;
    #endregion

    #region Khởi tạo variables Information Box 

    static GameObject InforBox; //Object box thông tin
    static Text InforBoxText; //Nội dung trong box thông tin

    #endregion
    public static void Initialize()
    {
        if (MessageCanvas != null)
            return;
        LoadUserData();
        MessageCanvas = (Canvas)MonoBehaviour.Instantiate(Resources.Load<Canvas>("Prefabs/UI/MessageCanvas"), new Vector3(0, 0, 0), Quaternion.identity);
        ControlFunctions = MessageCanvas.transform.GetChild(2).GetComponent<ControlFunctions>();
        SetupConfirmBox();
        SetupMessage();
        SetupInforBox();
        AppVersion = Application.version;
        if (Sound == null)
            Sound = MessageCanvas.transform.GetChild(1).GetComponent<AudioSource>();
    }

    public static void LoadUserData()
    {
        //var str = PlayerPrefs.GetString(StrData);
        //var strsave = !string.IsNullOrEmpty(str) ? Securitys.Decrypt(str).ToString() : "";
        //if (string.IsNullOrEmpty(str))
        //{
        //    UserPlayer = new Player();
        //}
        //else
        //{
        //    UserPlayer = JsonUtility.FromJson<Player>(strsave);
        //}

        //str = PlayerPrefs.GetString(StrSetting);
        //strsave = !string.IsNullOrEmpty(str) ? Securitys.Decrypt(str).ToString() : "";
        //if (string.IsNullOrEmpty(str))
        //{
        //    Settings = new SettingModel();
        //    Settings.Language = Application.systemLanguage.ToString().StartsWith("Vietnam") ? 1 : 0;
        //    Settings.MusicEnable = Settings.SoundEnable = true;
        //}
        //else
        //{
        //    Settings = JsonUtility.FromJson<SettingModel>(strsave);
        //}
        //SaveUserData();
        //Languages.SetupLanguage(Settings.Language);
    }

    public static void SaveUserData()
    {
        //UserPlayer.Country = Application.systemLanguage.ToCountryCode();
        //PlayerPrefs.SetString(StrData, Securitys.Encrypt(JsonUtility.ToJson(UserPlayer)).ToString());
        //PlayerPrefs.SetString(StrSetting, Securitys.Encrypt(JsonUtility.ToJson(Settings)).ToString());
    }

    public static string ToCountryCode(this SystemLanguage language)
    {
        string result;
        if (COUTRY_CODES.TryGetValue(language, out result))
        {
            return result;
        }
        else
        {
            return COUTRY_CODES[SystemLanguage.Unknown];
        }
    }
    private static readonly Dictionary<SystemLanguage, string> COUTRY_CODES = new Dictionary<SystemLanguage, string>()
        {
            { SystemLanguage.Afrikaans, "ZA" },
            { SystemLanguage.Arabic    , "SA" },
            { SystemLanguage.Basque    , "US" },
            { SystemLanguage.Belarusian    , "BY" },
            { SystemLanguage.Bulgarian    , "BJ" },
            { SystemLanguage.Catalan    , "ES" },
            { SystemLanguage.Chinese    , "CN" },
            { SystemLanguage.Czech    , "HK" },
            { SystemLanguage.Danish    , "DK" },
            { SystemLanguage.Dutch    , "BE" },
            { SystemLanguage.English    , "US" },
            { SystemLanguage.Estonian    , "EE" },
            { SystemLanguage.Faroese    , "FU" },
            { SystemLanguage.Finnish    , "FI" },
            { SystemLanguage.French    , "FR" },
            { SystemLanguage.German    , "DE" },
            { SystemLanguage.Greek    , "JR" },
            { SystemLanguage.Hebrew    , "IL" },
            { SystemLanguage.Icelandic    , "IS" },
            { SystemLanguage.Indonesian    , "ID" },
            { SystemLanguage.Italian    , "IT" },
            { SystemLanguage.Japanese    , "JP" },
            { SystemLanguage.Korean    , "KR" },
            { SystemLanguage.Latvian    , "LV" },
            { SystemLanguage.Lithuanian    , "LT" },
            { SystemLanguage.Norwegian    , "NO" },
            { SystemLanguage.Polish    , "PL" },
            { SystemLanguage.Portuguese    , "PT" },
            { SystemLanguage.Romanian    , "RO" },
            { SystemLanguage.Russian    , "RU" },
            { SystemLanguage.SerboCroatian    , "SP" },
            { SystemLanguage.Slovak    , "SK" },
            { SystemLanguage.Slovenian    , "SI" },
            { SystemLanguage.Spanish    , "ES" },
            { SystemLanguage.Swedish    , "SE" },
            { SystemLanguage.Thai    , "TH" },
            { SystemLanguage.Turkish    , "TR" },
            { SystemLanguage.Ukrainian    , "UA" },
            { SystemLanguage.Vietnamese    , "VN" },
            { SystemLanguage.ChineseSimplified    , "CN" },
            { SystemLanguage.ChineseTraditional    , "CN" },
            { SystemLanguage.Unknown    , "US" },
            { SystemLanguage.Hungarian    , "HU" },
        };


    #region Setup and control Confirm Box 

    //Setup Confirm Box
    private static void SetupConfirmBox()
    {
        ConfirmBox = GameObject.Find("ConfirmBox");
        ConfirmBoxOK = MessageCanvas.transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        ConfirmBoxCancel = MessageCanvas.transform.GetChild(0).transform.GetChild(3).GetComponent<Button>();
        ConfirmBoxText = MessageCanvas.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>();
        ConfirmBoxCancel.onClick.AddListener(() => CancelConfirmBox());
        ConfirmBoxOK.onClick.AddListener(() => AcceptConfirmBox());
        ConfirmBoxResult = 0;
        ConfirmBox.SetActive(false);
    }
    //Hủy confirm box
    private static void CancelConfirmBox()
    {
        ConfirmBox.SetActive(false);
        ConfirmBoxText.text = "";
        ConfirmBoxResult = 2; //Result = cancel
    }
    //Chấp nhận confirm box
    private static void AcceptConfirmBox()
    {
        ConfirmBox.SetActive(false);
        ConfirmBoxText.text = "";
        ConfirmBoxResult = 1; //Result = ok
    }

    /// <summary>
    /// Hiển thị màn hình xác nhận
    /// </summary>
    /// <param name="text"></param>
    public static void ShowConfirmDialog(string text)
    {
        try
        {
            ConfirmBoxResult = 0;
            ConfirmBox.SetActive(true);
            ConfirmBoxText.text = text;
        }
        catch { }
    }

    #endregion

    #region Setup and control Message 
    private static void SetupMessage()
    {
        Message = new List<GameObject>(); // new GameObject[MessageMax];
        MessageText = new Text[MessageMax];
        MessageImage = new Image[MessageMax];
        MessageOpacity = new float[MessageMax];
        MessageRect = new RectTransform[MessageMax];
        for (int i = 0; i < MessageMax; i++)
        {
            Message.Add((GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/PanelMessage"), new Vector3(0, 0, 0), Quaternion.identity));
            Message[i].transform.SetParent(MessageCanvas.transform, false);
            MessageRect[i] = Message[i].transform.GetChild(0).transform.GetChild(1).GetComponent<RectTransform>();
            MessageImage[i] = Message[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
            MessageText[i] = Message[i].transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
            MessageOpacity[i] = 1f;
            Message[i].SetActive(false);
        }
    }

    /// <summary>
    /// Trả về object đang rảnh
    /// </summary>
    private static int GetSlot()
    {
        var find = Message.FindIndex(x => !x.activeSelf);
        if (find != -1) //Nếu tìm thấy
        {
            Message[find].transform.position = new Vector3(0, 0, 0); //Set lại tọa độ
            Message[find].SetActive(true); //Hiển thị mess lên
        }
        return find;
    }

    /// <summary>
    /// Hiển thị thông báo
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static IEnumerator ShowMessage(string text)
    {
        var slotmessage = GetSlot();
        if (slotmessage != -1)
        {
            MessageText[slotmessage].text = text;
            MessageRect[slotmessage].sizeDelta = new Vector2(MessageText[slotmessage].preferredWidth, MessageRect[slotmessage].sizeDelta.y);
            var rect = Message[slotmessage].transform;
            var startPos = Message[slotmessage].transform.position;
            var targetPos = startPos + new Vector3(0, 5f, 0);
            float time = 0;
            float rate = 1 / 2f; //2 giây
            yield return new WaitForSeconds(.3f);
            while (time < 1)
            {
                time += rate * Time.deltaTime;
                rect.transform.position = Vector2.Lerp(startPos, targetPos, AnimCurve.Evaluate(time));
                yield return 0;
            }
            //Gán lại tọa độ sau khi move xong
            rect.position = targetPos;
            Message[slotmessage].SetActive(false);
            MessageText[slotmessage].text = "";
        }
        //========================================
    }

    /// <summary>
    /// Disable toàn bộ message đang hiển thị (dành cho chuyển scene mà vẫn đang hiển thị)
    /// </summary>
    public static void DisableAllMessenger()
    {
        var count = Message.Count;
        for (int i = 0; i < count; i++)
        {
            Message[i].SetActive(false);
        }
    }

    #endregion

    #region Setup and control Infor box 

    /// <summary>
    /// Khởi tạo information box
    /// </summary>
    static void SetupInforBox()
    {
        InforBox = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/InformationBox"), new Vector3(0, 0, 0), Quaternion.identity);
        InforBox.transform.SetParent(MessageCanvas.transform, false);
        InforBox.SetActive(false);
        InforBoxText = InforBox.transform.GetChild(1).GetComponent<Text>(); //Text nội dung của box
    }

    /// <summary>
    /// Khởi tạo sự kiện information cho button - hiển thị bảng thông tin khi dí chết button
    /// </summary>
    /// <param name="obj">Object button</param>
    /// <param name="eventDown">Sự kiện nhấn</param>
    /// <param name="eventUp">Sự kiện nhả</param>
    public static void CreateBoxDownUp(GameObject obj, UnityEngine.Events.UnityAction<BaseEventData> eventDown, UnityEngine.Events.UnityAction<BaseEventData> eventUp)
    {
        EventTrigger trigger = obj.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        var pointerUp = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown; //Event nhấn
        pointerUp.eventID = EventTriggerType.PointerUp; //Event nhả
        pointerDown.callback.AddListener(eventDown);
        pointerUp.callback.AddListener(eventUp);
        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }

    /// <summary>
    /// Show bảng thông tin (Đang chưa sử dụng, chưa viết hoàn thành)
    /// </summary>
    /// <param name="rec">Chiều dài rộng</param>
    /// <param name="pos">Tọa độ của box</param>
    /// <param name="space">Khoảng cách từ pos</param>
    /// <param name="posID">Vị trí so với con trỏ khi tap, 1->9</param>
    /// <param name="content">Nội dung trong box</param>
    public static void ShowBoxInformation(Vector2 rec, Vector3 pos, Vector2 space, sbyte posID, string content)
    {
        InforBoxText.text = content;
        var sizebox = InforBox.GetComponent<RectTransform>().sizeDelta = new Vector2(rec.x + 16, InforBoxText.GetComponent<RectTransform>().sizeDelta.y + 16);
        //Debug.Log (lineCount);
        //Debug.Log (sizebox);
        switch (posID)
        { //Set tọa độ dựa trên posID, 1 = topleft, 2 = top, 3 = topright, 4 = left, 5 = mid, 6 = right, 7 = botleft, 8 = bot, 9 = botright
            case 1:
                InforBox.GetComponent<RectTransform>().position = pos - new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(sizebox.x / 2 + space.x, sizebox.y / 2 + space.y, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(sizebox.x / 2 + space.x, sizebox.y / 2 + space.y, 0)).y, 0);
                break;
            case 2:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos).x, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(rec.x / 2, rec.y / 2, 0)).y, 0);
                break;
            case 3:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos + new Vector3(rec.x / 2, rec.y / 2, 0)).x, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(rec.x / 2, rec.y / 2, 0)).y, 0);
                break;
            case 4:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos - new Vector3(rec.x / 2, rec.y / 2, 0)).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                break;
            case 5:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                break;
            case 6:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos + new Vector3(rec.x / 2, rec.y / 2, 0)).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                break;
            case 7:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos - new Vector3(rec.x / 2, rec.y / 2, 0)).x, Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(rec.x / 2, rec.y / 2, 0)).y, 0);
                break;
            case 8:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos).x, Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(rec.x / 2, rec.y / 2, 0)).y, 0);
                break;
            case 9:
                InforBox.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(pos + new Vector3(rec.x / 2, rec.y / 2, 0)).x, Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(rec.x / 2, rec.y / 2, 0)).y, 0);
                break;
            default:
                InforBox.GetComponent<RectTransform>().position = Vector2.zero;
                break;
        }
        InforBox.GetComponent<RectTransform>().position = pos;
        InforBox.gameObject.SetActive(true);
    }

    /// <summary>
    /// Ẩn bảng thông tin
    /// </summary>
    public static void HideBoxInformation()
    {
        InforBox.gameObject.SetActive(false);
    }

    #endregion

    #region Setup and control Sound, Music 

    /// <summary>
    /// Khởi tạo nhạc nền
    /// </summary>
    private static void SetupBGM()
    {
        if (GameSystem.Settings.MusicEnable || GameSystem.Settings.SoundEnable)
        {
            BGM = MessageCanvas.transform.GetChild(1).GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Chạy nhạc nền
    /// </summary>
    /// <param name="type">0: nhạc nền chung, 1: nhạc nền battle</param>
    public static void RunBGM(int type)
    {
        if (GameSystem.Settings.MusicEnable)
        {
            if (BGM == null)
                SetupBGM();
            if (!BGM.isPlaying)
            {
                BGM.clip = type.Equals(0) ? Resources.Load<AudioClip>("Audio/BGM/Main" + UnityEngine.Random.Range(0, 3).ToString()) : Resources.Load<AudioClip>("Audio/BGM/Battle" + UnityEngine.Random.Range(0, 4).ToString());
                BGM.Play(0);
            }
        }
    }

    /// <summary>
    /// Dừng nhạc nền
    /// </summary>
    public static void StopBGM()
    {
        if (BGM != null)
            BGM.Stop();
    }

    public static IEnumerator PlaySound(AudioClip audioFile, float timeDelay)
    {
        if (GameSystem.Settings.SoundEnable)
        {
            if (BGM == null)
                SetupBGM();
            yield return new WaitForSeconds(timeDelay);
            BGM.PlayOneShot(audioFile);
        }
    }

    /// <summary>
    /// Fadeout BGM
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        if (GameSystem.Settings.MusicEnable)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }

    #endregion

    #region UI Controller 

    /// <summary>
    /// Khởi tạo các prefab UI
    /// </summary>
    /// <param name="type"></param>
    public static void InitializePrefabUI(int slot, string nameUI)
    {
        if (slot == 1)
            ADS.HideBanner();
        ObjectUI[slot] = (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/" + nameUI), new Vector3(0, 0, 0), Quaternion.identity);
        ObjectUI[slot].GetComponent<Canvas>().worldCamera = Camera.main;
        ObjectUI[slot].GetComponent<Canvas>().planeDistance = 8;
    }

    /// <summary>
    /// Gỡ bỏ các prefab UI và giải phóng bộ nhớ
    /// </summary>
    /// <param name="type"></param>
    public static void DisposePrefabUI(int slot)
    {
        MonoBehaviour.Destroy(ObjectUI[slot]);
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// Xóa bỏ toàn bộ các object con từ object cha, giải phóng bộ nhớ
    /// </summary>
    /// <param name="obj"></param>
    public static void DisposeAllObjectChild(GameObject obj)
    {
        var count = obj.transform.childCount;
        for (var i = count - 1; i >= 0; i--)
        {
            MonoBehaviour.Destroy(obj.transform.GetChild(i).gameObject);
        }
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// Xóa bỏ 1 game object và cho phép giải phóng bộ nhớ
    /// </summary>
    /// <param name="obj">Object cần loại bỏ</param>
    /// <param name="isRefreshMemory">Có giải phóng bộ nhớ luôn ko ?</param>
    public static void DisposeObjectCustom(GameObject obj, bool isRefreshMemory)
    {
        MonoBehaviour.Destroy(obj);
        if (isRefreshMemory)
            Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// Xóa danh sách object và cho phép giải phóng bộ nhớ 
    /// </summary>
    /// <param name="objList"></param>
    /// <param name="isRefreshMemory"></param>
    public static void DisposeObjectList<T>(List<T> objList, bool isRefreshMemory) where T : UnityEngine.Object
    {
        if (objList != null)
        {
            var count = objList.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                MonoBehaviour.Destroy(objList[i]);
            }
            if (isRefreshMemory)
                Resources.UnloadUnusedAssets();
        }
    }

    /// <summary>
    /// Thay đổi scale của 1 object UI theo thời gian
    /// </summary>
    /// <param name="type">0 = rectransform, 1 = transform</param>
    /// <param name="obj">Objec truyền vào</param>
    /// <param name="targetScale">Scalse sẽ thay đổi</param>
    /// <param name="duration">Thời gian thay đổi là bao lâu</param>
    /// <returns></returns>
    public static IEnumerator ScaleUI(int type, GameObject obj, Vector3 targetScale, float duration)
    {
        float time = 0;
        float rate = 1 / duration;
        Vector2 startPos = type == 0 ? obj.GetComponent<RectTransform>().localScale : obj.GetComponent<Transform>().localScale;
        var rect = type == 0 ? obj.GetComponent<RectTransform>() : obj.GetComponent<Transform>();
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            rect.localScale = Vector3.Lerp(startPos, targetScale, time);
            yield return 0;
        }
    }

    /// <summary>
    /// hay đổi scale của 1 object UI theo thời gian
    /// </summary>
    /// <param name="type">0 = rectransform, 1 = transform</param>
    /// <param name="obj">Objec truyền vào</param>
    /// <param name="startScale">Scalse ban đầu</param>
    /// <param name="targetScale">Scalse sẽ thay đổi</param>
    /// <param name="duration">Thời gian thay đổi là bao lâu</param>
    /// <returns></returns>
    public static IEnumerator ScaleUI(int type, GameObject obj, Vector3 startScale, Vector3 targetScale, float duration)
    {
        if (type.Equals(0))
            obj.GetComponent<RectTransform>().localScale = startScale;
        else
            obj.GetComponent<Transform>().localScale = startScale;
        float time = 0;
        float rate = 1 / duration;
        Vector2 startPos = type == 0 ? obj.GetComponent<RectTransform>().localScale : obj.GetComponent<Transform>().localScale;
        var rect = type == 0 ? obj.GetComponent<RectTransform>() : obj.GetComponent<Transform>();
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            rect.localScale = Vector3.Lerp(startPos, targetScale, time);
            yield return 0;
        }
    }

    /// <summary>
    /// Ẩn object sau 1 khoảng thời gian (dành cho UI)
    /// </summary>
    /// <param name="obj">Object cần ẩn</param>
    /// <param name="time">Thời gian delay</param>
    /// <returns></returns>
    public static IEnumerator HideObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }

    /// <summary>
    /// Di chuyển object tới vị trí trong khoảng time.
    /// Lưu ý, nếu type là rect, thì startPos và targetPos phải là anchorPosition, còn ko là position
    /// </summary>
    /// <param name="isRect">true = rectransform, false = transform</param>
    /// <param name="obj">object sẽ move</param>
    /// <param name="startPos">tọa độ bắt đầu</param>
    /// <param name="targetPos">tọa độ kết thúc</param>
    /// <param name="duration">thời gian move</param>
    /// <param name="animCurve">đường cong move</param>
    /// <returns></returns>
    public static IEnumerator MoveObjectCurve(bool isRect, GameObject obj, Vector2 startPos, Vector2 targetPos, float duration, AnimationCurve animCurve)
    {
        var rect = isRect ? obj.GetComponent<RectTransform>() : null;
        var rect2 = isRect ? null : obj.GetComponent<Transform>();
        float time = 0;
        float rate = 1 / duration;
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            if (isRect)
                rect.localPosition = Vector2.Lerp(startPos, targetPos, animCurve.Evaluate(time));
            else
                rect2.position = Vector2.Lerp(startPos, targetPos, animCurve.Evaluate(time));
            yield return null;
        }
        //Gán lại tọa độ sau khi move xong
        if (isRect)
            rect.localPosition = targetPos;
        else
            rect2.position = targetPos;
    }

    /// <summary>
    /// Xoay object theo góc và time
    /// </summary>
    /// <param name="isRect"></param>
    /// <param name="obj"></param>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static IEnumerator RotationObject(bool isRect, GameObject obj, Vector3 startPos, Vector3 targetPos, float duration)
    {
        var rect = isRect ? obj.GetComponent<RectTransform>() : null;
        var rect2 = isRect ? null : obj.GetComponent<Transform>();
        float time = 0;
        float rate = 1 / duration;
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            if (isRect)
                rect.eulerAngles = Vector3.Lerp(startPos, targetPos, time);
            else
                rect2.eulerAngles = Vector3.Lerp(startPos, targetPos, time);
            yield return null;
        }
        //Gán lại tọa độ sau khi move xong
        if (isRect)
            rect.eulerAngles = targetPos;
        else
            rect2.eulerAngles = targetPos;
    }

    /// <summary>
    /// Trả về số lớn nhất trong mảng
    /// </summary>
    public static int FindNumberHighest(int[] listNumber)
    {
        var result = listNumber[0];
        for (int i = 0; i < listNumber.Length; i++)
        {
            if (result > listNumber[i])
                result = listNumber[i];
        }
        return result;
    }

    /// <summary>
    /// Trả về index số lớn nhất trong mảng
    /// </summary>
    public static int FindSlotNumberHighest(int[] listNumber)
    {
        var result = 0;
        for (int i = 0; i < listNumber.Length; i++)
        {
            if (listNumber[result] < listNumber[i])
                result = i;
        }
        return result;
    }
    #endregion

    #region Object Controller 

    /// <summary>
    /// Check và trả về thứ tự object đang ko hoạt động
    /// </summary>
    /// <param name="listGameObject"></param>
    /// <returns></returns>
    public static int? CheckListObjectActive(List<GameObject> listGameObject)
    {
        var count = listGameObject.Count;
        for (int i = 0; i < count; i++)
        {
            if (!listGameObject[i].activeSelf)
            {
                return i;
            }
            if (i >= count - 1)
                return null;
        }
        return null;
    }

    /// <summary>
    /// Nhân bản GameObject từ list
    /// </summary>
    /// <param name="listGameObject"></param>
    /// <param name="position"></param>
    /// <param name="isSetParent"></param>
    /// <param name="parent"></param>
    public static void DuplicateGameObjectFromList(List<GameObject> listGameObject, Vector3 position, bool isSetParent, GameObject parentObject)
    {
        //Nhân bản object đầu tiên của mảng
        listGameObject.Add(MonoBehaviour.Instantiate(listGameObject[0], position, Quaternion.identity));

        //Set parent nếu như truyền tham số
        if (isSetParent && parentObject != null)
        {
            listGameObject[listGameObject.Count - 1].transform.SetParent(parentObject.transform, false);
        }
    }

    /// <summary>
    /// Acive object trong list và tạo mới nếu toàn bộ object đang active
    /// </summary>
    /// <param name="listGameObject"></param>
    /// <param name="position"></param>
    /// <param name="isSetParent"></param>
    /// <param name="parentObject"></param>
    public static void CheckExistAndCreateObjectFromList(List<GameObject> listGameObject, Vector3 position, bool isSetParent, GameObject parentObject)
    {
        //Lấy slot trong mảng đang deactive
        var slotDeactive = CheckListObjectActive(listGameObject);

        //Nếu có object đang deactive => active nó
        if (slotDeactive != null)
        {
            listGameObject[(int)slotDeactive].SetActive(true);
            listGameObject[(int)slotDeactive].transform.position = position;
        }
        else
        { //Tạo mới 1 object nếu toàn bộ object trong mảng đều đang active
            DuplicateGameObjectFromList(listGameObject, position, isSetParent, parentObject);
        }
    }

    /// <summary>
    /// Disable toàn bộ object của list
    /// </summary>
    /// <param name="listGameObject"></param>
    /// <param name="position"></param>
    /// <param name="isSetParent"></param>
    /// <param name="parentObject"></param>
    public static void DisableObjectFromList(List<GameObject> listGameObject)
    {
        var count = listGameObject.Count;
        for (int i = 0; i < count; i++)
            listGameObject[i].SetActive(false);
    }

    /// <summary>
    /// Sắp xếp tăng giảm dần trong mảng
    /// </summary>
    /// <param name="list">mảng</param>
    /// <param name="isDesc">true = tăng dần, false = giảm dần</param>
    public static void QuickSortList(List<int> listObject, bool isDesc)
    {
        var count = listObject.Count;
        for (int i = 0; i < count; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                if (listObject[i] > listObject[j])
                {
                    var temp = listObject[i];
                    listObject[i] = listObject[j];
                    listObject[j] = temp;
                }
            }
        }
    }
    #endregion

    /// <summary>
    /// Check ký tự đặc biệt
    /// </summary>
    public static bool HasSpecialChar(string input)
    {
        string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
        foreach (var item in specialChar)
        {
            if (input.Contains(item)) return true;
        }

        return false;
    }
}
