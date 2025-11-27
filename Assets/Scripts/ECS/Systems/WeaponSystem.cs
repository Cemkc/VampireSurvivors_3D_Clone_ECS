using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[BurstCompile]
internal partial struct WeaponSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<GameRunningTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        WeaponManager weaponManager = SystemAPI.GetSingleton<WeaponManager>();
        PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        DynamicBuffer<GameObjectInfo> objectInfoBuffer = SystemAPI.GetSingletonBuffer<GameObjectInfo>();

        float3 pivot = default;
        bool found = false;
        foreach (var obj in objectInfoBuffer)
        {
            if (obj.ObjectType == GameObjectType.Character1)
            {
                pivot = obj.Position;
                found = true;
                break;
            }
        }
        if (!found) return;

        float deltaTime = SystemAPI.Time.DeltaTime;
        int numberOfWeapons = weaponManager.NumberOfWeapons;
        float radius = weaponManager.Radius;

        new WeaponJob
        {
            Pivot = pivot,
            DeltaTime = deltaTime,
            Radius = radius,
            NumberOfWeapons = numberOfWeapons
        }.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct WeaponJob : IJobEntity
    {
        public float3 Pivot;
        public float DeltaTime;
        public float Radius;
        public int NumberOfWeapons;

        void Execute(ref LocalTransform localTransform, ref Weapon weapon)
        {
            weapon.Angle += weapon.RotateSpeed * DeltaTime;
            float angle = weapon.Angle + ((math.PI2 / NumberOfWeapons) * weapon.Number);

            float3 posOnCircle = new float3(math.sin(angle), 0.0f, math.cos(angle));
            float3 weaponPos = Pivot + posOnCircle * Radius;

            float3 weaponDir = math.normalize(weaponPos - Pivot);

            localTransform.Position = weaponPos;
            localTransform.Rotation = quaternion.LookRotation(weaponDir, new float3(0, 1, 0));
            localTransform.Scale = 1.0f;
        }
    }
}
