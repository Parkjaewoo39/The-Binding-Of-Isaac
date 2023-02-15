using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    private Animator IsaacImage;
    // Start is called before the first frame update
    void Start()
    {
        IsaacImage = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //IsaacHead.transform.rotation = Quaternion.FromToRotation(Vector3.zero, Vector3.right);
            //IsaacImage.SetBool("RightAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //IsaacImage.SetBool("RightAT", false);
        }
    }
   
}
