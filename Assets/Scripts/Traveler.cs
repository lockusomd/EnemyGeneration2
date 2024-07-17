using UnityEngine;

public class Traveler : MonoBehaviour
{
    [SerializeField] private Transform[] _places;

    private Transform _place;

    private float _speed;
    private int _indexPlace = 0;

    private void Start()
    {
        SetPlace();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _place.position, _speed * Time.deltaTime);

        if (transform.position == _place.position)
            GetNextPlace();
    }

    private void GetNextPlace()
    {
        _indexPlace = ++_indexPlace % _places.Length;

        SetPlace();
    }

    private void SetPlace()
    {
        _place = _places[_indexPlace];
    }
}