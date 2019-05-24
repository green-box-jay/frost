using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [Header("Object parametrs")]
    [SerializeField] private GameObject PanelSettings;
    [SerializeField] private GameObject SpinerShowObject;
    [SerializeField] private Button Buy_but;
    [SerializeField] private Button Go_but;
    [Header("Rotate spiner parametrs")]
    [SerializeField] private float SpeedRotateSpiner = 10;
    [Header("Data parametrs")]
    [SerializeField] private GameSettingsData Settings;
    [SerializeField] private SpinerData[] AllSpinners;
    [SerializeField] private List<SpinerData> BoughtSpiners;
    [Header("Visual parametrs")]
    [SerializeField] private Text NameSpinerText;
    [SerializeField] private Image RangLevelimg;
    [SerializeField] private Text AllRangsText;
    [SerializeField] private Text AllCoinsText;
    [SerializeField] private Slider RangNowSlider;

    private float RightSpeed;
    private int RandomWait;

    private int NowIndex = 0, MaxIndex;

    RangSystem RS;

    private void Awake()
    {
        
    }
    private void Start()
    {
        InActiveAllObjects();
        GetSave();

        ShowInfoSpiner(AllSpinners[0]);

        // Instalation
        RightSpeed = SpeedRotateSpiner;
        MaxIndex = AllSpinners.Length - 1;
        RS = GetComponent<RangSystem>();

        // Random spin
        RandomWait = Random.Range(0, 10);
        StartCoroutine(ChangeRotationSpin());
    }
    private void FixedUpdate()
    {
        SpinObjectShow();
    }

    public void Button(string Action)
    {
        switch (Action)
        {
            case "OpenSettings":
                PanelSettings.SetActive(true);
                break;
            case "ExitGame":
                Application.Quit();
                break;
        }
    }
    public void Arrow(bool IsRight)
    {
        if (IsRight)
        {
            if (NowIndex != MaxIndex)
                NowIndex++;
            else
                NowIndex = 0;
        }
        else
        {
            if (NowIndex != 0)
                NowIndex--;
            else
                NowIndex = MaxIndex;
        }

        ShowInfoSpiner(AllSpinners[NowIndex]);
    }
    public void TakeSpiner(SpinerData spinerData)
    {
        BoughtSpiners.Add(spinerData);
        spinerData.IsBought = true;
        Save();
    }
    public void Go(SpinerData spinerData)
    {
        Settings.NowChoosedSpiner = spinerData;
        SceneManager.LoadScene(1);
    }

    private void ShowInfoSpiner(SpinerData spinerData)
    {
        // Set texts
        NameSpinerText.text = spinerData.Name;
        // Set buttons
        Buy_but.onClick.RemoveAllListeners();
        Go_but.onClick.RemoveAllListeners();

        Buy_but.interactable = !spinerData.IsBought;
        Go_but.interactable = spinerData.IsBought;

        // Show mesh
        MeshFilter MF = SpinerShowObject.GetComponent<MeshFilter>();
        MF.mesh = spinerData.MeshSpiner;

        if (!spinerData.IsBought)
            Buy_but.onClick.AddListener(delegate { TakeSpiner(spinerData); ShowInfoSpiner(spinerData); });
        else
            Go_but.onClick.AddListener(delegate { Go(spinerData); });

        // Show level parametrs
        CountRang CR = Settings.AviableRangs[spinerData.NowRang];

        RangNowSlider.maxValue = CR.CountToHave;
        RangNowSlider.value = spinerData.NowCountRang;

        if (CR.AvatarRang != null)
            RangLevelimg.sprite = CR.AvatarRang;
    }
    private void InActiveAllObjects()
    {
        PanelSettings.SetActive(false);
        AllRangsText.text = "0";
    }
    private void SpinObjectShow()
    {
        Transform transformSpiner = SpinerShowObject.transform;

        transformSpiner.Rotate(new Vector3(0, 0, RightSpeed));
    }

    private IEnumerator ChangeRotationSpin()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomWait);
            float lastspeed = RightSpeed;
            RightSpeed = 0;
            yield return new WaitForSeconds(0.1f);
            RightSpeed = lastspeed;
            RandomWait = Random.Range(0, 10);
            RightSpeed = -RightSpeed;
        }
    }

    #region

    private void ResetAllDatas()
    {
        for (int i = 0; i < AllSpinners.Length; i++)
        {
            SpinerData spiner = AllSpinners[i];

            spiner.IsBought = false;
            spiner.NowCountRang = 0;
            spiner.NowRang = 0;
        }

        Settings.AllRangs = 0;
        Settings.AllCoins = 0;
    }
    private void Save()
    {
        if (BoughtSpiners.Count != 0)
        {
            print("Saved bought spiners");

            PlayerPrefs.SetInt("CountBS", BoughtSpiners.Count);

            for (int i = 0; i < BoughtSpiners.Count; i++)
            {
                PlayerPrefs.SetString("BSID" + i, BoughtSpiners[i].ID);
            }
        }
    }
    private void GetSave()
    {
        ResetAllDatas();

        if (PlayerPrefs.HasKey("CountBS"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("CountBS"); i++)
            { 
                for (int g = 0; g < AllSpinners.Length; g++)
                {
                    if (AllSpinners[g].ID == PlayerPrefs.GetString("BSID" + i))
                    {
                        SpinerData spiner = AllSpinners[g];

                        BoughtSpiners.Add(spiner);
                        spiner.IsBought = true;

                        if (PlayerPrefs.HasKey("NowRang" + spiner.ID))
                            spiner.NowRang = PlayerPrefs.GetInt("NowRang" + spiner.ID);
                        if (PlayerPrefs.HasKey("NowCountRang" + spiner.ID))
                            spiner.NowCountRang = PlayerPrefs.GetInt("NowCountRang" + spiner.ID);
                        break;
                    }
                }
            }
        }
        if (PlayerPrefs.HasKey("AllRangs"))
        {
            Settings.AllRangs = PlayerPrefs.GetInt("AllRangs");
            AllRangsText.text = Settings.AllRangs.ToString();
        }
        if (PlayerPrefs.HasKey("AllCoins"))
        {
            Settings.AllCoins = PlayerPrefs.GetInt("AllCoins");
            AllCoinsText.text = Settings.AllCoins.ToString();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        Save();
    }
    private void OnApplicationPause(bool pause)
    {
        Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}