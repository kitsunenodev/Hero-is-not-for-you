using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MotherController : MonoBehaviour
{
    public int speed;

    public DetectionMother detect;

    public Animator anim;
    
    public GameObject spawn1;

    public GameObject spawn2;

    public GameObject neutralPos;

    public int countBlock = 30;

    public int countLook = 20;

    public int countStart = 50;

    public float countCurrentLooking = 20;

    public float countCurrent;
    
    public int destination;

    public bool isMoving;

    public bool isLooking;

    public bool destinationKnown;
    
    public Vector3 localScale;

    private void Start()
    {
        countCurrent = countStart;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.isRunning)
        {
            if (isMoving == false)
            {
                if (isLooking)
                {
                    UpdateLookCount();
                }
                else
                {
                    UpdateCount();
                }
                
            }
            else
            {
                GetDestination();
            }
            
        }
        
    }
    
    //Update Counter before next look phase
    void UpdateCount()
    {
        if (countCurrent > 0)
        {
            countCurrent -= 0.1f;
        }
        else
        {
            isMoving = true;
            GetDestination();
            countCurrent = countBlock;
        }
    }

    //Update counter during look phase
    void UpdateLookCount()
    {
        if (countCurrentLooking > 0)
        { 
            Look();
            countCurrentLooking -= 0.1f;
        }
        else
        {
            isLooking = false;
            Neutral();
            countCurrentLooking = countLook;
        }
    }

    void Look()
    {
        if (!detect.gameObject.activeSelf)
        {
            detect.gameObject.SetActive(true);
        }
    }

    //Method to get the Location where the mother is going to appear (door/window)
    void GetDestination()
    {
        if (destinationKnown == false)
        {
            destination = Random.Range(1,3);
            if (destination == 1)
            {
                GameController.Instance.LookingPhase(destination);
                MoveTo(spawn1);
            }
            else
            {
                GameController.Instance.LookingPhase(destination);
                MoveTo(spawn2);
            }
            destinationKnown = true;
        }
        else
        {
            MoveTo(destination == 1 ? spawn1 : spawn2);
        }
    }

    //Method to move the mother to the spawn point
    void MoveTo(GameObject currentDestination)
    {
        Vector3 direction = new Vector3();
        if (currentDestination == spawn1 && localScale.x > 0)
        { 
            localScale = new Vector3(-localScale.x,
                localScale.y, localScale.z); 
            transform.localScale = localScale;
        }
        direction.x += localScale.x > 0 ? speed * Time.deltaTime : -(speed * Time.deltaTime);
        transform.position += direction;
        CheckPosition(currentDestination);
    }
    
    //Method to get back to the neutral spawn
    void Neutral()
    {
        GameController.Instance.WaitingPhase();
        if (localScale.x < 0)
        {
            localScale = new Vector3(-localScale.x,
                localScale.y, localScale.z);
            transform.localScale = localScale;
        }
        transform.position = neutralPos.transform.position;
        isLooking = false;
        detect.gameObject.SetActive(false);
        destinationKnown = false;
    }

    //Method to ensure the mother does not go further than her looking point
    void CheckPosition(GameObject currentDestination)
    {
        if ((currentDestination == spawn1 && transform.position.x < currentDestination.transform.position.x)
            || (currentDestination == spawn2 && transform.position.x > currentDestination.transform.position.x))
        {
            transform.position = currentDestination.transform.position;
            isMoving = false;
            isLooking = true;
        }
    }

    public void SpotPlayerBeingHit()
    {
        anim.SetTrigger("PlayerHit");
    }

    public void SpotPlayerHittingSomething()
    {
        anim.SetTrigger("DemonHit");
    }
    
    
}
