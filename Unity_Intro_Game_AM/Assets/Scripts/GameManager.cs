using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
#if UNITY_EDITOR
            if(!Application.isPlaying)
            {
              return null;
            }
#endif
            return instance;
        }
    }

    public bool isPaused = false;

    public GameObject pauseMenu;
    public PlayerController player;

 
    public Image healthBar;
    public TextMeshProUGUI clipCounter;
    public TextMeshProUGUI ammoCounter;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (SceneManager.GetActiveScene().name != "Main Menu")
            player = GameObject.Find("PlayerWrapper").transform.GetChild(0).GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            healthBar.fillAmount = Mathf.Clamp(((float)player.health / (float)player.maxHealth), 0, 1);

            if (player.weaponID < 0)
            {
                clipCounter.gameObject.SetActive(false);
                ammoCounter.gameObject.SetActive(false);
            }

            else
            {
                clipCounter.gameObject.SetActive(true);
                clipCounter.text = "Clip: " + player.currentClip + "/" + player.clipSize;

                ammoCounter.gameObject.SetActive(true);
                ammoCounter.text = "Ammo: " + player.currentAmmo;
            }

            if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;

                pauseMenu.SetActive(true);

                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
                Resume();
        }

    }

    public void Resume()
    {
        isPaused = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("0"))
        {
            Debug.Log("save key pressed");
            SaveSystem.Save();
        
        }

        if (Input.GetKeyDown("1"))
        {
            SaveSystem.Load();
        }
    }

}
