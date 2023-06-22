using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDetectionZone : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField] protected BoxCollider2D arrowZone;

    void Start()
    {
        arrowZone = transform.GetComponent<BoxCollider2D>();
        animator = GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            Debug.Log(other.transform.name);
            BounceBackArrow(other.transform);
        }
    }
    void BounceBackArrow(Transform arrow)
    {
        arrow.transform.localScale = new Vector3(-arrow.transform.localScale.x, arrow.transform.localScale.y, arrow.transform.localScale.z);
        int EnemyHitBox = LayerMask.NameToLayer("EnemyHitBox");
        arrow.gameObject.layer = EnemyHitBox;
        animator.SetBool(AnimatorString.hasArrowInRange, true);
        // Debug.Log("Current layer: " + gameObject.layer);
    }
}
