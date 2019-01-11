using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByakuyaSkill2 : TsukaimaSkill
{
    public override void FixedUpdate()
    {
        if (!stickOnPlayer)
        {
            if (collideWithPlayer)
            {
                stickOnPlayer = true;
                anim.SetBool("Stick", stickOnPlayer);
            }
            else
            {
                if (destroyElapsed > 0)
                {
                    destroyElapsed -= Time.deltaTime;
                    base.FixedUpdate();
                }
                else
                    Destroy(gameObject);
            }
        }
        else
        {
            if (stickElapsed > 0)
            {
                stickElapsed -= Time.deltaTime;
                var byakuya = gameObject.GetComponentInParent<Byakuya>();
                transform.position = byakuya.target.transform.position;
            }
            else
                Destroy(gameObject);
        }
    }
}
