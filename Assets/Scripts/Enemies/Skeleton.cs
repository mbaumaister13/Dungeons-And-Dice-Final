﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Panda;

public class Skeleton : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 50f;
        damage = 15;
        attackSpeed = 3f;
        moveSpeed = 80f;
    }

    [Task]
    public void seekPlayer(){
        float dir = Mathf.Sign((player.transform.position-rb.transform.position).x);
        rb.velocity = new Vector2(dir*moveSpeed*Time.fixedDeltaTime,rb.velocity.y);
        Task.current.Succeed();
    }
    [Task]
    public override void canSeePlayer(){
        Vector2 targetDir = rb.transform.position - player.transform.position;
        if(targetDir.magnitude <5){
            animator.SetBool("playerNear",true);
            Task.current.Succeed();
        }
        else{
            animator.SetBool("playerNear",false);
            Task.current.Fail();
        }
    }

    public override void attack(){
        player.takeDamage(damage);
    }
   
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"&&canAttack){
            attack();
            canAttack = false;
            animator.SetTrigger("explode");
            Destroy(gameObject,0.5f);
        }
    }
}
