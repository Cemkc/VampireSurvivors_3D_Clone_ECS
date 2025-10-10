using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLogic : MonoBehaviour, IGameRunning
{
    [SerializeField] private CharacterStats _characterStats;
    private float _moveSpeed;
    private Vector2 _moveVector;

    public Action<int> OnDamageTaken;

    void Awake()
    {
        AssignStats(_characterStats);
    }

    private void OnEnable()
    {
        if(PlayerInput.Instance)
            PlayerInput.Instance.InputActions.Player.Move.SubscribeAll(MoveInputCallback);
    }

    private void Start()
    {
        PlayerInput.Instance.InputActions.Player.Move.SubscribeAll(MoveInputCallback);
    }

    void Update()
    {
        transform.position += new Vector3(_moveVector.x, 0.0f, _moveVector.y) * _moveSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.InputActions.Player.Move.UnsubscribeAll(MoveInputCallback);
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

    private void AssignStats(CharacterStats characterStats)
    {
        _moveSpeed = characterStats.MoveSpeed;
    }

    public CharacterStats GetStartStats()
    {
        return _characterStats;
    }

    public DamageableType GetDamageableType()
    {
        return DamageableType.Character;
    }
}
