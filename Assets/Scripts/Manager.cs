using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject toSpawn;
    public GameObject spawnPoint;
    public List<GameObject> cameras;
    public KeyCode spawnNuttyKey = KeyCode.R;
    public KeyCode changeCameraKey = KeyCode.T;

    int cameraIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(spawnNuttyKey))
        {
            var nutty = Instantiate(toSpawn);
            nutty.transform.position = spawnPoint.transform.position;
            nutty.transform.rotation = spawnPoint.transform.rotation;
        }

        if (Input.GetKeyDown(changeCameraKey))
        {
            foreach (var camera in cameras)
                camera.SetActive(false);

            cameraIndex++;

            if (cameraIndex == cameras.Count)
                cameraIndex = 0;

            cameras[cameraIndex].SetActive(true);
        }
    }
}
