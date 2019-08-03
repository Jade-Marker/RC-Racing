using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Text lapText;
    [SerializeField] GameObject lossText;
    [SerializeField] AudioClip crashSound;
    [SerializeField][Range(0f,1f)] float crashVolume = 1f;
    [SerializeField] ParticleSystem deathFx;

    Track currentTrack;
    CheckpointHandler checkpointHandler;
    EnemyMovement enemy;
    AudioSource audioSource;
    SpriteRenderer playerSprite;
    LevelManager levelManager;
    bool moving = false;
    bool moveLeft = false;
    bool alive = true;
    int currentLap = 0;

    const float a = 1440f/13.332f;
    const float b = 108f;
    const float c = 960f;
    const float d = 540f;

    void Start() {
        currentTrack = FindObjectOfType<Track>();
        checkpointHandler = GetComponent<CheckpointHandler>();
        enemy = FindObjectOfType<EnemyMovement>();
        audioSource = GetComponent<AudioSource>();
        playerSprite = GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (!(checkpointHandler.finished))
        {
            if (moving && alive)
            {
                Vector3 movementVector = new Vector3();
                float rotationAngle = 0f;

                if (moveLeft)
                {
                    movementVector = gameObject.transform.up;
                    rotationAngle = rotationSpeed * Time.deltaTime;
                }
                else
                {
                    movementVector = gameObject.transform.up;
                    rotationAngle = -rotationSpeed * Time.deltaTime;
                }
                movementVector *= playerSpeed * Time.deltaTime / 100f;

                gameObject.transform.Rotate(0, 0, rotationAngle, Space.Self);
                gameObject.transform.Translate(movementVector, Space.World);

                moving = false;

                Vector2 currPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Vector2Int imgPos = PlayerCoordsToImgCoords(currPos);
                Color posColor = currentTrack.pathImg.GetPixel(imgPos.x, imgPos.y);
                if (posColor == Color.black)
                {
                    PlayerDeath();
                }

                if (currentLap != checkpointHandler.GetLapNo())
                {
                    currentLap = checkpointHandler.GetLapNo();
                    lapText.text = "Lap " + currentLap.ToString() + "/" + currentTrack.Laps;
                }
            }
        }
        else {
            enemy.PlayerFinished();
        }
    }

    private Vector2Int PlayerCoordsToImgCoords(Vector2 currPos) {
        return new Vector2Int(Mathf.RoundToInt(a * currPos.x + c), Mathf.RoundToInt(b * currPos.y + d));
    }

    private void PlayerDeath()
    {
        alive = false;
        playerSprite.enabled = false;
        var vfx = Instantiate(deathFx, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);
        levelManager.RestartLevel(5f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        audioSource.PlayOneShot(crashSound, crashVolume);
    }

    public void EnemyFinished() {
        if (!(checkpointHandler.finished)) {
            alive = false;
            lossText.SetActive(true);
            levelManager.RestartLevel(5f);
        }
    }

    public void Left()
    {
        moving = true;
        moveLeft = true;
    }

    public void Right()
    {
        moving = true;
        moveLeft = false;
    }
}
