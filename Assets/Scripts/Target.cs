using System.Collections.Generic;
using UnityEngine;

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

        _componentMover.SetTarget((_checkpoint.transform.position - transform.position).normalized);
    }

    private void Update()
    {
        if (IsCheckpointReached())
            ChangeDirection();
    }

    private bool IsCheckpointReached()
    {
        float distance = Vector3.Distance(_checkpoint.transform.position, transform.position);

        return distance < 0.5f;
    }

    private void ChangeDirection()
    {
        _checkpointIndex++;

        if (_checkpointIndex == _checkpoints.Count)
            _checkpointIndex = 0;

        _checkpoint = _checkpoints[_checkpointIndex];

        _componentMover.SetTarget((_checkpoint.transform.position - transform.position).normalized);
    }
}
