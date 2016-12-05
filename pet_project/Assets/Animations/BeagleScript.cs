using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;

//Script to make dog move based on voice commands

public class BeagleScript : MonoBehaviour {

    //Speech to text
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    Animator anim; //calls on "animator" variable, called "anim", to contain animator object
    int sitHash = Animator.StringToHash("Sit");
    int speakHash = Animator.StringToHash("Speak");
    int upHash = Animator.StringToHash("Up");

    void checkIfDead() { 
        if (transform.rotation.eulerAngles.z == 90)
        {
            Debug.Log("Was playing dead..");
            transform.rotation = Quaternion.AngleAxis(0, Vector3.back);
            transform.position = new Vector3(0, 0.1f, 0);

        }
    }

    // Use this for initialization
    void Start () {

        Debug.Log("Waking Sir Tumbles");

        anim = GetComponent<Animator>();
        
        keywords.Add("Sit", () =>
        {
            anim.SetTrigger(sitHash);
            Debug.Log("Sit");

        });

        keywords.Add("Up", () =>
        {
            anim.SetTrigger(upHash);

            //if dead brings back up
            checkIfDead();

            Debug.Log("Up");
        });

        keywords.Add("Good dog", () =>
        {
            anim.SetTrigger(upHash);
            Debug.Log("Up, good dog");

            //if dead brings back up
            checkIfDead();
        });

        keywords.Add("Good boy", () =>
        {
            anim.SetTrigger(upHash);
            Debug.Log("Up, good boy");

            //if dead brings back up
            checkIfDead();
        });

        keywords.Add("Good girl", () =>
        {
            anim.SetTrigger(upHash);
            Debug.Log("Up, good girl");

            //if dead brings back up
            checkIfDead();
        });

        keywords.Add("Play dead", () =>
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            transform.position = new Vector3(0, 0.1f, 0);
            Debug.Log("Play dead");

        });

        keywords.Add("Speak", () =>
        {
            anim.SetTrigger(speakHash);
            AudioSource audio = GetComponent<AudioSource>(); //gets audiosource
            audio.PlayDelayed(0.4f);
            Debug.Log("Speak");

        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
