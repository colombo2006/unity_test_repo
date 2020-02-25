using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    protected void Start() {
        OpenURL("https://slotoking.casino");
    }

    public static void OpenURL(string url) {
        var playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var webClass = new AndroidJavaClass("com.playgenesis.webtest.WebActivity");
        
        var activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        var intent = new AndroidJavaObject("android.content.Intent", activity, webClass);
        intent.Call<AndroidJavaObject>("putExtra", "url", url);
        activity.Call("startActivity", intent);
    }
}
