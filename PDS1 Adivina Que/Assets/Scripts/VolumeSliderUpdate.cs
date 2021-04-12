using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUpdate : MonoBehaviour
{

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        Debug.Log(DataMantainer.Volumen);
        slider.value = DataMantainer.Volumen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
