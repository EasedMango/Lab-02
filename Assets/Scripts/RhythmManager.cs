using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    public float firstBeatOffset;

    public float beatsPerLoop;

    public int completedLoops = 0;

    public float loopPositionInBeats;

    public float loopPositionInAnalog;

    public static RhythmManager instance;

    public float timeBetweenBeats;


    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        secPerBeat = 60 / songBpm;

        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();
    }

    public bool OnBeat(float shootEveryXBeats)
    {

       return (RhythmManager.instance.loopPositionInBeats % shootEveryXBeats) <= 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime-firstBeatOffset);

        songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;
        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }
}
