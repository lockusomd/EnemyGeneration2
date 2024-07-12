using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy _prefab;

    private ObjectPool<Enemy> _pool;

    private int _defaultCapacity = 5;
    private int _maxSize = 10;
    private int _repeatRate = 2;

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

        enemy.transform.position = GetPosition();
        enemy.GetComponent<Mover>().SetDirection(GetDirection().normalized);
        enemy.gameObject.SetActive(true);
    }

    private Vector3 GetPosition()
    {
        Vector3 position = new Vector3(GetSpawnPoint().x, GetSpawnPoint().y, GetSpawnPoint().z);

        return position;
    }

    private Vector3 GetSpawnPoint()
    {
        Vector3[] spawnPoints =
        {
            new Vector3(2,0,-2),
            new Vector3(2,0,2),
            new Vector3(-2,0,2),
            new Vector3(-2,0,-2)
        };

        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private Vector3 GetDirection()
    {
        int numberX;
        int numberZ;

        do
        {
            numberX = Random.Range(-1, 1);
            numberZ = Random.Range(-1, 1);
        } while (numberX == 0 && numberZ == 0);

        return new Vector3(numberX, 0, numberZ);
    }

    private Quaternion GetRotation()
    {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
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
