using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private Target _target;

    public event Action<Enemy> Died;

    private Mover _componentMover;

    private void Start()
    {
        _componentMover = GetComponent<Mover>();
    }

    private void Update()
    {
        _componentMover.SetDirection((_target.transform.position - transform.position).normalized);
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
