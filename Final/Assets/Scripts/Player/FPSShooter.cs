using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.VFX;

public class FPSShooter : MonoBehaviour {
    public Camera cam;
    
    public GameObject fireball;
    public GameObject ice;
    public Transform LHFirePoint, RHFirePoint, IceFirePoint1, IceFirePoint2, IceFirePoint3, IceFirePoint4, IceFirePoint5;
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public string spell;
    static public int spellLevel = 1;
    
    [SerializeField] private Flamethrower Flamethrower;
    

    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        spell = "Fire";
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (spell == "Ice") {
                spell = "Fire";
            } else if (spell == "Fire") {
                spell = "Ice";
            }
            Debug.Log("Key down");
        }
        if (gameObject.GetComponent<FPSController>().mouseOff) {
                if (Input.GetButton("Fire1") && Time.time >= timeToFire) {
                    timeToFire = Time.time + 1f / fireRate;
                    ShootProjectile();
                }

                if (Input.GetButtonDown("Fire2")) {
                  Flamethrower.Shoot();
                }

                if (Input.GetButtonUp("Fire2")) {
                   Flamethrower.StopShooting();
                }
                
        }
    }

    void ShootProjectile() {
       
       if (spell == "Fire") {
       /*    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);
        */
           if (leftHand) {
               leftHand = false;
               InstantiateProjectile(LHFirePoint);
           }
           else {
               leftHand = true;
               InstantiateProjectile(RHFirePoint);
           }
       }

       if (spell == "Ice") { //probably make an array ._.
           ShootIce(IceFirePoint1);
           ShootIce(IceFirePoint2);
           ShootIce(IceFirePoint3);
           ShootIce(IceFirePoint4);
           ShootIce(IceFirePoint5);
       }
    }

    void InstantiateProjectile(Transform firePoint) {
        GameObject projectileObj = Instantiate(fireball, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);
    }

    void ShootIce(Transform firePoint) {
        GameObject projectileObj = Instantiate(ice, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse); 
    }


}
