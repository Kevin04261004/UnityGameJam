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
    public bool isBlinked = false;
    public enum EParamType
    {
        headX = 0, // -30 ~ 30
        headY, // -30 ~ 30
        headZ, // -30 ~ 30
        blinkLeft, // -1 ~ 1
        blinkRight, // -1 ~ 1
        eyeX, // -1 ~ 1
        eyeY, // -1 ~ 1
        breath,
        frontHairX, // -1 ~ 1
        frontHairY, // -1 ~ 1
        backHairX, // -1 ~ 1
        backMiddleHairX, // -1 ~ 1
        backDownHairX, // -1 ~ 1
    }

    public CubismParameter[] Parameters;
    public float maxDistance = 1;
    public float blinkTime = 0;
    public float breathTime = 0;
    private void Awake()
    {
        cam = Camera.main;
        Parameters = new CubismParameter[CubismModel.Parameters.Length];
        
        for (int i = 0; i < CubismModel.Parameters.Length; ++i)
        {
            Parameters[i] = CubismModel.Parameters[i];
        }
    }

    private void LateUpdate()
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

        blinkTime -= Time.deltaTime;
        if (blinkTime <= 0)
        {
            Parameters[(int)EParamType.blinkLeft].Value = isBlinked ? 1 : 0;
            Parameters[(int)EParamType.blinkRight].Value = isBlinked ? 1 : 0;

            blinkTime = isBlinked ? Random.Range(0.2f, 0.3f) : Random.Range(1,1.5f);
            isBlinked = !isBlinked;
        }
        
        breathTime -= Time.deltaTime;
        if (breathTime <= 0)
        {
            if (Parameters[(int)EParamType.breath].Value == 1)
            {
                Parameters[(int)EParamType.breath].Value = -1;
            }
            else
            {
                Parameters[(int)EParamType.breath].Value = 1;
            }
        
            breathTime = 0.5f;
        }
    }


}