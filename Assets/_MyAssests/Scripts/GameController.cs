using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject playerBoy;


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

    [SerializeField]
    GameObject stairsObj;
    [SerializeField]
    float stairsMaxLength = 3.5f;
    [SerializeField]
    float growingfactor = 0.05f;

    IEnumerator mycorutine;


    private void Awake()
    {
        ResetGameplay();
        mycorutine = StartGrowingCorutine();
    }

    private void ResetGameplay()
    {
        foreach (var item in pillarsList)
        {
            Destroy(item);
        }
        pillarsList.Clear();

        SpawnPillar(pillarPrefab, pillarStartingPosVec2);



    }

    private void SpawnPillar(GameObject obj, Vector2 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);
        pillarsList.Add(obj);
    }

    //private void Playboy(GameObject char )


    private void SpawnStairs(GameObject obj, Vector2 pos)
    {
        stairsObj = Instantiate(obj, pos, Quaternion.identity);
        SpriteRenderer spriteRenderer = stairsObj.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, 0);
    }


    public void OnClick_Stairs()
    {
        /* if (!startStairs)
             return;*/
        if (stairsObj == null)
        {
            SpawnStairs(stairsPrefab, stairsStartingPos);
        }
        
        Debug.Log("Started");
        startStairs = true;
    }
    IEnumerator StartGrowingCorutine()
    {
        StairsIncreasingActivity();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(mycorutine);
    }

    [SerializeField]
    bool startIncreaingStair = false;

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
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, 0);
            }
            else
            {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y + growingfactor);
            }
        }

    }



    public void OnClick_StopGrowth()
    {
        Debug.Log("Stop");
        startStairs = false;
    }

    public void AlignStairs()
    {
      // stairsObj.
    }

}
