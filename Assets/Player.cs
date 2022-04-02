using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        //Call the code for moveing the character
        MovementCode();

        //Code for the characters needs
        float factors = 1;
        if(body.velocity.magnitude > 1)
        {
            factors += 0.5f;
        }

        thirst -= thirstTick * Time.deltaTime * factors;
        hunger -= hungerTick * Time.deltaTime * factors;
        energy -= energyTick * Time.deltaTime * factors;

        thistBar.updateBar(thirst, maxThirst);
        hungerBar.updateBar(hunger, maxHunger);
        energyBar.updateBar(energy, maxEnergy);

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
}
