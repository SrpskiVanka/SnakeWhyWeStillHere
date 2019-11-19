using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    public int height;
    public int width;
    [FormerlySerializedAs("food")] public GameObject foodPrefab;
    public GameObject foodInstance;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
    }


    public bool Inside(Vector3 position)
    {
        if (position.x < -width / 2f)
        {
            return false;
        }

        if (position.x > width / 2f)
        {
            return false;
        }

        if (position.y < -height / 2f)
        {
            return false;
        }

        if (position.y > height / 2f)
        {
            return false;
        }

        return true;
    }

    public void CreateFood()
    {
        foodInstance = Instantiate(foodPrefab, new Vector2(Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2)), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateFood();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}