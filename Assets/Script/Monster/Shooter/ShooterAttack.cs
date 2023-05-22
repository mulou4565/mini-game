using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : Monster
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player")) {
            collision.GetComponent<Move>().Hurt();
        } //½ÇÉ«µôÑª
        if (!collision.gameObject.CompareTag("Monster") && !collision.gameObject.CompareTag("other")) Destroy(this.gameObject);

    }
}
