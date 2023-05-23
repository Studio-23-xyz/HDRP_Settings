using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MobileUtilsScript : MonoBehaviour {
 
    private int FramesPerSec;
    private float frequency = 0.2f;
 
 
    protected Text _text;
 
    void Start(){
        _text = GetComponent<Text>();
        StartCoroutine(FPS());
    }
 
    private IEnumerator FPS() {
        for(;;){
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
           
            // Display it
 
           
            _text.text = Mathf.RoundToInt(frameCount / timeSpan).ToString();
        }
    }
 
 
    
}