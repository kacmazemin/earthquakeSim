using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class readFile : MonoBehaviour
{
    //file paths
    string xLightPath = "Assets/eq/eq-light-x.txt";
    string zLightPath = "Assets/eq/eq-light-z.txt";
    string xStrongPath = "Assets/eq/eq-strong-x.txt";
    string zStrongPath = "Assets/eq/eq-strong-z.txt";

    ArrayList xValues = new ArrayList();
    ArrayList zValues = new ArrayList();

    public GameObject timeProgress;

    Slider timeSlider;

    public GameObject textArea;

    Text textAreaComponent;

    public GameObject floor;

    Transform floorT;

    Rigidbody rbFloor;

    private IEnumerator coroutine;

    int currentValue;

    float veloX;
    float veloZ;

    float avarageVelo;

    public Dropdown drp;

    bool startButtonState = true;

    //read files and return as arrayList{string,string}
    ArrayList WriteString(string filePath)
    {
        ArrayList datas = new ArrayList();

        string line;

        using (StreamReader reader = new StreamReader(filePath))
        {
            line = reader.ReadLine();

            while (line != null)
            {
                datas.Add(line.Split(','));
                line = reader.ReadLine();
            }
            reader.Close();
        }
        return datas;
    }

    void Start()
    {
        timeSlider = timeProgress.GetComponent<Slider>();
        textAreaComponent = textArea.GetComponent<Text>();

    }

    public void startEq()
    {
        //control for first start
        if (startButtonState)
        {
            drp.interactable = false;

            floorT = floor.GetComponent<Transform>();
            rbFloor = floor.GetComponent<Rigidbody>();
            //light eq
            if (drp.GetComponent<Dropdown>().value == 0)
            {
                xValues = WriteString(xLightPath);                
                zValues = WriteString(zLightPath);

            }
            //strong eq
            if (drp.GetComponent<Dropdown>().value == 1)
            {
                xValues = WriteString(xStrongPath);
                zValues = WriteString(zStrongPath);

            }
            //set values for first start
            currentValue = 0;

            timeSlider.maxValue = float.Parse(((string[])xValues[xValues.Count - 1])[0]);

            coroutine = Fade();

            StartCoroutine(coroutine);

            startButtonState = false;

        }
        //for pause>start
        if (!startButtonState)
        {
            StartCoroutine(coroutine);

        }
    }

    public void stopEq()
    {
        StopCoroutine(coroutine);
        rbFloor.velocity =Vector3.zero;

    }

    IEnumerator Fade()
    {
        for (int i = currentValue; i < xValues.Count; i++)
        {

            currentValue = i;

            rbFloor.velocity = new Vector3(float.Parse(((string[])xValues[i])[1]), 0f, float.Parse(((string[])zValues[i])[1]));

            timeSlider.value = float.Parse(((string[])xValues[i])[0]);
            //delay for 0.01f second
            yield return new WaitForSeconds(.01f);

            //calculate avarage velocity
            veloX += float.Parse(((string[])xValues[i])[1]);

            veloZ += float.Parse(((string[])zValues[i])[1]);

            textAreaComponent.text =
                "Anlık x ivme="+ float.Parse(((string[])xValues[i])[1]) +
                "\nOrtalama X ivme=" + veloX/currentValue +
                "\nAnlık Z ivme=" + float.Parse(((string[])xValues[i])[1])+
                "\nOrtalama Z ivme="+veloZ/currentValue;
        }
        // 
        currentValue = 0;
        startButtonState = true;
        timeSlider.value = 0;
        drp.interactable = true;
        rbFloor.velocity = Vector3.zero;
       
    }

    void Update()
    {
        
    }

}
