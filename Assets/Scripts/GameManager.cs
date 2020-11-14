using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager instance;

    [SerializeField] PlayerControler player = null;
    [SerializeField] MasterController master = null;

    private void Awake()
    {
        if (instance != null)
        {
            // Detruire les objets liés à l'instance dont le joueur
            Destroy(player.gameObject);
            Destroy(master.gameObject);
            // Detruire le GameManager
            Destroy(gameObject); // attention à toujours detruire le gameobject sinon ca detruit juste le com^ponent
        }
        else
        {
            instance = this;
            // eventuellement faire des initialisations
        }

    }


  




    // Example static public
    static public void MethodExample()
    {
        if (instance != null)
        {
            // ....
            instance.doSmtgExample();
            // ....
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
    }

    // Example private
    private void doSmtgExample()
    {

    }
}
