using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByakuyaAura : Aura
{
    public bool faceRight;
    // Start is called before the first frame update

    // Update is called once per frame
    public override void FixedUpdate()
    {
        transform.position = new Vector3(follow.transform.position.x - 1f, follow.transform.position.y + 0.5f, 0);
        var byakuya = follow.GetComponent<Byakuya>();
        if (((byakuya.target.transform.position.x < transform.position.x && !faceRight) ||
            (byakuya.target.transform.position.x > transform.position.x && faceRight)))
            Flip();
    }
    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        faceRight = false;
    }
}
