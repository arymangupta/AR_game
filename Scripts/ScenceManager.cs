using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
    public static int Levles =1;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void LEVEL1()
    {
        Levles = 8;
    }
    public void LEVEL2()
    {
        Levles = 5;
    }
    public void LEVEL3()
    {
        Levles = 2;
    }
    public void GameScence()
    {
        SceneManager.LoadScene(1);
    }
}
