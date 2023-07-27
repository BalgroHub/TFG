using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetector : MonoBehaviour
{
    [SerializeField]
    private bool active;

    private void Start()
    {
        active = false;
    }

    public void SetActive(bool bo)
    {
        active = bo;
    }
    private void FixedUpdate()
    {
        if(active)
        {
            RaycastHit hit;
            Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition) , out hit );

            //If its this gameobject
            if ( hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                GridMaster.Instance.SetCurrentCursorGrid(transform.parent.parent.gameObject);
                GridMaster.Instance.currentParent = transform.parent.parent.GetChild(0);
            }

        }
        
    }

   
}
