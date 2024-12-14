using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNhiemVu : MonoBehaviour
{
    public NPC npc; // Tham chiếu đến NPC

    private void OnDestroy()
    {
        if (npc != null)
        {
            npc.UpdateQuestProgress();
        }
    }
}
