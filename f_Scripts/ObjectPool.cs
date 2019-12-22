using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool :MonoBehaviour
{

    public string tagToFind = "Enemy";
    private List<GameObject> freePool = null;
    private List<GameObject> usedPool = null;

    private Vector3 objectHoldpos;
    private IEnumerator coroutine;

    public void Init()
    {
        freePool = new List<GameObject>();
        usedPool = new List<GameObject>();
        objectHoldpos = Vector3.down * int.MaxValue;
        foreach (var obj in GameObject.FindGameObjectsWithTag(tagToFind))
        {
            AddGameObjectToFreePool(obj);
        }

    }
    void AddGameObjectToUsedPool(GameObject obj)
    {
        if(usedPool==null){
            Debug.LogError("usedPool is not initialized");
            return;
        }
        usedPool.Add(obj);
        obj.SetActive(true);
    }
    void AddGameObjectToFreePool(GameObject obj)
    {
        if(freePool==null){
            Debug.LogError("freePool is not initialized");
            return;
        }
        freePool.Add(obj);
        obj.SetActive(false);
        obj.transform.position = objectHoldpos;
    }
    public GameObject GetGameObject()
    {
        GameObject obj = null;
        if (freePool.Count > 0)
        {
            obj = freePool[0];
            freePool.RemoveAt(0);
            AddGameObjectToUsedPool(obj);
        }else{
            Debug.LogError("freePool is empty");
        }
        return obj;
    }

    public bool DestroyGameObject(GameObject obj , float time=0)
    {
        /*dealy before deleting the object */
        coroutine = Delay(time);
        StartCoroutine(coroutine);

        if (usedPool.Remove(obj))
        {
            AddGameObjectToFreePool(obj);
            return true;
        }
        else
        {
            Debug.LogError("No object found in usedPool of name " + obj.name);
        }
        return false;

    }

    public void SetGameObjectTransform(GameObject obj, Vector3 spawnPos ,Quaternion rot , Vector3 scale)
    {
        if (usedPool.Find(x => x == obj))
        {
            obj.transform.position = spawnPos;
            obj.transform.rotation = rot;
            obj.transform.localScale = scale;
        }
        else
        {
            Debug.LogError("No object found in usedPool of name " + obj.name);
        }
    }

    public void OnMouseUpp()
    {
        Debug.Log("Free " + freePool.Count);
        Debug.Log("Free " + usedPool.Count);
        GameObject obj = GetGameObject();
        Debug.Log("Free " + freePool.Count);
        Debug.Log("Free " + usedPool.Count);
        Debug.Log(obj);
        // Debug.Log(DestroyGameObject(obj));
        Debug.Log("Free " + freePool.Count);
        Debug.Log("Free " + usedPool.Count);
    }

    IEnumerator Delay(float delaytime){
        yield return new WaitForSeconds(delaytime);
    }

}
