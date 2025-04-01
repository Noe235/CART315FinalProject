using System.Runtime.ExceptionServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class FPSShooter : MonoBehaviour {
    public Camera cam;
    
    public GameObject fireball;
    public GameObject ice;
    public Transform LHFirePoint, RHFirePoint, IceFirePoint1, IceFirePoint2, IceFirePoint3, IceFirePoint4, IceFirePoint5; //from left ot right
    public float projectileSpeed = 30;
    public float fireRate = 4;
    static public string spell;
   // static public string [] spells= {"Fire","Ice"}; // proably a better way to make the spell but its too long to change now ._.
    //static int currentSpellIndex = 0;
    
    static public int spellLevelFire = 1;
    static public int spellLevelIce = 1;
    
    [SerializeField] private GameObject playerManager;
    [SerializeField] private GameObject Flamethrower;
    

    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;
   




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        spell = "Fire";
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManager.GetComponent<PlayerManager>().UpdateWand();
        playerManager.GetComponent<PlayerManager>().UpdateSpell();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (spell == "Ice") {
                spell = "Fire";
            } else if (spell == "Fire") {
                spell = "Ice";
            }
            playerManager.GetComponent<PlayerManager>().UpdateWand();
            playerManager.GetComponent<PlayerManager>().UpdateSpell();
            Debug.Log(spell);
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            if (spellLevelFire <3) {
                spellLevelFire++;
            }
            else {
                spellLevelFire = 1;
            }
            Debug.Log(spellLevelFire);
            playerManager.GetComponent<PlayerManager>().UpdateWand();
            
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            if (spellLevelIce <3) {
                spellLevelIce++;
            }
            else {
                spellLevelIce = 1;
            }
            Debug.Log(spellLevelIce);
            playerManager.GetComponent<PlayerManager>().UpdateWand();
        }

        CheckMouse();
        
    

        if (gameObject.GetComponent<FPSController>().mouseOff) {
            if (spellLevelFire == 3 && spell == "Fire") {
                if (Input.GetButtonDown("Fire1")) {
                    Flamethrower.gameObject.SetActive(true);
                }

                if (Input.GetButtonUp("Fire1")) {
                    Flamethrower.gameObject.SetActive(false);
                }
            }
            else {

                if (Input.GetButton("Fire1") && Time.time >= timeToFire) {
                    timeToFire = Time.time + 1f / fireRate;
                    ShootProjectile();
                }
            }


        }
    }

    private void CheckMouse() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // Scroll up
        {
            if (spell == "Fire") {
                spell = "Ice";
            } else {
                spell = "Fire";
            }
            
        }
        else if (scroll < 0f) // Scroll down
        {
            if (spell == "Fire") {
                spell = "Ice";
            }else {
                spell = "Fire";
            }
        }
        playerManager.GetComponent<PlayerManager>().UpdateWand();
        playerManager.GetComponent<PlayerManager>().UpdateSpell();
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
           
           ShootIce(IceFirePoint3);
           if (spellLevelIce >= 2) {
               ShootIce(IceFirePoint2);
               ShootIce(IceFirePoint4);
               if (spellLevelIce >= 3) {
                   ShootIce(IceFirePoint1);
                   ShootIce(IceFirePoint5);
               }
           }
       }
    }

    void InstantiateProjectile(Transform firePoint) {
        GameObject projectileObj = Instantiate(fireball, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);
    }

    void ShootIce(Transform firePoint) {
        GameObject projectileObj = Instantiate(ice, firePoint.position, firePoint.rotation) as GameObject;
        projectileObj.GetComponent<Rigidbody>().AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse); 
    }

   /* public string GetCurrentSpell()
    {
        return spells[currentSpellIndex]; // Get current spell as a string
        */
    }

