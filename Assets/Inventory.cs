using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject item;
    [SerializeField]
    GameObject hand;
    [SerializeField]
    Vector3 position;

    [SerializeField]
    bool hasPickedUP;

    private void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand");
        position = item.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasPickedUP)
        {
            GameObject obj = Instantiate(item, transform.position, Quaternion.identity);
            obj.transform.parent = hand.transform;
            obj.transform.localPosition = position;
            obj.transform.localScale = hand.transform.localScale;
            GetComponent<SpriteRenderer>().enabled = false;
            hasPickedUP = true;
        }
       
    }



}
