using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
    public float timer = 180;
    public GameObject coin;
    public GameObject speedUpCoin;
    public GameObject luckyCoin;
    public AudioClip gameOver;
    public Text text;
    private GameObject gameOverCanvas;
    private AudioSource audioSource;
    private RawImage shadow;
    private float counter = 0;
    private Player player;
    private float wait_weight = 2;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shadow = GameObject.FindGameObjectWithTag("Shadow").GetComponent<RawImage>();
        audioSource = gameObject.GetComponent<AudioSource>();
        shadow.color = new Color(0, 0, 0, 0);
        gameOverCanvas = GameObject.FindGameObjectWithTag("GameOver");
        gameOverCanvas.SetActive(false);
    }

    void Update() {
        wait_weight = player.is_lucky ? 0.3f : 2;
        if (timer >= 0){
            if (counter < 0){
                counter = Random.value * wait_weight;
                float x = (float)(Random.value - 0.5) * 2 * 18f;
                float z = (float)(Random.value - 0.5) * 2 * 18f;
                float random_value = Random.Range(0, 10) + 1;
                switch (random_value) {
                    case 1:
                        CreateCoin(x, z, speedUpCoin);
                        break;
                    case 2:
                        CreateCoin(x, z, luckyCoin);
                        break;
                    default:
                        CreateCoin(x, z, coin);
                        break;
                }
            } else {
                counter -= Time.deltaTime;
            }
            timer -= Time.deltaTime;
            text.text = "Time: " + ((int)timer).ToString();
        } else {
            if (player.is_playing) {
                audioSource.Stop();
                audioSource.PlayOneShot(gameOver);
            }
            player.is_playing = false;
            gameOverCanvas.SetActive(true);
            shadow.color = new Color(0, 0, 0, 0.5f);
        }
    }

    void CreateCoin(float x, float z, GameObject coinObject) {
        GameObject instance = (GameObject)Instantiate(coinObject, new Vector3(x, 30.0f, z), Quaternion.identity);
        Destroy(instance, 2.55f);
    }

    public void OnClickRestart() {
        SceneManager.LoadScene("game");
    }

    public void OnClickGoTitle() {
        SceneManager.LoadScene("title");
    }
}
