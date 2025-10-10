using OOP.HFSMScripts;

namespace OOP.GameStates
{
    public abstract class GameState : State
    {
        protected GameStateMachineRunner _context;
        protected GameStateFactory _factory;

        public GameState(GameStateMachineRunner context, GameStateFactory factory) : base(context)
        {
            _context = context;
            _factory = factory;
        }
    }
}