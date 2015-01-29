// Based off inScope Studio's Health Bar Tutorial
// https://www.youtube.com/watch?v=NgftVg3idB4

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public RectTransform healthTransform;
    private float yPos;
    private float maxXValue; // 100% health = startPos - barLength
    private float minXValue;

    private int currentHealth;
    private float currentXValue;

    private int CurrentHealth {
        get {return currentHealth;}
        set {
            currentHealth = value;
            HandleHealth();
        }
    }

    public int maxHealth;

    public Text healthText;

    // access
    public Image visualHealth;

    public float coolDown;
    private bool onCoolDown;

    private float distToGround;
    

    // Use this for initialization
    void Start()
    {   
        onCoolDown = false;

        yPos = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        HandleMovement();

        distToGround = collider.bounds.extents.y;

        // Hide if invisible is enabled after start but we want it to be disabled 
        // so the health bar will be able to grow
//		if(healthTransform.GetComponent<CanvasRenderer>().hideIfInvisible = true) {
//            healthTransform.GetComponent<CanvasRenderer>().hideIfInvisible = false;
//        }
    }

    private void HandleHealth() {
        healthText.text = "Health: " + currentHealth;
        currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);

        // Transform health bar into correct osition
        healthTransform.position = new Vector3(currentXValue, yPos); 

        // If > 50% health, then transform R
        if (currentHealth > maxHealth / 2) {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth/2, maxHealth, 255, 0), 255, 0, 255);

        } 
        // If < 50% health, then transform G
        else {
            visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth/2, 0, 255), 0, 255);
        }
    }

    IEnumerator CoolDownDamage() {
        onCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
    }

    // Handles the player's movement
    private void HandleMovement() {   
        // Used for making the movement framerate independent
        float translation = speed * Time.deltaTime;

        // Used to move player based on axes the function fetches
        // Axes are multiplied by translation so that we move an amount in the game per second instead of per frame
        // Makes frame rate independent
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }

    void OnTriggerStay(Collider other) {
        Debug.Log("Collided");
        if (other.gameObject.tag == "Damage") {
            if(currentHealth >= 1) {
                StartCoroutine(CoolDownDamage());
                CurrentHealth -= 1;
                Debug.Log("Damage");
            }
        }

        if (other.gameObject.tag == "Health") {
            if(currentHealth < maxHealth) {
                StartCoroutine(CoolDownDamage());
                CurrentHealth += 1;
                Debug.Log("Health");
            }
        }
        Debug.Log("Done colliding");
    }


    // health   0   50  100
    // posX     -10 -5  0
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax) {
        return ((x - inMin) * (outMax - outMin)) / (inMax - inMin) + outMin;

    }




}
