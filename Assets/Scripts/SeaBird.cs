using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeaBird : MonoBehaviour
{
    private int choice;
    private float speed = 1.5f;
    private bool haschanged, reached;

    private GameObject[] smallFishes;
    private Vector2 target;
    private GameObject fish;

    public Sprite withFish, withoutFish;

    private int dir;

    private Vector3 previousPosition;

    public AIState currentState = AIState.Relax;
    public enum AIState
    {
        Wonder,
        Chase,
        Relax,
    }
    // Start is called before the first frame update
    void Start()
    {
        choice = 0;
        target = new Vector2(Random.Range(-8f, 8f), 4.9f);
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x > previousPosition.x)
        {
            dir = 1;
            this.transform.localScale = new Vector3(-0.4f, 0.4f, 0);
        }
        else
        {
            dir = -1;
            this.transform.localScale = new Vector3(0.4f, 0.4f, 0);
        }

        previousPosition = transform.position;

        switch (currentState)
        {
            case AIState.Wonder:
                if (choice == 0)
                {
                    this.transform.position += speed * Time.deltaTime * Vector3.left;
                } else if (choice == 1)
                {
                    this.transform.position += speed * Time.deltaTime * Vector3.right;
                } else
                {
                    if (Vector2.Distance(transform.position, target) <= 0.1f)
                    {
                        choice++;
                        target = new Vector2(Random.Range(-8f, 8f), 4.9f);
                    } else
                    {
                        transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime * speed);
                    }
                }
                if (this.transform.position.x < 0.1f && this.transform.position.x > -0.1f)
                {
                    choice = 2;
                }
                if (choice == 5)
                {
                    smallFishes = GameObject.FindGameObjectsWithTag("smallFish");
                    if (smallFishes.Length == 0)
                    {
                        currentState = AIState.Relax;
                    } else
                    {
                        fish = smallFishes[Random.Range(0, smallFishes.Length)];
                        while (!fish.GetComponent<SmallFish>().inScreen)
                        {
                            fish = smallFishes[Random.Range(0, smallFishes.Length)];
                        }
                        fish.GetComponent<SmallFish>().currentState = SmallFish.AIState.Air;
                        reached = false;
                        currentState = AIState.Chase;
                    }
                    
                }
                break;
            case AIState.Chase:
                
                if (reached)
                {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(0, 4.9f), Time.deltaTime * (speed + 4));
                    if (Vector2.Distance(transform.position, new Vector2(0, 4.9f)) < 0.3f)
                    {
                        haschanged = false;
                        currentState = AIState.Relax;
                    }
                } else
                {
                    if (!fish)
                    {
                        reached = true;
                        this.GetComponent<SpriteRenderer>().sprite = withFish;
                    }
                    else if (!fish || Vector2.Distance(transform.position, fish.transform.position) < 0.5f)
                    {
                        reached = true;
                        this.GetComponent<SpriteRenderer>().sprite = withFish;
                        Destroy(fish);
                    }
                    else
                    {
                        transform.position = Vector2.Lerp(transform.position, fish.transform.position, Time.deltaTime * (speed + 4));
                    }
                }
                break;
            case AIState.Relax:

                if (!haschanged) {
                    Vector2 newPos;
                    if (dir == -1)
                    {
                        newPos = new Vector2(-10.8f, 4.9f);
                    }
                    else
                    {
                        newPos = new Vector2(10.8f, 4.9f);
                    }
                    transform.position = Vector2.Lerp(transform.position, newPos, Time.deltaTime * speed);
                    if (Vector2.Distance(transform.position, newPos) < 0.1f)
                    {
                        Invoke("ChangeBack", Random.Range(4f, 7f));
                        haschanged = true;
                    }
                }

                break;
        }
    }
    void ChangeBack()
    {
        this.GetComponent<SpriteRenderer>().sprite = withoutFish;
        choice = Random.Range(0, 2);
        if (choice == 0)
        {
            this.transform.position = new Vector3(10.8f, 4.9f, 0);
        }
        else
        {
            this.transform.position = new Vector3(-10.8f, 4.9f, 0);
            this.transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        }
        currentState = AIState.Wonder;
    }
}
