using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed = 2.0f;
    public Vector3 swipeOffset;
    public GameObject swipePrefab;
    float attackTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime; //counter to attack start
        if (attackTimer >= attackSpeed)
        {
            Instantiate(swipePrefab, transform.position + swipeOffset, transform.rotation, transform);// instantiate attack, attaching it to the player
            attackTimer = 0f;   
        }
    }
}
