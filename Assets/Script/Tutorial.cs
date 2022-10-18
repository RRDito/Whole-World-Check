using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject Example;
    public GameObject Canvas;
    public GameObject MC, WPerks;
    public Text TText;

    public Sprite MovePlateCircle, AttackMovePlate, SwordIcon, MBonus, OBonus;

    private string CurrentLesson; //used to chain lessons
    private bool ReadStatus;

    public void PiecesEnabled(bool enabler) //this enables or disable the collider of pieces and moveplates
    {
        GameObject[] AllThePieces = GameObject.FindGameObjectsWithTag("Chessman");
        for (int i = 0; i < AllThePieces.Length; i++)
        {
            AllThePieces[i].GetComponent<BoxCollider2D>().enabled = enabler;
        }

        GameObject[] AllTheMP = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < AllTheMP.Length; i++)
        {
            AllTheMP[i].GetComponent<BoxCollider2D>().enabled = enabler;
        }
    }

    void Start()
    {
        //Reset(); //for testing purposes 
    }

    void Update()
    {
       if (Canvas.activeSelf) { PiecesEnabled(false); }           
    }

    public void Reset()
    {
        PlayerPrefs.SetString("Tut Intro", "");
        PlayerPrefs.SetString("Tut ClickPiece", "");
        PlayerPrefs.SetString("Tut Move", "");
        PlayerPrefs.SetString("Tut MoveChart", "");
        PlayerPrefs.SetString("Tut MoveChartDots", "");
        PlayerPrefs.SetString("Tut MoveChartLine", "");
        PlayerPrefs.SetString("Tut MoveChartSkip", "");
        PlayerPrefs.SetString("Tut MoveChartSkipAllies", "");
        PlayerPrefs.SetString("Tut Stats", "");
        PlayerPrefs.SetString("Tut Attack", "");
        PlayerPrefs.SetString("Tut BattleForecast", "");
        PlayerPrefs.SetString("Tut AttackReady", "");
        PlayerPrefs.SetString("Tut AttackColors", "");
        PlayerPrefs.SetString("Tut AttackDodge", "");
        PlayerPrefs.SetString("Tut CheckEnemy", "");
        PlayerPrefs.SetString("Tut PerkDisplay", "");
        PlayerPrefs.SetString("Tut OutnumberBonus", "");
        PlayerPrefs.SetString("Tut MoraleBonus", "");
        PlayerPrefs.SetString("Tut Promotion", "");
        PlayerPrefs.SetString("Tut UniquePieces", "");
        PlayerPrefs.SetString("Tut PieceTaken", "");
        PlayerPrefs.SetString("Tut GameOverVersus", "");
        PlayerPrefs.SetString("Tut GameOverCampaign", "");
        
    }

    public void ShowTutorial(string Lesson)
    {
        ReadStatus = false;
        CheckRead(Lesson);

        if (!ReadStatus && !Canvas.activeSelf) //Canvas.activeSelf should tell us if another tutorial page is open
        {
			GameObject audioManager = GameObject.FindGameObjectWithTag("AudioManager");
			audioManager.GetComponent<AudioManager>().PlaySound("Chime");
			
            CurrentLesson = Lesson;
            Canvas.SetActive(true);
            PiecesEnabled(false);
            Arrow.SetActive(false);
            Example.SetActive(false);
            GetLesson(Lesson);
        }
    }

    void CheckRead(string LessonName)
    {
        switch (LessonName)
        {
            case "Intro":
                if (PlayerPrefs.GetString("Tut Intro") == "Read") { ReadStatus = true; }
                break;

            case "ClickPiece":
                if (PlayerPrefs.GetString("Tut ClickPiece") == "Read") { ReadStatus = true; }
                break;

            case "Move":
                if (PlayerPrefs.GetString("Tut Move") == "Read") { ReadStatus = true; }
                break;

            case "MoveChart":
                if (PlayerPrefs.GetString("Tut MoveChart") == "Read") { ReadStatus = true; }
                break;

            case "MoveChartDots":
                if (PlayerPrefs.GetString("Tut MoveChartDots") == "Read") { ReadStatus = true; }
                break;

            case "MoveChartLine":
                if (PlayerPrefs.GetString("Tut MoveChartLine") == "Read") { ReadStatus = true; }
                break;

            case "MoveChartSkip":
                if (PlayerPrefs.GetString("Tut MoveChartSkip") == "Read") { ReadStatus = true; }
                break;

            case "MoveChartSkipAllies":
                if (PlayerPrefs.GetString("Tut MoveChartSkipAllies") == "Read") { ReadStatus = true; }
                break;

            case "Stats":
                if (PlayerPrefs.GetString("Tut Stats") == "Read") { ReadStatus = true; }
                break;

            case "Attack":
                if (PlayerPrefs.GetString("Tut Attack") == "Read") { ReadStatus = true; }
                break;

            case "BattleForecast":
                if (PlayerPrefs.GetString("Tut BattleForecast") == "Read") { ReadStatus = true; }
                break;

            case "AttackReady":
                if (PlayerPrefs.GetString("Tut AttackReady") == "Read") { ReadStatus = true; }
                break;

            case "AttackColors":
                if (PlayerPrefs.GetString("Tut AttackColors") == "Read") { ReadStatus = true; }
                break;

            case "AttackDodge":
                if (PlayerPrefs.GetString("Tut AttackDodge") == "Read") { ReadStatus = true; }
                break;

            case "CheckEnemy":
                if (PlayerPrefs.GetString("Tut CheckEnemy") == "Read") { ReadStatus = true; }
                break;

            case "PerkDisplay":
                if (PlayerPrefs.GetString("Tut PerkDisplay") == "Read") { ReadStatus = true; }
                break;

            case "OutnumberBonus":
                if (PlayerPrefs.GetString("Tut OutnumberBonus") == "Read") { ReadStatus = true; }
                break;

            case "MoraleBonus":
                if (PlayerPrefs.GetString("Tut MoraleBonus") == "Read") { ReadStatus = true; }
                break;

            case "Promotion":
                if (PlayerPrefs.GetString("Tut Promotion") == "Read") { ReadStatus = true; }
                break;

            case "UniquePieces":
                if (PlayerPrefs.GetString("Tut UniquePieces") == "Read") { ReadStatus = true; }
                break;

            case "PieceTaken":
                if (PlayerPrefs.GetString("Tut PieceTaken") == "Read") { ReadStatus = true; }
                break;

            case "GameOverVersus":
                if (PlayerPrefs.GetString("Tut GameOverVersus") == "Read") { ReadStatus = true; }
                break;

            case "GameOverCampaign":
                if (PlayerPrefs.GetString("Tut GameOverCampaign") == "Read") { ReadStatus = true; }
                break;
        }
    }

    void GetLesson(string LessonName)
    {
        switch (LessonName)
        {
            case "Intro": //happens in Game
                TText.text = "Welcome to your first game of the Whole World is in Check \n Let's learn the basics!  \n\n Close this message to continue";                
                break;

            case "ClickPiece": //chained
                TText.text = "Lets start by moving the pieces \n\n Click the piece you want to move to select it";
                break;

            case "Move": //happens in Chessman
                TText.text = "These circles represent your possible moves \n\n Click in one of those circles and your piece will move to that square \n\n *you can move only once per turn";
                
                GameObject m = GameObject.FindWithTag("MovePlate");
                Arrow.SetActive(true);
                Vector3 mpos = m.transform.position;
                Arrow.transform.position = mpos;
                Arrow.transform.localScale = new Vector3(1, 1);
                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = MovePlateCircle;
                Example.GetComponent<Image>().color = new Color (0.33f , 0.33f, 0.33f);
                break;

            case "MoveChart": //happens in Chessman
                TText.text = "In the right top corner of the board, you can see a representation of how the selected piece will move";

                Arrow.SetActive(true);                
                Arrow.transform.position = MC.transform.position;                
                Arrow.transform.localScale = new Vector3(3, 3);
                break;

            case "MoveChartDots": //chained
                TText.text = "A dot represents that it can both move and attack \n\n An empty circle means it can only move \n\n A cross means it can only attack ";

                Arrow.SetActive(true);
                Arrow.transform.position = MC.transform.position;
                Arrow.transform.localScale = new Vector3(3, 3);
                break;

            case "MoveChartLine": //happens in Chessman
                TText.text = "The symbols in the borders mean that the piece can move indefinitely in that direction";

                Arrow.SetActive(true);
                Arrow.transform.position = MC.transform.position;
                Arrow.transform.localScale = new Vector3(3, 3);
                break;

            case "MoveChartSkip": //happens in Chessman
                TText.text = "If the dots are golden, then this piece can skip other pieces";

                Arrow.SetActive(true);
                Arrow.transform.position = MC.transform.position;
                Arrow.transform.localScale = new Vector3(3, 3);
                break;

            case "MoveChartSkipAllies": //happens in Chessman
                TText.text = "If the dots are blue, then this piece can skip allied pieces only";

                Arrow.SetActive(true);
                Arrow.transform.position = MC.transform.position;
                Arrow.transform.localScale = new Vector3(3, 3);
                break;

            case "Stats": //happens in Chessman
                TText.text = "When you select a piece you can see how much HP it has and ATK \n\n HP stands for Hit Points and if it reaches 0 the piece is destroyed \n ATK is the power of the Attack of this piece ";

                GameObject w = GameObject.FindWithTag("WStats");
                Arrow.SetActive(true);
                Arrow.transform.position = w.transform.position;
                Arrow.transform.localScale = new Vector3(4.0f , 2.0f);
                break;

            case "Attack": //happens in MovePlate
                TText.text = "When one of your pieces can attack an enemy piece, an Attack symbol will appear \n\n Click on it to start an Attack ";

                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = AttackMovePlate;
                Example.GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
                break;

            case "BattleForecast": //happens in MovePlate
                TText.text = "Here you can see the future outcome of the attack \n\n You have to click on the target piece again to confirm the attack";

                GameObject bf = GameObject.FindWithTag("BattleForecast");
                Arrow.SetActive(true);
                Arrow.transform.position = bf.transform.position;
                Arrow.transform.localScale = new Vector3(3.0f, 2.0f);
                break;

            case "AttackReady": //happens in MovePlate
                TText.text = "That's it! Now you have declared an attack! \n Your turn is not over yet, you can declare as much attacks as possible in a single turn or even change the objective of an attack that you already declared \n\n The attacks will be performed at the end of your turn, and your turn will only end after you move a piece";
                break;

            case "AttackColors": //chained
                TText.text = "As you can see, pieces that are going to be attacked are highlighted in a color, and the pieces that are attacking have a sword icon with the same color as their targets \n\n After an attack is declared, the piece will keep on attacking its target every turn until the target is destroyed or if the piece declares a new target";

                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = SwordIcon;
                Example.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                break;

            case "AttackDodge": //happens in Chessman
                TText.text = "If the target of an attack moves out of the range of the attacker, then it will avoid the attack \n However, the piece is still marked and will be attacked again if it is in range";

                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = SwordIcon;
                Example.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
                break;

            case "CheckEnemy": // chained to Stats
                TText.text = "You can check the enemy pieces move range, ATK or HP by just clicking and selecting them";
                break;

            case "PerkDisplay": // happens in PerksDisplay
                TText.text = "This widget will show you the different perks for your Set \n Every Set starts with at least 2 main perks and will gain 2 bonuses when certain requirements are met \n\n Click on the buttons to read the bonuses and perks";

                Arrow.SetActive(true);
                Arrow.transform.position = WPerks.transform.position;
                Arrow.transform.localScale = new Vector3(4, 1.8f);
                break;

            case "OutnumberBonus":  // happens in PerksDisplay or Chessman
                TText.text = "The Outnumber Bonus will be activated when you take 6 enemy pieces \n\n It usually involves an enhancement of some of your own pieces";

                Arrow.SetActive(true);
                Arrow.transform.position = WPerks.transform.position;
                Arrow.transform.localScale = new Vector3(4, 1.8f);

                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = OBonus;
                Example.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;

            case "MoraleBonus":  // happens in PerksDisplay or Chessman
                TText.text = "The Morale Bonus activates when your King's HP is lower than 25 \n\n It usually involves a negative bonus for the enemy pieces";

                Arrow.SetActive(true);
                Arrow.transform.position = WPerks.transform.position;
                Arrow.transform.localScale = new Vector3(4, 1.8f);

                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = MBonus;
                Example.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;

            case "Promotion":  // happens in PerksDisplay or Chessman
                TText.text = "When a Pawn arrives to the opposite end of the board it will promote to an Unique Piece, depending on your Set \n\n Also, your last Pawn remaining will always promote";

                Arrow.SetActive(true);
                Arrow.transform.position = WPerks.transform.position;
                Arrow.transform.localScale = new Vector3(4, 1.8f);                
                break;

            case "UniquePieces": //chained
                TText.text = "Unique Pieces are stronger pieces that you can access by promoting Pawns \n\n Remember to check the Move Chart on the top corner to learn how they move";

                Arrow.SetActive(true);
                Arrow.transform.position = WPerks.transform.position;
                Arrow.transform.localScale = new Vector3(4, 1.8f);

                GameObject p = GameObject.FindWithTag("PromotionWhite");
                Example.SetActive(true);
                Example.GetComponent<Image>().sprite = p.GetComponent<Image>().sprite;
                Example.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;

            case "PieceTaken": //happens in Chessman
                TText.text = "When a Piece's HP is reduced to 0 it is taken \n\n The piece that gave the final blow will be moved to the square of the victim, always remember this as it could put your piece in danger";
                break;
                 
            case "GameOverVersus":  //happens in Chessman
                TText.text = "The game ends when a King is defeated \n\n The defeated King loses and the surviving King wins!";
                break;

            case "GameOverCampaign":  //happens in Chessman
                TText.text = "You will lose if your King is defeated! \n\n You will win if you manage to kill all the enemy pieces!";
                break;
        }
    }

    public void ChainLessons() //this activates in the Pass button
    {
        LessonRead();

        switch (CurrentLesson)
        {
            case "Intro":
                ShowTutorial("ClickPiece");
                break;

            case "MoveChart":
                ShowTutorial("MoveChartDots");
                break;

            case "AttackReady":
                ShowTutorial("AttackColors");
                break;

            case "Stats":
                ShowTutorial("CheckEnemy");
                break;

            case "Promotion":
                ShowTutorial("UniquePieces");
                break;
        }
    }

    public void LessonRead() //this activates in the Pass buton
    {
        switch (CurrentLesson)
        {
            case "Intro":
                PlayerPrefs.SetString("Tut Intro", "Read");
                break;

            case "ClickPiece":
                PlayerPrefs.SetString("Tut ClickPiece", "Read");
                break;

            case "Move":
                PlayerPrefs.SetString("Tut Move", "Read");
                break;

            case "MoveChart":
                PlayerPrefs.SetString("Tut MoveChart", "Read");
                break;

            case "MoveChartDots":
                PlayerPrefs.SetString("Tut MoveChartDots", "Read");
                break;

            case "MoveChartLine":
                PlayerPrefs.SetString("Tut MoveChartLine", "Read");
                break;

            case "MoveChartSkip":
                PlayerPrefs.SetString("Tut MoveChartSkip", "Read");
                break;

            case "MoveChartSkipAllies":
                PlayerPrefs.SetString("Tut MoveChartSkipAllies", "Read");
                break;

            case "Stats":
                PlayerPrefs.SetString("Tut Stats", "Read");
                break;

            case "Attack":
                PlayerPrefs.SetString("Tut Attack", "Read");
                break;

            case "BattleForecast":
                PlayerPrefs.SetString("Tut BattleForecast", "Read");
                break;

            case "AttackReady":
                PlayerPrefs.SetString("Tut AttackReady", "Read");
                break;

            case "AttackColors":
                PlayerPrefs.SetString("Tut AttackColors", "Read");
                break;

            case "AttackDodge":
                PlayerPrefs.SetString("Tut AttackDodge", "Read");
                break;

            case "CheckEnemy":
                PlayerPrefs.SetString("Tut CheckEnemy", "Read");
                break;

            case "PerkDisplay":
                PlayerPrefs.SetString("Tut PerkDisplay", "Read");
                break;

            case "OutnumberBonus":
                PlayerPrefs.SetString("Tut OutnumberBonus", "Read");
                break;

            case "MoraleBonus":
                PlayerPrefs.SetString("Tut MoraleBonus", "Read");
                break;

            case "Promotion":
                PlayerPrefs.SetString("Tut Promotion", "Read");
                break;

            case "UniquePieces":
                PlayerPrefs.SetString("Tut UniquePieces", "Read");
                break;

            case "PieceTaken":
                PlayerPrefs.SetString("Tut PieceTaken", "Read");
                break;

            case "GameOverVersus":
                PlayerPrefs.SetString("Tut GameOverVersus", "Read");
                break;

            case "GameOverCampaign":
                PlayerPrefs.SetString("Tut GameOverCampaign", "Read");
                break;
        }
    }

}
