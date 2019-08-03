using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    Track currTrack;
    [SerializeField] bool[] checkpointStates;
    [SerializeField] private bool startState;
    [SerializeField] int LapNo = 0;

    void Start()
    {
        currTrack = FindObjectOfType<Track>();
        //checkpointStates
    }

    void Update()
    {
        if (startState) {
            bool anyCheckpointsNotCrossed = false;

            foreach (bool checkpointState in checkpointStates) {
                if (checkpointState == false) {
                    anyCheckpointsNotCrossed = true;
                }
            }

            if (!anyCheckpointsNotCrossed) {
                LapNo += 1;
                startState = false;
                for (int i = 0; i < checkpointStates.Length; i++) {
                    checkpointStates[i] = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == currTrack.start.gameObject)
        {
            startState = true;
        }
        else
        {

            Checkpoint[] checkpoints = currTrack.checkpoints;
            int index = 0;

            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (other.gameObject == checkpoint.gameObject)
                {
                    checkpointStates[index] = true;
                }
                index++;
            }
        }
    }
}
