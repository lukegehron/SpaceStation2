using UnityEngine;
using SpeckleUnity;
using SpeckleCore; // needed for referencing SpeckleObjects
using System.Collections.Generic;
using Newtonsoft.Json;

public class GetSpeckleProperties : MonoBehaviour
{
    // hold a reference to the manager. Need to be logged before adding receivers
    public SpeckleUnityManager manager;
    //public Material mMat;
    //public Material[] mats = new Material[152];

    // run a method like this after selecting the object you want via raycast or another way
    public void PrintObjectData(GameObject gameObjectKey)
    {
        // use the gameobject as an ID to get back the data associated to it
        if (manager.TryGetSpeckleObject(gameObjectKey, out SpeckleObject data))
        {
            // basic data is stored at the root of the SpeckleObject
            //Debug.Log(data._id);
            // Debug.Log(data.Owner);
            // the type field could give you a bit more insight on the property schema.
            // Debug.Log(data.Type);
            //Debug.Log(data.Properties.ToString());

            // The more interesting data is kept in the Properties dictionary.
            // It's a dictionary of string keys to object values. You have to cast the values
            // into the type you think they are. The schema of this dictionary is basically
            // never consistent. Revit objects are super different amongst themselves let 
            // alone the differences between different clients. A value could even be
            // another dictionary! Your best bet is to just look for the properties you
            // need rather than try to get everything.
            //Dictionary<string, string> openWith = new Dictionary<string, string>();
            if (data.Properties.TryGetValue("elementId", out object propertyUID))
            {
                Debug.Log(propertyUID);
                //Debug.Log("im'in");
                //var json1 = JsonConvert.SerializeObject(propertyUID);
                //Debug.Log(json1);
            }
            if (data.Properties.TryGetValue("parameters", out object propertyValue))
            {
                
                //Debug.Log(propertyValue.ToString());
                var json = JsonConvert.SerializeObject(propertyValue);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                //Debug.Log(json);
                
                if (dictionary.TryGetValue("Comments", out string propertyValue1))
                {
                    Debug.Log(propertyValue1);
                    //Debug.Log("success");
                    Color mColor = new Color();



                    if (ColorUtility.TryParseHtmlString("#" + propertyValue1, out mColor))
                    {
                        
                        //mMat.SetColor("_Color" + propertyValue1.ToString(), mColor);
                        //gameObject.GetComponent<Image>().color = mColor;
                        //Renderer goRenderer = gameObject.GetComponent<Renderer>();
                        //goRenderer.material.SetColor("_Color", mColor);
                        //goRenderer.material = mMat;
                        Debug.Log("success");
                       // goRenderer.material.SetColor("_Color" + propertyValue1.ToString(), mColor);
                    }
                    


                   //Get the Renderer component from the new cube
                   //var cubeRenderer = cube.GetComponent<Renderer>();

                //Call SetColor using the shader property name "_Color" and setting the color to red
                //cubeRenderer.material.SetColor("_Color", Color.red);
                }
            }
            
        }
        else
        {
            Debug.LogError("The GameObect was either null or not a SpeckleObject");
        }
    }

    public void Start()
    {
        PrintObjectData(gameObject);
    }
}