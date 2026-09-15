using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponPrefabs;
    private GameObject currentWeapon;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float bulletSpeed = 40f;

    public Transform handR;

    public void ChangeWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weaponPrefabs.Length)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentWeapon = Instantiate(weaponPrefabs[weaponIndex], handR.position, handR.rotation, handR);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeapon.SetActive(true);
        }
    }

    public void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogWarning("Cannot shoot: firePoint is null!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed; 
        }

        Destroy(bullet, 3f);
    }

    void Start()
    {
        ChangeWeapon(0); // Equip weapon at start
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}