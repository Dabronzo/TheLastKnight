using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start() 
        {
            canvasGroup = GetComponent<CanvasGroup>();

            // //debug 
            // StartCoroutine(FadeOutIn());
        }

        //Nesting Coroutine
        //for debugging
        // IEnumerator FadeOutIn()
        // {
        //     yield return FadeOut(2f);
        //     print("fading out");
        //     yield return FadeIn(2.5f);
        //     print("fading in");
        // }


        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;

                //waiting to the end of the frame
                yield return null;
            }
            
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;

                //waiting to the end of the frame
                yield return null;
            }
            
        }
   
    }
}
