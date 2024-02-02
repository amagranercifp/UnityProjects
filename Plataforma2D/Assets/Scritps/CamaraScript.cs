using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject John;

    // Update is called once per frame
    void Update()
    {
        // Estas lineas son para prevenir un error al morir John
        // ya que hacemos calculos mas abajo sobre el.
        if(John== null){
            return;
        }

        Vector3 position = transform.position;
        position.x = John.transform.position.x;
        transform.position = position;
    }
}
