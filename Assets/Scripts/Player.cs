using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float speed = 12f;
    public Text text;
    public Text resultText;
    public AudioClip getCoin;
    public bool is_playing;
    public bool is_speed_up;
    public bool is_lucky;
    private float speed_up_time;
    private float lucky_time;
    private AudioSource audioSource;
    private float max_wide = 19f;
    private int coin_counter = 0;
    private Vector3 scl;

    void Start() {
        is_playing = true;
        is_speed_up = false;
        is_lucky = false;
        text.text = "Score: " + coin_counter.ToString();
        resultText.text = text.text;
        audioSource = gameObject.GetComponent<AudioSource>();
        scl = this.transform.localScale;
    }

    void Update() {
        if (is_playing) {
            Vector3 pos = this.transform.position;
            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");
            Vector3 mov = new Vector3(hor, 0, ver);
            if (hor != 0 || ver != 0) {
                pos += mov * speed * Time.deltaTime * (is_speed_up ? 2 : 1) / Mathf.Sqrt(Mathf.Abs(hor)+Mathf.Abs(ver));
            }
            if (Mathf.Abs(pos.x) > max_wide) {
                pos.x = Mathf.Sign(pos.x) * max_wide;
            }
            if (Mathf.Abs(pos.z) > max_wide) {
                pos.z = Mathf.Sign(pos.z) * max_wide;
            }
            this.transform.position = pos;
            if (is_speed_up) {
                this.transform.localScale = new Vector3(scl.x*1.5f, scl.y, scl.z*1.5f);
                speed_up_time -= Time.deltaTime;
                if (speed_up_time < 0) {
                    is_speed_up = false;
                }
            } else {
                this.transform.localScale = scl;
            }
            if (is_lucky) {
                lucky_time -= Time.deltaTime;
                if (lucky_time < 0) {
                    is_lucky = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (is_playing){
            if (other.CompareTag("Coin") || other.CompareTag("SpeedUpCoin") || other.CompareTag("LuckyCoin")) {
                if (other.CompareTag("SpeedUpCoin")) {
                    is_speed_up = true;
                    speed_up_time = 15f;
                }
                if (other.CompareTag("LuckyCoin")) {
                    is_lucky = true;
                    lucky_time = 15f;
                }
                coin_counter += 1;
                text.text = "Score: " + coin_counter.ToString();
                resultText.text = text.text;
                audioSource.PlayOneShot(getCoin);
                Destroy(other.gameObject);
            }
        }
    }
}
