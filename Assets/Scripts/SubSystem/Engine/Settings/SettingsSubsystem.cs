using System;
using Unity.Entities;
using UnityEngine;

public partial class SettingsSubsystem : EngineSubsystemBase
{
    private PersistenceSubsystem _persistenceSubsystem;

    public Entity _settingsEntity;
    private static readonly string SETTINGS_FILENAME = "Settings.json";

    protected override void OnCreate()
    {
        _settingsEntity = EntityManager.CreateEntity();
        EntityManager.SetName(_settingsEntity, "Global_Settings_Singleton");
        
        EntityManager.AddComponentData(_settingsEntity, new VideoSettings { });
        EntityManager.AddComponentData(_settingsEntity, new InputSettings {});
        EntityManager.AddComponentData(_settingsEntity, new GameplaySettings{});

        _persistenceSubsystem = World.GetExistingSystemManaged<PersistenceSubsystem>();

        LoadAndApply();
    }

    private void LoadAndApply()
    {
        if(null == _persistenceSubsystem)
        {
            return;
        }
        var loadedData = _persistenceSubsystem.LoadFromFile<GlobalSettingsData>(SETTINGS_FILENAME);

        EntityManager.SetComponentData(_settingsEntity, loadedData._video);
        EntityManager.SetComponentData(_settingsEntity, loadedData._input);
        EntityManager.SetComponentData(_settingsEntity, loadedData._gameplay);
    }

    public void CommitAndSave()
    {
        // 현재 ECS 엔티티의 최신 상태를 긁어모아 객체 생성
        GlobalSettingsData dataToSave = new GlobalSettingsData
        {
            _video = EntityManager.GetComponentData<VideoSettings>(_settingsEntity),
            _input = EntityManager.GetComponentData<InputSettings>(_settingsEntity),
            _gameplay = EntityManager.GetComponentData<GameplaySettings>(_settingsEntity),
            _version = 1,
            _lastSavedTime = System.DateTime.Now.ToString()
        };
        
        _persistenceSubsystem.SaveToFile(SETTINGS_FILENAME, dataToSave);
    }

    public void Rollback()
    {
        LoadAndApply();
    }

    protected override void OnUpdate()
    {
        foreach (var video in SystemAPI.Query<RefRO<VideoSettings>>().WithChangeFilter<VideoSettings>())
        {
            ApplyVideoSettings(video.ValueRO);
        }

        foreach (var input in SystemAPI.Query<RefRO<InputSettings>>().WithChangeFilter<InputSettings>())
        {
            ApplyInputSettings(input.ValueRO);
        }

        foreach (var gamePlay in SystemAPI.Query<RefRO<GameplaySettings>>().WithChangeFilter<GameplaySettings>())
        {
            ApplyGamePlaySettings(gamePlay.ValueRO);
        }
    }
    
    private void ApplyVideoSettings(VideoSettings settings)
    {
        if(Screen.fullScreen != settings._fullScreen)
        {
            Screen.fullScreen = settings._fullScreen;
        }
    }

    private void ApplyInputSettings(InputSettings settings)
    {
        
    }

    private void ApplyGamePlaySettings(GameplaySettings settings)
    {
        
    }
}
