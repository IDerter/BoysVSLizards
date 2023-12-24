using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Slidingfight : MonoBehaviour
namespace BoysVsLizards
{
    [SerializeField] GameObject LeftBorder, RightBorder, SlidingBar, RightStopper, LeftStopper, CenterHitPart;
    public bool Attack = true;
    [SerializeField] private GameObject[] CloseSideParts , FarSideParts;
    [SerializeField] private GameObject VictoryButton, MissedButton, HitButton, EnemyImage;
    [SerializeField] private GameObject EnemyForegroundHealthBar, PlayerForegroundHealthBar;
    [SerializeField] private float DamageOfPlayer = 0.15f, DamageOfEnemy=0.33f;
    [SerializeField] private float _speedSlider;

    private Color ChangingColor;
    private bool EnemyWasHit = false;

    private bool GoRight = true, GoLeft = false;
    public class SlidingFight : MonoBehaviour
    {
        [SerializeField] GameObject LeftBorder, RightBorder, SlidingBar, RightStopper, LeftStopper, CenterHitPart;
        private bool Attack = false;
        [SerializeField] private GameObject[] CloseSideParts, FarSideParts;
        [SerializeField] private GameObject ClickRegisterButton, FlowChart;
        [SerializeField] private Image EnemyForegroundHealthBar, PlayerForegroundHealthBar, EnemyImage;
        public float DamageOfPlayer = 0.15f, DamageOfEnemy = 0.33f;
        private Color ChangingColor;
        private bool EnemyWasHit = false;
        private CanvasGroup FightCanvasCG;
        [SerializeField] private float SliderSpeed;
        [SerializeField] VariableReference Fungusint;

        private bool GoRight = true;

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
        private void Start()
        {
            ChangingColor.b += Time.deltaTime * 3;
            ChangingColor.g += Time.deltaTime * 3;
            EnemyImage.GetComponent<Image>().color = ChangingColor;
            FightCanvasCG = GetComponent<CanvasGroup>();
            ChangingColor = EnemyImage.GetComponent<Image>().color;
        }
        
        if (Attack)
        private void Update()
        {
            HandleMovement();
            if (Input.GetMouseButtonDown(0))
            if (EnemyWasHit)
            {
                GetClick();
                if (ChangingColor.b <= 0)
                    EnemyWasHit = false;
                ChangingColor.b -= Time.deltaTime * 3;
                ChangingColor.g -= Time.deltaTime * 3;
                EnemyImage.color = ChangingColor;
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
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, RightBorder.transform.position, _speedSlider * Time.deltaTime);
            if (SlidingBar.transform.position.x > RightStopper.transform.position.x)
            else if (ChangingColor.b <= 1)
            {
                GoLeft = true;
                GoRight = false;
                ChangingColor.b += Time.deltaTime * 3;
                ChangingColor.g += Time.deltaTime * 3;
                EnemyImage.color = ChangingColor;
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
            if (FightCanvasCG.alpha >= 1f)
                StopAllCoroutines();
            FightCanvasCG.alpha += 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            yield return FadeInCanvas();
        }
        else if (GoLeft)
        private IEnumerator FadeOutCanvas()
        {
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, LeftBorder.transform.position, _speedSlider * Time.deltaTime);
            if (SlidingBar.transform.position.x < LeftStopper.transform.position.x)
            if (FightCanvasCG.alpha <= 0f)
            {
                GoRight = true;
                GoLeft = false;
                StopAllCoroutines();
            }
            FightCanvasCG.alpha -= 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            yield return FadeOutCanvas();
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
        private void Break()
        {
            VictoryButton.GetComponent<Button>().onClick.Invoke();
            StopAllCoroutines();
            Attack = false;
            StartCoroutine(FadeOutCanvas());
        }
        Break();
    }
    private void GetClick()
    {
        bool HitEnemy = false;
        if (Mathf.Abs(CenterHitPart.transform.position.x - SlidingBar.transform.position.x) < 0.7f)
        public void StartTheFight()
        {
            HitEnemy = true;
            ApplyDamageToEnemy(DamageOfPlayer * 2f);
            StopAllCoroutines();
            Attack = true;
            StartCoroutine(FadeInCanvas());
        }
        else
        private void HandleMovement()
        {
            for (int i = 0; i < CloseSideParts.Length; i++)
            Vector3 TargetPos;
            if (GoRight)
            {
                if (Mathf.Abs(CloseSideParts[i].transform.position.x - SlidingBar.transform.position.x) < 0.7f)
                TargetPos = RightBorder.transform.position;
                if (SlidingBar.transform.position.x > RightStopper.transform.position.x)
                {
                    HitEnemy = true;
                    ApplyDamageToEnemy(DamageOfPlayer);
                    GoRight = false;
                }
            }
            for (int i = 0; i < FarSideParts.Length; i++)
            else 
            {
                if (Mathf.Abs(FarSideParts[i].transform.position.x - SlidingBar.transform.position.x) < 0.7f)
                TargetPos = LeftBorder.transform.position;
                if (SlidingBar.transform.position.x < LeftStopper.transform.position.x)
                {
                    HitEnemy = true;
                    ApplyDamageToEnemy(DamageOfPlayer / 2);
                    GoRight = true;
                }
            }
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, TargetPos, SliderSpeed * Time.deltaTime);
        }
        if (!HitEnemy)
        public void ApplyDamageToPlayer()
        {
            Attack = false;
            StopAllCoroutines();
            StartCoroutine(FadeOutCanvas());
            MissedButton.GetComponent<Button>().onClick.Invoke();
            PlayerForegroundHealthBar.GetComponent<Image>().fillAmount -= DamageOfEnemy;
        }
        else
        private void ApplyDamageToEnemy(float Damage)
        {
            if (EnemyForegroundHealthBar.GetComponent<Image>().fillAmount > 0f)
            EnemyForegroundHealthBar.fillAmount -= Damage;
            if (EnemyForegroundHealthBar.fillAmount <= 0f)
            {
                Attack = false;
                HitButton.GetComponent<Button>().onClick.Invoke();
                Fungusint.Set<int>(1);//killed
                EnemyWasHit = true;
                Attack = false;
            }
            else
                Fungusint.Set<int>(0);//hit
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
                Fungusint.Set<int>(2);//We got hit
            }
            ClickRegisterButton.GetComponent<Button>().onClick.Invoke();
            HitEnemy = false;
        }
        HitEnemy = false;
    }
}
}