using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] skins;

    public static UnityAction<Enemy> OnEnemyKilled;
    public static UnityAction<Enemy> OnPlayerHit;
    public static bool IsPlay;

    int lifes = 0;

    // Update is called once per frame
    void Update()
    {
        if (!IsPlay) return;

        transform.Translate(Vector2.right * Time.deltaTime * 0.2f * -Mathf.Sign(transform.position.x));

        if(Mathf.Abs(transform.position.x) < 1f)
        {
            transform.Translate(Vector2.right * Mathf.Sign(transform.position.x) * 2);
            OnPlayerHit?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Bullet>().Hit();

        lifes -= 1;

        if(lifes <= 0)
        {
            OnEnemyKilled?.Invoke(this);
        }
    }

    public void Play(int _lifesCount, float _moveSide)
    {
        lifes = _lifesCount;
        gameObject.SetActive(true);

        transform.localScale = new Vector2(_moveSide, 1f);

        int rnd = Random.Range(0, skins.Length);
        for (var i = 0; i < skins.Length; i++)
            skins[i].SetActive(i == rnd);
    }
}
