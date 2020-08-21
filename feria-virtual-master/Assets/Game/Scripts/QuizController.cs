using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class QuizController : MonoBehaviour
{
    private Text questionText;

    private GameObject[] answersGO;
    private List<Button> answersText = new List<Button>();

    public int Score;

    private Text scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovementScript>().Immobile = true;
        questionText = GameObject.FindGameObjectWithTag("Question").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("QuizScore").GetComponent<Text>();
        answersGO = GameObject.FindGameObjectsWithTag("Answer");
        foreach (GameObject ans in answersGO)
        {
            answersText.Add(ans.GetComponent<Button>());
        }
        Score = 0;
        InitiateQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Score.ToString();
        if (Score == 50)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovementScript>().Immobile = false;
            Score = 0;
            GameObject.FindGameObjectWithTag("QuizGame").SetActive(false);
        }
        else if(Score == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovementScript>().Immobile = true;
        }
    }

    private void InitiateQuiz()
    {
        int index = Random.Range(0, 4);
        string[] questions = {"¿Pregunta 1?", "¿Pregunta 2?", "¿Pregunta 3?", "¿Pregunta 4?"};
        questionText.text = questions[index];
        for (int i = 0; i < answersText.Count; i++)
        {
            string respuesta;
            if (i == index)
                respuesta = "Correcta";
            else
                respuesta = "Incorrecta";
            answersText[i].GetComponentInChildren<Text>().text = respuesta;
            answersText[i].onClick.AddListener(() => ValidacionRespuesta(respuesta));
        }
    }

    private void ValidacionRespuesta(string s)
    {
        if (s.Equals("Correcta"))
        {
            Debug.Log("Acertado");
            Score += 10;
        }
        else
        {
            Debug.Log("Equivocado");
            Score -= 10;
        }

        foreach (Button btn in answersText)
        {
            btn.onClick.RemoveAllListeners();
        }
        InitiateQuiz();
    }
    
}
