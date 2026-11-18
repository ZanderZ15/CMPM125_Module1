using UnityEngine;

public class BeautifulCorner : MonoBehaviour
{
    Vector3 beautyCornerCoords;

    void Start()
    {
        beautyCornerCoords = new Vector3(110f, .5f, 9f);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            other.transform.position = beautyCornerCoords;
        }
    }


}
