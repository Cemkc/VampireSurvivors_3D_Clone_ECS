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

        private void Update()
        {
            _gameState.UpdateStates();
        }

        private void FixedUpdate()
        {
            _gameState.FixedUpdateStates();
        }

        public void SwitchState(State state)
        {
            _gameState = state;
        }
    }
}