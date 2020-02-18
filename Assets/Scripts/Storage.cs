using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Storage
{
    public static GameBook gameBook;
    public static List<Mission> missions { get; set; }

    private static int missionId;
    public static int MissionId
    {
        get { return missionId; }
        set
        {
            missionId = value;
            gameBook.UpdateMission(missions[value]);
        }
    }
    public static int addMission(Mission mission)
    {
        try { missions.Add(mission); }
        catch (NullReferenceException) { 
            missions = new List<Mission>(); 
            missions.Add(mission); 
        };
        return missions.Count - 1;
    }


    private static int evidenceCount;
    public static int EvidenceCount
    {
        get {
            evidenceCount++;
            return evidenceCount;
        }
    }
}

public class Mission
{
    public string text;
    public int maxProgress = 1;
    public int progress = 0;
    public MissionObject missionObject;

    public Mission(MissionObject missionObject, string text)
    {
        this.text = text;
        this.missionObject = missionObject;
    }

    public Mission(MissionObject missionObject, string text, int maxProgress)
    {
        this.text = text;
        this.maxProgress = maxProgress;
        this.missionObject = missionObject;
    }

    public void AddProgress()
    {
        progress++;
        Storage.gameBook.UpdateMission(this);
        if (progress >= maxProgress)
        {
            missionObject.EndMission();
        }
    }
}
