using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum E_PlayerState
    {
        NONE = 0,
        ITEM1
    }

    E_PlayerState state;

    public float walkSpeed;
    public float rotSpeed;

    public int score;
    public int life;


    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void init()
    {
        walkSpeed = 5f;
        rotSpeed = 500f;

        score = 0;
        life = 3;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W)) //¾Õ
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S)) //µÚ
            transform.Translate(0, 0, -walkSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A)) //¿Þ
            transform.Rotate(Vector3.up * -rotSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D)) //¿À
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);

    }

    public void disLife()
    {
        life--;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            life = 0;

            gameObject.SetActive(false);
        }
    }
}
