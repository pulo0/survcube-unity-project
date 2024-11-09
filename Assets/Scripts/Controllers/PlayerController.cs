using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Float variables
    public float horizontalInput;
    public float verticalInput;
    private float speedX = 10f;
    private float speedY = 10f;
    private float speedOnPowerup = 20f;
    private float normalSpeed = 10f;
    private float volumeScale = 0.5f;
    private float shootVolume = 0.3f;
    private float cameraSize = 15f;
    private float cameraSizeBack = 10f; 
    
    //Int variables
    private int pointValue = 1;

    //Bool variables
    public bool gameOver;
    public bool hasPowerup = false;
    public bool isAbleToDoubleChangeCamera = false;
    public bool canShoot = false;

    //GameObjects variables
    public GameObject powerup;
    public GameObject projectile;
    private GameObject pointer;

    //Audio variables
    private AudioSource playerAudio;
    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip pickupSound;
    public AudioClip pickupHealthSound;

    //Sprite variables
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer powerupSpriteRenderer;
    private SpriteRenderer pointerSpriteRenderer;

    //Componets variables
    private BoxCollider2D playerCollider;
    private Camera cam;
    private LayerMask hitMask = 1;

    //Scripts variables
    private HealthSystem healthSystem;
    private GameManager gameManager;

    //Particles variables
    public ParticleSystem bleedingParticle;
    public ParticleSystem powerupParticle;
    public ParticleSystem healthParticle;

    //Renderers variables
    private TrailRenderer powerupTrail;

    public Joystick joystick;

    public float timer;

    
    void Awake()
    {
        gameOver = true;

        playerCollider = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        powerupTrail = GetComponent<TrailRenderer>();
        powerupSpriteRenderer = powerup.gameObject.GetComponent<SpriteRenderer>();
        pointerSpriteRenderer = GameObject.Find("Pointer").GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        pointer = GameObject.Find("Pointer");
        healthSystem = GetComponent<HealthSystem>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }


    void Update()
    {
        //Inputs
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
        
        //Changing camera size by user input
        if(Input.GetKeyDown(KeyCode.V) && !gameOver && isAbleToDoubleChangeCamera == false && !gameManager.canResume) 
        {
            cam.orthographicSize = cameraSize;
            isAbleToDoubleChangeCamera = true;
        }
        else if(Input.GetKeyDown(KeyCode.V) && !gameOver && isAbleToDoubleChangeCamera == true)
        {
            cam.orthographicSize = cameraSizeBack; 
            isAbleToDoubleChangeCamera = false;
        }
        
        if(!gameOver && !gameManager.canResume)
        {
            timer += Time.deltaTime;

            //Moving player on X and Y axis
            transform.Translate(Vector2.up * speedY * Time.deltaTime * verticalInput, Space.World);
            transform.Translate(Vector2.right * speedX * Time.deltaTime * horizontalInput, Space.World);  
        }

        
        //If you don't shoot, collider of a player is enabled
        if(canShoot == false)
            {
                playerCollider.enabled = true;
                canShoot = true;
            } 
        
        //Shooting Input
        if(Input.touchCount > 0 && !gameOver && !gameManager.canResume)
        {
            int i = Input.touchCount;
            Touch t = Input.touches[i];
            Vector2 offset = new Vector2(10, 0);

            Debug.Log(t.position.x);

            if(t.position.x >= offset.x || t.position.x <= -offset.x)
            {
                //Direction of a ray
                Vector2 mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 rayDirection = (mouseScreenPos - (Vector2) pointer.transform.position).normalized;
                transform.up = rayDirection;
                float distance = 20f;
                float speed = 80f;

            playerAudio.PlayOneShot(shootSound, shootVolume);


            if(canShoot == true && timer >= 0.15f)
            {
                timer = 0;
                playerCollider.enabled = false; 
                
                //Where ray goes to
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance, hitMask);
                canShoot = false;

                //Creates new projectile to instantiate it when the ray is drawn
                GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
                newProjectile.GetComponent<Rigidbody2D>().velocity = rayDirection * speed;

                if(hit.collider && hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.transform.gameObject);
                    Instantiate(bleedingParticle, hit.transform.position, bleedingParticle.transform.rotation);
                    playerAudio.PlayOneShot(damageSound, volumeScale);
                    gameManager.UpdateScore(pointValue++);

                }
                else
                {
                    Debug.Log("Nothing hit");
                }
            }
            
            }

        }
                
    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
      
       if(other.gameObject.CompareTag("Powerup"))
       {
           speedX = speedOnPowerup;
           speedY = speedOnPowerup; 
           hasPowerup = true;
           powerupTrail.enabled = true;
           Destroy(other.gameObject);
           StartCoroutine(PowerupCountdownRoutine());
           ParticlesOnPowerup();
           spriteRenderer.color = powerupSpriteRenderer.color;
           pointerSpriteRenderer.color = powerupSpriteRenderer.color;
           playerAudio.PlayOneShot(pickupSound, volumeScale);
       }

       if(other.gameObject.CompareTag("Health Powerup"))
       {
           healthSystem.health++;
           Destroy(other.gameObject);
           ParticlesOnHealth();
           playerAudio.PlayOneShot(pickupHealthSound, volumeScale);
       } 
    }

    //Time of a powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        speedX = normalSpeed;
        speedY = normalSpeed;

        spriteRenderer.color = Color.white;
        pointerSpriteRenderer.color = Color.white;
        powerupTrail.enabled = false;
    }

    void ParticlesOnPowerup()
    {
        Instantiate(powerupParticle, transform.position, powerupParticle.transform.rotation);
    }

    void ParticlesOnHealth()
    {
        Instantiate(healthParticle, transform.position, healthParticle.transform.rotation);
    }  
}
