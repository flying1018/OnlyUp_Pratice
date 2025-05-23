using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;
    [SerializeField] private Image HPImage;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
    }
    void Start()
    {
        Health(currentHP, maxHP);
    }


    public void Health(float current, float max)
    {
        HPImage.fillAmount = current / max;
    }

    public void Damage(float damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        Health(currentHP, maxHP);
    }
}
