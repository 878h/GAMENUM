using UnityEngine;
using UnityEngine.UI;

public class PuzzleGame : MonoBehaviour
{

    /*Player need to guess the sequence within an amount of set time.
     * if player answer correct, the question will move to the next one, if wrong, nothing will happens. 
     * After timer ran out, end game and calculate total score*/

    public Text sequenceText;
    public InputField inputField; //input (answer placement)
    public Text resultText;
    public Text scoreText; // score
    public Text timerText; // time
    public Color correctColor;
    public Color wrongColor;

    private int[] sequence;
    private int misNumber;
    private int missingNumber;
    private int score = 0;
    public float timer = 30f;
    private bool gameActive = true;

    void Start()
    {
        GenerateSequence();
        UpdateScore();
        UpdateTimer();
    }

    void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
        }
    }

    void GenerateSequence()
    {
        int commDif = Random.Range(1, 20); // the difference in each sequences
        int startVal = Random.Range(1, 100); // value of start
        sequence = new int[6]; // length of sequence
        misNumber = Random.Range(0, sequence.Length); //random unknown number placement

        for (int i = 0; i < sequence.Length; i++) //for loop helping in sequence limit
        {
            sequence[i] = startVal + i * commDif; //allow for generating randm # on random range of 1 - 100
        }

        // cal missing #
        missingNumber = startVal + misNumber * commDif;

        UpdateSeqText();
    }


    void UpdateSeqText() //update the text
    {
        string seqStr = ""; //create string for storing int
        for (int i = 0; i < sequence.Length; i++)
        {
            seqStr += (i == misNumber) ? "..." : sequence[i].ToString(); //if index matches the unknown #, else display sequence
            if (i < sequence.Length - 1)
                seqStr += ", ";
        }
        sequenceText.text = seqStr;
    }

    public void CheckAnswer() //check if answer is correct
    {
        if (!gameActive) return;

        int playerAnswer;
        if (int.TryParse(inputField.text, out playerAnswer))
        {
            if (playerAnswer == missingNumber)
            {
                resultText.text = "Correct! Generating new sequence...";
                resultText.color = correctColor;
                IncrementScore();
                Invoke("GenerateNewSequence", 2f);
            }
            else
            {
                resultText.text = "Incorrect. Try again!";
                resultText.color = wrongColor;
            }
        }
        else
        {
            resultText.text = "Please enter a valid number.";
            resultText.color = wrongColor;
        }
    }

    private void IncrementScore() //add 1 score for each correct answer
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore() //update score for each correct answer
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void GenerateNewSequence() //if correct gen new seq
    {
        resultText.text = "";
        inputField.text = "";
        GenerateSequence();
    }

    private void UpdateTimer() //timer, end = endgame and cal score
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer <= 0)
            {
                End();
            }
        }
    }

    private void End() // cal total scoring
    {
        gameActive = false;
        resultText.text = "Time's up! Total score: " + score.ToString();
    }
}






