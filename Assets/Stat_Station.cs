using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Station : MonoBehaviour
{

    public GameObject bubble;

    public bool active = false;

    public Player player;

    public float thirstGain, energyGain, hungerGain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        bubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        bubble.SetActive(false);
    }

    private void Update()
    {
        if (active)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(player.thirst < player.maxThirst)
                {
                    player.thirst += Time.deltaTime * thirstGain;
                }

                if (player.hunger < player.maxHunger)
                {
                    player.hunger += Time.deltaTime * hungerGain;
                }

                if (player.energy < player.maxEnergy)
                {
                    player.energy += Time.deltaTime * energyGain;
                }
            }
        }
    }
}
