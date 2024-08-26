using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public CameraClick CameraAmount;
    public int chance;
    public Animator animator;
    private bool HasMadeChance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (CameraAmount.animator.GetCurrentAnimatorStateInfo(0).IsName("Back") )
        {
            if(HasMadeChance== false) 
            {
                chance = Random.Range(1, 10);
            }
            HasMadeChance = true;
        }
        else
        {
            HasMadeChance = false;  
        }
        if (chance == 5)
        {
            animator.SetBool("Can_appear", true);
            Debug.Log("HE IS HERE");
        }
    }

    public void TestFucntion()
    {
        Debug.Log("Gone");
        animator.SetBool("Can_appear", false);
        chance = 0;
    }
}
