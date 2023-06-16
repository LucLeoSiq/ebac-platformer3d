using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 0;

    private bool checkpointActivated = false;
    private string checkpointKey = "CheckpointKey";
    public SFXType sfxType;

    private void OnTriggerEnter(Collider other)
    {
        // Checks if Player gets in range of the CheckPoint Collider 
        if(!checkpointActivated && other.transform.tag == "Player")
        { 
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckPoint();
        SaveManager.Instance.SaveLastCheckpoint(key);
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        PlaySFX();
    }
    
    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckPoint()
    {
        if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
            PlayerPrefs.SetInt(checkpointKey, key);

        CheckpointManager.Instance.SaveCheckPoint(key);

        checkpointActivated = true;
    }
}
