using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static int mrgeCheck
    {
        get { return PlayerPrefs.GetInt("mrgeCheck"); }
        set { PlayerPrefs.SetInt("mrgeCheck", value); }
    }
    public static int CoinsCount
    {
        get { return PlayerPrefs.GetInt("CoinsCount"); }
        set { PlayerPrefs.SetInt("CoinsCount", value); }
    }
    public static int SoundPref
    {
        get { return PlayerPrefs.GetInt("SoundPref"); }
        set { PlayerPrefs.SetInt("SoundPref", value); }
    }
    public static int MusicPref
    {
        get { return PlayerPrefs.GetInt("MusicPref"); }
        set { PlayerPrefs.SetInt("MusicPref", value); }
    }
    public static int VibratePref
    {
        get { return PlayerPrefs.GetInt("VibratePref"); }
        set { PlayerPrefs.SetInt("VibratePref", value); }
    }

    public static int LevelNumberDraw
    {
        get { return PlayerPrefs.GetInt("LevelNumberDraw"); }
        set { PlayerPrefs.SetInt("LevelNumberDraw", value); }
    }


    public static int LevelNumberMatch
    {
        get { return PlayerPrefs.GetInt("LevelNumberMatch"); }
        set { PlayerPrefs.SetInt("LevelNumberMatch", value); }
    }
    public static int TotalWins
    {
        get { return PlayerPrefs.GetInt("TotalWins"); }
        set { PlayerPrefs.SetInt("TotalWins", value); }
    }

    public static int TotalGames
    {
        get { return PlayerPrefs.GetInt("TotalGames"); }
        set { PlayerPrefs.SetInt("TotalGames", value); }
    }


    public static int tutorialPrefs
    {
        get { return PlayerPrefs.GetInt("tutorialPrefs"); }
        set { PlayerPrefs.SetInt("tutorialPrefs", value); }
    }

    public static int rateUsPrefs
    {
        get { return PlayerPrefs.GetInt("rateUsPrefs"); }
        set { PlayerPrefs.SetInt("rateUsPrefs", value); }
    }

    public static int privacyOpener
    {
        get { return PlayerPrefs.GetInt("privacyOpener"); }
        set { PlayerPrefs.SetInt("privacyOpener", value); }
    }


    public static int PencilChoose
    {
        get { return PlayerPrefs.GetInt("PencilChoose"); }
        set { PlayerPrefs.SetInt("PencilChoose", value); }
    }


    public static int favCount
    {
        get { return PlayerPrefs.GetInt("favCount"); }
        set { PlayerPrefs.SetInt("favCount", value); }
    }

    public static int PlayerSkin
    {
        get { return PlayerPrefs.GetInt("PlayerSkin"); }
        set { PlayerPrefs.SetInt("PlayerSkin", value); }
    }



    public static int AdonMatch
    {
        get { return PlayerPrefs.GetInt("AdonMatch"); }
        set { PlayerPrefs.SetInt("AdonMatch", value); }
    }


    public static int AdonMerge
    {
        get { return PlayerPrefs.GetInt("AdonMerge"); }
        set { PlayerPrefs.SetInt("AdonMerge", value); }
    }
}
