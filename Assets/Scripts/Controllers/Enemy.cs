using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  //Float variables
  public float speed = 5f;
  private float speedOnPowerup = 10f;
  private float normalSpeed = 5f;

  //Int variables
  public int randomPickup;
  public int randomNumber;
  private int maxChance = 11;
  private int minChance = 1;

  //Bool variables
  public bool hasPowerup = false;

  //GameObjects variables
  private GameObject player;
  public GameObject powerup;

  //Renderers variables
  private SpriteRenderer enemySpriteRenderer;
  private TrailRenderer powerupEnemyTrail;

  //Scripts variables
  private PlayerController playerController;


  void Start()
  {

    enemySpriteRenderer = GetComponent<SpriteRenderer>();
    powerupEnemyTrail = GetComponent<TrailRenderer>();
    player = GameObject.Find("Player");
    playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    enemySpriteRenderer.color = Color.cyan;
  }

  void Update()
  {

    randomPickup = Random.Range(minChance, maxChance);
    randomNumber = Random.Range(minChance, maxChance);

    if (!playerController.gameOver)
    {
      Vector3 lookDirection = (player.transform.position - transform.position).normalized;

      transform.Translate(lookDirection * speed * Time.deltaTime);
    }

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Powerup") && randomPickup == randomNumber)
    {
      speed = speedOnPowerup;
      hasPowerup = true;

      Destroy(other.gameObject);

      powerupEnemyTrail.enabled = true;
      StartCoroutine(EnemyPowerupCountdownRoutine());
      enemySpriteRenderer.color = Color.yellow;

    }
  }
  IEnumerator EnemyPowerupCountdownRoutine()
  {
    yield return new WaitForSeconds(3);
    hasPowerup = false;

    powerupEnemyTrail.enabled = false;

    speed = normalSpeed;
    enemySpriteRenderer.color = Color.cyan;
  }
}
