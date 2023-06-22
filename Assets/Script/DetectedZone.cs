using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectedZone : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D zoneCollider;
    public UnityEvent noCollider;
    public List<Collider2D> detectedColliderAll = new List<Collider2D>();
    void Start()
    {
        zoneCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground"))
            detectedColliderAll.Add(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        detectedColliderAll.Remove(other);
        noCollider?.Invoke();

    }
}
