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
            IsaacImage.SetBool("RightAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            IsaacImage.SetBool("RightAT", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("LeftAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("LeftAT", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IsaacImage.SetBool("UpAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            IsaacImage.SetBool("UpAT", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("DownAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("DownAT", false);
        }
    }
   
}
