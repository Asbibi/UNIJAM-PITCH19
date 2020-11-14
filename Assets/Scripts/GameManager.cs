using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager instance;

    [SerializeField] PlayerControler player = null;
    [SerializeField] MasterController master = null;
    public float gameTime = 60;

    public delegate void troubleDone(Vector3 playerPosition);
    static public event troubleDone onTroubleDone;

    private void Awake()
    {
        if (instance != null)
        {
            // Detruire les objets liés à l'instance dont le joueur
            Destroy(player.gameObject);
            Destroy(master.gameObject);
            // Detruire le GameManager
            Destroy(gameObject); // attention à toujours detruire le gameobject sinon ca detruit juste le component
        }
        else
        {
            instance = this;
            StartGame();
        }

    }

    private void StartGame()
    {
        master.InitTimes(0, gameTime);
    }

    // Example static public
    static public void EndGame()
    {
        if (instance != null)
        {
            //
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
    }

    static public void NotifyTroubleDone(int actionScore)
    {
        if (instance != null)
        {
            //onTroubleDone(instance.player.transform.position);
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
    }
    static public void ReportPlayer(int sanctionScore)
    {
        //...
        Debug.LogWarning("Reported");
    }
}
