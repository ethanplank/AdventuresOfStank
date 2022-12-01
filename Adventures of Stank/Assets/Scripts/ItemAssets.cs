using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    //sets Instance to what to what is being currently being used
    private void Awake()
    {
        Instance = this;
    }

    //Sprites for items
    public Sprite swordSprite;
    public Sprite gunSprite;
}
