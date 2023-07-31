using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform start;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        transform.position = start.position;
    }

    public void Death()
    {
        transform.position = start.position;
        playerMovement.NormalGravity();
    }
}
