using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DatoInteresante : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelDato;
    [SerializeField] TextMeshProUGUI labelTema;


    // Start is called before the first frame update
    void Start()
    {
        if (DataMantainer.IdMateria <= 5)
        {
            var info = GetComponent<DatabaseConnection>().ObtenerDato(DataMantainer.IdMateria).Split('\n');

            var tema = info[0].Split(':')[1];
            var dato = info[1];

            labelTema.text = tema;
            labelDato.text = dato;
        }
        else
        {
            int random = (int)Random.Range(1, 6);
            var info = GetComponent<DatabaseConnection>().ObtenerDato(random).Split('\n');

            var tema = info[0].Split(':')[1];
            var dato = info[1];

            labelTema.text = tema;
            labelDato.text = dato;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
