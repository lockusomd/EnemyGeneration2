using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Target : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> _checkpoints = new List<Checkpoint>();
    private Checkpoint _checkpoint;
    private Mover _componentMover;

    private int _checkpointIndex = 0;

    private void Start()
    {
        _componentMover = GetComponent<Mover>();

        _checkpoint = _checkpoints[_checkpointIndex];

        SetTargetToMover();
    }

    private void Update()
    {
        if (IsCheckpointReached())
            ChangeDirection();
    }

    private bool IsCheckpointReached()
    {
        float minDistance = 0.5f;

        return Vector3.Distance(_checkpoint.transform.position, transform.position) < minDistance;
    }

    private void ChangeDirection()
    {
        _checkpointIndex = ++_checkpointIndex % _checkpoints.Count;

        _checkpoint = _checkpoints[_checkpointIndex];

        SetTargetToMover();
    }

    private void SetTargetToMover()
    {
        _componentMover.SetTarget((_checkpoint.transform.position - transform.position).normalized);
    }
}
