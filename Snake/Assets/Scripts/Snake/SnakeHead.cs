using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeHead : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public event UnityAction BlockCollide;
    public event UnityAction<int> BonusCollect;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 newposition)
    {
        _rigidbody2D.MovePosition(newposition);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Block>(out Block block))
        {
            BlockCollide?.Invoke();
            block.Fill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Bonus>(out Bonus bonus))
        {
            BonusCollect?.Invoke(bonus.Collect());
        }
    }
}
