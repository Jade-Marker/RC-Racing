using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] int lookDistance = 5;
    [SerializeField] GameObject winText;
    [SerializeField] ParticleSystem deathFx;

    Track currentTrack;
    CheckpointHandler checkpointHandler;
    PlayerMovement player;
    SpriteRenderer enemySprite;
    LevelManager levelManager;
    bool alive = true;
    int currentLap = 0;

    const float a = 1440f / 13.332f;
    const float b = 108f;
    const float c = 960f;
    const float d = 540f;

    void Start()
    {
        currentTrack = FindObjectOfType<Track>();
        checkpointHandler = GetComponent<CheckpointHandler>();
        player = FindObjectOfType<PlayerMovement>();
        enemySprite = GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (!(checkpointHandler.finished))
        {
            if (alive)
            {
                Vector3 movementVector = new Vector3();
                float rotationAngle = 0f;

                bool movingForward = false;
                bool turning = false;
                bool turningLeft = false;

                Vector2Int imgCheck = EnemyCoordsToImgCoords(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
                float currentRotation = transform.rotation.eulerAngles.z;

                imgCheck.x -= Mathf.RoundToInt(lookDistance * Mathf.Sin(currentRotation * Mathf.Deg2Rad));
                imgCheck.y += Mathf.RoundToInt(lookDistance * Mathf.Cos(currentRotation * Mathf.Deg2Rad));

                Color imgCheckColor = currentTrack.pathImg.GetPixel(imgCheck.x, imgCheck.y);
                if (imgCheckColor != Color.black)
                {
                    movingForward = true;
                }

                imgCheck = EnemyCoordsToImgCoords(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
                currentRotation += 90f;

                imgCheck.x -= Mathf.RoundToInt(lookDistance * Mathf.Sin(currentRotation * Mathf.Deg2Rad));
                imgCheck.y += Mathf.RoundToInt(lookDistance * Mathf.Cos(currentRotation * Mathf.Deg2Rad));

                imgCheckColor = currentTrack.pathImg.GetPixel(imgCheck.x, imgCheck.y);
                if (imgCheckColor != Color.black)
                {
                    turning = true;
                    turningLeft = true;
                }

                imgCheck = EnemyCoordsToImgCoords(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
                currentRotation = transform.rotation.eulerAngles.z;
                currentRotation -= 90f;

                imgCheck.x -= Mathf.RoundToInt(lookDistance * Mathf.Sin(currentRotation * Mathf.Deg2Rad));
                imgCheck.y += Mathf.RoundToInt(lookDistance * Mathf.Cos(currentRotation * Mathf.Deg2Rad));

                imgCheckColor = currentTrack.pathImg.GetPixel(imgCheck.x, imgCheck.y);
                if (imgCheckColor != Color.black)
                {
                    turning = true;
                    turningLeft = false;
                }

                if (movingForward)
                {
                    movementVector = gameObject.transform.up;
                }

                if (turning)
                {
                    if (turningLeft)
                    {
                        rotationAngle = rotationSpeed * Time.deltaTime;
                    }
                    else
                    {
                        rotationAngle = -rotationSpeed * Time.deltaTime;
                    }
                }

                movementVector *= enemySpeed * Time.deltaTime / 100f;

                gameObject.transform.Rotate(0, 0, rotationAngle, Space.Self);
                gameObject.transform.Translate(movementVector, Space.World);

                Vector2 currPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Vector2Int imgPos = EnemyCoordsToImgCoords(currPos);
                Color posColor = currentTrack.pathImg.GetPixel(imgPos.x, imgPos.y);
                if (posColor == Color.black)
                {
                    EnemyDeath();
                }

                if (currentLap != checkpointHandler.GetLapNo())
                {
                    currentLap = checkpointHandler.GetLapNo();
                }
            }
        }
        else
        {
            player.EnemyFinished();
        }
    }

    private Vector2Int EnemyCoordsToImgCoords(Vector2 currPos)
    {
        return new Vector2Int(Mathf.RoundToInt(a * currPos.x + c), Mathf.RoundToInt(b * currPos.y + d));
    }

    private void EnemyDeath()
    {
        alive = false;
        enemySprite.enabled = false;
        var vfx = Instantiate(deathFx, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);
    }

    public void PlayerFinished()
    {
        if (!(checkpointHandler.finished))
        {
            alive = false;
            winText.SetActive(true);
            levelManager.NextLevel(2f);
        }
    }
}
