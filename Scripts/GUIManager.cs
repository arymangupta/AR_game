using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIManager : MonoBehaviour
{
    [Header("Header UI Elements")]
    public Button ChatButton;
    public Button AudioButton;
    public Button LeaderBoard;

    [Space(10, order = 0)]
    [Header("Audio Elements", order = 1)]
    public GameObject AudioSource;
    public Sprite AudioON;
    public Sprite AudioOFF;
    bool IsAudioOn;

    [Header("STATUS")]
    public Animator Bloodanim;
    public Text ActiveCount;
    public Text EliminatedCount;
    public Text KillCount;
    int active, eliminated, killcount;
    //[ColorUsage(true, true, 0.0f, 0.5f, 0.0f, 0.5f)]
    //[SerializeField]
    //private Color colorHdr;
    void Start()
    {
        UnnyNet.UnnyNetBase.InitializeUnnyNet();

        ChatButton.onClick.AddListener(ChatOpen);

        LeaderBoard.onClick.AddListener(LeaderBoardOpen);

        AudioButton.onClick.AddListener(AudioController);   
    }

      void ChatOpen()
    {
        Debug.Log("Chat Button Pressed");
        UnnyNet.UnnyNetBase.OpenUnnyNet();
    }

    void LeaderBoardOpen()
    {
        UnnyNet.UnnyNet.OpenLeaderboards();
    }

     void AudioController()
    {
        if (IsAudioOn)
        {
            Debug.Log("IsAudioOn"+IsAudioOn);
            AudioSource.SetActive(true);
            AudioButton.GetComponent<Image>().sprite = AudioON;
            IsAudioOn = false;
        }
        else
        {
            Debug.Log("IsAudioOn" + IsAudioOn);
            AudioSource.SetActive(false);
            AudioButton.GetComponent<Image>().sprite = AudioOFF;
            IsAudioOn = true;
        }
    }

    private void OnEnable()
    {
        BombEnemyLVL1.IsAttacking += ShowBlood;
        BombEnemyLVL1.IsNotAttacking += HideBlood;
        BombEnemyLVL1.IsActive += UpdateActiveCount;
        BombEnemyLVL1.IsEliminated += UpdateEliminateCount;
    }
    void ShowBlood()
    {
        Bloodanim.SetTrigger("showblood");
    }
    void HideBlood()
    {
        Bloodanim.SetTrigger("hideblood");
    }

    void UpdateActiveCount()
    {
        active++;
        ActiveCount.text = "Active: " + active.ToString();
    }

    void UpdateEliminateCount()
    {
        active--;
        eliminated++;
        ActiveCount.text = "Active: " + active.ToString();
        EliminatedCount.text = "Eliminated: " + eliminated.ToString();
    }

    public void updateKillCount()
    {
        KillCount.text = "SCORE: " + eliminated.ToString();
    }

}
