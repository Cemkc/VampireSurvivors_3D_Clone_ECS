using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Settings-Configs/Upgrades/WeaponUpgrade")]
public class WeaponUpgrade : CharUpgrade
{
    private EntityManager m_EntityManager;

    public override void Init()
    {
        m_EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }
    
    public override UpgradeTypes GetUpgradeType()
    {
        return UpgradeTypes.Weapon;
    }

    public override void ApplyUpgrade()
    {
        Entity weaponManagerEntity = m_EntityManager.CreateEntityQuery(typeof(WeaponManager)).GetSingletonEntity();
        WeaponManager weaponManager = m_EntityManager.GetComponentData<WeaponManager>(weaponManagerEntity);

        weaponManager.DamagePerHit += 1;
        weaponManager.DamagePerHit = Mathf.Clamp(weaponManager.DamagePerHit, 0, 9);
        
        m_EntityManager.SetComponentData(weaponManagerEntity, weaponManager);
    }
}
