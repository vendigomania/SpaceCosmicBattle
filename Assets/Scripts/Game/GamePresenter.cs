using MatchThreeMachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private Button homeBtn;
        [SerializeField] private GameObject[] hearts; 

        [SerializeField] private TMP_Text coinsLable;
        [SerializeField] private TMP_Text scoreLable;

        [SerializeField] private BoardLogic board;
        [SerializeField] private SkillBtn recoveryBtn;
        [SerializeField] private SkillBtn shotThroughBtn;
        [SerializeField] private SkillBtn fireSpeed2xBtn;

        [SerializeField] private ResultScreen resultScreen;

        [Header("Ingame")]
        [SerializeField] private Transform character;
        [SerializeField] private Animator characterAnimator;

        [SerializeField] Enemy enemyPrefab;
        [SerializeField] Bullet bulletPrefab;

        public static GamePresenter Instance { get; private set; }

        int heartsCount;
        float timeOfSession;

        int score;
        int earned;
        int abilitiesUsed;

        private void Start()
        {
            Instance = this;

            homeBtn.onClick.AddListener(Home);
            board.OnMatch += OnMatch;

            recoveryBtn.OnClickAction += () =>
            {
                abilitiesUsed++;
                heartsCount = Mathf.Min(heartsCount + 1, hearts.Length);
                UpdateView();
            };

            shotThroughBtn.OnClickAction += () =>
            {
                abilitiesUsed++;
                throughShot = true;
            };
            fireSpeed2xBtn.OnClickAction += () =>
            {
                abilitiesUsed++;
                forceShot = 1 + PlayerPrefs.GetInt("firingSpeed", 0);
            };

            Enemy.OnEnemyKilled += OnKillEnemy;
            Enemy.OnPlayerHit += OnGetHit;
        }

        private void Update()
        {
            timeOfSession += Time.deltaTime;
        }

        public void StartGame()
        {
            gameObject.SetActive(true);

            timeOfSession = 0;
            
            heartsCount = 3 + PlayerPrefs.GetInt("lifeExtension", 0);

            Enemy.IsPlay = true;

            StartCoroutine(DefendRoutine());
            StartCoroutine(EnemiesSpawnRoutine());

            score = 0;
            earned = 0;
            abilitiesUsed = 0;

            UpdateView();
        }

        public void SetOff()
        {
            gameObject.SetActive(false);
        }

        public void Home()
        {
            MainController.Instance.BackToStartMenu();
        }

        private void OnMatch(TileTypeDataAsset _type, int count)
        {
            IngameData.Coins += count;
            earned += count;

            Bullet.type = _type.id;

            AudioManager.Reload();

            UpdateView();
        }

        #region enemy callback
        private void OnKillEnemy(Enemy _enemy)
        {
            score++;

            UpdateView();

            Destroy(_enemy.gameObject);

            //set new target
            var enemyList = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if(enemyList.Count() == 0) return;

            var minD = enemyList.Min(en => Mathf.Abs(en.transform.position.x));
            target = enemyList.First(en => Mathf.Abs(en.transform.position.x) == minD);
        }

        private void OnGetHit(Enemy _enemy)
        {
            Destroy(_enemy.gameObject);

            heartsCount--;
            UpdateView();
            if(heartsCount == 0)
            {
                ShowResult();
            }
        }
        #endregion

        private void ShowResult()
        {
            StopGame();

            resultScreen.Show(score, earned, abilitiesUsed);
        }

        private void StopGame()
        {
            StopAllCoroutines();

            var enemyList = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach(var enemy in enemyList) Destroy(enemy.gameObject);

            Enemy.IsPlay = false;

            var bulletsList = FindObjectsByType<Bullet>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var bullet in bulletsList) Destroy(bullet.gameObject);


        }

        private void UpdateView()
        {
            for(int o = 0; o < hearts.Length; o++)
            {
                hearts[o].gameObject.SetActive(o < heartsCount);
            }

            coinsLable.text = IngameData.Coins.ToString();
            scoreLable.text = score.ToString();
        }

        int forceShot = 0;
        bool throughShot;
        Enemy target;

        IEnumerator DefendRoutine()
        {
            while(true)
            {

                var time = 3f * (100f - PlayerPrefs.GetInt("speed", 0) * 5) / 100;

                if (target != null)
                {
                    if (forceShot > 0)
                    {
                        forceShot--;
                        time /= 2;
                    }

                    characterAnimator.Play("Shot");
                    character.localScale = new Vector3(-Mathf.Sign(target.transform.position.x), 1f, 1f);
                    var bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = Vector2.up * 2.2f;
                    if(throughShot)
                    {
                        bullet.Initialize(2 + PlayerPrefs.GetInt("shotThrough", 0), Mathf.Sign(target.transform.position.x) > 0);
                        throughShot = false;
                    }
                    else
                    {
                        bullet.Initialize(1, Mathf.Sign(target.transform.position.x) > 0);
                    }
                }

                yield return new WaitForSeconds(time);
            }
        }

        IEnumerator EnemiesSpawnRoutine()
        {
            while (heartsCount > 0)
            {
                var enemy = Instantiate(enemyPrefab);
                var distance = 3f / 0.5625f * Screen.width / Screen.height;
                enemy.transform.position = new Vector2(Random.Range(0, 2) == 0? -distance : distance, 2f);
                enemy.Play(1 + Mathf.FloorToInt(timeOfSession / 60f), -Mathf.Sign(enemy.transform.position.x));

                if(target == null)
                {
                    target = enemy;
                }

                yield return new WaitForSeconds(Random.Range(4, 8));
            }
        }
    }
}
