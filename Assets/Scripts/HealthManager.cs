using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float invincibilityLength;
    private float invincibilityCounter;
    public Renderer playerRenderer;
    public PlayerController thePlayer;
    private float flashCounter;
    public float flashLength = 0.1f;
    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;
    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;
    
    void Start()
    {
        currentHealth = maxHealth;        
        respawnPoint= thePlayer.transform.position;
    }

    
    void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack) 
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                thePlayer.Knockback(direction);
                invincibilityCounter = invincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    public void Respawn()
    {
        
        
        //player.transform.position = transform.position;
        //currentHealth = maxHealth;
        
        
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        GameObject player = GameObject.Find("Player");
        CharacterController characterController = player.GetComponent<CharacterController>();
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        yield return new WaitForSeconds(respawnLength);
        isFadeToBlack = true;
        
        characterController.enabled = false;
        yield return new WaitForSeconds(waitForFade);
        isFadeToBlack= false;
        isFadeFromBlack = true;
        isRespawning = false;
        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;
        characterController.enabled = true;
        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint= newPosition;
    }
}
