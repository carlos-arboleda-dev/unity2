using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class colectable : MonoBehaviour
{
    public enum CollectableType { Standard, SpeedBoost }
    public CollectableType type = CollectableType.Standard;
    public float speedMultiplier = 1.5f;
    public float boostDuration = 3f;
    public Color speedBoostColor = Color.green;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            move player = other.GetComponent<move>();
            Renderer playerRenderer = other.GetComponent<Renderer>();

            if (player != null)
            {
                if (type == CollectableType.SpeedBoost)
                {
                    StartCoroutine(ApplySpeedBoost(player, playerRenderer));
                }
                
                player.cambioObjeto = true;
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ApplySpeedBoost(move player, Renderer playerRenderer)
    {
        Color originalColor = playerRenderer.material.color;
        float originalSpeed = player.speed;
        
        player.speed *= speedMultiplier;
        playerRenderer.material.color = speedBoostColor;
        
        yield return new WaitForSeconds(boostDuration);
        
        player.speed = originalSpeed;
        playerRenderer.material.color = originalColor;
    }
}