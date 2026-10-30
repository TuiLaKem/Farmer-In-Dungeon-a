using UnityEngine;

public class HouseTele : MonoBehaviour
{
    public Transform inside;
    public GameObject E;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<PlrControl>().setTeleport(true, inside);
            E.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlrControl>().setTeleport(false, null);
            E.SetActive(false);
        }
    }

    
}
