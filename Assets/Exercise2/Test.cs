using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject hindenGameObject;
    string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        //HidenGameObject(hindenGameObject);
        //PrintString(cars);
        for (int a = 0; a < 3; a++)
        {
            for (int b = 0; b < 3; b++)
            {
                print(a + "," + b);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void HidenGameObject(GameObject hidenGameobject)
    // {
    //     hidenGameobject.SetActive(false);
    // }

    // public void PrintString(string[] myStrings)
    // {
    //     for (int i = 0; i < myStrings.Length; i++)
    //     {
    //         print(myStrings[i]);
    //     }
    // }
}
