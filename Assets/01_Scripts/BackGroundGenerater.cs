using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackGroundGenerater : MonoBehaviour
{
    [SerializeField] private int bgCount = 10;
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float xInterval = 4.1f;
    
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject lastBG;
    
    [SerializeField] private List<Sprite> Base;
    [SerializeField] private List<Sprite> Back;
    [SerializeField] private List<Sprite> Middle;
    [SerializeField] private List<Sprite> Front;
    
    private void Start()
    {
        for (int i = 0; i < bgCount; i++)
        {
            GameObject go = GenerateBG();
            go.transform.position = new Vector2(xInterval * i, yOffset);
            go.name = $"BG_{i}";
        }
        Instantiate(lastBG).transform.position = new Vector2((bgCount -1 ) * xInterval + 9.25f, yOffset);

    }

    public GameObject GenerateBG()
    {
        GameObject go = Instantiate(prefab,this.transform);

        SpriteRenderer[] spriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();

        SpriteRenderer baseSpriteRenderer = spriteRenderers[0];
        SpriteRenderer backSpriteRenderer = spriteRenderers[1];
        SpriteRenderer middleSpriteRenderer = spriteRenderers[2];
        SpriteRenderer frontSpriteRenderer = spriteRenderers[3];
        
        // add base
        baseSpriteRenderer.sprite = Base[Random.Range(0, Base.Count)];
        baseSpriteRenderer.sortingLayerName = "BG";
        baseSpriteRenderer.sortingOrder = 0;
        //add back
        backSpriteRenderer.sprite = Back[Random.Range(0, Back.Count)];
        backSpriteRenderer.sortingLayerName = "BG";
        backSpriteRenderer.sortingOrder = 1;
        // add middle
        middleSpriteRenderer.sprite = Middle[Random.Range(0, Middle.Count)];
        middleSpriteRenderer.sortingLayerName = "BG";
        middleSpriteRenderer.sortingOrder = 2;
        // add front
        frontSpriteRenderer.sprite = Front[Random.Range(0, Front.Count)];
        frontSpriteRenderer.sortingLayerName = "BG";
        frontSpriteRenderer.sortingOrder = 3;

        return go;

    }
    
    
    
}
