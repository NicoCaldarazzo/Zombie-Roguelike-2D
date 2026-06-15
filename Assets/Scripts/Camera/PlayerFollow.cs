using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    private Transform playerPosition;
    private float cameraZ;
    [SerializeField] private float followSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        cameraZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosition.position.x, playerPosition.position.y, cameraZ),followSpeed * Time.deltaTime);
    }
}
