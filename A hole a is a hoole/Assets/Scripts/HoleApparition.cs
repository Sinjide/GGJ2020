﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleApparition : MonoBehaviour
{
    public GameObject PrefabHole;
    private Transform rt;
    List<GameObject> Holes = new List<GameObject>();
    private float apparitionTime = 2f;
    private float _timerApparition = 0.0f;
    private static float apparitionReduceValue = 0.3f;
    private float _reduceValueApparition = 60f;
    private float _timerReduceValue = 0.0f;
    float Width = 0f;
    float Height = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Width = 10.0f;
        Height = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timerApparition += Time.deltaTime;
        if (_timerApparition >= apparitionTime)
        {
            _timerApparition = 0f;
            Debug.Log("Create a Hole");
            CreateAHole();
        }

        _timerReduceValue += Time.deltaTime;
        if (_timerReduceValue >= _reduceValueApparition)
        {
            _timerReduceValue = 0f;
            apparitionTime -= apparitionReduceValue;
        }
    }

    public void CreateAHole()
    {
        if (Holes.Count < 10)
        {
            Debug.Log("I create a hole");
            rt = PrefabHole.transform;
            float x = Random.Range(-Width, Width);
            float y = Random.Range(-Height, Height);

            for (int i = 0; i < Holes.Count; i++)
            {
                if ((Mathf.Abs((x - Holes[i].transform.position.x)) < 0.3f)
                    || (Mathf.Abs((y - Holes[i].transform.position.y)) < 0.3f))
                {
                    x = Random.Range(-Width, Width);
                    y = Random.Range(-Height, Height);
                    i = 0;
                }
            }

            Holes.Add(Instantiate(PrefabHole, new Vector3(x, y, 0), Quaternion.identity));
            GetComponent<WaterMove>().waterMoving = true;
            GetComponent<WaterMove>().IncreaseWaterSpeed();
        }
        else if (Holes.Count == 0)
        {
            GetComponent<WaterMove>().waterMoving = false;
        }
    }

    public void DestroyAHole(GameObject Hole)
    {
        if (Holes.Count > 0)
        {
            Holes.Remove(Hole);
            Destroy(Hole);
            GetComponent<WaterMove>().ReduceWaterSpeed();
            GetComponent<ScoringManager>().IncreaseScore(100);
            Debug.Log("Remove a hole !");
        }
    }
}
