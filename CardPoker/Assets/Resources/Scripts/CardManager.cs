using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS_Cam;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class Card
{
    public string CardType;   // Q = 1, W = 2, E = 3, R = 4
    public int CardNum;
    public string CardColor;
}

public class CardManager : MonoBehaviour
{
    List<Card> Cards = new List<Card>();

    GameManager gamemanager;

    public void Resetcubesize() { Cubes[Cubes.Count - 1].transform.localScale = Cubes[0].transform.localScale; } // 전부 다 같은 사이즈로 바꾸는걸 고민해볼것. 사이즈가 바뀔때 이펙트 넣는것도 고려

    private RTS_Camera cam;

    bool xstate;
    bool zstate;

    bool stackcheck = true;
    int stack = 0;
    public static int numItem;

    int numWhiteCard;
    public static int playCard;
    public static bool boom = false;
    
    private void Start()
    {
        playCard = 0;
        FloatingTextController.Initialize();
        numItem = 0;
        numWhiteCard = 0;
    }

    public int numCube()
    {
        playCard = Cubes.Count - numWhiteCard;
        return Cubes.Count;
    }

    public bool getxState()
    {
        if (Cubes.Count <= 1)
            return true;
        else
            return xstate;
    }

    public bool getzState()
    {
        if (Cubes.Count <= 1)
            return true;
        else
            return zstate;
    }

    private List<GameObject> Cubes = new List<GameObject>();

    public void InputCard(string Type, int Num, string Color)
    {
        Cards.Add(new Card { CardType = Type, CardNum = Num, CardColor = Color });
    }

    public void CreateCube(GameObject[] Cube_Type1, GameObject[] Cube_Type2)
    {
        if (Cards[Cards.Count - 1].CardType == "Q" || Cards[Cards.Count - 1].CardType == "E")
        {
            Cubes.Add(GameObject.Instantiate(Cube_Type1[Cards[Cards.Count - 1].CardNum]));
        }
        else
        {
            Cubes.Add(GameObject.Instantiate(Cube_Type2[Cards[Cards.Count - 1].CardNum]));
        }

        if (Cubes.Count - 1 == 0)
        {
            Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(0.0f, 0.05f, 0.0f);
        }
        else
        {
            if (Cards[Cards.Count - 1].CardType == "Q" || Cards[Cards.Count - 1].CardType == "E")
            {
                Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x - Cubes[Cubes.Count - 2].transform.localScale.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z);
                //Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z);
                Cubes[Cubes.Count - 1].transform.localScale = Cubes[Cubes.Count - 2].transform.localScale;
            }
            else
            {
                Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z - Cubes[Cubes.Count - 2].transform.localScale.z);
                //Cubes[Cubes.Count - 1].transform.localPosition = new Vector3(Cubes[Cubes.Count - 2].transform.position.x, Cubes[Cubes.Count - 2].transform.position.y + 0.1f, Cubes[Cubes.Count - 2].transform.position.z);
                Cubes[Cubes.Count - 1].transform.localScale = Cubes[Cubes.Count - 2].transform.localScale;
            }
        }

        if (Cubes.Count > 1)
        {
            GameObject.Find("GameManager").transform.gameObject.tag = "NotTarget";

            for (int i = 0; i < Cubes.Count - 1; i++)
                Cubes[i].transform.gameObject.tag = "NotTarget";

            Cubes[Cubes.Count - 2].transform.gameObject.tag = "NowTarget";
        }
        
        Cubes[Cubes.Count - 1].transform.localRotation = Quaternion.identity;
    }

    public void ResizeCube(bool card)
    {
        if (Cubes.Count > 1)
        {
            float hangover;

            if (card == true)
            {
                hangover = Cubes[Cubes.Count - 1].transform.position.x - Cubes[Cubes.Count - 2].transform.position.x;
                SplitCubeOnX(hangover);
            }
            else
            {
                hangover = Cubes[Cubes.Count - 1].transform.position.z - Cubes[Cubes.Count - 2].transform.position.z;
                SplitCubeOnZ(hangover);
            }
        }
        usedItem();
    }

    public void usedItem()
    {
        numItem = 5;
    }

    private void SplitCubeOnX(float hangover)
    {
        float newXSize = Cubes[Cubes.Count - 2].transform.localScale.x - Mathf.Abs(hangover);

        float newXPosition = Cubes[Cubes.Count - 2].transform.position.x + (hangover / 2);


        Cubes[Cubes.Count - 1].transform.localScale = new Vector3(newXSize, Cubes[Cubes.Count - 1].transform.localScale.y, Cubes[Cubes.Count - 1].transform.localScale.z);
        Cubes[Cubes.Count - 1].transform.position = new Vector3(newXPosition, Cubes[Cubes.Count - 1].transform.position.y, Cubes[Cubes.Count - 1].transform.position.z);
    }

    private void SplitCubeOnZ(float hangover)
    {
        float newZSize = Cubes[Cubes.Count - 2].transform.localScale.z - Mathf.Abs(hangover);

        float newZPosition = Cubes[Cubes.Count - 2].transform.position.z + (hangover / 2);

        Cubes[Cubes.Count - 1].transform.localScale = new Vector3(Cubes[Cubes.Count - 1].transform.localScale.x, Cubes[Cubes.Count - 1].transform.localScale.y, newZSize);
        Cubes[Cubes.Count - 1].transform.position = new Vector3(Cubes[Cubes.Count - 1].transform.position.x, Cubes[Cubes.Count - 1].transform.position.y, newZPosition);
    }

    public float LastCubePosion()
    {
        if (Cubes.Count > 1)
            return Cubes[Cubes.Count - 2].transform.position.y;
        else
            return Cubes[0].transform.position.y;
    }

    public void CheckCollision()
    {
        if (Cubes.Count > 1)
        {
            if (Cubes[Cubes.Count - 1].transform.position.x > Cubes[Cubes.Count - 2].transform.position.x - Cubes[Cubes.Count - 1].transform.localScale.x &&
                Cubes[Cubes.Count - 1].transform.position.x < Cubes[Cubes.Count - 2].transform.position.x + Cubes[Cubes.Count - 1].transform.localScale.x)
            {
                xstate = true;
            }
            else
            {
                xstate = false;
            }

            if (Cubes[Cubes.Count - 1].transform.position.z > Cubes[Cubes.Count - 2].transform.position.z - Cubes[Cubes.Count - 1].transform.localScale.z &&
                Cubes[Cubes.Count - 1].transform.position.z < Cubes[Cubes.Count - 2].transform.position.z + Cubes[Cubes.Count - 1].transform.localScale.z)
            {
                zstate = true;
            }

            else
            {
                zstate = false;
            }
        }
    }

    public void ControlCube(int num)
    {
        int temp = Cards.Count;
        boom = true;
        for (int i = num; i > 0; i--)
        {
            Cubes[temp - i].GetComponent<FixedCube>().fixedCube(stack - 1);
            Cards[temp - i].CardNum = 10;
            Cards[temp - i].CardType = "Joker";
            Cards[temp - i].CardColor = "White";
            numWhiteCard++;
            Debug.Log("whitecard : " + numWhiteCard);
        }
    }

    private void stackCheck()
    {
        if (stackcheck == true)
            stack++;
        else
            stack--;

        if (stack > 12)
            stackcheck = false;

        else if (stack < 2)
            stackcheck = true;
    }

    private void checkItem()
    {
        numItem = Random.Range(1,5);

        Debug.Log("item : " + numItem);

        for (int i = 1; i < Cubes.Count; i++)
        {
            Cubes[i].transform.localScale = Cubes[0].transform.localScale;
        }
        for (int i = 0; i < Cubes.Count - 1; i++)
        {
            Cubes[i].transform.localPosition = new Vector3(Cubes[Cubes.Count - 1].transform.localPosition.x, Cubes[i].transform.localPosition.y, Cubes[Cubes.Count - 1].transform.localPosition.z);
        }

        if (numItem == 2)
            ItemTextController.CreateFloatingText("Accel!", Cubes[Cubes.Count - 1].transform);
        if (numItem == 3)
            ItemTextController.CreateFloatingText("Slow!", Cubes[Cubes.Count - 1].transform);
        if (numItem == 4)
            ItemTextController.CreateFloatingText("Defence!", Cubes[Cubes.Count - 1].transform);
    }

    public void scoreCheck()
    {
        int num1, num2, num3;

        if (Cubes.Count > 1)
        {
            if (Cards[Cards.Count - 1].CardColor == Cards[Cards.Count - 2].CardColor)           ///////////////////// 같은 색 일때
            {
                if (Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == 1 || Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == -1)  // 연속된 수 일때 제거
                {
                    num1 = Cards[Cards.Count - 2].CardNum + 1;
                    num2 = Cards[Cards.Count - 1].CardNum + 1;

                    int difscore = Score.scoreValue;

                    Score.scoreValue += (num1 + num2) * MultipleScore.multipleValue;

                    difscore = Score.scoreValue - difscore;

                    Debug.Log("m : " + MultipleScore.multipleValue);
                    Debug.Log("1 : " + num1);
                    Debug.Log("2 : " + num2);

                    FloatingTextController.CreateFloatingText("+" + difscore.ToString(), Cubes[Cubes.Count - 1].transform);
                    stackCheck();
                    ControlCube(2);
                    checkItem();
                    MultipleScore.multipleValue = 2;
                    Score.endingScore += difscore;
                }

                else if (Cubes.Count > 2)
                {
                    if (Cards[Cards.Count - 2].CardColor == Cards[Cards.Count - 3].CardColor) // 세개가 같은 색 일때
                    {
                        num1 = Cards[Cards.Count - 3].CardNum + 1;
                        num2 = Cards[Cards.Count - 2].CardNum + 1;
                        num3 = Cards[Cards.Count - 1].CardNum + 1;

                        int difscore = Score.scoreValue;

                        Score.scoreValue += (num1 + num2 + num3) * MultipleScore.multipleValue;

                        difscore = Score.scoreValue - difscore;

                        Debug.Log("m : " + MultipleScore.multipleValue);
                        Debug.Log("1 : " + num1);
                        Debug.Log("2 : " + num2);
                        Debug.Log("3 : " + num3);

                        FloatingTextController.CreateFloatingText("+" + difscore.ToString(), Cubes[Cubes.Count - 1].transform);
                        stackCheck();
                        ControlCube(3);
                        checkItem();
                        MultipleScore.multipleValue = 3;
                        Score.endingScore += difscore;
                    }
                }
            }

            else // 다른 색 일때
            {
                if (Cards[Cards.Count - 1].CardNum == Cards[Cards.Count - 2].CardNum) // 같은 숫자일때
                {
                    num1 = Cards[Cards.Count - 2].CardNum + 1;
                    num2 = Cards[Cards.Count - 1].CardNum + 1;

                    int difscore = Score.scoreValue;
                    Score.scoreValue += (num1 + num2) * MultipleScore.multipleValue;
                    difscore = Score.scoreValue - difscore;

                    Debug.Log("m : " + MultipleScore.multipleValue);
                    Debug.Log("1 : " + num1);
                    Debug.Log("2 : " + num2);

                    FloatingTextController.CreateFloatingText("+" + difscore.ToString(), Cubes[Cubes.Count - 1].transform);
                    stackCheck();
                    ControlCube(2);
                    checkItem();
                    MultipleScore.multipleValue = 2;
                    Score.endingScore += difscore;
                }

                if (Cubes.Count > 2)
                {
                    if (Cards[Cards.Count - 1].CardColor == Cards[Cards.Count - 3].CardColor)
                    {
                        if (Cards[Cards.Count - 1].CardNum - Cards[Cards.Count - 2].CardNum == 1)
                        {
                            if (Cards[Cards.Count - 2].CardNum - Cards[Cards.Count - 3].CardNum == 1)
                            {
                                num1 = Cards[Cards.Count - 3].CardNum + 1;
                                num2 = Cards[Cards.Count - 2].CardNum + 1;
                                num3 = Cards[Cards.Count - 1].CardNum + 1;

                                int difscore = Score.scoreValue;

                                Score.scoreValue += (num1 + num2 + num3) * MultipleScore.multipleValue;

                                difscore = Score.scoreValue - difscore;

                                Debug.Log("m : " + MultipleScore.multipleValue);
                                Debug.Log("1 : " + num1);
                                Debug.Log("2 : " + num2);
                                Debug.Log("3 : " + num3);

                                FloatingTextController.CreateFloatingText("+" + difscore.ToString(), Cubes[Cubes.Count - 1].transform);
                                stackCheck();
                                ControlCube(3);
                                checkItem();
                                MultipleScore.multipleValue = 3;
                                Score.endingScore += difscore;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log(Score.endingScore);
    }
}
