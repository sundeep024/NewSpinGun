using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    [SerializeField] public GameObject[] _prefabs;
    [SerializeField] private float _minX, _maxX;
    private bool isSpawn = true;
    public GameObject coinHolder;


    public static CollectObject COInstance { get; private set; }
    private void Awake()
    {
        if(COInstance !=null && COInstance != this)
        {
            Destroy(this);
        }
        else
        {
            COInstance = this;
        }
    }
    IEnumerator ReSpawningObjects()
    {
        while (isSpawn)
        {
            //wait 1 second to spawn new Object
            yield return new WaitForSeconds(1.5f);

            //get Random value of X
            float spawnRange = Random.Range(_minX, _maxX);

            //set the Spawn position of X and Y and generate every time new position
            Vector2 SpawnPosition = new Vector2(spawnRange, transform.position.y);

            //generate new Object(Coin, Bullete, MultiBullet) every 1 second at different position with rotation 
            GameObject clone = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], SpawnPosition, Quaternion.identity);

            //set clone parent
            clone.transform.SetParent(coinHolder.transform);

            // after every 4 second Clone object destroyed
            Destroy(clone, 4.0f);
        }
    }
}
