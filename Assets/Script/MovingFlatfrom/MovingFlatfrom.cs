using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatfrom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] int currentWaypoint = 1;
    [SerializeField] float speed;
    void Move()
    {
        if (Vector2.Distance(transform.position, wayPoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint > wayPoints.Count - 1)
            {
                currentWaypoint = 0;
            }
            // transform.localScale = new Vector2(transform.localScale.x > 0 ? -1 : 1, transform.localScale.y);
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypoint].position, Time.deltaTime * speed);
    }
    void Start()
    {
        Transform wayPointsOjb = transform.parent.Find("WaypointHolder");
        foreach (Transform item in wayPointsOjb)
        {
            wayPoints.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("CC");
            other.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
         

            other.gameObject.transform.SetParent(null);
        }
    }
}
