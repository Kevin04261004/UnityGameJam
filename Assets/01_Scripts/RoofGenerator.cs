using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoofGenerator : MonoBehaviour
{
    
    [SerializeField] private Falling_Object fallingObjectPrefab; // falling object 변경 필요 
    [SerializeField] private List<Sprite> roofSprites;
    [SerializeField] private Transform point;
    [SerializeField] private List<Transform> points; // Vector2 , 그래프 편집 툴 전환 필요
    private List<Falling_Object> _fallingObjects;

    //[SerializeField] private int soundInterval = 3;
    
    [Range(0.1f,5.0f)]
    [SerializeField] private float fallDelay;

    [Range(0, 1000)] [SerializeField] private int variableRange;
    [Range(0f, 100f)] [SerializeField] private float fallSpeed;

    [SerializeField]private bool fallInAwake = false;
    [SerializeField] private float intervalValue = 1;

    private WaitForSeconds fallDelayWFS;
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

        points = point.GetComponentsInChildren<Transform>().ToList();
        points.Remove(point.transform);
        
        // sort points by x pos 
        points.Sort((transform1, transform2) => { return transform1.position.x.CompareTo(transform2.position.x);});
        
        GenerateRoof();


        fallDelayWFS = new WaitForSeconds(fallDelay);

    }

    private void GenerateRoof()
    {
        //AnimationCurve

        //float width =  fallingObjectPrefab.transform.localScale.x;

        for (int i = 0; i < points.Count - 1; i++)
        {
            int interval = (int)((points[i + 1].position.x - points[i].position.x) / intervalValue);
            
            float yInterval =  (points[i + 1].position.y - points[i].position.y) / interval;
            float xInterval = (points[i + 1].position.x - points[i].position.x) / interval;
            
            for (int j = 0; j < interval; j++)
            {
                Falling_Object fallingObject = Instantiate(fallingObjectPrefab, transform, true);
                GameObject go = fallingObject.gameObject;
                go.SetActive(false);
                int rand = Random.Range(0, roofSprites.Count);
                go.GetComponent<SpriteRenderer>().sprite = roofSprites[rand];
                //go.GetComponent<SpriteRenderer>().color = rand == 0 ? Color.white : Color.red;
                
                float xPos = xInterval *  j;
                float yPos = (yInterval *  j) + (Random.Range(-variableRange,variableRange) / 100f);
                go.transform.position = points[i].position +  new Vector3(xPos, yPos);
                go.gameObject.name = $"FallObject_{i}_{j}";
                
                
                _fallingObjects.Add(fallingObject);
                fallingObject.FallSpeed = fallSpeed +(((fallSpeed * i)) +((fallSpeed * j) / interval)) ;
                //Debug.Log( $"{fallingObject.name} :: {fallingObject.FallSpeed}");
                

            }
            
            
        }
    }

    private void Start()
    {
        if (fallInAwake)
            Fallobjects();
    }

    public void Fallobjects()
    {
        StartCoroutine(IFallObjects(fallDelayWFS));
    }
    
    private IEnumerator IFallObjects(WaitForSeconds fallingDelay)
    {
        foreach (var obj in _fallingObjects)
        {
            obj.IsFall = true; 
            obj.gameObject.SetActive(true);
            yield return fallingDelay;    
        }
        
        
    }
    
    
    
}
