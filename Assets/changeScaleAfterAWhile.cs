using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScaleAfterAWhile : MonoBehaviour
{
    int count = 0;
    bool scaler = true;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scaler) {
            count++;
            

            if (count > 1250)
            {
                Debug.Log(count);

                go.transform.position = new Vector3(-0.46f,-0.07f,-0.5f);
                go.transform.rotation = Quaternion.Euler(0, -90, 0);
                go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                scaler = false;


            }
        }
        
    }
}
