using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoofGenerator : MonoBehaviour
{
    
    [SerializeField] private Falling_Object fallingObjectPrefab; // falling object 변경 필요 
    [SerializeField] private List<Sprite> roofSprites;
    [SerializeField] private List<Transform> points; // Vector2 , 그래프 편집 툴 전환 필요
    private List<Falling_Object> _fallingObjects;
    
    private void Awake()
    {
        if (fallingObjectPrefab == null)
        {
            Debug.LogWarning("Prefab is not exist!!");
            return;
        }

        if (_fallingObjects == null)
        {
            _fallingObjects = new List<Falling_Object>();
        }
        _fallingObjects.Clear();
        
        // sort points by x pos 
        points.Sort((transform1, transform2) => { return transform1.position.x.CompareTo(transform2.position.x);});
        
        
        //AnimationCurve

        float width =  fallingObjectPrefab.transform.localScale.x;

        for (int i = 0; i < points.Count - 1; i++)
        {
            int interval = (int)((points[i + 1].position.x - points[i].position.x) / width);
            
            float yInterval =  (points[i + 1].position.y - points[i].position.y) / interval;
            float xInterval = (points[i + 1].position.x - points[i].position.x) / interval;
            
            for (int j = 0; j < interval; j++)
            {
                Falling_Object fallingObject = Instantiate(fallingObjectPrefab);
                GameObject go = fallingObject.gameObject;
                int rand = Random.Range(0, 2);
                go.GetComponent<SpriteRenderer>().sprite = roofSprites[rand];
                go.GetComponent<SpriteRenderer>().color = rand == 0 ? Color.white : Color.red;
                
                float xPos = xInterval *  j;
                float yPos = yInterval *  j;
                go.transform.position = points[i].position +  new Vector3(xPos, yPos);
                go.gameObject.name = $"FallObject_{i}_{j}";
                
                _fallingObjects.Add(fallingObject);

            }
            
            
        }
        
        
    }

    private void Start()
    {
        StartCoroutine(IFallObjects(new WaitForSeconds(0.2f)));
    }

    private IEnumerator IFallObjects(WaitForSeconds fallingDelay)
    {
        foreach (var obj in _fallingObjects)
        {
            obj.IsFall = true; 
            yield return fallingDelay;    
        }
        
        
    }
    
    
    
}
