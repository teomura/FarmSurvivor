using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 2f;
    private bool canMove = true;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");// locking on the player
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x;

        if (canMove)        
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canMove = false; //stopping enemies on collision
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       canMove = true;
    }
}
