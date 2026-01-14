using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    bool onBeat;
    public float beatDuration;
    public float bpm;



    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RhythmKeeper());


    }

    // Update is called once per frame
    void Update()
    {
        
        


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
