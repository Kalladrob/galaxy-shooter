using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    
    [SerializeField]
    private int powerupID; //0 = triple shot ; 1 = speed boost ; 2 = shield

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with " + other);
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                if(powerupID == 0) {
                    player.TripleShotPowerupOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedUpPowerupOn();
                }
                else if(powerupID == 2)
                {

                }
                
                
                
                
            }

            Destroy(this.gameObject);
        }
    }
}
