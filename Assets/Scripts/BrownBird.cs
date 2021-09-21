using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : BirdScript
{
    private bool hit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hit && collision.collider.name!="Background")
        {
            hit = true;
            Rigidbody2D ribi = GetComponent<Rigidbody2D>();
            ribi.bodyType = RigidbodyType2D.Kinematic;
            transform.parent = collision.transform;
            ribi.velocity = Vector3.zero;
        }
    }
}
