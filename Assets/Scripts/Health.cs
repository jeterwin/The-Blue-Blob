using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;

    public float HP = 30;

    [SerializeField] float InvincibilityTime;

    public GameObject DeathUI;

    [SerializeField] Image HeartBar;

    [SerializeField] Image FullHeartBar;

    [SerializeField] AudioSource AudioSource;

    [SerializeField] AudioClip DeathSFX;

    [SerializeField] AudioClip DamageTakenSFX;

    Animator HealthAnimator;

    bool CanTakeDamage = true;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        HP = PlayerPrefs.GetInt("Health", 3);
        FullHeartBar.fillAmount = HP / PlayerPrefs.GetInt("MaxHealth", 6);
        HeartBar.fillAmount = HP / PlayerPrefs.GetInt("MaxHealth", 6);

        HealthAnimator = HeartBar.GetComponent<Animator>();
    }
    public IEnumerator TakeDamage(int Damage, float JumpMultiplier, bool ShouldGetInvincibility)
    {
        if(CanTakeDamage)
        {            
            if(ShouldGetInvincibility)
            {
                CanTakeDamage = false;
                StartCoroutine(Damageable());
            }
            HealthAnimator.Play("Damage");
            AudioSource.PlayOneShot(DamageTakenSFX);
            HP -= Damage;            
            HeartBar.fillAmount = HP / PlayerPrefs.GetInt("MaxHealth", 6);
            if(HP == 0)
            {
                DeathUI.SetActive(true);
                AudioSource.PlayOneShot(DeathSFX);
                MovementScript.Instance.CanMove = false;
            }
            MovementScript.Instance.rb.AddForce(Vector2.up * MovementScript.Instance.JumpHeight * JumpMultiplier, ForceMode2D.Impulse);        

            yield return null;
        }
        else
            yield return null;
    }
    IEnumerator Damageable()
    {
        float ElapsedTime = 0;
        while(ElapsedTime < InvincibilityTime)
        {
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        CanTakeDamage = true;
        yield return null;
    }
}