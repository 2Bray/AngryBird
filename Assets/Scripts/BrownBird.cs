using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : BirdScript
{
    private bool hit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        RigidBody = GetComponent<Rigidbody2D>();
        RigidBody.bodyType = RigidbodyType2D.Kinematic;
        RigidBody.velocity = Vector3.zero;
        StartCoroutine(countDount());
    }

    private IEnumerator countDount()
    {
        yield return new WaitForSeconds(2);
        transform.GetChild(0).gameObject.SetActive(true);
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        cc.radius *= 4;
        cc.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Background" && collision.name!="GameController")
        {
            Transform T = collision.transform;
            Vector2 moveTo = (T.position-transform.position);
            float distance = Vector2.Distance(T.position, transform.position);
            T.GetComponent<Rigidbody2D>().velocity = (Vector2.one + moveTo) * 10 / distance;
            if (collision.tag == "Enemy") Destroy(collision.gameObject);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

}
