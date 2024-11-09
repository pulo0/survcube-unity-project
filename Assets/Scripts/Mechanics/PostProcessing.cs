using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    //Float variables
    private float speed = 5f;
    private float maxCAIntensity = 1f;
    private float minCAIntensity = 0f;
    private float maxBloomIntensity = 15f;
    private float minBlommIntensity = 6f;

    //Bool variables
    public bool canChangePitch = false;

    //Scripts variables
    private PlayerController playerController;

    //Post Process variables
    public PostProcessVolume postProcessVolume;
    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    void Update()
    {
        if(playerController.hasPowerup)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, maxCAIntensity, speed * Time.deltaTime);
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, maxBloomIntensity, speed * Time.deltaTime);
            canChangePitch = true;
            StartCoroutine(EffectCountdown());
        }
    }

    public IEnumerator EffectCountdown()
    {
        yield return new WaitForSeconds(5);
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, minCAIntensity, speed * Time.deltaTime);
        bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, minBlommIntensity, speed * Time.deltaTime);
        canChangePitch = false;
    }
}
