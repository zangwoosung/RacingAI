using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras; // Assign your cameras in the Inspector
    private int currentIndex = 0;

    void Start()
    {
        //// Ensure only the first camera is active at start
        //for (int i = 0; i < cameras.Length; i++)
        //{
        //    cameras[i].gameObject.SetActive(i == currentIndex);
        //}
    }
    public void Initialize(Camera[] _cameras )
    {
        this.cameras = _cameras;
        // Ensure only the first camera is active at start
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentIndex);
        }
    }   

    public void SwitchCamera()
    {
       
        cameras[currentIndex].gameObject.SetActive(false);

    
        currentIndex = (currentIndex + 1) % cameras.Length;

       
        cameras[currentIndex].gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // or any key you want
        {
            SwitchCamera();
        }
    }
}
