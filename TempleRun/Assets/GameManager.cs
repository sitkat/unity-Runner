using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Platform;
    public float Speed = 0.05f;
    public GameObject[] SegmentsPrefab;

    private int Tick = 0;
    private List<GameObject> Segments = new List<GameObject>();
    private GameObject LastSegment;

    private void Start()
    {
        LastSegment = GameObject.FindGameObjectWithTag("Start");
        Segments.Add(LastSegment);
        for (int i = 0; i < 5; i++) GenerateSegment();
    }

    private void FixedUpdate()
    {
        Platform.transform.Translate(Vector2.down * Speed);

        if (Tick == 150)
        {
            GenerateSegment();
            Tick = 0;
        }
        else Tick++;

        if (Segments.Count > 50)
        {
            Destroy(Segments[0]);
            Segments.RemoveAt(0);
        }
    }

    private void GenerateSegment()
    {
        int index = Random.Range(0, SegmentsPrefab.Length);
        GameObject SelectedSegment = SegmentsPrefab[index];

        foreach(Transform In in SelectedSegment.transform)
        {
            if (In.tag == "In")
            {
                foreach (Transform Out in LastSegment.transform)
                {
                    if (Out.tag == "Out")
                    {
                        GameObject CurrentSegment = Instantiate(SelectedSegment);
                        CurrentSegment.transform.position = Out.position - In.localPosition;
                        CurrentSegment.transform.parent = Platform.transform;
                        Segments.Add(CurrentSegment);
                        LastSegment = CurrentSegment;
                    }
                }
            }
        }
    }
}
