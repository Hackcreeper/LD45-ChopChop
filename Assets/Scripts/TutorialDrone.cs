using DroneTargets;
using UnityEngine;

public class TutorialDrone : Drone
{
    private void Awake()
    {
        Targets = new IDroneTarget[]
        {
            new PlayerTarget(),
        };
    }

    public override void TakeDamage(int amount = 1)
    {
        Player.Instance.PlaySound(hitSound);
        Health.Sub(amount);

        Flashing = .15f;
        meshRenderer.material.mainTexture = redTexture;

        Rigidbody.AddRelativeForce(0, 0, -10f, ForceMode.Impulse);
        ResetAttackTimer();

        if (Health.Get() > 0)
        {
            return;
        }

        BoxCollider.enabled = true;
        CapsuleCollider.enabled = false;
        Animator.enabled = false;
        Active = false;

        Resources.Instance.Add(ResourceType.Metal, Random.Range(1, 5));
        Resources.Instance.Add(ResourceType.Oil, Random.Range(2, 10));

        Tutorial.Tutorial.Instance.Finish();
    }
}