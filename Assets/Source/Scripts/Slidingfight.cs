using System.Collections;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

namespace BoysVsLizards
{
    public class SlidingFight : MonoBehaviour
    {
        [SerializeField] GameObject LeftBorder, RightBorder, SlidingBar, RightStopper, LeftStopper, CenterHitPart;
        private bool Attack = false;
        [SerializeField] private GameObject[] CloseSideParts, FarSideParts;
        [SerializeField] private GameObject ClickRegisterButton;
        [SerializeField] private Image EnemyForegroundHealthBar, PlayerForegroundHealthBar, EnemyImage;
        [SerializeField] private int _playerDamage = 1;
        private Color ChangingColor;
        private bool EnemyWasHit = false;
        private CanvasGroup FightCanvasCG;
        [SerializeField] private float SliderSpeed, DetectableDistance;
        // [SerializeField] VariableReference Fungusint;
        [SerializeField] private Flowchart _flowchart;
        
        private bool GoRight = true;
        private float _enemyHealth;
        private float _currentEnemyHealth;
        private float _currentHealth;
        private float _health;
        private float _enemyDamage;
        private string _victoryBlockName, _loseBlockName;

        public void Init(float enemyHealth, float enemyDamage, string victoryBlockName, string loseBlockName)
        {
            _health = 10f;
            _currentHealth = _health;
            _enemyHealth = enemyHealth;
            _currentEnemyHealth = _enemyHealth;
            _enemyDamage = enemyDamage;
            _victoryBlockName = victoryBlockName;
            _loseBlockName = loseBlockName;
            PlayerForegroundHealthBar.fillAmount = 1f;
            EnemyForegroundHealthBar.fillAmount = 1f;
            StartTheFight();
        }

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
            while (FightCanvasCG.alpha < 1f)
            {
                FightCanvasCG.alpha += 0.01f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            yield return null;
        }
        private IEnumerator FadeOutCanvas()
        {
            while (FightCanvasCG.alpha > 0f)
            {
                FightCanvasCG.alpha -= 0.01f;
                yield return new WaitForSecondsRealtime(0.01f); 
            }

            StopAllCoroutines();
            gameObject.SetActive(false);
            yield return null;
        }
        private void Break()
        {
            // StopAllCoroutines();
            // Attack = false;
            // StartCoroutine(FadeOutCanvas());
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

        private void ApplyDamageToEnemy(float damage)
        {
            _currentEnemyHealth -= damage;
            EnemyForegroundHealthBar.fillAmount = _currentEnemyHealth / _enemyHealth;

            if (_currentEnemyHealth <= 0f)
            {
                Victory();
            }

            Break();
        }

        private void GetClick()
        {

            bool HitEnemy = false;

            if (Mathf.Abs(CenterHitPart.transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)

            {
                HitEnemy = true;
                Debug.Log("Apply Damage1!");
                ApplyDamageToEnemy(_enemyDamage * 2);
            }
            else
            {
                for (int i = 0; i < CloseSideParts.Length; i++)
                {
                    if (Mathf.Abs(CloseSideParts[i].transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)
                    {
                        HitEnemy = true;
                        Debug.Log("Apply Damage2!");
                        ApplyDamageToEnemy(_enemyDamage);
                    }
                }
                for (int i = 0; i < FarSideParts.Length; i++)
                {
                    if (Mathf.Abs(FarSideParts[i].transform.position.x - SlidingBar.transform.position.x) < DetectableDistance)
                    {
                        HitEnemy = true;
                        ApplyDamageToEnemy(_enemyDamage / 2f);
                    }
                }
            }
            if (!HitEnemy)
            {
                PlayerHit();
            }
            ClickRegisterButton.GetComponent<Button>().onClick.Invoke();
            HitEnemy = false;
        }

        private void Victory()
        {
            _flowchart.ExecuteBlock(_victoryBlockName);
            EnemyWasHit = true;
            Attack = false;
            EndBattle();
        }

        private void PlayerHit()
        {
            _currentHealth -= _enemyDamage;
            PlayerForegroundHealthBar.fillAmount = _currentHealth / _health;

            if (_currentHealth <= 0)
            {
                Lose();
            }
        }

        private void Lose()
        {
            _flowchart.ExecuteBlock(_loseBlockName);
            EnemyWasHit = true;
            Attack = false;
            EndBattle();
        }

        private void EndBattle()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutCanvas());
        }
    }
}