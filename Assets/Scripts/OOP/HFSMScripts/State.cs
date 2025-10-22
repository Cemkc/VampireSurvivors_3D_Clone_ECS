using UnityEngine;

namespace OOP.HFSMScripts
{
    public abstract class State 
    {
        private bool _isRootState = true;
        protected IStateMachineRunner _context;
        protected State m_SubState;
        protected State m_SuperState;

        public State(IStateMachineRunner context) => _context = context;
        
        public State(){}

        public State SubState => m_SubState;

        public abstract void EnterState();

        protected abstract void UpdateState();
        
        public abstract void FixedUpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();
        
        public static void UpdateStates(State state){ // This function allows for a chained multi-substate architecture by calling update of every substate of supdates.
            if (state.m_SuperState != null)
            {
                UpdateStates(state.m_SuperState);
            }
            
            state.UpdateState();
        }

        public static void FixedUpdateStates(State state){
            if (state.m_SuperState != null)
            {
                FixedUpdateStates(state.m_SuperState);
            }
            
            state.FixedUpdateState();
        }
        
        public static void EnterStates(State state)
        {
            if (state.m_SuperState != null)
            {
                EnterStates(state.m_SuperState);
            }
            state.EnterState();
        }

        public static void ExitStates(State state){
            if (state.m_SuperState != null)
            {
                ExitStates(state.m_SuperState);
            }
            
            state.ExitState();
        }

        protected void SwitchState(State newState)
        {
            ExitStates(this);
            EnterStates(newState);

            State rootState = newState;

            while (rootState.m_SuperState != null)
            {
                rootState = rootState.m_SuperState;
            }
            
            _context.SetRunnerState(rootState);
        }

        protected void SetSuperState(State superState){
            _isRootState = false;
            m_SuperState = superState;
            superState.m_SubState = this;
        }
    }
}