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
    public Vector3 smallDir, WhiteTarget, screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //small fish
        SmallFishNumber = Random.Range(3, 8);
        generateSmallFish();
        InvokeRepeating("changeDirection", 2, Random.Range(2f,5f));
        smallDir = Random.insideUnitSphere.normalized;

        //White fish
        generateWhiteFish();
        WhiteTarget = SmallFish.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //white fish
        if (WhiteFish.transform.position == GameManager.instance.WhiteTarget)
        {
            WhiteTarget = SmallFish.transform.position;
        }


    }

    void generateSmallFish()
    {
        float xpos = Random.Range(-5, 5);
        float ypos = Random.Range(-5, 5);
        for (int i = 0; i < SmallFishNumber; i++)
        {
            Instantiate(SmallFish, new Vector3(xpos - Random.Range(-1f,1f),ypos - Random.Range(-1f, 1f), 0), Quaternion.identity);
        }
    }

    void generateWhiteFish()
    {
        float xpos = Random.Range(-5, 5);
        float ypos = Random.Range(-5, 5);
        Instantiate(WhiteFish, new Vector3(xpos, ypos, 0), Quaternion.identity);
    }

    void changeDirection()
    {
        smallDir = Random.insideUnitSphere.normalized;
    }
}
