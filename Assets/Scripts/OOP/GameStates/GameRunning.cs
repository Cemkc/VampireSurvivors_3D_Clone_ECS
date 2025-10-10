using OOP.HFSMScripts;
using UnityEngine;

namespace OOP.GameStates
{
    public class GameRunning : GameState
    {
        public GameRunning(GameStateMachineRunner context, GameStateFactory factory) : base(context, factory) { }
        
        public override void EnterState()
        {
            Debug.Log("Game is running!");
            PlayerInput.Instance.InputActions.Player.Enable();

            foreach (var monoBehaviour in GameObject.FindObjectsByType<MonoBehaviour>
                     (FindObjectsInactive.Include, FindObjectsSortMode.InstanceID))
            {
                if (monoBehaviour is IGameRunning)
                {
                    monoBehaviour.gameObject.SetActive(true);
                }
            }
            
        }

        protected override void UpdateState()
        {
            CheckSwitchState();      
        }

        public override void FixedUpdateState()
        {
            // Debug.Log("Running state");
        }

        public override void ExitState()
        {
            PlayerInput.Instance.InputActions.Player.Disable();
            
            foreach (var monoBehaviour in GameObject.FindObjectsByType<MonoBehaviour>
                         (FindObjectsInactive.Include, FindObjectsSortMode.InstanceID))
            {
                if (monoBehaviour is IGameRunning)
                {
                    monoBehaviour.gameObject.SetActive(false);
                }
            }
        }

        public override void CheckSwitchState()
        {
            if (PlayerInput.Instance.InputActions.Player.Pause.WasPerformedThisFrame())
            {
                SwitchState(_factory.GetGameState(GameStateType.Paused));
            }
        }

        public override void InitializeSubState()
        {
            
        }
    }
}