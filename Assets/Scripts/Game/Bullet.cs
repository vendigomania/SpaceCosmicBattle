using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int type;

    public GameObject[] skins;

    int count;

    public void Initialize(int _count = 0, bool right = false)
    {
        for (int i = 0; i < skins.Length; i++)
            skins[i].SetActive(i == type);

        count = _count;

        transform.rotation = Quaternion.Euler(0f, 0f, right ? 180f : 0f);

        switch(type)
        {
            case 0:
                AudioManager.Blast();
                break;
            case 1:
                AudioManager.Laser();
                break;
            case 2:
                AudioManager.Boom();
                break;
            case 3:
                AudioManager.Pulse();
                break;
            case 4:
                AudioManager.Thunder();
                break;
        }

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 5f);
    }

    public void Hit()
    {
        count--;

        if (count == 0)
            gameObject.SetActive(false);
    }
}
