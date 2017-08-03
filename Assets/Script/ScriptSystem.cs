using UnityEngine;
using System.Collections;
using System;

public class ScriptSystem : MonoBehaviour
{

    public static int dimSize = 5;
    public static int dimLenght = dimSize * 2;
    public static float delayAnimationCube = 0.3f;
    public GameObject prefabCube;
    private GameObject[,] prefabCubes = new GameObject[dimLenght + 1, dimLenght + 1];

    void Start()
    {
        GameObject instancePrefabCube = null;

        for (int i = -dimSize; i <= dimSize; i++)
        {
            for (int k = -dimSize; k <= dimSize; k++)
            {
                float positionX = i * 1.40f;
                float positionY = k * 1.70f;
                float positionZ = Math.Abs(i) + Math.Abs(k);

                instancePrefabCube = (GameObject)Instantiate(prefabCube, new Vector3(positionX, positionY, positionZ), transform.rotation);
                prefabCubes[i + dimSize, k + dimSize] = instancePrefabCube;
            }
        }
    }

    int r = 0;
    float timeLeft = delayAnimationCube;
    void Update()
    {
        if (r != dimLenght)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = delayAnimationCube;

                ArrayList cubePositionsToAnimate = new ArrayList();
                for (int i = 0; i <= 360; i++)
                {
                    int x = (int)(r * Math.Cos(Math.PI / 180 * i));
                    int y = (int)(r * Math.Sin(Math.PI / 180 * i));

                    if (x % 1 == 0 && y % 1 == 0)
                    {
                        bool alreadyAdded = false;
                        foreach (Vector2 item in cubePositionsToAnimate)
                        {
                            if (item.x == x && item.y == y)
                                alreadyAdded = true;
                        }

                        if (!alreadyAdded)
                            cubePositionsToAnimate.Add(new Vector2(x, y));
                    }
                }

                foreach (Vector2 item in cubePositionsToAnimate)
                {
                    int x = (int)item.x + dimSize;
                    int y = (int)item.y + dimSize;

                    if ((x >= 0 && x < dimLenght + 1) && (y >= 0 && y < dimLenght + 1))
                    {
                        Animator cubeAnimator = prefabCubes[x, y].GetComponentInChildren<Animator>();
                        cubeAnimator.Play("RotateAnimation");
                        //StartCoroutine(RestartAnimation(cubeAnimator, 12f));
                    }
                }
                r++;
            }
        }
    }

    IEnumerator RestartAnimation(Animator animator, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //animator.Play("RotateAnimation", -1, 0f);
        //StartCoroutine(RestartAnimation(animator, 12f));
    }
}