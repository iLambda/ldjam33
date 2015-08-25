using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class CombatMusicControl : MonoBehaviour {

    public AudioMixerSnapshot inScene1;
    public AudioMixerSnapshot inScene2;
    public AudioMixerSnapshot inScene3;

    //public float bpm = 120f;

    //private float m_TransitionIn;
    //private float m_TransitionOut;
    //private float m_QuarterNote;
    private float m_TransitionTime;

	// Use this for initialization
	void Start () {
        m_TransitionTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0) return;
        if ((StatusUpdater.humanCount <= 2) )
        {
            //Debug.Log("Hi from sound manager !");
            if ( (StatusUpdater.humanCount <= 1) && (ApocalypticPopManager.wavesNumber > 0) )
            {
             Debug.Log("No Humans yet");
             if (ApocalypticPopManager.HumanWavesNumber < ApocalypticPopManager.wavesNumber/3)
             {
                inScene3.TransitionTo(m_TransitionTime);
                Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber);
             }
             else if (ApocalypticPopManager.HumanWavesNumber < 2*ApocalypticPopManager.HumanWavesNumber / 3)
             {
                 inScene2.TransitionTo(m_TransitionTime);
                 Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber);
             }
             else
             {
                 inScene1.TransitionTo(m_TransitionTime);
                 Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber);
             }
         }



        }
	}
}
