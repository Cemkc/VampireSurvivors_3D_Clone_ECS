using System;
using System.Collections.Generic;
using OOP.HFSMScripts;
using UnityEngine;

namespace OOP.GameStates
{
    public enum GameStateType
    {
        None,
        Running,
        Paused,
        LevelUp,
        GameOver,
    }
    
    public class GameStateFactory
    {
        private Dictionary<GameStateType, State> _gameStates; 
        
        public GameStateFactory(GameStateMachineRunner stateMachineRunner)
        {
            _gameStates = new Dictionary<GameStateType, State>();

            foreach (var type in Enum.GetValues(typeof(GameStateType)))
            {
                switch (type)
                {
                    case GameStateType.Running:
                        _gameStates.Add(GameStateType.Running, new GameRunningState(stateMachineRunner, this));
                        break;
                    case GameStateType.Paused:
                        _gameStates.Add(GameStateType.Paused, new GamePausedState(stateMachineRunner, this));
                        break;
                    case GameStateType.LevelUp:
                        _gameStates.Add(GameStateType.LevelUp, new LevelUpState(stateMachineRunner, this));
                        break;
                    case GameStateType.GameOver:
                        _gameStates.Add(GameStateType.GameOver, new GameOverState(stateMachineRunner, this));
                        break;
                    default:
                        break;
                }
            }
            
        }

        public State GetGameState(GameStateType stateType)
        {
            _gameStates.TryGetValue(stateType, out State gameState);
            if (gameState == null)
            {
                Debug.LogError("Game State is null");
            }
            return gameState;
        }
        
    }
}
