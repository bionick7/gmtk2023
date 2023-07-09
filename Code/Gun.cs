using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    private const int MAX_BULLETS = 100;

    private Bullet[] Pool = new Bullet[MAX_BULLETS];
    private int Iterator = 0;

    private float angle = 0;

    public GameObject BulletPrefab;
    public Hero Hero;

    private void Start() {
        for (int i=0; i < MAX_BULLETS; i++) {
            GameObject bulletObj = Instantiate<GameObject>(BulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bulletObj.SetActive(false);
            Pool[i] = bullet;
        }
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine() {
        while (true) {
            if (Hero.MovState == MovementState.Moving) {
                angle = 0;
            } else {
                angle = -Mathf.PI / 4f;
            }
            Bullet bullet = SpawnBulletFromPool();
            bullet.Velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 50f;
            yield return new WaitForSeconds(.1f);
        }
    }

    private Bullet SpawnBulletFromPool() {
        Bullet res = Pool[Iterator];
        res.gameObject.SetActive(true);
        res.transform.position = transform.position;
        Iterator++;
        if (Iterator >= 100) Iterator = 0;
        return res;
    }
}
