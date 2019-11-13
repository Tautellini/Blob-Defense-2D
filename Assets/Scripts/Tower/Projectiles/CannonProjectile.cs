﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : Projectile
{
    void Start() { }
    public void Update()
    {
        //Go to target
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_.transform.position, step);
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        impact();
        Destroy(gameObject);
    }

    public override void shootAt(GameObject target)
    {
        target_ = target;
    }
    protected override void impact()
    {
    }
}
