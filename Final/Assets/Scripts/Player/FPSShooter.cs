using UnityEngine;
using UnityEngine.VFX;

public class FPSShooter : MonoBehaviour
{
    public Camera cam;
    public VisualEffect flamethrower;
    public GameObject projectile;
    public Transform LHFirePoint, RHFirePoint;
    public float projectileSpeed = 30;
    public float fireRate = 4;

    
    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        flamethrower.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire) {
            timeToFire = Time.time + 1f / fireRate;
            ShootProjectile();
        }
        
        if(Input.GetButtonDown("Fire2"))
        {
            flamethrower.Play();
        }

        if (Input.GetButtonUp("Fire2")) {
            flamethrower.Stop();
        }
    }
    void ShootProjectile() {
       Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
       RaycastHit hit;
       
       if (Physics.Raycast(ray, out hit)) 
           destination = hit.point;
       else 
           destination = ray.GetPoint(1000);

       if (leftHand) {
           leftHand = false;
           InstantiateProjectile(LHFirePoint);
       }
       else {
           leftHand = true;
           InstantiateProjectile(RHFirePoint);
       }
    }

    void InstantiateProjectile(Transform firePoint) {
        GameObject projectileObj = Instantiate (projectile, firePoint.position, Quaternion.identity);
      projectileObj.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);
    }
}
