using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : WeaponInventory
{
    public List<Dialogue> dialogues;

    public int branchingDialogueIndex = 100;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(!hasPickedUP)
        {
            base.weaponPickup();
            updateDialogueOutcome();
            FindObjectOfType<DialogueManager>().startDialogue(dialogues);
        }

       
        
    }


    protected override Weapon findWeapon()
    {
        int randomIndex = Random.Range(0, possibleWeapon.Length - 1);
        Weapon obj = possibleWeapon[randomIndex];
        return obj;
    }

   void updateDialogueOutcome()
    {
        if (branchingDialogueIndex >= dialogues.Count)
            return;

        string result = string.Format("Muy Bien, \"{0}\" in Spanish is \"{1}\"", weapon.weaponName, weapon.spanishName);
        FindObjectOfType<DialogueManager>().branchedDialogueOption(dialogues, branchingDialogueIndex, result);
    }
}
