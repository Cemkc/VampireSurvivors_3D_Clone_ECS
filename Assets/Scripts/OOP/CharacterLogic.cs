using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLogic : Damageable
{
    [SerializeField] private CharacterStats _characterStats;
    
    private float _moveSpeed;
    private Vector2 _moveVector;

    private void Awake()
    {
        AssignStats(_characterStats);
    }

    public override void TakeDamage(int damageAmount)
    {
        Debug.Log("Character with id; " +  ID + " has taken damage!");
    }

    void Update()
    {
        transform.position += new Vector3(_moveVector.x, 0.0f, _moveVector.y) * _moveSpeed * Time.deltaTime;
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

    public DamageableType GetDamageableType()
    {
        return DamageableType.Character;
    }
}
