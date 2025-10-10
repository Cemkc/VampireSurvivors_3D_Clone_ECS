using OOP.HFSMScripts;
using Unity.Entities;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace OOP.GameStates
{
    public class GamePaused : GameState
    {
        private SimulationSystemGroup _simulationSystemGroup;

        public GamePaused(GameStateMachineRunner context, GameStateFactory factory) : base(context, factory)
        {
            _simulationSystemGroup = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<SimulationSystemGroup>();   
        }
        
        public override void EnterState()
        {
            Debug.Log("Entered paused state!");
            
            PlayerInput.Instance.InputActions.UI.Enable();
            _simulationSystemGroup.Enabled = false;
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<InitializationSystemGroup>().Enabled = false;
            
            foreach (var monoBehaviour in GameObject.FindObjectsByType<MonoBehaviour>
                         (FindObjectsInactive.Include, FindObjectsSortMode.InstanceID))
            {
                if (monoBehaviour is IGamePaused)
                {
                    monoBehaviour.enabled = true;
                }
            }
        }

        protected override void UpdateState()
        {
            CheckSwitchState();
        }

        public override void FixedUpdateState()
        {
        }

        public override void ExitState()
        {
            // Debug.Log("Exit paused state!");
            PlayerInput.Instance.InputActions.UI.Disable();
            _simulationSystemGroup.Enabled = true;
            World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<InitializationSystemGroup>().Enabled = true;
            
            foreach (var monoBehaviour in GameObject.FindObjectsByType<MonoBehaviour>
                         (FindObjectsInactive.Include, FindObjectsSortMode.InstanceID))
            {
                if (monoBehaviour is IGamePaused)
                {
                    monoBehaviour.enabled = false;
                }
            }
        }

        public override void CheckSwitchState()
        {
            if (PlayerInput.Instance.InputActions.UI.Unpause.WasPerformedThisFrame())
            {
                Debug.Log("Switch that state please");
                SwitchState(_factory.GetGameState(GameStateType.Running));
            }
        }

        public override void InitializeSubState()
        {
            
        }
    }
}