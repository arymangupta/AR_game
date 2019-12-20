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
    public static Spwaner SpawnerScript;
    [SerializeField]
    GameObject EnimiesPrefab;
    [SerializeField]
    GroundPlaneManager GroundPlanemanager;

    Camera ArCamera;

    public static Vector3 Hitposition;//POSITION OF PLAYER

    float SpawnTimer;
    float SpawnTime;
    bool IsGrounded;
    public float SpawnDistance=6;
    public int n;
    bool Spwan;
    public GameManager gameManager;
    #endregion




    public readonly bool isGrounded;
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void Start()
    {
        ArCamera = Camera.main;

    }

    private void FixedUpdate()
    {

        Debug.Log(IsGrounded);
        Debug.Log(SpawnTimer);
        SpawnEnemies();
    }
    #region Spwaner Function
    public void SpawnEnemies()
    {
        SpawnTime = ScenceManager.Levles;
        if (!GroundPlanemanager.IsPlaneDetected)
        {
            return;
        }


        Vector3 RayOrigin = ArCamera.transform.position;
        Vector3 RayDirection = Vector3.down;

        RaycastHit RayHit;

        if (Physics.Raycast(RayOrigin, RayDirection, out RayHit))
        {
            if (RayHit.transform.tag == "Ground")
            {
                IsGrounded = true;
                Hitposition = RayHit.point;
                RayHit.transform.position = RayHit.point;
            }
            else
            {
                IsGrounded = false;
            }

        }
        if (IsGrounded && !gameManager.IsGameOver)

            SpawnTimer -= Time.deltaTime;

        if (SpawnTimer <= 0)
        {
            Spawning();
            SpawnTimer = 5;
            Spwan = false;
        }
    }
    void Spawning()
    {
        Debug.Log("Spawwned");
        Vector3 CameraForward = ArCamera.transform.position;
        CameraForward.y = 0;
        CameraForward.Normalize();
        Vector3 SpwanPoint = Hitposition + SpawnDistance * CameraForward;
        SpwanPoint = Quaternion.AngleAxis(Random.Range(-45, 45), Vector3.up) * SpwanPoint;
        GameObject EnimeInstance = Instantiate(EnimiesPrefab, SpwanPoint, Quaternion.identity);
    }
    #endregion


}