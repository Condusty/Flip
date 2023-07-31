using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem finishParticle;

    private AudioSource audioSource;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(finishParticle, transform.position, Quaternion.identity);
        audioSource.Play();
        StartCoroutine(LoadLevelSelect());
    }

    private IEnumerator LoadLevelSelect()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LevelSelect");
    }
}
