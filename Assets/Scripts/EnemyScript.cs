using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    public float Health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    void OnDestroy() => OnEnemyDestroyed(gameObject);

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (col.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            //Hitung damage yang diperoleh
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}