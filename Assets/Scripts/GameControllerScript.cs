using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TrailControllerScript TCS;
    public SlingShooterScript SlingShooter;
    public List<BirdScript> Birds;
    public List<EnemyScript> Enemies;
    private BirdScript _shotBird;
    private BoxCollider2D TapCollider;
    private bool gameOver = false;


    void Start()
    {
        TapCollider = GetComponent<BoxCollider2D>();

        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        SlingShooter.InitiateBird(Birds[0]);
        TapCollider.enabled = false;
        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }

        if (Birds.Count == 0 && !gameOver) StartCoroutine(Lose());
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0 && !gameOver) Win();
    }

    public void AssignTrail(BirdScript bird)
    {
        TCS.SetBird(bird);
        StartCoroutine(TCS.SpawnTrail());
        TapCollider.enabled = true;
    }


    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(4);
        if (!gameOver)
        {
            panel.SetActive(true);
            panel.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Win()
    {
        gameOver = true;
        panel.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(true);
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void ClickRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickNextStage()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}