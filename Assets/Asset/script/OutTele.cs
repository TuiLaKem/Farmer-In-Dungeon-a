using UnityEngine;
using UnityEngine.Playables;

public class OutTele : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform outside;
    public PlayableDirector dir;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.transform.position = outside.position;
            dir.Play();
        }
    }

    
}
