using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearCartas : MonoBehaviour
{
    public GameObject CartaPrefab;
    public int ancho;
    //public int alto;
    public Transform CartasParent;
    private List<GameObject> cartas = new List<GameObject>();

    
    public Texture2D[] texturas;

    public int contadorClicks;
    public Text textoContadorIntentos;
    public  Carta CartaMostrada;
    public bool sePuedeMostrar= true;

    // Start is called before the first frame update
    void Start()
    {
        Crear();
    }


    public void Crear()
    {
        int cont = 0;
        for (int i = 0; i < ancho; i++)
        {
            for (int x = 0; x < ancho; x++)
            {
                float factor = 9.0f/ancho;
                Vector3 posicionTemp = new Vector3(x*factor, 0, i*factor);
                GameObject cartaTemp = Instantiate(CartaPrefab,posicionTemp,Quaternion.Euler(new Vector3(0,180,0)));
                cartaTemp.transform.localScale *= factor;
                cartas.Add(cartaTemp);

                cartaTemp.GetComponent<Carta>().posicionOriginal = posicionTemp;
                cartaTemp.GetComponent<Carta>().idCarta = cont;
                cartaTemp.transform.parent = CartasParent;

                cont++;
            }
        }
        AsignarTexturas();
        Barajar();
    }

    void AsignarTexturas()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            cartas[i].GetComponent<Carta>().AsignarTextura(texturas[(i)/2]);
        }
    }

    void Barajar()
    {
        
        int aleatorio;

        for (int i = 0; i < cartas.Count; i++)
        {
            aleatorio = Random.Range(i, cartas.Count);

            cartas[i].transform.position = cartas[aleatorio].transform.position;
            cartas[aleatorio].transform.position = cartas[i].GetComponent<Carta>().posicionOriginal;

            cartas[i].GetComponent<Carta>().posicionOriginal = cartas[i].transform.position;
            cartas[aleatorio].GetComponent<Carta>().posicionOriginal = cartas[aleatorio].transform.position;
        
        }
    }

    public void HacerClick(Carta _carta)
    {
        if (CartaMostrada==null)
        {
            CartaMostrada = _carta;
        }
        else
        {
            
            if (CompararCartas(_carta.gameObject, CartaMostrada.gameObject))
            {
                print("Felicidades!");
            }
            else
            {
                _carta.EsconderCarta();
                CartaMostrada.EsconderCarta();
                contadorClicks++;
            }
            CartaMostrada = null;
        }
        
        ActualizarUI();
    }

    public bool CompararCartas(GameObject carta1, GameObject carta2)
    {
        bool resultado;
        if (carta1.GetComponent<MeshRenderer>().material.mainTexture== carta2.GetComponent<MeshRenderer>().material.mainTexture)
        {
            resultado = true;
        }
        else
        {
            resultado = false;
        }
        return resultado;

    }

    public void ActualizarUI()
    {
        textoContadorIntentos.text = "Intentos: " + contadorClicks;
    }
    
}
