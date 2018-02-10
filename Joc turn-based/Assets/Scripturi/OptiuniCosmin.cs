using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptiuniCosmin : MonoBehaviour {
    public Slider slide;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void volume()
    {
        AudioSource audio = FindObjectOfType<AudioSource>();
        if(audio!=null)
        audio.volume = slide.value;
    }
}
