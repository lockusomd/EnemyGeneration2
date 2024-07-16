using UnityEngine;

public class Mover : MonoBehaviour
{
    private float _speed = 5f;
    private Vector3 _target;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_target * _speed * Time.deltaTime);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}