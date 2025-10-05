using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public static CharacterInput Instance;
    
    private PlayerInputActions _inputActions; 
    [SerializeField] private CharacterLogic _characterLogic;

    public PlayerInputActions InputActions => _inputActions;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        _inputActions = new PlayerInputActions();
    }

    public void OnEnable()
    {
        _inputActions.Player.Enable();
        
        _inputActions.Player.Move.SubscribeAll(_characterLogic.MoveInputCallback);
    }

    public void OnDisable()
    {
        _inputActions.Player.Move.UnsubscribeAll(_characterLogic.MoveInputCallback);
    }
}

