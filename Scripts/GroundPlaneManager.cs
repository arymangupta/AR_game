using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class GroundPlaneManager : MonoBehaviour
{
    public GameObject Ground;
    public ARPlaneManager PlaneManager;
    public ARRaycastManager RayManager;
    public bool IsPlaneDetected;
    public static GroundPlaneManager GPMInstance;
    void Start()
    {

        if (Ground != null)
            Ground.SetActive(false);
    }
    private void FixedUpdate()
    {
        TouchManager();
    }
  
    public void TouchManager()
    {
        if (Input.touchCount > 0)
        {
            Touch touch0 = Input.GetTouch(0);

            if (touch0.phase == TouchPhase.Began)
            {
                var RayHit = new List<ARRaycastHit>();

                if (RayManager.Raycast(touch0.position, RayHit, TrackableType.Planes))
                {
                    if (Ground != null && IsPlaneDetected == false)
                    {
                        Pose pose = RayHit[0].pose;
                        Ground.transform.position = pose.position;
                        Ground.SetActive(true);
                        IsPlaneDetected = true;
                        PlaneManager.enabled = false;


                        ARPlane[] planes;
                        planes = FindObjectsOfType<ARPlane>();

                        foreach (var plane in planes)
                        {
                            plane.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}



