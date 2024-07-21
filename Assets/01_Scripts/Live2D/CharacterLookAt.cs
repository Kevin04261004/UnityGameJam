using System;
using System.Collections;
using Live2D.Cubism.Core;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterLookAt : MonoBehaviour
{
    public Transform startTr;
    private Camera cam;
    public CubismModel CubismModel;
    public enum EParamType
    {
        headX = 0, // -30 ~ 30
        headY, // -30 ~ 30
        headZ, // -30 ~ 30
        smileLeft, // -1 ~ 1
        blinkLeft, // -1 ~ 1
        blinkRight, // -1 ~ 1
        smileRight, // -1 ~ 1
        eyeX, // -1 ~ 1
        eyeY, // -1 ~ 1
        frontHairX, // -1 ~ 1
        frontHairY, // -1 ~ 1
        backHairX, // -1 ~ 1
        backMiddleHairX, // -1 ~ 1
        backDownHairX, // -1 ~ 1
    }

    public CubismParameter[] Parameters;
    public float maxDistance = 1;
    public float blinkTime = 0;
    private WaitForSeconds blinkWFS = new WaitForSeconds(0.3f);
    private Coroutine blinkCoroutine;
    private void Awake()
    {
        cam = Camera.main;
        Parameters = new CubismParameter[CubismModel.Parameters.Length];
        
        for (int i = 0; i < CubismModel.Parameters.Length; ++i)
        {
            Parameters[i] = CubismModel.Parameters[i];
        }
    }

    private void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint((Vector2)Input.mousePosition);

        Vector2 dir = (pos - (Vector2)startTr.position).normalized;
        float distance = Vector2.Distance(pos, startTr.position);

        if (distance > maxDistance)
        {
            distance = maxDistance;
        }
        print($"Dir: {dir}, normalized: {dir.normalized} , distance: {distance}");

        Parameters[(int)EParamType.headX].Value = dir.x * (distance / maxDistance) * 30f;
        Parameters[(int)EParamType.headY].Value = dir.y * (distance / maxDistance) * 30f;
        Parameters[(int)EParamType.headZ].Value = dir.x * (distance / maxDistance) * 30f;
        Parameters[(int)EParamType.eyeX].Value = dir.x * (distance / maxDistance);
        Parameters[(int)EParamType.eyeY].Value = dir.y * (distance / maxDistance);

        // blinkTime += Time.deltaTime;
        // if (blinkTime > 1f) //Random.Range(0.5f, 1f))
        // {
        //     blinkTime = 0;
        //     if (blinkCoroutine == null)
        //     {
        //         blinkCoroutine = StartCoroutine(Blink());
        //     }
        // }
    }
    //
    // private IEnumerator Blink()
    // {
    //     Parameters[(int)EParamType.blinkLeft].Value = 0f;
    //     Parameters[(int)EParamType.blinkRight].Value = 0f;
    //     yield return blinkWFS;
    //     Parameters[(int)EParamType.blinkLeft].Value = 1f;
    //     Parameters[(int)EParamType.blinkRight].Value = 1f;
    //
    // }
    
    
}