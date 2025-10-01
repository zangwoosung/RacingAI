using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera[] cameras; // Assign your cameras in the Inspector
    int currentIndex = 0;

    List<Camera> cameraList = new List<Camera>();   
    void Start()
    {
        //// Ensure only the first camera is active at start
        //for (int i = 0; i < cameras.Length; i++)
        //{
        //    cameras[i].gameObject.SetActive(i == currentIndex);
        //}
    }
    public void Initialize( )
    {
        //find world first
        
        GameObject world = GameObject.FindWithTag("World");

       
        cameraList = world.GetComponentsInChildren<Camera>().ToList();
        
        for (int i = 0; i < cameraList.Count; i++)
        {
            cameraList[i].gameObject.SetActive(i == currentIndex);
        }
    }   

     void SwitchCamera()
    {

        cameraList[currentIndex].gameObject.SetActive(false);

    
        currentIndex = (currentIndex + 1) % cameraList.Count;


        cameraList[currentIndex].gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // or any key you want
        {
            SwitchCamera();
        }
    }
}
