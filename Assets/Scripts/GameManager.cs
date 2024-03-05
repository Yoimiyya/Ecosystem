using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;




    //small fish
    public GameObject SmallFish;
    public GameObject WhiteFish;
    private int SmallFishNumber;
    public Vector3 whiteDir;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //small fish
        SmallFishNumber = Random.Range(3, 8);
        InvokeRepeating("generateSmallFish", 0, Random.Range(5f, 8f));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("whiteFish") == null)
        {
            generateWhiteFish();
        }
    }

    void generateSmallFish()
    {
        float xpos;
        float ypos = Random.Range(-5, 5);
        float radians;
        int side = Random.Range(0,2);
        if (side == 0)
        {
            xpos = 10f;
            radians = Random.Range(110f, 250f) * Mathf.Deg2Rad;
        }
        else
        {
            xpos = -10f;
            radians = Random.Range(310f, 410f) * Mathf.Deg2Rad;
        }
        for (int i = 0; i < SmallFishNumber; i++)
        {
            GameObject newFish = Instantiate(SmallFish, new Vector3(xpos - Random.Range(-1f,1f),ypos - Random.Range(-1f, 1f), 0), Quaternion.identity);
            newFish.GetComponent<SmallFish>().smallDir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
        }
    }

    void generateWhiteFish()
    {
        float xpos;
        float ypos = Random.Range(-5, 5);
        float radians;
        int side = Random.Range(0, 2);
        if (side == 0)
        {
            xpos = 8f;
            radians = Random.Range(110f, 250f) * Mathf.Deg2Rad;
        }
        else
        {
            xpos = -8f;
            radians = Random.Range(310f, 410f) * Mathf.Deg2Rad;
        }
        GameObject newFish = Instantiate(WhiteFish, new Vector3(xpos, ypos, 0), Quaternion.identity);
        newFish.GetComponent<whiteFish>().whiteDir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
    }

}
