using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBaseBehavior
{
    private SettingsSubsystem _settingsSubsystem;

    public Button _applyBtn;
    public Button _CancelBtn;

    public Toggle _fullScreen;

    protected override void OnAwakeUI()
    {
        _settingsSubsystem = GetSubsystem<SettingsSubsystem>();

        RegistEvent();
    }

    protected override void OnRefreshUI()
    {
        LoadOption();
    }

    private void LoadOption()
    {
        var video = _entityManager.GetComponentData<VideoSettings>(_settingsSubsystem._settingsEntity);
        _fullScreen.isOn = video._fullScreen;
    }

    private void RegistEvent()
    {
        _applyBtn.onClick.AddListener(OnApply);
        _CancelBtn.onClick.AddListener(OnCancel);
        
        _fullScreen.onValueChanged.AddListener((isFullScreen)=>{
            var video = _entityManager.GetComponentData<VideoSettings>(_settingsSubsystem._settingsEntity);
            video._fullScreen = isFullScreen;
            _entityManager.SetComponentData(_settingsSubsystem._settingsEntity, video);
        });
    }
    
    private void OnApply()
    {
        _settingsSubsystem?.CommitAndSave();
    }

    private void OnCancel()
    {
        _settingsSubsystem?.Rollback();
        gameObject.SetActive(false);
    }
}
