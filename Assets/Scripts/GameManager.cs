using UnityEngine;
using UnityEngine.SceneManagement; //Scene Manager

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance { get; private set; }

    public int world { get; private set; } //which world we are in
    public int stage { get; private set; } //which stage we are in
    public int lives { get; private set; } //life count

    private void Awake()
    {
        if (Instance == null) //if there is instance
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Don't destroy when we load another scene
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null; //Assigned instance will become null
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;

        LoadLevel(1,1);
   }

    private void LoadLevel(int world,int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}"); //1-1,1-2,...
    }

    public void NextLevel()
    {
        //if(world==1 && stage==10)//Don't forget to check if it is last level 
        LoadLevel(world, stage + 1); //next level
    }

    public void ResetLevel(float delay) //For making of delaying
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel() //After Character dies
    {
        lives--; //Life count - 1

        if (lives > 0) //if there is life left
        {
            LoadLevel(world, stage);
        } else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        /* SceneManager.LoadScene("GameOver"); //Load Custom Game Over Scene */
        // Invoke(nameof(NewGame), 3f); //3 seconds after that call NewGame function
        NewGame(); //Other option
    }

}

