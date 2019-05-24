using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game Settings")]
public class GameSettingsData : ScriptableObject
{
    public SpinerData NowChoosedSpiner;
    public int AllRangs;
    public int AllCoins;
    public CountRang[] AviableRangs;
}
[System.Serializable]
public class CountRang
{
    public int CountToHave;
    public Sprite AvatarRang;
}
