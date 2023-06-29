using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
/*
 * - Initial length of the snake.
- Increase length per apple consumed.
- Speed of the snake.
- Speed of the bullet.
- Rate of shooting.
- Spawning rate of the enemy.
- Apple target per level.
 */
/*
    [SerializeField] private int initialSnakeLength;
    [SerializeField] private int snakeSizeIncreade;
    [SerializeField] private int snakeSpeed;
    [SerializeField] private int bulletSpeed;
    [SerializeField] private int InitialSnakeLength;
    [SerializeField] private int InitialSnakeLength;
*/
    public int InitialSankeLength { get; private set; }
    public int SnakeSizeIncreade { get; private set; }
    public int SnakeSpeed { get; private set; }
    public int BulletSpeed { get; private set; }
    public int BulletRate { get; private set; }
    public int ShootRate { get; private set; }
    public int AppleGoal { get; private set; }

    [field:SerializeField] public Vector2 GridSize { get; set; }
    [field:SerializeField] public Vector2 CenterPosition { get; set; }

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject WallPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWalls()
    {
        //Instantiate(WallPrefab, new Vector3(centerPosition.x - GridSize, centerPosition.y, 0), Quaternion.identity);
    }
    
    private void SpawnEnemy()
    {
        
    }
}
