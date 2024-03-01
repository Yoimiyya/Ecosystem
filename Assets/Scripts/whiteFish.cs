using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class whiteFish : MonoBehaviour
{
    private AIState currentState = AIState.Wonder;
    private float speed;
    public enum AIState
    {
        Wonder,
        Chase,
        Air,
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AIState.Wonder:
                
                break;
            case AIState.Chase:
                transform.Translate(speed * Time.deltaTime * GameManager.instance.WhiteTarget);
                break;
            case AIState.Air:

                break;
        }
    }
}
