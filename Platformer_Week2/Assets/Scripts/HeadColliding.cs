using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && transform.position.y > other.transform.position.y)
        {
            Destroy(other.gameObject);
            GameManager.instance.IncreaseScore(10);
        }
    }
}
