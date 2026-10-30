using UnityEngine;

public class bed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject E;
    public GameObject plr;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        plr = collision.gameObject;
        plr.GetComponent<PlrControl>().setAtBed(true);
        E.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        plr.GetComponent<PlrControl>().setAtBed(false);
        E.SetActive(false);
        plr = null;
    }
}
