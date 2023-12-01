using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager manager;
    GameObject player;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetGameManager(Component go)
    {
        manager = go.GetComponent<GameManager>();
        player = manager.player;
    }

    private void Move()
    {
        if (player)
        {
            transform.LookAt(player.transform);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.disLife();

            ObjectDestroy();
        }
    }

    public void ObjectDestroy()
    {
        gameObject.SetActive(false);
    }
}
