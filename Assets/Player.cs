using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody2D body;

    public float speed;

    public float speedCap;

    public SpriteRenderer face;

    public List<Sprite> sprites;


    public int maxEnergy = 100, maxHunger = 100, maxThirst = 100;
    public float energy, hunger, thirst;

    public float energyTick, hungerTick, thirstTick;

    public UI_StatBar thistBar, hungerBar, energyBar;

    public static Player me;

    public int coffeCups;

    public List<GameObject> cupCovers;

    public int fails = 0;

    public List<GameObject> failCovers;

    public TMPro.TextMeshProUGUI scoreText, endScoreText;

    public float score;

    public bool over;

    public GameObject lostMenu;

    // Start is called before the first frame update
    void Start()
    {
        me = this;
        energy = maxEnergy;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        if (!over)
        {
            //Call the code for moveing the character
            MovementCode();

            //Code for the characters needs
            float factors = 1;
            if (body.velocity.magnitude > 1)
            {
                factors += 0.5f;
            }

            thirst -= thirstTick * Time.deltaTime * factors;
            hunger -= hungerTick * Time.deltaTime * factors;
            energy -= energyTick * Time.deltaTime * factors;

            thistBar.updateBar(thirst, maxThirst);
            hungerBar.updateBar(hunger, maxHunger);
            energyBar.updateBar(energy, maxEnergy);


            if (thirst <= 0 || energy <= 0 || hunger <= 0)
            {
                Debug.Log("game over");
                over = true;
                endGame();
            }
        }
    }

    public void fail_coffe()
    {
        if(fails < 3)
        {
            failCovers[fails].SetActive(false);
            fails++;
        }
        else
        {
            Debug.Log("game over");
            over = true;
            endGame();
        }
    }

    public void pick_up_coffe()
    {
        cupCovers[coffeCups].SetActive(false);
        coffeCups += 1;
    }

    public bool deliver_coffe()
    {
        if (coffeCups > 0)
        {
            score += 5;
            scoreText.text = "$" + score;
            coffeCups -= 1;
            cupCovers[coffeCups].SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }


    public void MovementCode()
    {
        //Movement code
        if (Input.GetKey(KeyCode.A))
        {
            if (body.velocity.x > -speedCap)
            {
                body.AddForce(-transform.right * speed * Time.deltaTime);
            }
            else
            {

            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (body.velocity.x < speedCap)
            {
                body.AddForce(transform.right * speed * Time.deltaTime);
            }
            else
            {

            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (body.velocity.y < speedCap)
            {
                body.AddForce(-transform.up * speed * Time.deltaTime);
            }
            else
            {

            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (body.velocity.y < speedCap)
            {
                body.AddForce(transform.up * speed * Time.deltaTime);
            }
            else
            {

            }
        }

        //Clamp the velocity when it gets super low

        if (body.velocity.x < 0.1 && body.velocity.x > -0.1)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }

        if (body.velocity.y < 0.1 && body.velocity.y > -0.1)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
        }


        if (body.velocity.x != 0)
        {
            if (body.velocity.x > 0)
            {
                face.sprite = sprites[2];
            }

            if (body.velocity.x < 0)
            {
                face.sprite = sprites[3];
            }
        }

        if (body.velocity.y != 0)
        {
            if (body.velocity.y > 0)
            {
                face.sprite = sprites[1];
            }

            if (body.velocity.y < 0)
            {
                face.sprite = sprites[0];
            }
        }
    }

    public void endGame()
    {
        endScoreText.text = "YOU MADE: $" + score;
        lostMenu.SetActive(true);
    }

    public void goToScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
