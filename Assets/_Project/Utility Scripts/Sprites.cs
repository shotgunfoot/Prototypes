using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprites", menuName = "Holder/Sprites")]
public class Sprites : ScriptableObject
{
    [SerializeField]
    public List<Sprite> sprites;

    public Sprite blankSprite;

    public Sprite GetSpriteWithName(string _name)
    {

        Sprite sprite = blankSprite;
        foreach(Sprite spr in sprites)
        {
            if(spr.name == _name)
            {
                sprite = spr;
            }
        }

        return sprite;
    }
}
