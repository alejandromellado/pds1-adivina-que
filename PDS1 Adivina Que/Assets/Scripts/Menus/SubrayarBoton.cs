using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;


// Autor: Alejandro Mellado

public class SubrayarBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI label;

    int sizeIncrease = 6;
    
    // Subraya y aumenta el tamaño del texto cuando el cursor entra al boton
    public void OnPointerEnter(PointerEventData eventData)
    {
        label.fontSize += sizeIncrease;
        label.fontStyle = FontStyles.Underline | FontStyles.Bold;
    }

    // Remueve el subrayado y disminuye el tamaño del texto cuando el cursor sale del boton
    public void OnPointerExit(PointerEventData eventData)
    {
        label.fontSize -= sizeIncrease;
        label.fontStyle = FontStyles.Bold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
