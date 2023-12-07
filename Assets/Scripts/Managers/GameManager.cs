using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para acceder al GameManager desde cualquier parte del juego
    public static GameManager Instance;

    public Vector3 LastCheckPointPos;

    [SerializeField]private bool _villageFree = false;

    public bool VillageFree => _villageFree;
    // Variables para el juego
    [SerializeField]private int lives = 4;
    [SerializeField]private int maxlives = 4;
    private int score = 0;
    private bool isGameOver = false;

    public bool IsGameOver => isGameOver;
    public UIManager UIManager;
    
    
    [SerializeField] private ThirdPersonController Player;
    private Animator anim;

    // Enum para el estado del juego
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private GameState gameState = GameState.Playing;

    // Evento para notificar un cambio en el estado del juego
    public delegate void GameStateChangedAction(GameState newState);
    public event GameStateChangedAction OnGameStateChanged;

    // Evento para notificar cambios en la puntuaci�n
    public delegate void ScoreChangedAction(int newScore);
    public event ScoreChangedAction OnScoreChanged;

    // Evento para notificar cambios en las vidas
    public delegate void LivesChangedAction(int newLives);
    public event LivesChangedAction OnLivesChanged;

    

    // Evento para notificar el final del juego
    public delegate void GameOverAction();
    public event GameOverAction OnGameOver;

    private void Awake()
    {

        // Configura el Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
         anim = Player.GetComponentInParent<Animator>();

    }

    // M�todos para controlar el juego

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            OnScoreChanged?.Invoke(score);
            if (score >= 4)
            {
                _villageFree = true;
            }
        }
    }

    public void LoseScore(int points)
    {
        if (!isGameOver)
        {
            score -= points;
            score = Mathf.Max(score, 0); // Asegurarse de que el puntaje no sea negativo.
            OnScoreChanged?.Invoke(score);
        }
    }



    public void Addife(int amount)
    {
        if (!isGameOver)
        {
            lives += amount;
            lives = Mathf.Min(lives, maxlives); // Asegurarse de que las vidas no superen el maximo.
            OnLivesChanged?.Invoke(lives);
            
        }
        
    }


    public void LoseLife(int amount)
    {
        if (!isGameOver)
        {
            anim.SetTrigger("damage");
            lives -= amount;
            lives = Mathf.Max(lives, 0); // Asegurarse de que las vidas no sean negativas.
            OnLivesChanged?.Invoke(lives);
            
            if (lives <= 0)
            {
               EnterEndGameState();
            }
            
        }
        
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
        OnGameStateChanged?.Invoke(gameState);

        switch (newState)
        {
            case GameState.Playing:
                EnterPlayingState();
                break;
            case GameState.Paused:
                EnterPausedState();
                break;
            case GameState.GameOver:
                EnterEndGameState();
                break;
            
        }
    }

   

    public void EnterEndGameState()
    {
        anim.SetTrigger("GameOver");
        isGameOver = true;
        gameState = GameState.GameOver;
        Player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        OnGameOver?.Invoke();
        StartCoroutine(GameOverUI());
     
    }
    IEnumerator GameOverUI()
    {
        
        yield return new WaitForSeconds(3);
        UIManager.ShowGameOverPanel();

    }

    public void EnterPlayingState()
    {
        // Reanudar la l�gica de juego
        Time.timeScale = 1.0f; // Asegurarse de que el tiempo de juego est� en su velocidad normal.
        Cursor.lockState = CursorLockMode.Locked;
        gameState = GameState.Playing;
        UIManager.ShowPlayPanel();
        
    }
    public void EnterPausedState()
    {
        // Pausar la l�gica de juego
        Time.timeScale = 0.0f; // Poner el tiempo de juego en 0 pausa la l�gica de juego.
        Cursor.lockState = CursorLockMode.None;
        UIManager.ShowPausePanel();
        
        
    }

    public void Restart()
    {
        isGameOver = false;
        Player.enabled = true;
        lives = maxlives;
        anim.SetTrigger("Restart");
        Player.transform.position = LastCheckPointPos;

    }



    public int GetScore()
    {
        return score;
    }
    public int GetMaxLives()
    {
        return maxlives;
    }
    public int GetLives()
    {
        return lives;
    }
    

    public GameState GetGameState()
    {
        return gameState;
    }

    
}
