using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCartas : MonoBehaviour
{
    public GameObject CartaPrefab;
    public int Ancho;
    public int Alto;

    public Material Blanco;
    public Material Negro;
    // Start is called before the first frame update
    void Start()
    {
        Crear();
    }


    public void Crear()
    {
        int cont = 0;
        for (int i = 0; i < Ancho; i++)
        {
            for (int x = 0; x < Alto; x++)
            {
                GameObject cartaTemp = Instantiate(CartaPrefab, new Vector3(x, 0, i), Quaternion.identity);
                if ((i+x)% 2==0)
                {
                    cartaTemp.GetComponent<Carta>().PonerColor(Negro);
                }
                else
                {
                    cartaTemp.GetComponent<Carta>().PonerColor(Blanco);
                }
                cartaTemp.GetComponent<Carta>().NumCarta = cont;
                cont++;
            }
        }
    }

    
}
