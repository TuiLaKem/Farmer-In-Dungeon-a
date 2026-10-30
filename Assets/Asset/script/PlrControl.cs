using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlrControl : MonoBehaviour
{
    [Header("Movement & Player")]
    public InputAction action;
    public GameObject plr;
    private Rigidbody2D rb;
    float speed = 10f;
    Vector2 move;
    bool lookatTree;
    GameObject currTree;
    bool canTele;
    Transform teleTo;
    public PlayableDirector pla;
    GameObject house;
    bool objectInRange;
    bool atShop;
    bool atBed;
    public Transform inside;
    public Transform outside;

    [Header("Raycast")]
    public Transform rayPos;
    public float rayLen = 2.5f;
    public LayerMask treeMask;
    

    [Header("Tiles Set")]
    public Tilemap t_map;
    public Sprite tilled_tiles;
    public Transform tile_off;
    public Tile watered_tiles;
    public Tilemap other_map;
    public Tilemap cropMap;

    public Tile seed;
    


    [Header("Inventory")]
    public Inventory inv;
    public GameObject invDisplay;
    public GameObject holdItem;
    public GameObject highlightInv;
    public int lastInvSize;
    public int lastHeld = 0;
    public GameObject pickup;
    public GameObject topickup;
    [Header("Managers")]
    public GameObject pauseManager;
    public TManage tilesManager;
    public GameObject shopp;



    public Animator animControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 120;
        action.Enable();
        rb = GetComponent<Rigidbody2D>();
        //checkInv();
        lastInvSize = inv.inventory.Count;
        loadInv();
        setHoldItem(0);
        selectInv(0);
        tilesManager.cropTile = cropMap;
        animControl= GetComponent<Animator>();
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(rayPos.position, transform.right * rayLen, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(rayPos.position, transform.right, rayLen, treeMask);

        if (hit.collider != null)
        {
            lookatTree = true;
            currTree = hit.transform.gameObject;
        }
        else 
        {
            lookatTree = false;
        }


        move = action.ReadValue<Vector2>().normalized;

        if (move.y < 0)
        {
            animControl.SetBool("Down", true);
        }

        else if (move.y > 0) 
        {
            animControl.SetBool("Up", true);
        }
        else
        {
            animControl.SetBool("Down", false);
            animControl.SetBool("Up", false);
        }


        if (move.x > 0)
        {
            animControl.SetBool("Right", true);
        }
        else if (move.x < 0)
        {
            animControl.SetBool("Right", true);
        }
        else 
        {
            animControl.SetBool("Right", false);
            
        }
        //rb.linearVelocity = move * speed;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (holding() == "Hoe") { change_tiles(); }
            if (holding() == "Axe" && lookatTree) { currTree.GetComponent<ChopTree>().takeDmg(1); }
            if ((   holding() == "Cabage_seed"
                ||  holding() == "Carrot_seed"
                ||  holding() == "Potato_seed"
                )
                && isTilledTile()) 
            {
                Vector3Int cell = t_map.WorldToCell(tile_off.position);
                cropMap.SetTile(cell, seed);
                tilesManager.addCrop(cell, holding());
                removedItem(lastHeld);
            }

        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {

            tilesManager.printList();

        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            tilesManager.nextDay();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame) 
        {
            removedItem(lastHeld);
        }


        if (objectInRange && Keyboard.current.eKey.wasPressedThisFrame && checkFull())
        {
            for (int i = 0; i < inv.inventory.Count; i++)
            {
                if (inv.inventory[i] == null)
                {
                    inv.inventory[i] = topickup;
                    break;
                }
            }

            setHoldItem(lastHeld);
            
            updateInv();

            topickup.SetActive(false);
        }

        if (canTele && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            pla.Play();
            //
        }

        if (atShop && Keyboard.current.eKey.wasPressedThisFrame)
        {
            shopp.GetComponent<Shop>().openShop();
        }

        if (atBed && Keyboard.current.eKey.wasPressedThisFrame) 
        {
            tilesManager.nextDay();
            pla.Play();
        }

        if (objectInRange)
        {
            pickup.SetActive(true);
        }
        else 
        {
            pickup.SetActive(false);
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectInv(0); setHoldItem(0); lastHeld = 0;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectInv(1); setHoldItem(1); lastHeld = 1;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            selectInv(2); setHoldItem(2); lastHeld = 2;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            selectInv(3); setHoldItem(3);   lastHeld = 3;
        }
        if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            selectInv(4); setHoldItem(4); lastHeld = 4;
        }
        if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            selectInv(5); setHoldItem(5); lastHeld = 5;
        }


        if (checkInv() > lastInvSize)
        {
            lastInvSize = checkInv();
            setHoldItem(lastHeld);
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            pauseManager.GetComponent<PausedMan>().pause();
        }

        //if (Keyboard.current.rKey.wasPressedThisFrame ) 
        //{
        //    string curr = holding();
        //    if (curr == "None")
        //    {
        //        // do nothing
        //    }
        //    else 
        //    {
        //        if (curr == "Hoe") 
        //        {
                
        //        }
        //    }
        //}


        check_flip(move.x);
    }
    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }

    public void updateInv()
    {
        foreach (Transform item in invDisplay.transform)
        {

            UnityEngine.UI.Image img = item.GetComponent<UnityEngine.UI.Image>();
            //print(img.sprite);
            //print(img.name);
            if (img.sprite == null)
            {
                img.sprite = topickup.transform.gameObject.GetComponent<SpriteRenderer>().sprite;
                Color c = topickup.GetComponent<SpriteRenderer>().color;
                c.a = 1f;

                img.color = c;

                img.gameObject.SetActive(true);

                break;
            }

            //print(item.name);
        }
    }

    // Change tiles at location
    void change_tiles()
    {
        Vector3Int cell = t_map.WorldToCell(tile_off.position);
        //Vector3Int cello = other_map.WorldToCell(tile_off.position);
        //t_map.SetTile(cell, rt);

        Debug.Log(t_map.GetSprite(cell).name);
        if (t_map.GetSprite(cell).name == tilled_tiles.name)
        {
            other_map.SetTile(cell, watered_tiles);
        }



        ////self

        ////Debug.Log("Cell1: " + cell + "Cell2: " + cell2);


        //Debug.Log(t_map.GetTile(cell_up));


    }

    // Sprite Directions
    private void check_flip(float x_move)
    {
        if (x_move > 0)
        {
            plr.transform.rotation = Quaternion.identity;
        }
        else if (x_move < 0)
        {
            plr.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Dbug Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }


    // check inventory capacity
    private int checkInv() 
    {
        return inv.inventory.Count;
        
    }

    // print all inventory item
    private void printInv() 
    {
        foreach (GameObject item in inv.inventory) 
        {
            print(item.name);
        }
    }

    // load inventory
    private void loadInv() 
    {
        int co = 0;
        foreach (Transform slot in invDisplay.transform) 
        {

            if (inv.inventory[co] == null)
            {
                //continue;
                co += 1;
            }
            else 
            {
                UnityEngine.UI.Image child = slot.GetComponent<UnityEngine.UI.Image>(); ;
                GameObject currItem = inv.inventory[co];


                //print(currItem);
                print(child.gameObject.name);

                child.sprite = currItem.GetComponent<SpriteRenderer>().sprite;
                child.color = currItem.GetComponent<SpriteRenderer>().color;
                co += 1;
            }

            
        }
    }

    private void selectInv(int i) 
    {
        foreach (Transform item in highlightInv.transform)
        {
            item.gameObject.SetActive(false);
        }

        Transform child = highlightInv.transform.GetChild(i);
        child.gameObject.SetActive(true);


        
    }

    public void setHoldItem(int i) 
    {

        try
        {
            GameObject a = inv.inventory[i];
        }
        catch (ArgumentOutOfRangeException) 
        {
            holdItem.GetComponent<SpriteRenderer>().sprite = null;
            return;
        }

        if (inv.inventory[i] == null) 
        {
            holdItem.GetComponent<SpriteRenderer>().sprite = null;
            return;
        }
        holdItem.GetComponent<SpriteRenderer>().sprite = inv.inventory[i].gameObject.GetComponent<SpriteRenderer>().sprite;
        holdItem.GetComponent<SpriteRenderer>().color = inv.inventory[i].gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void removedItem(int i) 
    {
        inv.inventory[i].gameObject.transform.position = gameObject.transform.position;
        inv.inventory[i].gameObject.transform.rotation = gameObject.transform.rotation;
        inv.inventory[i].SetActive(true);
        inv.inventory[i] = null;
        UnityEngine.UI.Image img = invDisplay.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>();
        img.sprite = null;
        img.gameObject.SetActive(false);
        setHoldItem(i);
        lastInvSize = checkInv();
    }


    private string holding() 
    {
        if (!(inv.inventory[lastHeld].gameObject)) { return "None"; }
        if (inv.inventory[lastHeld].gameObject == null) 
        {
            return "None";
        }
        print(inv.inventory[lastHeld].gameObject.name);
        return inv.inventory[lastHeld].gameObject.name;
    }



    public void activateInrange(bool a, GameObject b) 
    {
        objectInRange = a;
        topickup = b;
    }
    public void setTeleport(bool a, Transform b) 
    {
        canTele = a;
        teleTo = b;
    }
    public void setAtShop(bool a) 
    {
        atShop = a;

    }

    private bool checkFull()
    {
        foreach (GameObject item in inv.inventory)
        {
            if (item == null)
            {
                return true;
            }
        }

        return false;
    }

    public void telePlayer(string a) 
    {
        if (a == "out")
        {
            transform.position = outside.position;
        }
        else 
        {
            transform.position = inside.position;
        }
        
    }

    public void disableControl() 
    {
        action.Disable();
    }
    public void enableControl()
    {
        action.Enable();
    }

    private bool isTilledTile() 
    {
        Vector3Int cell = t_map.WorldToCell(tile_off.position);

        if (other_map.GetSprite(cell))
        {
            if(other_map.GetSprite(cell).name == "watered_dirt_0")
                print(other_map.GetSprite(cell).name);
                return true;
            
        }

        return false;
    }

    public void setAtBed(bool a) { atBed = a; }
}
