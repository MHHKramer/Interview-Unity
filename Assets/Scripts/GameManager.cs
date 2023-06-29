using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public Vector2 GridSize { get; set; }
    [field:SerializeField] public Vector2 CenterPosition { get; set; }

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject WallPrefab;
    [SerializeField] private GameObject ApplePrefab;

    
    [SerializeField] private float EnemySpawnRateInSeconds = 10.0f;
    [SerializeField] private Transform WallContainer;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
        SpawnWalls();
        SpawnApple();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWalls()
    {
        for (int x = -1; x <= GridSize.x; x++)
        {
            for (int y = -1; y <= GridSize.y; y++)
            {
                if (x == -1 || y == -1 || x == GridSize.x || y == GridSize.y)
                {
                    GameObject wall = Instantiate(WallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    wall.transform.parent = WallContainer;
                }
            }
        }
    }
    
    private IEnumerator EnemySpawnCoroutine()
    {
        yield return new WaitForSeconds(EnemySpawnRateInSeconds);
        SpawnEnemy();
    }
    
    private void SpawnEnemy()
    {
        int randomPositionX = (int)Random.Range(0, GridSize.x);
        int randomPositionY = (int)Random.Range(0, GridSize.y);

        Vector3 randomPosition = new Vector3(randomPositionX, randomPositionY, 0);
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] snake = GameObject.FindGameObjectsWithTag("Snake");
        
        //TODO Check positon

        
        Instantiate(EnemyPrefab, randomPosition, Quaternion.identity);
        StartCoroutine(EnemySpawnCoroutine());
    }

    public void SpawnApple()
    {
        int randomPositionX = (int)Random.Range(0, GridSize.x);
        int randomPositionY = (int)Random.Range(0, GridSize.y);

        Vector3 randomPosition = new Vector3(randomPositionX, randomPositionY, 0);
        
        Instantiate(ApplePrefab, randomPosition, Quaternion.identity);
    }
}
