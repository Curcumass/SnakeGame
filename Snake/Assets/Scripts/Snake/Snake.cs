using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SnakeGenerator))]
[RequireComponent(typeof (SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private float _speed;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private SnakeHead _head;

    private SnakeInput _snakeInput;
    private List<Segment> _tail;
    private SnakeGenerator _snakeGenerator;

    public event UnityAction<int> SizeUpdate;

    private void Awake()
    {
        _snakeInput = GetComponent<SnakeInput>();
        _snakeGenerator = GetComponent<SnakeGenerator>();
        _tail = _snakeGenerator.CreateTail(_size);

        SizeUpdate?.Invoke(_tail.Count);
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _snakeInput.GetDirectionToClick(_head.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;

        foreach(var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _lerpSpeed * Time.deltaTime);
            previousPosition = tempPosition;
        }

        _head.Move(nextPosition);
    }
    private void OnEnable()
    {
        _head.BlockCollide += OnBlockColldie;
        _head.BonusCollect += OnBonusCollect;
    }
    private void OnDisable()
    {
        _head.BlockCollide -= OnBlockColldie;
        _head.BonusCollect -= OnBonusCollect;
    }
    private void OnBlockColldie()
    {        
        Segment deletedSegment = _tail[_tail.Count - 1];
        _tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);

        SizeUpdate?.Invoke(_tail.Count);
    }

    private void OnBonusCollect(int bonusSize)
    {
       _tail.AddRange(_snakeGenerator.CreateTail(bonusSize));
        SizeUpdate?.Invoke(_tail.Count);
    }

}
