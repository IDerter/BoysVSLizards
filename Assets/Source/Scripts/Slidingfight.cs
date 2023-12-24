using Fungus;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BoysVsLizards
{
    public class Slidingfight : MonoBehaviour
    {
        [SerializeField] GameObject LeftBorder, RightBorder, SlidingBar, RightStopper, LeftStopper, CenterHitPart;
        private bool Attack = false;
        [SerializeField] private GameObject[] CloseSideParts, FarSideParts;
        [SerializeField] private GameObject ClickRegisterButton;
        [SerializeField] private Image EnemyForegroundHealthBar, PlayerForegroundHealthBar, EnemyImage;
        [SerializeField] private float DamageOfPlayer = 0.15f, DamageOfEnemy = 0.33f;
        private Color ChangingColor;
        private bool EnemyWasHit = false;
        private CanvasGroup FightCanvasCG;
        [SerializeField] private float SliderSpeed, DetectableDistance;
        [SerializeField] VariableReference Fungusint;

        private bool GoRight = true;

        private void Start()
        {
            FightCanvasCG = GetComponent<CanvasGroup>();
            ChangingColor = EnemyImage.GetComponent<Image>().color;
        }
        private void Update()
        {
            if (EnemyWasHit)
            {
                if (ChangingColor.b <= 0)
                    EnemyWasHit = false;
                ChangingColor.b -= Time.deltaTime * 3;
                ChangingColor.g -= Time.deltaTime * 3;
                EnemyImage.color = ChangingColor;
            }
            else if (ChangingColor.b <= 1)
            {
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
        private IEnumerator FadeOutCanvas()
        {
            if (FightCanvasCG.alpha <= 0f)
            {
                StopAllCoroutines();
            }
            FightCanvasCG.alpha -= 0.01f;
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
            Vector3 TargetPos;
            if (GoRight)
            {
                TargetPos = RightBorder.transform.position;
                if (SlidingBar.transform.position.x > RightStopper.transform.position.x)
                {
                    GoRight = false;
                }
            }
            else
            {
                TargetPos = LeftBorder.transform.position;
                if (SlidingBar.transform.position.x < LeftStopper.transform.position.x)
                {
                    GoRight = true;
                }
            }
            SlidingBar.transform.position = Vector3.Lerp(SlidingBar.transform.position, TargetPos, SliderSpeed * Time.deltaTime);
        }
        public void ApplyDamageToPlayer()
        {
            PlayerForegroundHealthBar.GetComponent<Image>().fillAmount -= DamageOfEnemy;
        }
        private void ApplyDamageToEnemy(float Damage)
        {
            EnemyForegroundHealthBar.fillAmount -= Damage;
            if (EnemyForegroundHealthBar.fillAmount <= 0f)
            {
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

            if (Mathf.Abs(CenterHitPart.transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)

            {
                HitEnemy = true;
                Debug.Log("Apply Damage1!");
                ApplyDamageToEnemy(DamageOfPlayer * 2f);
            }
            else
            {
                for (int i = 0; i < CloseSideParts.Length; i++)
                {
                    if (Mathf.Abs(CloseSideParts[i].transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)
                    {
                        HitEnemy = true;
                        Debug.Log("Apply Damage2!");
                        ApplyDamageToEnemy(DamageOfPlayer);
                    }
                }
                for (int i = 0; i < FarSideParts.Length; i++)
                {
                    if (Mathf.Abs(FarSideParts[i].transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)
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
    }
}