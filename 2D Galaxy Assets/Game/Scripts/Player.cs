using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ':' means 'inherits' ; 'MonoBehaviour' is needed for the script to be used on Unity

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;
    
    
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int _lives = 3;

    private Vector3 _startingPosition = new Vector3(0, -4.4f, 0);

    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;


    // Start is called before the first frame update
    private void Start()
    {
        //Debug.Log(transform.position);
        //current pos = new position
        transform.position = _startingPosition; //sets player's starting position
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) //if space is pressed
        {

            Shoot();

        }
                
    }

    private void Movement()
    {
        //Debug.Log("X: " + transform.position.x + ", Y: " + transform.position.y);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (isSpeedBoostActive == true)
        {
            transform.Translate((Vector3.right * Time.deltaTime * _speed * horizontalInput)*1.5f);
            transform.Translate((Vector3.up * Time.deltaTime * _speed * verticalInput)*1.5f);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }

        //setting y-axis limits
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.9f)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, 0);
        }

        //setting x-axis limits
        if (transform.position.x > 8.5f)
        {
            transform.position = new Vector3(8.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, (transform.position + new Vector3(0, 0.95f, 0)), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    public void Damage()
    {
        if (_lives > 0)
        {
            transform.position = _startingPosition;
            //Instantiate(this.gameObject, _startingPosition, Quaternion.identity);
            _lives--;
            Debug.Log("Remaining Lifes: " + _lives);
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("GAME OVER");
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedUpPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }

    public IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

}