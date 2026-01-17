using System;
using Unity.Entities;

[Serializable]
public struct VideoSettings : IComponentData 
{
    public bool _fullScreen;
}

[Serializable]
public struct InputSettings : IComponentData 
{
    public bool _useGamepad;
}

[Serializable]
public struct GameplaySettings : IComponentData 
{
    public int _languageIndex;
}