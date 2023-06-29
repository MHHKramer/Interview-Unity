using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float stepTime = 0.2f;

    private Vector2 direction = Vector2.right;

    private List<GameObject> body = new List<GameObject>();

    public GameObject bodyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCoroutine());
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
        
        //transform.Translate(direction * speed * Time.deltaTime);
    }

    private void HandleGrowth()
    {
        GameObject newBodyPart = Instantiate(bodyPrefab);
        newBodyPart.transform.position = body[body.Count - 1].transform.position;
        body.Add(newBodyPart);
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y);
            MoveBody();

            yield return new WaitForSeconds(stepTime);
        }
    }

    //TODO Move all segements of body
    private void MoveBody()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On Trigger Enter 2D");
        if (other.name == "Apple")
        {
            HandleGrowth();
        }
    }
}
