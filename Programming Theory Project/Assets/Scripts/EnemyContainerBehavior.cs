using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainerBehavior : MonoBehaviour
{
    public enum Direction { Right, Left }
    public Direction MoveDirection { get; set; }
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float speedIncreaseFactor = 1.3f;

    private float _movesPerSecond = 1.0f;

    public float movesPerSecond
    {
        get
        {
            return _movesPerSecond;
        }
        set
        {
            if (value > 0 && value < 5)
            {
                _movesPerSecond = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveDirection = Direction.Right;
        StartCoroutine(MoveEnemyContainer());        
    }

    IEnumerator MoveEnemyContainer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / _movesPerSecond);
            switch (MoveDirection)
            {
                case Direction.Right:
                    transform.Translate(Vector3.right * moveSpeed);
                    break;
                case Direction.Left:
                    transform.Translate(Vector3.left * moveSpeed);
                    break;
            }
        }
    }

    public void SwitchDirection()
    {
        if(MoveDirection == Direction.Left)
        {
            MoveDirection = Direction.Right;
        } else
        {
            MoveDirection = Direction.Left;
        }
    }

    public void MoveDown()
    {
        transform.Translate(Vector3.down * 0.3f);
        _movesPerSecond *= speedIncreaseFactor;
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        foreach(EnemyBehavior enemyBehavior in gameObject.GetComponentsInChildren<EnemyBehavior>())
        {
            enemyBehavior.StopAllCoroutines();
        }
    }
}
