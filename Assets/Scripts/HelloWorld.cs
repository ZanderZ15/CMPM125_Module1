using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public int anyNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Debug.Log("Hello I am "+ this.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
