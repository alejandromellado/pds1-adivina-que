using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class ToggleScrollView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;
    Toggle toggle;

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
}
