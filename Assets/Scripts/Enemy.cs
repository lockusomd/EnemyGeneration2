using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private Target _target;
    private Mover _componentMover;

    private float _speed = 1f;

    public event Action<Enemy> Died;

    private void Start()
    {
        _componentMover = GetComponent<Mover>();

        _componentMover.SetSpeed(_speed);
    }

    private void Update()
    {
        _componentMover.SetTarget((_target.transform.position - transform.position).normalized);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<Limiter>(out Limiter component))
        {
            Died?.Invoke(GetComponent<Enemy>());
        }
    }

    public void SetTarget(Target target)
    {
        _target = target;
    }
}
