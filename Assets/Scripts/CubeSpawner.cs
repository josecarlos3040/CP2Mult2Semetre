using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] GameObject[] cubes;
    [SerializeField] float spawnInterval;

    [SerializeField] float posMinX;
    [SerializeField] float posMaxX;
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawner()
    {
        while(player.isAlive)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomIndex = Random.Range(0, cubes.Length);

            GameObject cube = cubes[randomIndex];

            Vector3 spawnPosition = new Vector3(Random.Range(posMinX, posMaxX), transform.position.y, transform.position.z);

            Instantiate(cube, spawnPosition, Quaternion.identity);
        }


    }
}
