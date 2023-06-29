using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float shootingSpeed = 2.0f;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float stepTime = 0.2f;
    [SerializeField] private GameManager gameManager;

    
    private Vector2 direction = Vector2.right;

    private List<GameObject> body = new List<GameObject>();

    public GameObject bodyPrefab;
    public GameObject bulletPrefab;

    private Coroutine shootingCoroutine;
    private Coroutine movementCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        body.Add(gameObject);
        movementCoroutine = StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleShooting();   
        }
        
        //transform.Translate(direction * speed * Time.deltaTime);
    }

    private void HandleGrowth()
    {
        GameObject newBodyPart = Instantiate(bodyPrefab);
        newBodyPart.transform.position = body[body.Count - 1].transform.position;
        body.Add(newBodyPart);
        gameManager.SpawnApple();
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            Vector2 nextPosition = new Vector2(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y);

            foreach (GameObject bodyPart in body)      
            {
                if ((Vector2)bodyPart.transform.position == nextPosition)
                {
                    HandleGameOver(GameOverReason.SelfCollision);
                    yield break;
                }
            }

            if (nextPosition.x < 0 || nextPosition.x >= gameManager.GridSize.x || nextPosition.y < 0 || nextPosition.y >= gameManager.GridSize.y)
            {
                HandleGameOver(GameOverReason.WallCollision);
                yield break;
            }

            transform.position = nextPosition;
            
            MoveBody();

            yield return new WaitForSeconds(stepTime);
        }
    }

    //TODO Move all segements of body
    private void MoveBody()
    {
        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].transform.position = body[i - 1].transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            HandleGrowth();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Enemy")
        {
            HandleGameOver(GameOverReason.EnemyCollision);
            StopCoroutine(movementCoroutine);
        }
    }

    private void HandleGameOver(GameOverReason reason)
    {
        Debug.Log(reason.ToString());
    }

    private void HandleShooting()
    {
        if (shootingCoroutine != null)
        {
            return;
        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

        shootingCoroutine = StartCoroutine(StartShootingCoroutine());
    }

    private IEnumerator StartShootingCoroutine()
    {
        yield return new WaitForSeconds(shootingSpeed);
        shootingCoroutine = null;
    }
}
