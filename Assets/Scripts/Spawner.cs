using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Target _target;

    private ObjectPool<Enemy> _pool;

    private int _defaultCapacity = 5;
    private int _maxSize = 10;
    private int _repeatRate = 5;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => SetParameters(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Delete(enemy),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        StartCoroutine(GetEnemy());
    }

    private IEnumerator GetEnemy()
    {
        bool isWork = true;

        while (isWork)
        {
            _pool.Get();

            yield return new WaitForSeconds(_repeatRate);
        }
    }

    private void SetParameters(Enemy enemy)
    {
        enemy.Died += SendToPool;

        enemy.transform.position = transform.position;

        enemy.SetTarget(_target);

        enemy.gameObject.SetActive(true);
    }

    private void SendToPool(Enemy enemy)
    {
        enemy.Died -= SendToPool;

        _pool.Release(enemy);
    }

    private void Delete(Enemy enemy)
    {
        enemy.Died -= SendToPool;

        Destroy(enemy);
    }
}
