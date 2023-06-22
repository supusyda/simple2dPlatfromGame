using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;

    public Transform prefabs;
    [SerializeField] List<Transform> bulletPrefabs;
    string BulletPrefabs = "BulletPrefabs";
    public float shootTimer = 0;
    public float shootDelayTime = .5f;
    public string BulletName = "NormalArrow";
    public bool isFiring = false;

    protected virtual void Reset()
    {
        LoadBulletPrefabs();
        // LoadHolder();
        LoadFirePoint();

    }
    void LoadFirePoint()
    {
        firePoint = transform;

    }
    Transform GetBulletPrefabsByName(string bulletName)
    {
        // bulletPrefabs.Find()
        foreach (Transform prefab in bulletPrefabs)
        {
            if (prefab.name == bulletName)
                return prefab;
        }
        return null;
    }
    private void Update()
    {
        shootTimer = shootTimer + Time.deltaTime;
    }
    void LoadBulletPrefabs()
    {
        Transform bullets = transform.Find(BulletPrefabs);
        foreach (Transform bullet in bullets)
        {
            bulletPrefabs.Add(bullet);
        }
        Debug.Log("LoadBulletPrefabs");

    }
    void Start()
    {
        // this.prefabs = this.GetBulletPrefabsByName(BulletName);
        SetPrefab(BulletName);
    }
    public void SetPrefab(string BulletName)
    {
        if (isFiring == false)
        {
            this.BulletName = BulletName;
            this.prefabs = this.GetBulletPrefabsByName(this.BulletName);
        }


    }
    public void ShootOnly()
    {
        if (shootTimer >= shootDelayTime)
        {
            GameObject cc = Instantiate(prefabs.gameObject, firePoint.position, prefabs.transform.rotation);
            cc.transform.localScale = new Vector3(transform.localScale.x > 0 ? 1 : -1, cc.transform.localScale.y, cc.transform.localScale.z);
            cc.SetActive(true);
            shootTimer = 0;
        }

    }
    public void ShootInstant()
    {
        RaycastHit2D info = Physics2D.Raycast(firePoint.position, transform.right);

        if (info)
        {
            DamageReciever damageReciever = info.transform.GetComponentInChildren<DamageReciever>();
            if (damageReciever)
            {
                GameObject cc = Instantiate(prefabs.gameObject, firePoint.position, prefabs.transform.rotation);
            }

        }
    }
}
