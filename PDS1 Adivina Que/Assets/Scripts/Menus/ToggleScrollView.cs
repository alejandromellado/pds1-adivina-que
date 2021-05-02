using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class ToggleScrollView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Image answer;
    [SerializeField] Image question;

    Toggle toggle;

    DatabaseConnection database;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToggleGroup(ToggleGroup group)
    {
        toggle.group = group;
    }

    public void SetText(string text)
    {
        label.text = text;
    }

    public void CartaSeleccionada()
    {
        string carta = label.text;
        title.text = carta;

        database = GetComponent<DatabaseConnection>();
        var info = database.ObtenerCarta(carta);
        var file = info[0];
        var folder = database.ObtenerIdMateria(file);

        var answerRoute = string.Format("Cartas/{0}/{1}r", folder, file);
        var questionRoute = string.Format("Cartas/{0}/{1}p", folder, file);

        answer.sprite = Resources.Load<Sprite>(answerRoute);
        question.sprite = Resources.Load<Sprite>(questionRoute);
    }
}
