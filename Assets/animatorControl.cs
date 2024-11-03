using UnityEngine;
using System.Collections;
public class animatorControl : MonoBehaviour
{

public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown("W")){
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Run"); 
        }
    if (Input.GetKeyDown("A")){
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Run"); 
        }
    if (Input.GetKeyDown("S")){
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Run"); 
        }
    if (Input.GetKeyDown("D")){
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Run"); 
        }            
    if (Input.GetKeyDown("E")){
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Pickup");
        } 
    if (GetComponent<objValues>().holdingType !=null){
    anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_PickupRun");
        }
    else {
        anim.Play("CharacterArmature_CharacterArmature_CharacterArmature_Idle");
        }
    }
}
