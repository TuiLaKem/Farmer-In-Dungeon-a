using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera cam;
    public Transform plr;
    Vector3 velo = Vector3.zero;
    public float offset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (plr)
        {
            Vector3 playerPos = new Vector3(plr.position.x, plr.position.y + offset, -10f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, playerPos, ref velo, 0.3f);
        }
    }
}
