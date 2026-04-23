using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;
    public int weaponType;
    public bool shieldActive;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        if (!shieldActive)
        {
            lives--;
        }

        if (shieldActive)
        {
            shieldPrefab.SetActive(false);
            shieldActive = false;
        }
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shieldPrefab.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    speed = 10f;
                    thrusterPrefab.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    weaponType = 2;
                    gameManager.ManagePowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    weaponType = 3;
                    gameManager.ManagePowerupText(3);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 4:
                    shieldPrefab.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerupText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
        }
        if (whatDidIHit.tag == "Coin")
        {
            gameManager.AddScore(1);
            gameManager.PlaySound(3);
            Destroy(whatDidIHit.gameObject);
        }
    }
        void Movement()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

            float horizontalScreenSize = gameManager.horizontalScreenSize;
            float verticalScreenSize = gameManager.verticalScreenSize;

            if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
            {
                transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
            }

            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }

            if (transform.position.y <= -verticalScreenSize * .55f)
            {
                transform.position = new Vector3(transform.position.x, -verticalScreenSize * .55f, 0);
            }

        }
    }
