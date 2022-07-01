using Unity.Entities;
using Unity.MLAgents;
using UnityEngine;

class ReceivedInjuriesS : ComponentSystem
{
    RectTransform healthBar;
    CapsuleCollider[] capsuleColliders;
    bool bossAttack;

    protected override void OnStartRunning()
    {
        Entities.ForEach((StaticsCF statics, TriggerDamageC triggerDamage, GameObjectC gameObjectC, RagdollC ragdollC, OrAliveC orAlive, TakingDamageC takingDamage) =>
        {
            foreach (GameObject ragdoll in ragdollC.ragdolls)
            {
                ragdoll.GetComponent<Rigidbody>().detectCollisions = false;
            }
        });
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((StaticsCF statics, TriggerDamageC triggerDamage, GameObjectC gameObjectC, RagdollC ragdollC, OrAliveC orAlive, TakingDamageC takingDamage) =>
        {
            healthBar = gameObjectC.transform.GetChild(2).
                GetChild(0).
                GetChild(0).
                GetChild(0)
                .GetComponent<RectTransform>();

            if (takingDamage.time < takingDamage.endTime)
            {
                takingDamage.time += Time.DeltaTime;
                takingDamage.canHit = false;

                if (triggerDamage.animator && triggerDamage.animator.GetComponent<NCBossAttackC>())
                {
                    if (triggerDamage.animator.GetComponent<NCBossAttackC>().value && bossAttack)
                    {
                        takingDamage.canHit = true;
                        bossAttack = false;
                    }
                }
            }
            else if (triggerDamage.animator && triggerDamage.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && triggerDamage.animator.GetAnimatorTransitionInfo(0).duration == 0)
            {
                takingDamage.endTime = triggerDamage.animator.GetCurrentAnimatorStateInfo(0).length;
                takingDamage.time = 0;

                if (!triggerDamage.animator.GetComponent<NCBossAttackC>())
                {
                    takingDamage.canHit = true;
                }
                else
                {
                    bossAttack = true;
                }
            }

            if (triggerDamage.distance && takingDamage.canHit && triggerDamage.animator.GetComponent<OrAliveC>().value)
            {
                healthBar.anchorMax = new Vector2(healthBar.anchorMax.x - (float)triggerDamage.attack / statics.hp, 1);

                if (healthBar.anchorMax.x <= 0)
                {
                    orAlive.value = false;

                    MonoBehaviour.Destroy(gameObjectC.value.GetComponent<DecisionRequester>());

                    gameObjectC.value.GetComponent<Animator>().enabled = false;

                    capsuleColliders = gameObjectC.value.GetComponents<CapsuleCollider>();

                    foreach (var capsuleCollider in capsuleColliders)
                    {
                        if (!capsuleCollider.isTrigger)
                        {
                            capsuleCollider.enabled = false;
                            break;
                        }
                    }

                    foreach (GameObject ragdoll in ragdollC.ragdolls)
                    {
                        ragdoll.GetComponent<Collider>().enabled = true;
                        ragdoll.GetComponent<Rigidbody>().detectCollisions = true;
                        ragdoll.GetComponent<Rigidbody>().isKinematic = false;

                        Physics.IgnoreCollision(ragdoll.GetComponent<Collider>(), ragdollC.ignore);
                    }
                }
            }

            if (triggerDamage.distance)
            {
                if (gameObjectC.GetComponent<AttackC>())
                {
                    gameObjectC.GetComponent<AttackC>().value = true;
                }
            }
            else
            {
                if (gameObjectC.GetComponent<AttackC>())
                {
                    gameObjectC.GetComponent<AttackC>().value = false;
                }
            }
        });
    }
}
