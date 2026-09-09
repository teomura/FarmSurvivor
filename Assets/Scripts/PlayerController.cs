using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public void UpdatePosition(Vector3 pos)
    {
        _destination = pos;
    }

    [SerializeField]
    private Vector3 _destination; //stores the updated target position of the player
   
    
    
    
    public InputAction moveAction;
    //public InputAction clickToMove;
    public float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        //clickToMove.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse
        var distance = Vector3.Distance(_destination, transform.position);
        if (distance > 0.1f)
        {
            
            //direction = destination - source (math formula for direction)
            var direction = _destination - transform.position;
            direction.Normalize();//to avoid extralong vector - this normalizes it to 1

            transform.Translate(direction * speed * Time.deltaTime);//movement
        }


        //Keyboard
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveInput.x * Time.deltaTime * speed);
        transform.Translate(Vector3.up * moveInput.y * Time.deltaTime * speed);

        if (moveInput.x != 0) //change flip only on lateral input
        {
            if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
            
        
        GetComponent<Animator>().SetBool("isMoving", moveInput != Vector2.zero);

    }
}
