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
    }

    private void Update()
    {
        _componentMover.SetDirection((_checkpoint.transform.position - transform.position).normalized);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.position == _checkpoint.transform.position)
        {
            _checkpointIndex++;

            if (_checkpointIndex == _checkpoints.Count)
                _checkpointIndex = 0;

            _checkpoint = _checkpoints[_checkpointIndex];
        }
    }
}
