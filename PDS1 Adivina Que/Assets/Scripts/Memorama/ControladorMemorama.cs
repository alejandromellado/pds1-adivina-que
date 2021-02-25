using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using TMPro;

public class ControladorMemorama : MonoBehaviour
{
    private int _score = 0;
    private CartaMemorama _primerSeleccion;
    private CartaMemorama _segundaSeleccion;
    Dictionary<int, int> pares;
    private int paresEncontrados;

    public bool puedeEscoger
    {
        get { return _segundaSeleccion == null; }
    }

    DatabaseConnection database;
    Sprite[] imagenes;

    // Estas variables determinan la cantidad de cartas y el espacio entre las cartas en el eje X y Y
    public int filas;
    public int columnas;
    public float offsetX = 2.5f;
    public float offsetY = -2.5f;

    // Referencias
    [SerializeField] CartaMemorama cartaOriginal;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI materiaLabel;
    [SerializeField] int idMateria;


    // Start is called before the first frame update
    void Start()
    {
        ConfigurarPartida();
        CargarImagenes(idMateria, filas, columnas);
        CrearCartas();
    }

    void ConfigurarPartida()
    {
        idMateria = DataMantainer.IdMateria;
        materiaLabel.text = DataMantainer.Materia;

        switch (DataMantainer.Dificultad)
        {
            case 1:
                // Colocar carta original en posicion correcta
                cartaOriginal.transform.position = new Vector3(-2.55f, 1f, 0f);
                break;

            case 2:
                filas = 2;
                columnas = 5;

                // Colocar carta original en posicion correcta
                cartaOriginal.transform.position = new Vector3(-5.1f, 1f, 0f);
                cartaOriginal.transform.localScale = new Vector3(.4f, .4f, .4f);

                break;

            case 3:
                filas = 4;
                columnas = 4;

                // Colocar carta original en posicion correcta
                cartaOriginal.transform.position = new Vector3(-3.18f, 2.12f, 0f);
                cartaOriginal.transform.localScale = new Vector3(.35f, .35f, .35f);
                offsetX = 2f;
                offsetY = -2f;

                break;

            default:
                break;
        }
    }

    void CrearCartas()
    {
        // Crear un diccionario que nos permita saber rapidamente la pareja de la carta seleccionada
        CrearDiccionarioDePares(imagenes.Length);

        // Crear las cartas
        Vector3 posicionInicial = cartaOriginal.transform.position;

        // Crear un arreglo con un numero para cada carta.
        int[] numeros = Enumerable.Range(0, imagenes.Length).ToArray();
        numeros = BarajearArreglo(numeros);

        // Crear una columna de cartas por cada fila
        for (int i = 0; i < columnas; i++)
        {
            for (int j = 0; j < filas; j++)
            {
                CartaMemorama nuevaCarta;

                if (i == j && j == 0)
                {
                    nuevaCarta = cartaOriginal;
                }
                else
                {
                    nuevaCarta = Instantiate(cartaOriginal);
                }

                // Calcular el indice de la carta
                int indice = j * columnas + i;

                int id = numeros[indice];
                nuevaCarta.EstablecerCarta(id, imagenes[id]);

                // Calcular y aplicar la posicion de la nueva carta en relacion a la original
                float posX = (offsetX * i) + posicionInicial.x;
                float posY = (offsetY * j) + posicionInicial.y;

                nuevaCarta.transform.position = new Vector3(posX, posY, posicionInicial.z);
            }
        }
    }


    public void CartaSeleccionada(CartaMemorama carta)
    {
        if (_primerSeleccion == null)
        {
            _primerSeleccion = carta;
        }
        else
        {
            _segundaSeleccion = carta;
            StartCoroutine(RevisarPar());
        }
    }

    private IEnumerator RevisarPar()
    {
        if (pares[_primerSeleccion.id] == _segundaSeleccion.id)
        {
            _score+=100;
            Debug.Log("Score: " + _score);
            
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _primerSeleccion.Esconder();
            _segundaSeleccion.Esconder();
            _score -= 50;
        }

        _primerSeleccion = null;
        _segundaSeleccion = null;

        scoreLabel.SetText("Score: " + _score);
    }


    /* Funciones de utilidad */
    int[] BarajearArreglo(int[] arreglo)
    {
        int[] nuevoArreglo = arreglo.Clone() as int[];

        for (int i = 0; i < nuevoArreglo.Length; i++)
        {
            int tmp = nuevoArreglo[i];
            int r = Random.Range(i, nuevoArreglo.Length);
            nuevoArreglo[i] = nuevoArreglo[r];
            nuevoArreglo[r] = tmp;
        }

        return nuevoArreglo;
    }

    void CrearDiccionarioDePares(int cartas)
    {
        pares = new Dictionary<int, int>();

        for (int i = 0; i < imagenes.Length; i += 2)
        {
            pares.Add(i, i + 1);
            pares.Add(i + 1, i);
        }
    }

    /* Funciones para pares de cartas aleatorios */

    void CargarImagenes(int idMateria, int filas, int columnas)
    {
        int num_pares = (filas * columnas) / 2;

        database = GetComponent<DatabaseConnection>();

        List<string> cartas = SeleccionAleatoria(num_pares, database.ObtenerCartas(DataMantainer.IdTema));
        imagenes = EncontrarImagenes(cartas, idMateria);

    }

    Sprite[] EncontrarImagenes(List<string> cartas, int id)
    {
        var imagenes = new List<Sprite>();

        Debug.Log("Archivos a cargar:");

        foreach (var carta in cartas)
        {
            Debug.Log(string.Format("/Cartas/{0}/{1}_x", id, carta));
            imagenes.Add(Resources.Load<Sprite>(string.Format("Cartas/{0}/{1}p", id, carta)));
            imagenes.Add(Resources.Load<Sprite>(string.Format("Cartas/{0}/{1}r", id, carta)));
        }

        return imagenes.ToArray();
    }

    List<string> SeleccionAleatoria(int n, List<string> lista)
    {
        var nuevaLista = new List<string>();

        for (int i = 0; i < n; i++)
        {
            int r = Random.Range(0, lista.Count);
            string x = lista[r];
            nuevaLista.Add(x);
            lista.RemoveAt(r);
        }

        return nuevaLista;
    }
}

