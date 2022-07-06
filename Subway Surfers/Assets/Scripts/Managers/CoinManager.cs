using UnityEngine;
public class CoinManager : MonoBehaviour
{
    public float CoinCount;
    public UIManager UIManager;
    [SerializeField]private float scorePerSecond = 1f;

    private void Update()
    {
        if(GameManager.Instance.GameOver)
        {
            return;
        }
        if (!GameManager.Instance.GamePaused)
        {
            CoinCount += scorePerSecond * Time.deltaTime;
        }
        UIManager.ScoreUpdate(CoinCount);
    }
}
