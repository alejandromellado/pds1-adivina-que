using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlider : MonoBehaviour
{
    [SerializeField] Sprite[] images;
    [SerializeField] string[] instructions;
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] Image image;

    int currentSlide;
    int totalSlides;

    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 0;
        totalSlides = images.Length;

        SetSlide(currentSlide);
    }

    public void SetSlide(int number)
    {
        label.text = instructions[number];
        image.sprite = images[number];
    }

    public void NextSlide()
    {
        if (currentSlide < totalSlides - 1)
        {
            currentSlide++;
            SetSlide(currentSlide);
        }
    }

    public void PreviousSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            SetSlide(currentSlide);
        }
    }
}
