using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public GameObject[] randomItems;

    [SerializeField]
    bool hasEntered;

    [SerializeField]
    GameObject hand;

    [SerializeField]
    Vector3 position;

    private void Start()
    {
        hand = GameObject.FindGameObjectWithTag("Hand");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasEntered)
        {
            hasEntered = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().backPackState = 1;
            GameObject myObj = randomItem();
            GameObject obj = Instantiate(myObj, transform.position, Quaternion.identity);
            obj.transform.parent = hand.transform;
            obj.transform.localPosition = position;
            obj.transform.localScale = hand.transform.localScale;
            obj.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;

        }
    }

    GameObject randomItem()
    {

        int randomIndex = Random.Range(0, randomItems.Length - 1);
        GameObject obj = randomItems[randomIndex];
        position = obj.transform.position;
        return obj;

    }
}
