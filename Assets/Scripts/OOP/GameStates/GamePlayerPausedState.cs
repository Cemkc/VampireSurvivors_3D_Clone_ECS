using UnityEngine;

namespace OOP.GameStates
{
    public class GamePlayerPausedState : GameState
    {
        public GamePlayerPausedState(GameStateMachineRunner context, GameStateFactory factory) : base(context, factory)
        {
        }

        public override void Init()
        {
            SetSuperState(_factory.GetGameState(GameStateType.Paused));
        }

        public override void EnterState()
        {
            EnableMonoBehaviours<IGamePlayerPause>(false);
        }

        protected override void UpdateState()
        {
            Debug.Log("Update paused state");
            CheckSwitchState();
        }

        public override void FixedUpdateState()
        {
            
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchState()
        {
            if (PlayerInput.Instance.InputActions.UI.Unpause.WasPerformedThisFrame())
            {
                Debug.Log("Switch that state please");
                SwitchState(_factory.GetGameState(GameStateType.Running));
            }
        }
    }
}