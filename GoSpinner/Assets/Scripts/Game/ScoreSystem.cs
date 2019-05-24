using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int NowScore;
    [SerializeField] private GameSettingsData Settings;
    [Header("Visual parametrs")]
    [SerializeField] private Text TextScore;
    [SerializeField] private GameObject Panelover;
    [SerializeField] private Text OverTextScore;
    [SerializeField] private Text CoinsHereText;
    [Header("Game parametrs")]
    [SerializeField] private int EveryScoreAddHard = 20;
    [Header("Hard parametrs")]
    [SerializeField] private float MaxSpeed = 7;
    [SerializeField] private float AddToSpeedEveryHard = 0.2f;
    [Space(5)]
    [SerializeField] private int MinOffsetKillZones = 6;
    [SerializeField] private float MinusOffsetEveryHard = 0.2f;

    private int milseconds = 0;

    private int HardCount = 0;

    private bool CanScore = true;

    MoveRotate MR;
    CreateKillZones CKZ;

    private SpinerData NowChoosedSpiner;

    private void Start()
    {
        if (Settings.NowChoosedSpiner == null)
            SceneManager.LoadScene(0);

        // Instalation
        MR = FindObjectOfType<MoveRotate>();
        CKZ = FindObjectOfType<CreateKillZones>();

        TextScore.text = "";
        CoinsHereText.text = "";

        Panelover.SetActive(false);

        NowChoosedSpiner = Settings.NowChoosedSpiner;
    }
    private void FixedUpdate()
    {
        if (CanScore)
            CountScore();
    }

    public void LoseGame()
    {
        Panelover.SetActive(true);
        OverTextScore.text = string.Format("Your score: {0}", NowScore);

        FindObjectOfType<cameraGo>().SetLosedCamera();

        FindObjectOfType<MoveRotate>().CurrentSpeedMove = 0;
        FindObjectOfType<CreatePlatforms>().CanSpawn = false;
        FindObjectOfType<CreateKillZones>().CanSpawn = false;
        CanScore = false;
    }
    public void AddCoin()
    {
        Settings.AllCoins++;
        SetAllTexts();
        PlayerPrefs.SetInt("AllCoins", Settings.AllCoins);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void CountScore()
    {
        if (milseconds != 35)
            milseconds++;
        else
        {
            milseconds = 0;
            NowScore++;           
            SetAllTexts();
            CheckHard();
            AddRang();
        }
    }
    private void SetAllTexts()
    {
        TextScore.text = NowScore.ToString();
        CoinsHereText.text = Settings.AllCoins.ToString();
    }
    private void CheckHard()
    {
        if (MR.CurrentSpeedMove >= MaxSpeed && CKZ.OffsetNow <= MinOffsetKillZones)
            return;

        HardCount++;

        if (HardCount == EveryScoreAddHard)
        {
            if (MR.CurrentSpeedMove < MaxSpeed)
                MR.CurrentSpeedMove += AddToSpeedEveryHard;
            if (CKZ.OffsetNow > MinOffsetKillZones)
                CKZ.OffsetNow -= MinusOffsetEveryHard;

            print("Add hard level");

            HardCount = 0;
        }
    }
    private void AddRang()
    {
        NowChoosedSpiner.NowCountRang++;
        Settings.AllRangs++;

        PlayerPrefs.SetInt("NowCountRang" + NowChoosedSpiner.ID, NowChoosedSpiner.NowCountRang);
        PlayerPrefs.SetInt("AllRangs", Settings.AllRangs);

        if (NowChoosedSpiner.NowCountRang == Settings.AviableRangs[NowChoosedSpiner.NowRang].CountToHave)
        {
            NowChoosedSpiner.NowCountRang = 0;
            NowChoosedSpiner.NowRang++;
            PlayerPrefs.SetInt("NowRang" + NowChoosedSpiner.ID, NowChoosedSpiner.NowRang);
        }
    }
}
