using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera cam;
    [SerializeField] Transform followTarget;
    Vector2 startingPos;
    float startingZ;
    float clippingPlane => (cam.transform.position.z + (zDistantFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float zDistantFromTarget => transform.position.z - followTarget.transform.position.z;
    float parallaxFactor => Mathf.Abs(zDistantFromTarget) / clippingPlane;
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPos;

    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startingPos + canMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);//add starting z
    }
}
