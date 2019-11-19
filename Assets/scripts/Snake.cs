using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Vector2 direction;
    private Coroutine move;
    public List<Transform> Children;
    public GameObject tailPrefab;
    public Field field;
        
        IEnumerator moveSnake()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            var headPosition = transform.position;
            transform.position += (Vector3) direction / 2;
            var n = Children.Count - 1;
            for (var i = n; i > 0; i--)
            {
                Children[i].position = Children[i - 1].position;
            }

            if (n >= 0)
            {
                Children[0].position = headPosition;
            }

            if (field.Inside(transform.position) == false)
            {
                transform.position = Vector3.zero;
                direction = Vector2.zero;
                foreach (var child in Children)
                {
                    Destroy(child.gameObject);
                }
                Children.Clear();
                Destroy(field.foodInstance.gameObject);
                field.CreateFood();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        direction = Vector2.zero;
        StartCoroutine(moveSnake());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction.y == 0)
        {
            direction = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && direction.y == 0)
        {
            direction = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && direction.x == 0)
        {
            direction = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && direction.x == 0)
        {
            direction = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddTail();
        }

        if (field.foodInstance != null)
        {
            int snakeX = (int) gameObject.transform.position.x;
            int snakeY = (int) gameObject.transform.position.y;
            var food = field.foodInstance;
            int foodX = (int) food.transform.position.x;
            int foodY = (int) food.transform.position.y;

            if (snakeX == foodX && snakeY == foodY)
            {
                Destroy(food.gameObject);
                field.CreateFood();
                AddTail();
            }
        }
        }

    private void AddTail()
    {
        var newTail = Instantiate(tailPrefab);
        Vector3 position;
        if (Children.Count == 0)
        {
            position = transform.position;
        }
        else
        {
            position = Children.Last().position;
        }

        newTail.transform.position = position;
        Children.Add(newTail.transform);
        
    }
}