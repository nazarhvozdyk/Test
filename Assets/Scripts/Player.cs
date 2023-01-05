using UnityEngine;

public class Player : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
        GameManager.Instance.OnPlayerDead();
    }
}
