using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public List<Transform> nodes;
    public int nodeIndex = 0;
    public float speed = 1.5f;
    public float watchDistance = 2.0f;
    public Animator anim = null;
    public Transform target = null;
    public float impulseForce = 30;

    public enum States
    {
        patrol, idle, charge
    }

    public States state = States.idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case States.idle:
                UpdateIdle();
                break;

            case States.patrol:
                UpdatePatrol();
                break;

            case States.charge:
                UpdateCharge();
                break;
        }

        if (!anim)
        {
            Debug.Log("No Animator");
        }
        else
        {
           anim.SetFloat("Forward", 1);
        }


        if (Vector3.Distance(transform.position, target.position) < watchDistance)
        {
            state = States.charge;
        }
        else
        {
            state = States.patrol;
        }
    }

    void UpdateIdle()
    {
        if(Input.GetButtonDown("Jump"))
        {
            state = States.patrol;
        }
    }

    void UpdatePatrol()
    {
        var currentDestination = nodes[nodeIndex];
        var targetLookAtPosition = new Vector3(currentDestination.transform.position.x, transform.position.y, currentDestination.transform.position.z);

        transform.LookAt(targetLookAtPosition);
        transform.position = Vector3.Lerp(transform.position, nodes[nodeIndex].transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nodes[nodeIndex].transform.position) < 2)
        {
            nodeIndex = (nodeIndex + 1 == nodes.Count ? 0 : nodeIndex += 1);
        }
    }

    void UpdateCharge()
    {
        var targetLookAtPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetLookAtPosition);

        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
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
