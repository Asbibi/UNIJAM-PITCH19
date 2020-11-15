﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager instance;

    [SerializeField] PlayerControler player = null;
    [SerializeField] MasterController master = null;
    [SerializeField] Canvas canva = null;
    public Camera mainCam = null;
    public float gameTime = 60;

    [Header("Score")]
    [SerializeField] Text scoreText = null;
    [SerializeField] GameObject scoreTextUi = null;
    [SerializeField] GameObject sanctionTextUi = null;

    int score = 0;

    public delegate void troubleDone(Vector3 playerPosition);
    static public event troubleDone onTroubleDone;

    private void Awake()
    {
        if (instance != null)
        {
            // Detruire les objets liés à l'instance dont le joueur
            Destroy(player.gameObject);
            Destroy(master.gameObject);
            Destroy(canva.gameObject);
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

    //Fonction permettant de replacer le joueur devant son maître
    private void replacementPlayer()
    {
        instance.master.Stop();
        instance.player.setCanMove(false);
        instance.player.setReplacement(true);
        instance.master.Resume();
        //ajouter la perte de point sur la distance
    }

    // Actions et animations à effectuer quand le maître repère le joueur
    static public void MasterChecking()
    {
        if (instance != null)
        {
            //Si le joueur est derrière le maitre
            if (instance.master.transform.position.x > instance.player.transform.position.x + 2)//2 étant la distance tolérée derrière le maitre
            {
                instance.replacementPlayer();
            }
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
        if (instance.player.getReplacement())
        {
            instance.master.GetComponent<Animator>().SetBool("lookback", true);
        }
        else
        {
            instance.master.GetComponent<Animator>().SetBool("lookback", false);
        }
    }

    static public Transform GetMasterTransform()
    {
        if (instance != null)
        {
            return instance.master.transform;
        }
        else
        {
            Debug.LogError("GameManager instance null");
            return null;
        }
    }

    // Actions et animations à effectuer quand la partie est finie
    static public void EndGame()
    {
        if (instance != null)
        {
            Vector3 lastCamPos = instance.mainCam.transform.position;
            instance.mainCam.GetComponent<CameraControler>().enabled = false;
            instance.mainCam.transform.position = lastCamPos;
            Vector3 posCamMaster = new Vector3(instance.master.transform.position.x, 0, -10);
            instance.mainCam.transform.position = Vector3.Lerp(instance.mainCam.transform.position, posCamMaster, 2 * Time.deltaTime);
            instance.replacementPlayer();
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
            instance.score += actionScore;
            GameObject text = Instantiate(instance.scoreTextUi, instance.canva.transform);
            text.GetComponent<UIScoreText>().Init("+" + actionScore + " !", instance.mainCam, instance.player.transform.position);
            instance.UpdateTextScore();

            if (onTroubleDone != null)
                onTroubleDone(instance.player.transform.position);
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
    }
    static public void ReportPlayer(int sanctionScore)
    {
        if (instance != null)
        {
            instance.score -= sanctionScore;
            if (instance.score < 0)
                instance.score = 0;
            GameObject text = Instantiate(instance.sanctionTextUi, instance.canva.transform);
            text.GetComponent<UIScoreText>().Init("-" + sanctionScore + "...", instance.mainCam, instance.player.transform.position);
            instance.UpdateTextScore();
        }
        else
        {
            Debug.LogError("GameManager instance null");
        }
        Debug.LogWarning("Reported");
    }
    void UpdateTextScore()
    {
        scoreText.text = "Score : " + score;
    }
}
