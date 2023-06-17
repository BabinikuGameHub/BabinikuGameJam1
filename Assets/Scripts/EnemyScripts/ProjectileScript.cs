using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D _projectileRb;
    private float _bulletVelocity;
    private float _bulletLifeTime;

    // Start is called before the first frame update
    private void Awake()
    {
        _bulletLifeTime = 0.0f;
        _projectileRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_bulletLifeTime > 1f)
        {
            Destroy(gameObject);
        }
        _bulletLifeTime += Time.deltaTime;

    }

    // Update is called once per frame
    public void SetTrajectory(Vector2 shootDir, float velocity)
    {
        _bulletVelocity = velocity;
        _projectileRb.velocity = new Vector2(shootDir.x, shootDir.y).normalized * _bulletVelocity;
        float rot = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //염록이랑 충돌
            //염록이 데미지
            Destroy(gameObject);
        }
    }
}
