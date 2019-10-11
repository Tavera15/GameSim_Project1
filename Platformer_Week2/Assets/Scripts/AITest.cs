using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    public Transform target;
    public float watchDistance = 2.0f;
    public Animator anim = null;
    public float impulseForce = 30;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.position) < watchDistance)
        {
            var targetLookAtPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetLookAtPosition);

            transform.position = Vector3.Lerp(transform.position, target.position, Speed * Time.deltaTime);

            if (!anim)
            {
                Debug.Log("No Animator");
                return;
            }

            anim.SetFloat("Forward", 1);
            return;
        }

        anim.SetFloat("Forward", 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            float impulseHorizontally = ((other.transform.position.x < transform.position.x) ? -impulseForce : impulseForce);
            float impulseVertically = ((other.transform.position.z < transform.position.z) ? -impulseForce : impulseForce);

            other.GetComponent<Rigidbody>().AddForce(new Vector3(impulseHorizontally, 0, impulseVertically), ForceMode.Impulse);
            GameManager.instance.PlayerTakeDamage(20);
        }        
    }
}
