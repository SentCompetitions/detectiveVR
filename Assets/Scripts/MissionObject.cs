using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionObject : MonoBehaviour
{
    public Mission mission;
    public string missionText;
    public bool activateNow = false;
    public int missionId { get; private set; }
    [Space]
    public MissionObject nextMission;
    [Space]
    public bool isProgress;
    public int maxProgress;
    [Space]
    public Overlay overlay;

    // Start is called before the first frame update
    void Start()
    {
        if (isProgress) mission = new Mission(this, missionText, maxProgress);
        else mission = new Mission(this, missionText);
        missionId = Storage.addMission(mission);
        Debug.Log("[MissionManager] Added mission '" + mission.text + "' (" + missionId + ")");
        if (activateNow)
        {
            Debug.Log("[MissionManager] Activated mission '" + mission.text + "' (" + missionId + ")");
            overlay.SetText(mission.text, "Текущая миссия:");
            Storage.MissionId = missionId;
        }
    }

    public void EndMission()
    {
        if (!enabled) return;
        Debug.Log("[MissionManager] End mission '" + mission.text + "' (" + missionId + ")");
        overlay.SetText(mission.text, "Миссия завершенна:");
        enabled = false;
        if (nextMission != null)
        {           
            Storage.MissionId = Storage.missions.IndexOf(nextMission.mission);
        }
        else
        {
            Storage.MissionId = Storage.addMission(new Mission(this, "Конец!"));
        }
    }
}
