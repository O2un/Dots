
using System;

[Serializable]
public abstract class SaveDataBase
{
    public string _lastSavedTime;
    public int _version;
}

[Serializable]
public class GlobalSettingsData : SaveDataBase
{
    public VideoSettings _video;
    public InputSettings _input;
    public GameplaySettings _gameplay;

    public GlobalSettingsData()
    {
        _lastSavedTime = DateTime.Now.ToString();
        _version = 1;

        _video = new VideoSettings {_fullScreen = true};
        _input = new InputSettings {_useGamepad = false};
        _gameplay = new GameplaySettings {_languageIndex = 0};
    }
}

[Serializable]
public class PlayProgressSaveData : SaveDataBase
{
    public int _slotIndex;
    public int _totalPlayTime;

    public PlayProgressSaveData(int index)
    {
        _slotIndex = index;
        _lastSavedTime = DateTime.Now.ToString();
        _version = 1;
        _totalPlayTime = 0;
    }
}