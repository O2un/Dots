using Unity.Entities;
using Unity.Burst;

// 1. Engine Subsystem (엔진 가동 시 가장 먼저 실행)
[UpdateInGroup(typeof(InitializationSystemGroup))]
public abstract partial class EngineSubsystemBase : SystemBase { }

// 2. Game Subsystem (일반적인 게임 로직 그룹)
[UpdateInGroup(typeof(SimulationSystemGroup))]
public abstract partial class GameSubsystemBase : SystemBase { }

// 3. Edit Subsystem (에디터 모드 혹은 디버그 시에만 실행)
#if UNITY_EDITOR
[UpdateInGroup(typeof(PresentationSystemGroup))]
public abstract partial class EditSubsystemBase : SystemBase { }
#endif