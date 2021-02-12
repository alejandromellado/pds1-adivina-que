using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public Material colorCarta;
    public int NumCarta = 0;
    // Start is called before the first frame update
    public void OnMouseDown()
    {
        print(NumCarta.ToString());
    }

    // Update is called once per frame
    public void PonerColor(Material color_)
    {
        GetComponent<MeshRenderer>().material = color_;
        colorCarta = color_;
    }
}
