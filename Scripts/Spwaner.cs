using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Get the spawn point in the ground (hit the groung down and move to a distance e.g d=6)
If time is elapsed to spawn enemies
Roteate the spawn point t0 -45 to 45
and instantiate the enemy.

optimize:- check if time is elaseped then do raycast.
use object pooling
 */
public class Spwaner : MonoBehaviour
{
    #region Spawner
    [Header("Spawner")]

    /*Static variables */
    public static Spwaner SpawnerScript;
    public static Vector3 hitposition;//POSITION OF PLAYER

    /*public variables */
    public GameObject EnimiesPrefab;
    public GroundPlaneManager GroundPlanemanager;

    public float spawnTime = 5f;
    float spawnTimer;
    bool IsGrounded;
    public float SpawnDistance = 6;
    public readonly bool isGrounded;
    public GameManager gameManager;
    #endregion

    /*private variables */
    Camera ArCamera;
    private ObjectPool objectPool;
    RaycastHit RayHit;
    Vector3 RayOrigin;
    Vector3 RayDirection;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        objectPool = new ObjectPool();
        objectPool.Init();
    }
    private void Start()
    {
        ArCamera = Camera.main;
        spawnTimer = spawnTime;

    }

    private void FixedUpdate()
    {
        Spawner();
    }
    #region Spwaner Function
    public void Spawner()
    {
        if (!GroundPlanemanager.IsPlaneDetected)
        {
            return;
        }

        if (IsGrounded && !gameManager.IsGameOver)
            spawnTimer -= Time.deltaTime;


         RayOrigin = ArCamera.transform.position;
         RayDirection = Vector3.down;

        if (Physics.Raycast(RayOrigin, RayDirection, out RayHit))
        {
            if (RayHit.transform.tag == "Ground")
            {
                IsGrounded = true;
                hitposition = RayHit.point;
                RayHit.transform.position = RayHit.point;
            }
            else
            {
                IsGrounded = false;
            }

        }

        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnTime;
        }
    }
    void SpawnEnemy()
    {
        Debug.Log("Spawwned");
        Vector3 spwanPoint = CreateSpawnPoint(hitposition);
        GameObject enemyInstance = objectPool.GetGameObject();
        objectPool.SetGameObjectTransform(enemyInstance, spwanPoint, Quaternion.identity, Vector3.one);
        /* Make enemy ready for action */
        enemyInstance.GetComponent<Enemy>().InitEmeny(objectPool);

    }

    Vector3 CreateSpawnPoint(Vector3 hit)
    {
        Vector3 CameraForward = ArCamera.transform.position;
        CameraForward.y = 0;
        CameraForward.Normalize();
        Vector3 spwanPoint = hit + SpawnDistance * CameraForward;
        spwanPoint = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up) * spwanPoint;
        return spwanPoint;

    }

    #endregion


}