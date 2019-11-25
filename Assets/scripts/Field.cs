using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;
    public GameObject right;
    public GameObject left;
    
    public int height;
    public int width ;
    [FormerlySerializedAs("food")] public GameObject foodPrefab;
    public GameObject foodInstance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
    }


    public bool Inside(Vector3 position)
    {
        if (position.x < -width / 2f)
        {
            return false;
        }

        if (position.x > (width / 2f)-1)
        {
            return false;
        }

        if (position.y < -height / 2f)
        {
            return false;
        }

        if (position.y > (height / 2f)-1)
        {
            return false;
        }

        return true;
    }

    public void CreateFood(Snake snake)
    {
        var foodPosition = new Vector3(Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2));
        while (snake.Children.Any(x=>x.position == foodPosition))
        {
            foodPosition = new Vector3(Random.Range(-width / 2, width / 2),
                Random.Range(-height / 2, height / 2));
        }

        foodInstance = Instantiate(foodPrefab, foodPosition, Quaternion.identity);
    }

    public void Start()
    {
        CreateBorders();
    }

    public void CreateBorders()
    {
        width = Mathf.RoundToInt(height * (Screen.width / (float)Screen.height));
        top.transform.position = new Vector3(0, height/2f);
        bottom.transform.position = new Vector3(0, -height/2f);
        right.transform.position = new Vector3(width/2f, 0);
        left.transform.position = new Vector3(-width/2f, 0);
    }
}