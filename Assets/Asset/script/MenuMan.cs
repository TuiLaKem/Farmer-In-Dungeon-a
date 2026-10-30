using UnityEngine;

public class MenuMan : MonoBehaviour
{

    public GameObject opMenu;
    public GameObject volObj;

    public Camera camera;
    public Sprite volOn;
    public Sprite volOff;
    public AudioSource volSource;
    public UnityEngine.UI.Image currFps;

    private void Start()
    {
        volSource = gameObject.GetComponent<AudioSource>();
    }
    public void showOption() 
    {
        opMenu.gameObject.SetActive(true);
    }

    public void back() 
    {
        opMenu.gameObject.SetActive(false);
    }

    
    public void volControl(int i) 
    {
        if (i == 0) 
        {
            for (int j = volObj.transform.childCount-1; j>=0;j--) 
            {
                string a = "Vol" + j.ToString();
                Transform curr = volObj.transform.GetChild(j);
                if (!(curr.name == a))
                    continue;

                //latest on

                if (curr.gameObject.GetComponent<UnityEngine.UI.Image>().sprite == volOn) 
                {
                    curr.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = volOff;
                    break;
                }
                
            }
            volSource.volume -= 0.15f;

        }
        else
        {
            for (int j =  0; j < volObj.transform.childCount; j++)
            {
                string a = "Vol" + j.ToString();
                Transform curr = volObj.transform.GetChild(j);
                if (!(curr.name == a))
                    continue;

                //latest on

                if (curr.gameObject.GetComponent<UnityEngine.UI.Image>().sprite == volOff)
                {
                    curr.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = volOn;
                    break;
                }
            }
            volSource.volume += 0.15f;
        }
    }


    public void zoomControl(float i) 
    {
        switch (i) 
        {
            case 1f:
                camera.orthographicSize = 7.64f;
                break;
            case 3f:
                camera.orthographicSize = 3.64f;
                break;
            case 2f:
                camera.orthographicSize = 4.64f;
                break;
            case 0.5f:
                camera.orthographicSize = 10.64f;
                break;
            default:
                break;
        }
    }

    public void frameControl(int i) 
    {
        
        switch (i) 
        {
            case 30:
                Application.targetFrameRate = 30;
                break;
            case 60:
                Application.targetFrameRate = 60;
                break;
            case 120:
                Application.targetFrameRate = 120;
                break;
            default:
                break;

                
        }

        
    }

    
}
