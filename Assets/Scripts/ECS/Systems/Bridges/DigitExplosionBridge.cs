using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial class DigitExplosionBridge : SystemBase
{
    protected override void OnCreate()
    {
    }

    protected override void OnDestroy()
    {
    }

    protected override void OnUpdate()
    {
        foreach (var (digitExplosionEvent, entity) in SystemAPI.Query<RefRO<DigitExplosionEvent>>().WithEntityAccess())
        {
            var go = GameObject.Instantiate(VFXReferences.Instance.DigitExplosionEffect);
            AudioManager.Instance.Play(SoundLabel.DigitExplosionSound);
            go.transform.position = digitExplosionEvent.ValueRO.Position;
        }
    }
}