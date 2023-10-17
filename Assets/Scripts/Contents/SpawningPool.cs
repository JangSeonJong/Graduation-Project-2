using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    [SerializeField]
    int _maxMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 30.0f;

    [SerializeField]
    GameObject world;

    [SerializeField]
    GameObject enemyObj;

    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    public void SpawnMonster()
    {
        Vector3 randPos = Vector3.zero;
        GameObject go;
        int randomMonsterCount = Random.Range(1, _maxMonsterCount);

        while (_monsterCount < randomMonsterCount)
        {
            go = PoolManager.instance.Pop();

            if (go != null)
            {
                go.transform.position = RandomPos(go);
                go.transform.rotation = RandomRotation();
                go.transform.parent = world.transform;
                enemies.Add(go);
                _monsterCount++;
            }
            else
            {
                go = Instantiate(enemyObj, randPos, RandomRotation(), world.transform);
                go.transform.position = RandomPos(go);
                enemies.Add(go);

                _monsterCount++;
            }
        }
    }

    Vector3 RandomPos(GameObject go)
    {
        Vector3 randPos = Vector3.zero;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(5, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            NavMeshAgent nma = go.GetComponent<NavMeshAgent>();
            NavMeshPath path = new NavMeshPath();
            if (path != null)
            {
                if (nma.CalculatePath(randPos, path))
                {
                    break;
                }
            }
        }

        return randPos;
    }

    Quaternion RandomRotation()
    {
        Vector3 random = new Vector3(0, Random.Range(-180, 180), 0);

        return Quaternion.Euler(random);
    }

    public void ClearMonster()
    {
        if (enemies.Count > 0)
        {
            foreach (GameObject go in enemies)
            {
                go.SetActive(false);
                PoolManager.instance.Push(go);
            }
        }
    }
}
