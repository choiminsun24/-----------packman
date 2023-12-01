using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            player.score++;

            ObjectDestroy();
        }
    }

    public void ObjectDestroy()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
