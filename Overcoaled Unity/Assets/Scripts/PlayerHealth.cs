using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private float respawnCD;
    public float maxHealth;
     public Transform respawn;
    Rigidbody rb;
    PlayerMove move;
    Animator anim;
    [SerializeField] private GameObject reviveText;
    private int[] hurtSounds = new int[] { 16, 17, 18, 19};


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = GetComponent<PlayerMove>();
        respawn = GameObject.Find("Respawn").transform;
        anim = GetComponent<Animator>();
        
    }

    
    IEnumerator Respawn(float t)
    {
      //  reviveText.SetActive(false);
        GetComponent<PlayerInteraction>().Drop();
        transform.GetComponentInChildren<ParticleSystem>().Stop();
        transform.Find("Player Character").gameObject.SetActive(false);
        transform.position = respawn.position;
        transform.rotation = respawn.rotation;
        health = maxHealth;
        gameObject.GetComponent<Collider>().enabled = false;

        rb.isKinematic = true;
        move.enabled = false;
        yield return new WaitForSeconds(t);

        transform.Find("Player Character").gameObject.SetActive(true);
        transform.GetComponentInChildren<ParticleSystem>().Play();
        gameObject.GetComponent<Collider>().enabled = true;
        rb.isKinematic = false;
        move.enabled = true;
        health = maxHealth;
        anim.SetBool("down", false);
    }

    

    public void TakeDamage()
    {
        AudioManager.SharedInstance.PlayClip(hurtSounds[Random.Range(0, hurtSounds.Length - 1)], 1);
        health -= 1;
        if (health <= 0)
        {
            // StartCoroutine(Respawn(respawnCD));
            DownPlayer();
        }
    }

    private void KillPlayer()
    {
        

        StartCoroutine(Respawn(respawnCD));
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Death")
        {
            AudioManager.SharedInstance.PlayClip(15, 1);
            StartCoroutine(SlowTime());
            anim.SetBool("down", true);
            move.enabled = false;
            Invoke("KillPlayer", 2);
        }
    }

    private void DownPlayer()
    {
       // reviveText.SetActive(true);
        StartCoroutine(SlowTime());
        anim.SetBool("down", true);
        gameObject.tag = "PlayerDown";
        GameManager.GM.PlayerDown(1);
        GetComponent<PlayerMove>().enabled = false;
        GetComponent<PlayerInteraction>().Drop();
        GetComponent<PlayerInteraction>().enabled = false;
    }

    public void RevivePlayer()
    {
       // reviveText.SetActive(false);
        anim.SetBool("down", false);
        gameObject.tag = "Player";
        GameManager.GM.PlayerDown(-1);
        health = maxHealth;
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerInteraction>().enabled = true;
    }

    IEnumerator SlowTime()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSeconds(0.05f);

        Time.timeScale = 1f;
    }
}
