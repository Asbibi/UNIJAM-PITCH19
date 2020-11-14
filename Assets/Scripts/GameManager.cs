using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager instance;

    [SerializeField] PlayerControler player = null;
    [SerializeField] MasterController master = null;
    public float gameTime = 60;

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
        master.InitTimes(4, gameTime);
    }

    //Fonction permettant de replacer le joueur devant son maître
    private void replacementPlayer()
    {
        instance.master.Stop();
        instance.player.setCanMove(false);
        instance.player.setReplacement(true);
        instance.master.Resume();
    }

    // Actions et animations à effectuer quand le maître repère le joueur
    static public void MasterChecking()
    {
        if (instance != null)
        {
            //Si le joueur est derrière le maitre
            if(instance.master.transform.position.x > instance.player.transform.position.x + 2)//2 étant la distance tolérée derrière le maitre
            {
                instance.replacementPlayer();
            }
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
    }

    // Actions et animations à effectuer quand la partie est finie
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
}
