using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobEntitySpawner : MonoBehaviour, IGameRunning
{
    [Header("Spawn settings")]
    public float baseSpawnInterval = 3f;
    public float spawnRadius = 15f;
    public int mobsPerWave = 3;
    public float spawnHeightOffset = 0f;

    [Header("Difficulty scaling")]
    public float levelMultiplier = 0.15f;
    public int maxMobsPerWave = 30;
    public float minSpawnInterval = 0.3f;

    private EntityManager _entityManager;
    private EntityReferences _entityReferences;

    private Transform _player;
    private float _timer;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var query = _entityManager.CreateEntityQuery(typeof(EntityReferences));
        _entityReferences = _entityManager.GetComponentData<EntityReferences>(query.GetSingletonEntity());

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        int level = GetPlayerLevel();
        float scaledInterval = GetScaledInterval(level);

        _timer += Time.deltaTime;

        if (_timer >= scaledInterval)
        {
            _timer = 0;
            SpawnWave(level);
        }
    }

    void SpawnWave(int level)
    {
        int amount = math.clamp(
            mobsPerWave + Mathf.FloorToInt(level * 0.7f),
            mobsPerWave,
            maxMobsPerWave
        );

        for (int i = 0; i < amount; i++)
        {
            SpawnSingleMob();
        }
    }

    void SpawnSingleMob()
    {
        float angle = UnityEngine.Random.Range(0f, 360f) * math.TORADIANS;
        float3 pos = new float3(
            math.cos(angle) * spawnRadius,
            spawnHeightOffset,
            math.sin(angle) * spawnRadius
        ) + (float3)_player.position;

        Entity mob;
        
        int mobTypeChance = Random.Range(0, 2);
        if (mobTypeChance == 0)
        {
            mob = _entityManager.Instantiate(_entityReferences.MobPrefabEntity);
        }
        else
        {
            mob = _entityManager.Instantiate(_entityReferences.MobOrbitingPrefabEntity);    
        }

        _entityManager.SetComponentData(mob, new LocalTransform
        {
            Position = pos,
            Rotation = quaternion.identity,
            Scale = 1
        });
    }

    float GetScaledInterval(int level)
    {
        float value = baseSpawnInterval / (1f + level * levelMultiplier);
        return math.max(minSpawnInterval, value);
    }

    int GetPlayerLevel()
    {
        // Replace this: connect to your existing level system
        return CharacterXPManager.Instance.CharacterLevel;
    }

    public void OnStateEnable() => enabled = true;
    public void OnStateDisable() => enabled = false;
}
