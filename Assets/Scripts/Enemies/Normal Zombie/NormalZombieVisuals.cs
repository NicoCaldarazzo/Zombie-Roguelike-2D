using UnityEngine;

public class NormalZombieVisuals : MonoBehaviour
{
    [SerializeField] private Sprite zombieLeftRight;
    [SerializeField] private Sprite zombieUp;
    [SerializeField] private Sprite zombieDown;
    private SpriteRenderer rend;
    private ZombieTracking zombietracking;
    private Vector3 turnLeft = new Vector3( -1, 1, 1);
    private Vector3 turnRight = new Vector3( 1, 1, 1);
   void Awake()
    {
        zombietracking = GetComponent<ZombieTracking>();
        rend = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        Vector2 dir = zombietracking.Direction;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                rend.sprite = zombieLeftRight;
                transform.localScale = turnLeft;
            }
            else
            {
                rend.sprite = zombieLeftRight;
                transform.localScale = turnRight;
            }
                
        }
        else if (dir.y > 0)
        {
            rend.sprite = zombieUp;
        }
        else
        {
            rend.sprite = zombieDown;
        }
    }

}
