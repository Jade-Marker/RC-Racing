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
    [SerializeField] Path[] enemyPath;

    Track currentTrack;
    CheckpointHandler checkpointHandler;
    PlayerMovement player;
    SpriteRenderer enemySprite;
    LevelManager levelManager;
    bool alive = true;
    int currentLap = 0;
    int currentPath = 0;

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
                Vector2 newPos = Vector2.MoveTowards(transform.position, enemyPath[currentPath].transform.position, enemySpeed * Time.deltaTime);
                transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);



                var rotation = transform.rotation.eulerAngles;
                rotation.x = 0;
                rotation.y = 0;
                transform.rotation = Quaternion.Euler(rotation);

                if (transform.position == enemyPath[currentPath].transform.position) {
                    currentPath++;
                }

                if (currentPath >= enemyPath.Length) {
                    currentPath = 0;
                }

                LookAt2D(enemyPath[currentPath].transform, rotationSpeed, FacingDirection.RIGHT);

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

    enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }
    //source: https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
    void LookAt2D(Transform theTarget, float theSpeed, FacingDirection facing)
    {
        Vector3 vectorToTarget = theTarget.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * theSpeed);
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
