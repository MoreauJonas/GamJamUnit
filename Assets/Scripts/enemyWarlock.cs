using UnityEngine;
using UnityEngine.UI;

public class enemyWarlock : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

  

    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint =0;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

    //Si l'ennemi est quasiment arrivée à sa destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint= (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX; //flip l'ennemi selon la position 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
        }
    }
}
