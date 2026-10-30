using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool plrInRange;
    public Inventory inv;

    private GameObject plr;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            
            plr = collision.gameObject;
            plr.GetComponent<PlrControl>().activateInrange(true, gameObject);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plr.GetComponent<PlrControl>().activateInrange(false, null);
            plr = null;
        }
    }

    

    
}
