using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using TMPro;

public class ControladorDosJugadores : MonoBehaviour
{
    public GameObject MenuGanador;
    public GameObject Empate;
    private int _score = 0;
    private CartaMemoramaDos _primerSeleccion;
    private CartaMemoramaDos _segundaSeleccion;
    Dictionary<int, int> pares;
    public GameObject GanadorUno;
    public GameObject GanadorDos;
    private int paresEncontrados;
    private int paresTotales;
    private int errores = 0;
    public GameObject Correcto;
    public GameObject Incorrecto;
    public GameObject Turnodos;
    [SerializeField] GameObject interfazResultados;
    [SerializeField] TablaResultados tablaResultados;
    [SerializeField] TextMeshProUGUI nombreUsuario;

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
    public int jugador = 1;

    // Referencias
    [SerializeField] CartaMemoramaDos cartaOriginal;
    [SerializeField] TextMeshProUGUI scoreLabel;
    [SerializeField] TextMeshProUGUI materiaLabel;
    [SerializeField] int idMateria;
    [SerializeField] GameObject grupoCartas;


    // Start is called before the first frame update
    void Start()
    {
        ConfigurarPartida();
        CargarImagenes(filas, columnas);
        CrearCartas();
    }

    void ConfigurarPartida()
    {
        Debug.Log(DataMantainer.Materia);
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
                CartaMemoramaDos nuevaCarta;

                if (i == j && j == 0)
                {
                    nuevaCarta = cartaOriginal;
                    
                }
                else
                {
                    nuevaCarta = Instantiate(cartaOriginal);
                    nuevaCarta.gameObject.tag = "clon";
                }

                // Calcular el indice de la carta
                int indice = j * columnas + i;

                int id = numeros[indice];
                nuevaCarta.EstablecerCarta(id, imagenes[id]);
                nuevaCarta.transform.SetParent(grupoCartas.transform);

                // Calcular y aplicar la posicion de la nueva carta en relacion a la original
                float posX = (offsetX * i) + posicionInicial.x;
                float posY = (offsetY * j) + posicionInicial.y;

                nuevaCarta.transform.position = new Vector3(posX, posY, posicionInicial.z);
            }
        }
    }

    public void destruirclon()
    {
        var clones = GameObject.FindGameObjectsWithTag("clon");
        foreach (var clon in clones)
        {
            Destroy(clon);
        }

        StartCoroutine(mostrarCartaOriginal());
    }

    IEnumerator mostrarCartaOriginal()
    {
        cartaOriginal.reversoDeCarta.SetActive(false);
        yield return new WaitForSeconds(4);
        cartaOriginal.reversoDeCarta.SetActive(true);
    }

    public void CartaSeleccionada(CartaMemoramaDos carta)
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

    IEnumerator mostrarCorrecto()
    {
        Correcto.SetActive(true);
        yield return new WaitForSeconds(1);
        Correcto.SetActive(false);
    }

    IEnumerator mostrarTurnodos()
    {
        Turnodos.SetActive(true);
        yield return new WaitForSeconds(3);
        Turnodos.SetActive(false);
    }

    IEnumerator mostrarIncorrecto()
    {
        Incorrecto.SetActive(true);
        yield return new WaitForSeconds(1);
        Incorrecto.SetActive(false);
    }

    public void mostrarGanador()
    {
        GanadorUno.SetActive(true);
        
        
    }

    public void mostrarGanadorDos()
    {
        GanadorDos.SetActive(true);
        
        
    }

    public void mostrarEmpate()
    {
        Empate.SetActive(true);
    }

    private IEnumerator RevisarPar()
    {
        int totalscore1 = 0;
        int totalscore2 = 0;
        if (pares[_primerSeleccion.id] == _segundaSeleccion.id)
        {
            _score += 100;
            
            StartCoroutine(mostrarCorrecto());
            Debug.Log("Score: " + _score);
            paresEncontrados += 1;
            if (paresEncontrados == paresTotales)
            {
                print("ganaste");
                var idJugador = database.ObtenerJugador(DataMantainer.Nombre);
                //database.RegistrarScore(idJugador, DataMantainer.IdMateria, errores, _score);
                
                jugador ++;
                Debug.Log(jugador);
                //interfazResultados.SetActive(true);
                //var resultados = database.ObtenerPuntajes(DataMantainer.IdMateria);
                //tablaResultados.CargarPuntajes(resultados);
                totalscore1 = _score;
                
                if (jugador==2)
                {

                    StartCoroutine(mostrarTurnodos());
                    _score = 0;
                    destruirclon();
                    ConfigurarPartida();
                    CargarImagenes(filas, columnas);
                    CrearCartas();
                    paresEncontrados = 0;
                    totalscore2 = _score;
                    Debug.Log(jugador);
                    

                    


                }
                if (jugador == 3)
                {
                    MenuGanador.SetActive(true);
                    if (totalscore1 > totalscore2)
                    {
                        mostrarGanador();
                        
                    }
                    else if (totalscore2>totalscore1)
                    {
                        mostrarGanadorDos();
                    }
                    {
                        mostrarEmpate();

                    }
                }
                Debug.Log(jugador);


            }

            
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _primerSeleccion.Esconder();
            _segundaSeleccion.Esconder();
            _score -= 50;
            StartCoroutine(mostrarIncorrecto());
            errores++;
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

    void CargarImagenes(int filas, int columnas)
    {
        int idMateria = DataMantainer.IdMateria;
        paresTotales = (filas * columnas) / 2;

        database = GetComponent<DatabaseConnection>();
        List<string> cartas;

        if (DataMantainer.Materia == "Mixto" || DataMantainer.Materia == "Mixto Complejo")
        {
            cartas = SeleccionAleatoria(paresTotales, database.ObtenerCartas());
            imagenes = EncontrarImagenes(cartas);
        }
        else
        {
            cartas = SeleccionAleatoria(paresTotales, database.ObtenerCartas(DataMantainer.IdTema));
            imagenes = EncontrarImagenes(cartas, idMateria);
        }

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

    Sprite[] EncontrarImagenes(List<string> cartas)
    {
        var imagenes = new List<Sprite>();

        Debug.Log("Archivos a cargar:");


        foreach (var carta in cartas)
        {
            int i = database.ObtenerIdMateria(carta);

            Debug.Log(string.Format("Cartas/{0}/{1}p", i, carta));
            imagenes.Add(Resources.Load<Sprite>(string.Format("Cartas/{0}/{1}p", i, carta)));
            imagenes.Add(Resources.Load<Sprite>(string.Format("Cartas/{0}/{1}r", i, carta)));
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