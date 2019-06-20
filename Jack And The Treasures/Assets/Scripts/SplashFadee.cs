using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashFadee : MonoBehaviour {
    public Image splashImage;
    public string LoadLevel;


	// Use this for initialization
	IEnumerator Start () {
        splashImage.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(LoadLevel);

            
	}
    void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
