using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartsHealthSystem {
    public const int MAX_FRAGMENT_AMOUNT = 2;
    private List<Heart> heartList = new List<Heart>();

    // events for damage/heal
    public event EventHandler onDamage;
    public event EventHandler onHeal;
    public event EventHandler onDead;

    // initializer
    public HeartsHealthSystem(int heartAmount)
    {
        for(int i = 0; i < heartAmount; i++)
        {
            heartList.Add(new Heart(2));
        }

    }

    public List<Heart> getHeartsList()
    {
        return heartList;
    }

    // check if player is dead
    public bool isDead()
    {
        return heartList[0].GetCurrentFragmentAmount() == 0;
    }

    public void Damage(int damageAmount)
    {

        for(int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];

            // fully destroy a heart
            if(damageAmount >heart.GetCurrentFragmentAmount() )
            {
                damageAmount -= heart.GetCurrentFragmentAmount();
                heart.TakeDamage(heart.GetCurrentFragmentAmount());
            }
            else
            {  // if a heart can absorb damage
                heart.TakeDamage(damageAmount);
                break;
            }
        }

       
        onDamage(this, EventArgs.Empty);

        if (isDead()) onDead(this, EventArgs.Empty);
    }


    public bool Heal(int healAmount)
    {
        if (heartList[heartList.Count - 1].GetCurrentFragmentAmount() == MAX_FRAGMENT_AMOUNT) return false;
        for(int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            int missingFragment = MAX_FRAGMENT_AMOUNT - heart.GetCurrentFragmentAmount(); // how many fragments are gone
            
            if(healAmount > missingFragment)
            {
                healAmount -= missingFragment;
                heart.Heal(missingFragment);
            }
            else
            {
                heart.Heal(healAmount);
                break; // don't need to increase, since full heart is done
            }
        }
        onHeal(this, EventArgs.Empty);
        return true;
    }

    // information about each individual heart
    public class Heart {

        private int fragment;


        public Heart(int frag)
        {
            fragment = frag;
        }

        // get fragment
        public int GetCurrentFragmentAmount()
        {
            return fragment;
        }

        // reset fragment
        public void SetFragment(int frag)
        {
            fragment = frag;
        }

        // when heart took damage
        public void TakeDamage(int amount)
        {
            if(amount > fragment)
            {
                fragment = 0;
                
            }
            else
            {
                fragment -= amount;
            }
        }

        // when heart is healed
        public void Heal(int amount)
        {
            if(amount + fragment > MAX_FRAGMENT_AMOUNT)
            {
                fragment = MAX_FRAGMENT_AMOUNT;
            }
            else
            {
                fragment += amount;
            }
        }


    }

}
