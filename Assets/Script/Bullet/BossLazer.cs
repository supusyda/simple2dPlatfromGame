using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int defDistantRay = 100;
    [SerializeField] float damage = 20f;
    [SerializeField] Vector2 knockBack = new Vector2(2, 1);
    [SerializeField] ContactFilter2D contactFilter2D;
    RaycastHit2D[] playerHit = new RaycastHit2D[5];
    bool hasPlayer;




    private void Start()
    {
        transform.SetParent(GameObject.Find("Boss").transform);
    }
    void LaserRay()
    {

        hasPlayer = Physics2D.Raycast(transform.position, transform.right * transform.parent.localScale.x, contactFilter2D, playerHit) > 0;
        // DamageReciever damageReciever = hit.transform.GetComponentInChildren<DamageReciever>();
        // damageReciever.ReduceHp2(damage, knockBack);
        if (hasPlayer)
        {
            // Debug.Log(playerHit);
            DamageReciever damageReciever = playerHit[0].transform.GetComponentInChildren<DamageReciever>();
            Debug.Log(damageReciever);
            damageReciever.ReduceHp2(damage, knockBack);
        }


        // Debug.DrawLine(transform.position, transform.right * defDistantRay);
    }
    void OnDrawGizmosSelected()
    {
        if (transform != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.right);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.right * defDistantRay);
    }

    public void DestroyOjb()
    {
        Destroy(this.gameObject);
    }
}
