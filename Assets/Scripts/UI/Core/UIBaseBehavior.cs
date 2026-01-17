using UnityEngine;
using Unity.Entities;
using R3;

public abstract class UIBaseBehavior : MonoBehaviour
{
    protected EntityManager _entityManager;
    protected World _defaultWorld;
    
    private bool _isInitialized = false;
    protected CompositeDisposable _disposables = new CompositeDisposable();

    protected virtual void OnEnable()
    {
        if(_isInitialized)
        {
            OnRefreshUI();
        }
    }

    protected virtual void Start()
    {
        if (EnsureInitialized())
        {
            OnRefreshUI();
        }
    }

    protected bool EnsureInitialized()
    {
        if (_isInitialized)
        {
            return true;
        }
        
        _defaultWorld = World.DefaultGameObjectInjectionWorld;
        if (_defaultWorld == null || !_defaultWorld.IsCreated)
        {
            return false;
        }

        _entityManager = _defaultWorld.EntityManager;
        _isInitialized = true;
        
        OnAwakeUI();
        OnRefreshUI();
        
        return true;
    }

    protected T GetSubsystem<T>() where T : SystemBase
    {
        if (!EnsureInitialized()) 
        {
            return null;
        }
        
        return _defaultWorld.GetExistingSystemManaged<T>();
    }
    
    protected abstract void OnRefreshUI();
    protected virtual void OnAwakeUI() { }

    protected virtual void OnDestroy()
    {
        _disposables.Dispose();
        _isInitialized = false;
    }
}
