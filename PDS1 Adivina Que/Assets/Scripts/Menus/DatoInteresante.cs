using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DatoInteresante : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI labelDato;


    // Start is called before the first frame update
    void Start()
    {
        if (DataMantainer.IdMateria < 5)
        {
            labelDato.text = GetComponent<DatabaseConnection>().ObtenerDato(DataMantainer.IdMateria);
        }
        else
        {
            int r = (int)Random.Range(1, 6);
            labelDato.text = GetComponent<DatabaseConnection>().ObtenerDato(r);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
