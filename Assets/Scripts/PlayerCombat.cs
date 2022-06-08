using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Serializable]
    private struct Attack
    {
        public string name;
        public float delay;
        public float duration;
        public float window;
        public int damage;
    }

    [SerializeField] private Attack[] attackArray;
    private int pos = 0;

    private Collider weaponHitBox;

    private Animator animator;

    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponHitBox = transform.GetChild(1).GetChild(0).GetComponent<BoxCollider>();
        weaponHitBox.enabled = false;
    }

    void Update()
    {
        
    }

    public void MakeAttack()
    {
        StartCoroutine(TriggerAttack());
        pos++;
        if (pos >= attackArray.Length)
            pos = 0;
    }

    IEnumerator TriggerAttack()
    {
        Attack attack = attackArray[pos];

        animator.SetTrigger(attack.name);

        if (attack.delay > 0.0f)
            yield return new WaitForSeconds(attack.delay);

        weaponHitBox.enabled = true;
        yield return new WaitForSeconds(attack.duration);
        weaponHitBox.enabled = false;

        yield return null;
    }

    public void Interrupt()
    {
        weaponHitBox.enabled = false;
        DropCombo();
    }

    public void DropCombo()
    {
        pos = 0;
    }

}
