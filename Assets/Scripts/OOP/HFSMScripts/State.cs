namespace OOP.HFSMScripts
{
    public abstract class State 
    {
        protected bool _isRootState = true;
        protected bool _preserveSubstates = false;
        protected IStateMachineRunner _context;
        protected State _currentSubState;
        protected State _currentSuperState;

        public State(IStateMachineRunner context) => _context = context;
        
        public State(){}

        public State GetCurrentSubState { get { return _currentSubState; } }

        public abstract void EnterState();

        protected abstract void UpdateState();
        
        public abstract void FixedUpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchState();

        public abstract void InitializeSubState();

        public void UpdateStates(){ // This function allows for a chained multi-substate architecture by calling update of every substate of supdates.
            UpdateState();
            if(_currentSubState != null){
                _currentSubState.UpdateStates();
            }
        }

        public void FixedUpdateStates(){
            FixedUpdateState();
            if(_currentSubState != null){
                _currentSubState.FixedUpdateStates();  
            }
        }

        public void ExitStates(){
            if (_preserveSubstates){
                ExitState();
                return;
            }

            State subState = _currentSubState;
            while(subState != null){
                subState.ExitState();
                subState = subState._currentSubState;
            }
            ExitState();
        }

        protected void SwitchState(State newState){
            ExitStates();

            newState.EnterState();
            if (_preserveSubstates){
                newState._currentSubState = _currentSubState;
                newState._currentSubState.SetSuperState(newState);
            }
            else{
                newState.InitializeSubState();
            }

            if(_isRootState){
                _context.SwitchState(newState);
            }
            else if(_currentSuperState != null){
                _currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(State newSuperState){
            _currentSuperState = newSuperState;
        }

        protected void SetSubState(State newSubState){
            _currentSubState = newSubState;
            _currentSubState.SetSuperState(this);
        }
    }
}