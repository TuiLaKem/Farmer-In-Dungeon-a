using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject E;

    public GameObject brd;
    //public GameObject bg;

    public Inventory inv;

    public GameObject plr;
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
        plr = collision.gameObject;
        plr.GetComponent<PlrControl>().setAtShop(true);
        E.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        plr.GetComponent<PlrControl>().setAtShop(false);
        closeShop();
        E.SetActive(false);
        plr = null;
    }

    public void openShop()
    {
        brd.SetActive(true);
        //bg.SetActive(true);
    }
    public void closeShop() 
    {
        brd.SetActive(false);
        //bg.SetActive(false);
    }


    public void addItem(GameObject a) 
    {
        
        for (int i = 0; i < inv.inventory.Count; i++)
        {
            if (inv.inventory[i] == null)
            {
                inv.inventory[i] = a;
                break;
            }
        }
        plr.GetComponent<PlrControl>().topickup = a;
        plr.GetComponent<PlrControl>().setHoldItem(plr.GetComponent<PlrControl>().lastHeld);
        plr.GetComponent<PlrControl>().updateInv();
        plr.GetComponent<PlrControl>().topickup = null;
    }
}
