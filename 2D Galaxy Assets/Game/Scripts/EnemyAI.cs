using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    private float _speed = 1.5f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.08f, 8.08f), 7.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= -7.8f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }
        else
        {
            transform.position = new Vector3(Random.Range(-8.08f, 8.08f), 7.8f, 0);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log(this.tag + " Collided with " + other);
            Player player = other.GetComponent<Player>();
            //Destroy(other.gameObject);
            player.Damage();
        }else if(other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
        }
        
        Destroy(this.gameObject);
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

    }

    

}
