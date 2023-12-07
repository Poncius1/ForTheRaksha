using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public AudioClip completeEffect;


    public List<Mission> activeMissions = new List<Mission>();
    public List<Mission> completedMissions = new List<Mission>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddMission(Mission mission)
    {
        if (!activeMissions.Contains(mission))
        {
            activeMissions.Add(mission);
        }
    }

    public void CompleteMission(Mission mission)
    {
        if (activeMissions.Contains(mission))
        {
            SoundManager.Instance.PlayEffect(completeEffect);
            activeMissions.Remove(mission);
            completedMissions.Add(mission);
        }
    }

    public bool IsMissionActive(Mission mission)
    {
        return activeMissions.Contains(mission);
    }

    public bool IsMissionCompleted(Mission mission)
    {
        return completedMissions.Contains(mission);
    }

    public Mission GetMission(Type missionType)
    {
        foreach (Mission mission in activeMissions)
        {
            if (mission.GetType() == missionType)
            {
                return mission;
            }
        }
        return null;
    }
}
