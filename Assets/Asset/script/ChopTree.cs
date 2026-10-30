using UnityEngine;

public class ChopTree : MonoBehaviour
{
    int health = 10;
    float currRot = 3;

    public GameObject log;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void takeDmg(int dmg) 
    {
        if (health <= 1) 
        {
            treeDie();
        }
        health -= dmg;
        print(health);
        roCalc();
        gameObject.transform.Rotate(0, 0, currRot);
        
    }

    public void treeDie() 
    {
        gameObject.SetActive(false);
        Instantiate(log, gameObject.transform.position + new Vector3(2,-3,0), Quaternion.identity);
        Instantiate(log, gameObject.transform.position + new Vector3(-2, -4, 0), Quaternion.identity);
        Instantiate(log, gameObject.transform.position + new Vector3(0, -3, 0), Quaternion.identity);
    }

    public void roCalc() 
    {
        if (currRot > 0.5)
        {
            currRot = -1;
        }
        else if (currRot < -0.5)
        {
            currRot = 1;
        }
    }
}
