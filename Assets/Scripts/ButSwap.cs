using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButSwap : MonoBehaviour
{


    public Image image;
    public List<Sprite> spriteChoices;
    private int counter;
    private int currentSprite = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ImageChange()
    {
        counter++;
        if (counter == 1)
        {
            currentSprite++;
            counter = 0;
            if (currentSprite >= spriteChoices.Count)
            {
                currentSprite = 0;
            }
            image.sprite = spriteChoices[currentSprite];
        }


    }
}
