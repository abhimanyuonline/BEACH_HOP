using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject playerBoy;
    [SerializeField] Vector2 playerBoyInitialLoc = new Vector2();


    [SerializeField]
    private GameObject pillarPrefab;
    [SerializeField]
    Vector2 pillarStartingPosVec2 = new Vector2();
    [SerializeField]
    Vector2 nextPillarLocationVec2 = new Vector2();

    [SerializeField]
    GameObject stairsPrefab;
    [SerializeField]
    Vector2 stairsStartingPos = new Vector2();

    [SerializeField]
    List<GameObject> pillarsList = new List<GameObject>();


    [SerializeField]
    bool startStairs = false;

    [SerializeField] bool startIncreaingStair = false;

    [SerializeField]
    GameObject stairsObj;
    [SerializeField]
    float stairsMaxLength = 3.5f;
    [SerializeField]
    float growingfactor = 0.05f;


    [SerializeField]
    DataManager dataManager;

    [SerializeField] float ignoreWidth = 0.0f;


    public void OnClick_StartGameScene()
    {
        ResetGameplay();
    }

    private void ResetGameplay()
    {
        foreach (var item in pillarsList)
        {
            Destroy(item);
        }
        pillarsList.Clear();

        playerBoy.transform.SetPositionAndRotation(playerBoyInitialLoc, Quaternion.identity);
        SpawnPillar(pillarPrefab, pillarStartingPosVec2);
        SpawnPillar(pillarPrefab, nextPillarLocationVec2);
        startIncreaingStair = true;
    }

    int num = 0;
    private void SpawnPillar(GameObject obj, Vector2 pos)
    {
        GameObject obj2 = Instantiate(obj, pos, Quaternion.identity);
        obj2.name = "Pillar_" + num.ToString();
        pillarsList.Add(obj2);
        num++;
    }

    //private void Playboy(GameObject char )


    private void SpawnStairs(GameObject obj, Vector2 pos)
    {
        stairsObj = Instantiate(obj, pos, Quaternion.identity);
        SpriteRenderer spriteRenderer = stairsObj.GetComponent<SpriteRenderer>();
        stairsObj.transform.Rotate(0, 0, 90);
        spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
    }


    public void OnClick_Stairs()
    {
        if (!startIncreaingStair)
            return;

        if (stairsObj == null)
        {
            SpawnStairs(stairsPrefab, stairsStartingPos);
        }

        Debug.Log("Started");
        startStairs = true;

    }


    private void FixedUpdate()
    {
        if (startStairs)
        {
            StairsIncreasingActivity();
        }
    }
    void StairsIncreasingActivity()
    {
        if (stairsObj == null)
        {
            Debug.Log("object not found");
            return;
        }
        SpriteRenderer spriteRenderer = stairsObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (spriteRenderer.size.y > stairsMaxLength)
            {
                spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
            }
            else
            {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x + growingfactor, spriteRenderer.size.y );
            }
        }

    }



    public void OnClick_StopGrowth()
    {
        if (!startIncreaingStair)
            return;

        if (stairsObj == null)
            return;

        startIncreaingStair = false;

        Debug.Log("Stop");
        startStairs = false;
        AlignStairs();

    }

    public void AlignStairs()
    {
        stairsObj.transform.DOLocalRotate(new Vector3(0, 0,-90), 2.0f, RotateMode.LocalAxisAdd).OnComplete(StartMovingPlayer);
    }
    void StartMovingPlayer()
    {
        float length =stairsObj.transform.position.x + stairsObj.GetComponent<SpriteRenderer>().size.x * stairsObj.transform.localScale.x;
        Debug.LogError(length.ToString());
        playerBoy.transform.DOMoveX(length, 2).OnComplete(CheckPlayerStatus);
    }

    void CheckPlayerStatus()
    {
        GameObject currentTower = pillarsList[1];
        float diff = currentTower.transform.position.x - playerBoy.transform.position.x;
        Debug.Log(currentTower + "_diff_" + diff);
        if (Math.Abs(diff) < ignoreWidth)
        {

            Debug.Log("reached");
            SucssefullyReached();
            dataManager.UpdateScore();
        }
        else
        {
            Debug.Log("Failed");
            UnSucssefullyReached();
        }
    }

    void SucssefullyReached()
    {
        Destroy(stairsObj);

        playerBoy.transform.DOMoveX(pillarStartingPosVec2.x, 2);

        Vector2 nextGridPos = new Vector2(nextPillarLocationVec2.x + 2, nextPillarLocationVec2.y);
        SpawnPillar(pillarPrefab, nextGridPos);

        Vector2 prevGridPos = new Vector2(pillarStartingPosVec2.x - 2, pillarStartingPosVec2.y);
        pillarsList[0].transform.DOMoveX(prevGridPos.x, 2);
        pillarsList[1].transform.DOMoveX(pillarStartingPosVec2.x, 2).OnComplete(AfterSucessfullPlayerReach);
        pillarsList[2].transform.DOMoveX(nextPillarLocationVec2.x, 2);

    }


    void UnSucssefullyReached()
    {
        float currentY = playerBoy.transform.position.y;
        playerBoy.transform.DOMoveY(-7.0f, 2);
        stairsObj.transform.DOLocalRotate(new Vector3(0, 0, -90), 2.0f, RotateMode.LocalAxisAdd).OnComplete(AfterUnSucessfullPlayerReach);
    }

    void AfterSucessfullPlayerReach()
    {

        Destroy(pillarsList[0]);
        pillarsList.RemoveAt(0);
        startIncreaingStair = true;
    }

    void AfterUnSucessfullPlayerReach()
    {
        Destroy(stairsObj);
        dataManager.GameEndMenu();
    }


}
