using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Metronome : MonoBehaviour
{

    bool onBeat;
    public float beatDuration;
    public float bpm;


    public GameObject volumeObj;
    Volume volume;
    float effectWeight;


    // Start is called before the first frame update
    void Start()
    {
        volume = volumeObj.GetComponent<Volume>();

        StartCoroutine(RhythmKeeper());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (onBeat)
        {
            effectWeight += Time.deltaTime / 0f;
            if (effectWeight > 1)
            {
                effectWeight = 1;
            }
            volume.weight = effectWeight;
        }
        else
        {
            effectWeight -= Time.deltaTime / 0.1f;
            if (effectWeight < 0)
            {
                effectWeight = 0;
            }
            volume.weight = effectWeight;
        }


    }



    IEnumerator RhythmKeeper()
    {
        onBeat = true;

        yield return new WaitForSeconds(beatDuration / 2);

        while (true)
        {
            onBeat = false;

            yield return new WaitForSeconds((1 /(bpm / 60)) - beatDuration);

            onBeat = true;

            yield return new WaitForSeconds(beatDuration);
        }
    }

    public bool IsOnBeat()
    {
        return onBeat;
    }


}
