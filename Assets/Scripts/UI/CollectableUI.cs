using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectableUI : MonoBehaviour
{
    [Header("Scene Reference UI")]
    public Image collectableImage;
    public TextMeshProUGUI collectionText;

    [Header("Collectables Information")]
    public Sprite[] collectionSprites;
    [TextArea(3, 10)]
    public string[] collectionTextArea;


    public void UpdateCollectableUI(int collectableIndex)
    {
        if (collectableIndex >= 0 && collectableIndex < collectionSprites.Length)
        {
            collectableImage.sprite = collectionSprites[collectableIndex];
            collectionText.text = collectionTextArea[collectableIndex]; 
        }
    }
}
