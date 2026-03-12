using UnityEngine;

public class myball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " 와 충돌함");

        if(collision.gameObject.tag == "ground")
        {
            Debug.Log("땅과 충돌");
        }
    }

     void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리어 안으로 들어옴");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("트리어 밖으로 나감");
    }
}
