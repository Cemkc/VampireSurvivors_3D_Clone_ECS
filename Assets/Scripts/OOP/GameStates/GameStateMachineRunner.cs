using System;
using System.Collections.Generic;
using OOP.HFSMScripts;
using UnityEngine;

namespace OOP.GameStates
{
    public class GameStateMachineRunner : MonoBehaviour, IStateMachineRunner
    {
        private GameStateFactory _factory;
        private State _gameState;

        void Awake()
        {
            _factory = new GameStateFactory(this);

            _gameState = _factory.GetGameState(GameStateType.Running);
        }

        private void Start()
        {
            State.EnterStates(_gameState);
        }

        private void Update()
        {
            State.UpdateStates(_gameState);
        }

        private void FixedUpdate()
        {
            State.FixedUpdateStates(_gameState);
        }

        public void SetRunnerState(State state)
        {
            _gameState = state;
        }
    }
}