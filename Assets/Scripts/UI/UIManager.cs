﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public MusicManager MM;

    private Slider volume;
    private Dropdown difficulty;
    private Button quit;
    void Start() {
        volume = GameObject.Find("Pause Menu").transform.Find("Music Settings").transform.Find("Volume").gameObject.GetComponent<Slider>();
        if (SceneManager.GetActiveScene().name == "StartScene") {
            difficulty = GameObject.Find("Pause Menu").transform.Find("Difficulty").transform.GetComponentInChildren<Dropdown>();
        }
        quit = GameObject.Find("Pause Menu").transform.Find("QuitButton").gameObject.GetComponent<Button>();
        quit.onClick.AddListener(quitGame);
        GameManager.UI = this;
    }

    void quitGame() {
        Application.Quit();
    }
    void setDifficulty(string diff) {
        GameManager.difficult = diff;
    }
    void Update() {
        if (Input.GetKeyDown("escape")) {
            GameManager.pauseGame();
        }
        if(difficulty!=null){
           setDifficulty(difficulty.captionText.text);
        }
    }

    public void volumeUpdate(float vol) {
        MM.SetVolume(vol);
    }

    public void toggleMusic(bool isOn) {
        volume.interactable = isOn;
        MM.SetVolume(isOn ? volume.value : 0f);
        Debug.Log("Music Enabled: " + isOn);
    }

}
