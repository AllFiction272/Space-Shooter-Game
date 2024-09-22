using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        
        while (_stopSpawning == false)
        {
            //hier wird er wieder gespawnt
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0); //wo er erscheinen muss
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity); //damit er erscheint
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f); // wie lange er braucht um wieder zu erscheinen
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        while (_stopSpawning == false)
        {
            Vector3 powPosToSpawn = new Vector3(Random.Range(-10f, 10f), 7, 0); // wo und welchen zeitraum sie gespawnt werden 
            int randomPowerUp = Random.Range(0, 3); // welche powerups gespawn werden
            Instantiate(_powerUps[randomPowerUp], powPosToSpawn, Quaternion.identity); // so werden die Powerups gespawnt
            yield return new WaitForSeconds(Random.Range(3f, 8f)); // wie lange sie halten

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true; // spiel stopt wenn der spieler tot ist

    }
}
