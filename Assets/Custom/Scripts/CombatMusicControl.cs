using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class CombatMusicControl : MonoBehaviour
{

    public AudioMixerSnapshot inScene1;
    public AudioMixerSnapshot inScene2;
    public AudioMixerSnapshot inScene3;
    private float m_TransitionTime;

    // Use this for initialization
    void Start()
    {
        m_TransitionTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if ((StatusUpdater.humanCount <= 2))
        {
            Debug.Log(inScene3.name + inScene2.name + inScene1.name);
            if ((StatusUpdater.humanCount <= 1) && (ApocalypticPopManager.wavesNumber > 0))
            {
                Debug.Log("No Humans yet");
                if (ApocalypticPopManager.HumanWavesNumber < ApocalypticPopManager.wavesNumber / 3)
                {
                    inScene3.TransitionTo(m_TransitionTime);
                    Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber + " " + inScene3.name);
                }
                else if (ApocalypticPopManager.HumanWavesNumber < 2 * ApocalypticPopManager.HumanWavesNumber / 3)
                {
                    inScene2.TransitionTo(m_TransitionTime);
                    Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber + " " + inScene3.name);
                }
                else
                {
                    inScene1.TransitionTo(m_TransitionTime);
                    Debug.Log("Wave : " + ApocalypticPopManager.HumanWavesNumber + " " + inScene3.name);
                }
            }
        }
    }
}
