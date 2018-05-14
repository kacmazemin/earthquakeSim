using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddObject : MonoBehaviour {


    public GameObject myMesh;

    GameObject floor;

    float randomX;
    float randomZ;

    public void addMeshToWorld(){

        floor = GameObject.Find("floor");

        //GameObject Renderer a cast edildi. Sınırları bulmak için
        Renderer floorMesh = floor.GetComponent<Renderer>();

        if (floor && myMesh)
        {

         randomX = UnityEngine.Random.Range(floorMesh.bounds.min.x, floorMesh.bounds.max.x);
         randomZ = UnityEngine.Random.Range(floorMesh.bounds.min.z, floorMesh.bounds.max.z);

         Instantiate(myMesh, new Vector3(randomX, 16, randomZ), Quaternion.identity);

        }
    }

}
