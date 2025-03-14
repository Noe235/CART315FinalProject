using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float followspeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followspeed * Time.deltaTime);
        transform.forward = target.transform.position - transform.position;
    }
}
