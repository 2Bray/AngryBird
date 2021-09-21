using UnityEngine;

public class YellowBirdScript : BirdScript
{
    [SerializeField] private float _boostForce = 100;
    private bool _hasBoost = false;

    public void Boost()
    {
        if (State == BirdState.Thrown && !_hasBoost)
        {
            RigidBody.AddForce(RigidBody.velocity * _boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}