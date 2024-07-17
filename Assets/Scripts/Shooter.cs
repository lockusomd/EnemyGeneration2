using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transformOfTarget;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        bool isWork = true;
        WaitForSeconds wait = new WaitForSeconds(_timeWaitShooting);

        while (isWork)
        {
            var direction = (_transformOfTarget.position - transform.position).normalized;

            var bullet = Instantiate(_prefab, transform.position, Quaternion.LookRotation(direction));

            bullet.GetComponent<Rigidbody>().velocity = direction * _speed;

            yield return wait;
        }
    }
}