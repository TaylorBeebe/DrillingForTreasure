using UnityEngine;

/// <summary>
/// Handles the saving, recalling, and applying of all PlayerPrefs for the application.
/// </summary>
static public class PlayerPrefsHandler
{
    /// <summary>
    /// Storing the PlayerPrefs keys in constants is a good practice!
    /// This saves you from having to make multiple changes in your code should you change the key value,
    /// allows you to make use of Intellisense for typing out the key (intead of mistyping the actual string),
    /// and since it is public and const you can access it anywhere without needing an instance of this class
    /// (i.e. by typing PlayerPrefsHandler.MUTE_INT).
    /// I like to append my PlayerPrefs keys with the type of the pref (i.e. _INT, _STR, _F)
    /// </summary>
    #region PlayerPrefs keys
    public const string USERNAME_STR = "username";
    public const string SFXMUTE_INT = "sfxmute";
    public const string MUSICMUTE_INT = "musicmute";
    public const string FLOOR_INT = "floor";
    public const string MINERALS_INT = "minerals";
    public const string SCRAP_INT = "scrap";
    public const string HIGHSCORE_TIME_STRING = "highscoretime"; //UTC string
    public const string HIGHSCORE_MINERALS_INT = "highscoreminerals";
    #endregion

    private const bool DEBUG_ON = true;

    /// <summary>
    /// This method should call all other methods that will apply saved or default preferences.
    /// We should call this as soon as possible when loading our application.
    /// </summary>
    static public void RestorePreferences()
    {
        SetUsername(GetUsername());
        SetMusicMute(GetMusicMute());
        SetSFXMute(GetSFXMute());
        SetFloor(0);
        SetMinerals(0);
        SetScrap(0);
        SetHighscoreTime(GetHighscoreTime());
        SetHighscoreMinerals(GetHighscoreMinerals());
    }

    static public void SetFloor(int floor)
    {
        PlayerPrefs.SetInt(FLOOR_INT, floor);
    }

    static public int GetFloor()
    {
        return PlayerPrefs.GetInt(FLOOR_INT);
    }

    static public void SetMinerals(int minerals)
    {
        PlayerPrefs.SetInt(MINERALS_INT, minerals);
    }

    static public int GetMinerals()
    {
        return PlayerPrefs.GetInt(MINERALS_INT);
    }

    static public void SetScrap(int scrap)
    {
        PlayerPrefs.SetInt(SCRAP_INT, scrap);
    }

    static public int GetScrap()
    {
        return PlayerPrefs.GetInt(SCRAP_INT);
    }

    static public void SetHighscoreTime(string time)
    {
        PlayerPrefs.SetString(HIGHSCORE_TIME_STRING, time);
    }

    static public string GetHighscoreTime()
    {
        return PlayerPrefs.GetString(HIGHSCORE_TIME_STRING);
    }

    static public void SetHighscoreMinerals(int minerals)
    {
        PlayerPrefs.SetInt(HIGHSCORE_MINERALS_INT, minerals);
    }

    static public int GetHighscoreMinerals()
    {
        return PlayerPrefs.GetInt(HIGHSCORE_MINERALS_INT);
    }

    static public void SetSFXMute(bool isMuted)
    {
        PlayerPrefs.SetInt(SFXMUTE_INT, isMuted ? 1 : 0);
        AudioManager.sfxMute = isMuted;
    }

    static public bool GetSFXMute()
    {
        return PlayerPrefs.GetInt(SFXMUTE_INT, 0) == 1;
    }

    static public void SetMusicMute(bool isMuted)
    {
        PlayerPrefs.SetInt(MUSICMUTE_INT, isMuted ? 1 : 0);
        AudioManager.musicMute = isMuted;
    }

    static public bool GetMusicMute()
    {
        return PlayerPrefs.GetInt(MUSICMUTE_INT, 0) == 1;
    }

    static public void SetUsername(string username)
    {
        PlayerPrefs.SetString(USERNAME_STR, username);
    }

    static public string GetUsername()
    {
        return PlayerPrefs.GetString(USERNAME_STR, "");
    }
}