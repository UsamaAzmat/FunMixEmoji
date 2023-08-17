using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class SpriteRendererLayout : MonoBehaviour
{
    //public float spacing = 1.0f;
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [Button]
    private void AdjustSprites()
    {
        spriteRenderers.Clear();
        SpriteRenderer[] sps = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sps.Length; i++)
        {
            spriteRenderers.Add(sps[i]);
        }
        //float xOffset = 0.0f;
        //for (int i = 0; i < spriteRenderers.Count; i++)
        //{
        //    Vector3 newPosition = transform.position + new Vector3(0.0f, xOffset, 0.0f);
        //    spriteRenderers[i].transform.position = newPosition;

        //    xOffset += spriteRenderers[i].bounds.size.x + spacing;
        //}


    }


    public string spriteFolder = "Sprites"; // Folder path within Resources folder
    public string spriteSuffix = "b"; // Suffix to match

    public List<Sprite> filteredSprites = new List<Sprite>();


    [Button]
    private void LoadSpritesWithSuffix()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(spriteFolder);

        foreach (Sprite sprite in allSprites)
        {
            if (sprite.name.EndsWith(spriteSuffix))
            {
                filteredSprites.Add(sprite);
            }
        }
    }
}
