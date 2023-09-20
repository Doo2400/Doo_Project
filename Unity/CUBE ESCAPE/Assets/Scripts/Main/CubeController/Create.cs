using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject startCube;
    public GameObject cubePrefab;
    //public GameObject[] spotPoints;

    public int size;

    void Awake()
    {
        /*Vector3 newPos = new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
        startCube.transform.position = newPos * size;*/

        //CubeStartSpot();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    /*void CubeStartSpot()
    {
        int randomIndex = Random.Range(0, spotPoints.Length);
        GameObject randomSpotPoint = spotPoints[randomIndex];

        startCube.transform.position = randomSpotPoint.transform.position;
    }*/
}