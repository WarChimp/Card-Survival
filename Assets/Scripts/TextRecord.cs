using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRecord : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Text _messageBox;
    private List<string> _storedMessages;
    private Queue<string> _storedTest;

    private string _resetMessage = "...";
    private Queue<string> _sentences;

    public static TextRecord instance;
    private bool finishedText = true;


    void Start()
    {
        instance = this;
        _messageBox.text = "...";
        _storedTest = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_storedTest.Count > 0)
            StartCoroutine(TypeSentence());
    }

    public void PostMessage(string desc)
    {
        //_messageBox.text = cardName + ": \n" + desc;
        //_storedMessages.Insert(0, desc);
        _storedTest.Enqueue(desc);
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        //_messageBox.text = "";


        if (finishedText)
        {
            _messageBox.text = "";

            for (int i = 0; i <= _storedTest.Count; i++)
            {
                _messageBox.text += "\n";
                var sentence = _storedTest.Dequeue();
                finishedText = false;
                foreach (char letter in sentence.ToCharArray())
                {
                    _messageBox.text += letter;
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(1f);
                finishedText = true;
                Debug.LogFormat("There are: <color=red>{0}</color> messages left.", _storedTest.Count);
            }
        }

        
        
        
    }

    public void ResetTextBox()
    {
        //StartCoroutine(TypeSentence(_resetMessage));
    }

    private void PasteSentences(Queue sentence)
    {
        
    }
}
