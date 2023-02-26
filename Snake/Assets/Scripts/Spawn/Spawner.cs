using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBetweenFullLine;
    [SerializeField] private int _distanceBetweenRandomLine;

    [Header("Block")]
    [SerializeField] private Block _block;
    [SerializeField] private int _blockSpawnChance;

    [Header("Wall")]
    [SerializeField] private Wall _wall;
    [SerializeField] private int _wallSpawnChance;

    private BlockSpawnPoint[] _blockSpawnPoints;
    private WallSpawnPoint[] _wallSpawnPoints;

    private void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenFullLine);
            GenerateRandomLine(_wallSpawnPoints, _wall.gameObject, _wallSpawnChance, _distanceBetweenFullLine, _wall.transform.localScale.y / 2f);
            GenerateFullLine(_blockSpawnPoints, _block.gameObject);
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomLine(_wallSpawnPoints, _wall.gameObject, _wallSpawnChance, _distanceBetweenRandomLine, _wall.transform.localScale.y / 2f);
            GenerateRandomLine(_blockSpawnPoints, _block.gameObject, _blockSpawnChance);
        }
    }
    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedObject)
    {
        for (int i = 0; i <spawnPoints.Length; i++)
        {
            Generate(spawnPoints[i].transform.position, generatedObject);
        }
    }

    private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance, int scaleY = 1, float offsetY = 0)
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            if(Random.Range(0, 100) < spawnChance)
            {
                GameObject element = Generate(spawnPoints[i].transform.position, generatedElement, offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            }
        }
    }

    private GameObject Generate(Vector3 spawnPoint, GameObject genetaredObject, float offsetY = 0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(genetaredObject, spawnPoint, Quaternion.identity, _container);
    }

    private void MoveSpawner(int distance)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
    }

}
