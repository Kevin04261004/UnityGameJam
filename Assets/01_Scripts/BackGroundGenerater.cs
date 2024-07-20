using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackGroundGenerater : MonoBehaviour
{
    [SerializeField] private int bgCount = 10;
    
    [Header("Offset and Intervalse")]
    [SerializeField] private float yOffset = 0;
    [SerializeField] private float xInterval = 2f;
    [SerializeField] private float LastXInterval = 4.1f;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject lastBG;
    
    [Header("Sprites")]
    [SerializeField] private List<Sprite> Base;
    [SerializeField] private List<Sprite> Back;
    [SerializeField] private List<Sprite> Middle;
    [SerializeField] private List<Sprite> Front;
    
    private void Start()
    {
        xInterval *= prefab.transform.localScale.x;
        
        for (int i = 0; i < bgCount; i++)
        {
            GameObject go = GenerateBG();
            go.transform.localPosition = new Vector2(xInterval * i, yOffset);
            go.name = $"BG_{i}";
        }
        Instantiate(lastBG,this.transform).transform.localPosition = new Vector2((bgCount -1 ) * xInterval + (LastXInterval * lastBG.transform.localScale.x), yOffset);

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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color =Color.red;
        Vector2 center = new Vector2( (xInterval * bgCount) + transform.position.x,transform.position.y);
        Gizmos.DrawWireCube( center,new Vector3((xInterval *2 * bgCount),10));
        
    }
}
