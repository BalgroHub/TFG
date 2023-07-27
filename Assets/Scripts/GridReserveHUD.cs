using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridReserveHUD : MonoBehaviour
{
    public bool collapsed;
    public bool moving;
    private bool expanding;
    private bool collapsing;
    private Animator myAnimator;

    public static GridReserveHUD Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        collapsed = true;
        moving = false;
        expanding = false;
        collapsing = false;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //If its pressed, it expands/collapses the reserve grid if not moving
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition) , out hit);
            
            //If its the colider mask 15
            if ( hit.collider != null && hit.collider.gameObject.layer == 15)
            {
                if (collapsed)
                {
                    ExpandGrid();
                }
                else
                {
                    CollapseGrid();
                }
            }
         
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            //If it colides with the gameplay arrow, then the game starts
            if (hit.collider != null && hit.collider.gameObject.layer == 16 && ButtonGameplay.Instance.canPress)
            {
                GameState.Instance.changeGamestate();
            }

            if (hit.collider != null && hit.collider.gameObject.layer == 17 )
            {
                LevelManager.Instance.RestartLevel();
            }
        }

        


    }

    public void ExpandGrid()
    {
        if(collapsed && !moving)
        {
            //Trigger the expand animation
            Debug.Log("Expanded");
            myAnimator.SetTrigger("Expand");
            collapsed = false;
            moving = true;
            expanding = true;

            StartCoroutine(AnimationCountdown());
        }
        
        
    }

    public void CollapseGrid()
    {
        if(!collapsed && !moving)
        {
            //Trigger the collapse animation
            Debug.Log("Collapsed");
            myAnimator.SetTrigger("Collapse");
            collapsed = true;
            moving = true;
            collapsing = true;

            StartCoroutine(AnimationCountdown());
        }
        
    }

    public void StopMoving()
    {
        moving = false;
    }

    /// <summary>
    /// Wait 50 frames, the exact time the animation will end
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimationCountdown()
    {
        float waitFrames = Time.deltaTime * 35;
        yield return new WaitForSecondsRealtime(waitFrames);
        moving = false;

    }
}
