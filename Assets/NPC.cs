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

    public bool shake = false;

    public float shakeGoal = 0;

    public float shakeSpeed = 1;

    public List<Color> skins;

    // Start is called before the first frame update
    void Start()
    {
        Material m = new Material(human);
        m.SetColor("Skin", skins[Random.Range(0,skins.Count)]);
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

            if(taskTime < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Player.me.fail_coffe();
                Bubble.SetActive(false);
                nextCoffe = Random.Range(5, 10);
                wantsCoffe = false;
            }

            if(taskTime < 5)
            {
                if (!shake)
                {
                    shake = true;
                    shakeGoal = 15;
                }
                else
                {
                    if (taskTime > 1) {
                        shakeSpeed = Mathf.Lerp(1, 0, (taskTime-1)/5);
                    }

                    if (transform.localEulerAngles.z < 180)
                    {
                        if (shakeGoal > 0 && transform.localEulerAngles.z > shakeGoal)
                        {
                            shakeGoal = -15;
                        }
                    }
                    if (transform.localEulerAngles.z > 180)
                    {
                        if (shakeGoal < 0 && transform.localEulerAngles.z - 360 < shakeGoal)
                        {
                            shakeGoal = 15;
                        }
                    }

                    if (shakeGoal > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + shakeSpeed);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - shakeSpeed);
                    }
                }
            }

            if (active)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Player.me.deliver_coffe())
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
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
