using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Slidingfight : MonoBehaviour
{
    [SerializeField] GameObject LeftBorder, RightBorder, SlidingBar, RightStopper, LeftStopper, CenterHitPart;
    public bool Attack = true;
    [SerializeField] private GameObject[] CloseSideParts , FarSideParts;
    [SerializeField] private GameObject VictoryButton, MissedButton, HitButton, EnemyImage;
    [SerializeField] private GameObject EnemyForegroundHealthBar, PlayerForegroundHealthBar;
    [SerializeField] private float DamageOfPlayer = 0.15f, DamageOfEnemy=0.33f;
    private Color ChangingColor;
    private bool EnemyWasHit = false;

    private bool GoRight = true, GoLeft = false;

    private void Start()
    {
        ChangingColor = EnemyImage.GetComponent<Image>().color;
    }
    private void Update()
    {
        if(EnemyWasHit)
        {
            if (ChangingColor.b <= 0)
                EnemyWasHit = false;
            ChangingColor.b -= Time.deltaTime*3;
            ChangingColor.g -= Time.deltaTime * 3;
            EnemyImage.GetComponent<Image>().color = ChangingColor;
        }
        else if(ChangingColor.b<=1)
        {
            ChangingColor.b += Time.deltaTime * 3;
            ChangingColor.g += Time.deltaTime * 3;
            EnemyImage.GetComponent<Image>().color = ChangingColor;
        }
        
        if (Attack)
        {
            HandleMovement();
            if (Input.GetMouseButtonDown(0))
            {
                GetClick();
            }
        }
    }
    private IEnumerator FadeInCanvas()
    {
        if (GetComponent<CanvasGroup>().alpha >= 1f)
            StopAllCoroutines();
        GetComponent<CanvasGroup>().alpha += 0.01f;
        yield return new WaitForSecondsRealtime(0.01f);
        yield return FadeInCanvas();
    }
    private IEnumerator FadeOutCanvas()
    {
        if (GetComponent<CanvasGroup>().alpha <= 0f)
        {
            StopAllCoroutines();
        }
        GetComponent<CanvasGroup>().alpha -= 0.01f;
        yield return new WaitForSecondsRealtime(0.01f);
        yield return FadeOutCanvas();
    }
    private void Break()
    {
        StopAllCoroutines();
        Attack = false;
        StartCoroutine(FadeOutCanvas());
    }
    public void StartTheFight()
    {
        StopAllCoroutines();
        Attack = true;
        StartCoroutine(FadeInCanvas());
    }
    private void HandleMovement()
    {
        if (GoRight)
        {
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, RightBorder.transform.position, 1.5f * Time.deltaTime);
            if (SlidingBar.transform.position.x > RightStopper.transform.position.x)
            {
                GoLeft = true;
                GoRight = false;
            }
        }
        else if (GoLeft)
        {
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, LeftBorder.transform.position, 1.5f * Time.deltaTime);
            if (SlidingBar.transform.position.x < LeftStopper.transform.position.x)
            {
                GoRight = true;
                GoLeft = false;
            }
        }
    }
    public void ApplyDamageToPlayer()
    {
        PlayerForegroundHealthBar.GetComponent<Image>().fillAmount -= DamageOfEnemy;
    }
    private void ApplyDamageToEnemy(float Damage)
    {
        EnemyForegroundHealthBar.GetComponent<Image>().fillAmount -= Damage;
        if (EnemyForegroundHealthBar.GetComponent<Image>().fillAmount <= 0f)
        {
            VictoryButton.GetComponent<Button>().onClick.Invoke();
        }
        Break();
    }
    private void GetClick()
    {
        bool HitEnemy = false;
        if (Mathf.Abs(CenterHitPart.transform.position.x - SlidingBar.transform.position.x) < 0.7f)
        {
            HitEnemy = true;
            ApplyDamageToEnemy(DamageOfPlayer * 2f);
        }
        else
        {
            for (int i = 0; i < CloseSideParts.Length; i++)
            {
                if (Mathf.Abs(CloseSideParts[i].transform.position.x - SlidingBar.transform.position.x) < 0.7f)
                {
                    HitEnemy = true;
                    ApplyDamageToEnemy(DamageOfPlayer);
                }
            }
            for (int i = 0; i < FarSideParts.Length; i++)
            {
                if (Mathf.Abs(FarSideParts[i].transform.position.x - SlidingBar.transform.position.x) < 0.7f)
                {
                    HitEnemy = true;
                    ApplyDamageToEnemy(DamageOfPlayer / 2);
                }
            }
        }
        if (!HitEnemy)
        {
            Attack = false;
            StopAllCoroutines();
            StartCoroutine(FadeOutCanvas());
            MissedButton.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            if (EnemyForegroundHealthBar.GetComponent<Image>().fillAmount > 0f)
            {
                Attack = false;
                HitButton.GetComponent<Button>().onClick.Invoke();
                EnemyWasHit = true;
            }
        }
        HitEnemy = false;
    }
}
