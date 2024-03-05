using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class whiteFish : MonoBehaviour
{
    public AIState currentState = AIState.Wonder;
    private float speed;
    public Vector3 whiteDir;
    public GameObject target;

    private GameObject seabird;
    private float radians;
    private Vector3 previousPosition;
    private Vector3 targetpos;
    public enum AIState
    {
        Wonder,
        Chase,
        Air,
    }
    // Start is called before the first frame update
    void Start()
    {
        seabird = GameObject.Find("SeaBird");
        speed = 1.5f;
        previousPosition = transform.position;
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
        if (transform.position.x > previousPosition.x)
        {
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(-0.2f, 0.2f, 0);
        }

        previousPosition = transform.position;


        switch (currentState)
        {
            case AIState.Wonder:
                this.transform.position += speed * Time.deltaTime * whiteDir;

                break;
            case AIState.Chase:
                speed = 2.5f;
                if (target != null)
                {
                    targetpos = target.transform.position - transform.position;
                    targetpos.Normalize();
                }
                // Lerp towards the target position
                transform.Translate(targetpos * speed * Time.deltaTime);
                break;
            case AIState.Air:
                whiteDir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
                this.transform.position += (speed+2) * Time.deltaTime * whiteDir;
                break;
        }
    }
}
