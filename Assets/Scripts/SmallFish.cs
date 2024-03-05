using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmallFish : MonoBehaviour
{
    public AIState currentState = AIState.Wonder;
    private float speed;
    private Vector3 fleeDirection, viewportPos;

    private Vector3 previousPosition;
    public bool inScreen;

    private float radians;
    private GameObject seabird;
    public Vector3 startPos, smallDir;
    public enum AIState
    {
        Wonder,
        Scare,
        Air,
    }

    // Start is called before the first frame update
 
    void Start()
    {
        previousPosition = transform.position;
        speed = 0.45f;
        seabird = GameObject.Find("SeaBird");
    }

    // Update is called once per frame
    void Update()
    {
        if (seabird.GetComponent<SeaBird>().currentState == SeaBird.AIState.Wonder && currentState != AIState.Air)
        {
            if (this.transform.position.y >= 3.15f)
            {
                radians = Random.Range(200f, 340f) * Mathf.Deg2Rad;
                currentState = AIState.Air;
            }
        }

        viewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (transform.position.x > previousPosition.x)
        {
            this.transform.localScale = new Vector3(0.05f, 0.05f, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(-0.05f, 0.05f, 0);
        }
        previousPosition = transform.position;
        if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
        {
            inScreen = true;
        } else
        {
            inScreen = false;
        }
            //change state
            GameObject whiteFish = GameObject.FindWithTag("whiteFish");

        //state
        switch (currentState)
        {
            case AIState.Wonder:
                    this.transform.position += speed * Time.deltaTime * smallDir;

                

                // Check if the object is within the screen bounds
                if (inScreen)
                {
                    if (whiteFish != null)
                    {
                        if (Vector2.Distance(this.transform.position, whiteFish.transform.position) < 2f)
                        {
                            whiteFish.GetComponent<whiteFish>().currentState = global::whiteFish.AIState.Chase;
                            whiteFish.GetComponent<whiteFish>().target = this.GameObject();
                            fleeDirection = transform.position - whiteFish.transform.position;
                            currentState = AIState.Scare;
                        }
                    }
                }

                break;
            case AIState.Scare:

                //sprite change
                if (fleeDirection.x < 0)
                {
                    this.transform.localScale = new Vector3(-0.05f, 0.05f, 0);
                }
                else
                {
                    this.transform.localScale = new Vector3(0.05f, 0.05f, 0);
                }

                transform.Translate(fleeDirection.normalized * 3 * Time.deltaTime);

                if (whiteFish != null)
                {
                    //change state
                    if (Vector3.Distance(this.transform.position, whiteFish.transform.position) > 4)
                    {
                        currentState = AIState.Wonder;
                    }
                }
                    
                //this.transform.position += speed * Time.deltaTime * GameManager.instance.smallDir;
                break;
            case AIState.Air:
                smallDir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
                this.transform.position += 3.5f * Time.deltaTime * smallDir;
                break;
        }
    }
}
