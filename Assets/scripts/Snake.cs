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
    public Vector3 turnPosition;
    private bool upKeyDown;
    private bool downKeyDown;
    private bool rightKeyDown;
    private bool leftKeyDown;

    IEnumerator moveSnake()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            var headPosition = transform.position;
            transform.position += (Vector3) direction;
            var n = Children.Count;
            for (var i = n-1; i > 0; i--)
            {
                Children[i].position = Children[i - 1].position;
            }

            if (Children.Any(x => x.position == transform.position))
            {
                Restart();
            }

            if (n > 0)
            {
                Children[0].position = headPosition;
            }

            if (field.Inside(transform.position) == false)
            {
                Restart();
            }
        }
    }

    private void Restart()
    {
        transform.position = Vector3.zero;
        direction = Vector2.zero;
        turnPosition = new Vector3(1, 1);
        foreach (var child in Children)
        {
            Destroy(child.gameObject);
        }

        Children.Clear();
        Destroy(field.foodInstance.gameObject);
        field.CreateFood(this);
    }
    
    void Start()
    {
        field.CreateFood(this);
        GameAnalyticsSDK.GameAnalytics.Initialize();
        transform.position = Vector3.zero;
        turnPosition = new Vector3(1, 1);
        direction = Vector2.zero;
        StartCoroutine(moveSnake());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != turnPosition)
        {
            upKeyDown |= Input.GetKeyDown(KeyCode.UpArrow);
            downKeyDown |= Input.GetKeyDown(KeyCode.DownArrow);
            leftKeyDown |= Input.GetKeyDown(KeyCode.LeftArrow);
            rightKeyDown |= Input.GetKeyDown(KeyCode.RightArrow);


            if (upKeyDown && direction.y == 0)
            {
                direction = Vector2.up;
                turnPosition = transform.position;
            }

            else if (downKeyDown && direction.y == 0)
            {
                direction = Vector2.down;
                turnPosition = transform.position;
            }

            else if (leftKeyDown && direction.x == 0)
            {
                direction = Vector2.left;
                turnPosition = transform.position;
            }

            else if (rightKeyDown && direction.x == 0)
            {
                direction = Vector2.right;
                turnPosition = transform.position;
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                AddTail();
            }
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
                field.CreateFood(this);
                AddTail();
            }
        }

        upKeyDown = false;
        downKeyDown = false;
        rightKeyDown = false;
        leftKeyDown = false;
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

    public void UpButtonClick()
    {
        upKeyDown = true;
    }

    public void DownButtonClick()
    {
        downKeyDown = true;
    }

    public void LeftButtonClick()
    {
        leftKeyDown = true;
    }

    public void RightButtonClick()
    {
        rightKeyDown = true;
    }
}