using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLogic : MonoBehaviour, IGameRunning
{
    [SerializeField] private CharacterStats _characterStatsAsset;
    private CharacterStats _characterCharacterStats;
    public CharacterStats CharacterStats => _characterCharacterStats;
    
    private float _moveSpeed;
    private Vector2 _moveVector;

    public Action<int> OnDamageTaken;

    void Awake()
    {
        _characterCharacterStats = Instantiate(_characterStatsAsset);
    }

    private void Start()
    {
        PlayerInput.Instance.InputActions.Player.Move.SubscribeAll(MoveInputCallback);
    }

    void Update()
    {
        transform.position += new Vector3(_moveVector.x, 0.0f, _moveVector.y) * _characterCharacterStats.MoveSpeed * Time.deltaTime;
    }

    public void MoveInputCallback(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            _moveVector = ctx.ReadValue<Vector2>();
        }

        if (ctx.phase == InputActionPhase.Canceled)
        {
            _moveVector = Vector2.zero;
        }
    }

    public DamageableType GetDamageableType()
    {
        return DamageableType.Character;
    }

    public void OnStateEnable()
    {
        if(PlayerInput.Instance)
            PlayerInput.Instance.InputActions.Player.Move.SubscribeAll(MoveInputCallback);

        enabled = true;
    }

    public void OnStateDisable()
    {
        PlayerInput.Instance.InputActions.Player.Move.UnsubscribeAll(MoveInputCallback);

        enabled = false;
    }
}
