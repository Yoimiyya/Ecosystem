using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    private AIState currentState = AIState.Wonder;
    private float speed;
    public enum AIState
    {
        Wonder,
        Scare,
        Air,
    }

    // Start is called before the first frame update
 
    void Start()
    {
        speed = 0.45f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.smallDir.x < 0)
        {
            this.transform.localScale = new Vector3(-0.05f,0.05f,0);
        } else
        {
            this.transform.localScale = new Vector3(0.05f, 0.05f, 0);
        }
        switch (currentState)
        {
            case AIState.Wonder:
                this.transform.position += speed * Time.deltaTime * GameManager.instance.smallDir;
                if (transform.position.x > GameManager.instance.screenBounds.x || transform.position.x < -GameManager.instance.screenBounds.x ||
                    transform.position.y > GameManager.instance.screenBounds.y || transform.position.y < -GameManager.instance.screenBounds.y)
                {
                    GameManager.instance.smallDir = -GameManager.instance.smallDir;
                }
                //change state
                if (Vector3.Distance(this.transform.position, GameManager.instance.WhiteFish.transform.position) < 1)
                {
                    currentState = AIState.Scare;
                }
                break;
            case AIState.Scare:
                Vector3 fleeDirection = Random.insideUnitCircle.normalized;
                fleeDirection += transform.position - GameManager.instance.WhiteFish.transform.position;
                transform.Translate(fleeDirection.normalized * (speed+1) * Time.deltaTime);

                //change state
                if (Vector3.Distance(this.transform.position, GameManager.instance.WhiteFish.transform.position) >= 1)
                {
                    currentState = AIState.Wonder;
                }
                //this.transform.position += speed * Time.deltaTime * GameManager.instance.smallDir;
                break;
            case AIState.Air:
                
                break;
        }
    }
}
