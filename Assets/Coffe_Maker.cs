using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffe_Maker : MonoBehaviour
{

    public GameObject bubble;

    public bool active = false;

    public bool coffeReady = false;

    public float coffeCooking;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            if (coffeReady)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Player.me.coffeCups < 4)
                    {
                        Player.me.pick_up_coffe();
                        bubble.SetActive(false);
                        coffeCooking = Random.Range(2, 5);
                        coffeReady = false;
                    }
                }
            }
        }

        if (!coffeReady)
        {
            coffeCooking -= Time.deltaTime;

            if(coffeCooking <= 0)
            {
                bubble.SetActive(true);
                coffeReady = true;
            }
        }
    }
}
