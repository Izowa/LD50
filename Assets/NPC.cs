using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public Material human;

    public SpriteRenderer ren;

    public Color skin, shirt;


    public GameObject Bubble;

    public bool active = false;

    public bool wantsCoffe = false;

    public float nextCoffe;

    public float taskTime;


    // Start is called before the first frame update
    void Start()
    {
        Material m = new Material(human);
        m.SetColor("Skin", skin);
        m.SetColor("Cloths", shirt);
        ren.sharedMaterial = m;

        nextCoffe = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (wantsCoffe == false)
        {
            if (nextCoffe > 0)
            {
                nextCoffe -= Time.deltaTime;
            }

            if (nextCoffe <= 0)
            {
                wantsCoffe = true;
                Bubble.SetActive(true);
                taskTime = Random.Range(10, 15);
            }
        }
        else
        {
            taskTime -= Time.deltaTime;

            if (active)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Player.me.deliver_coffe())
                    {
                        Bubble.SetActive(false);
                        nextCoffe = Random.Range(5, 10);
                        wantsCoffe = false;
                    }                
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

}
