using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGenerator : MonoBehaviour
{
    [SerializeField] private Segment _segment;

    public List<Segment> CreateTail(int count)
    {
        List<Segment> tail = new List<Segment>();

        for(int i = 0; i < count; i++)
        {
            tail.Add(Instantiate(_segment, transform));
        }
        return tail;
    }
}
