using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WindowGraph : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        // var valList = new List<float> { 5, 98.5f, 56, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        // ShowGraph(valList);
    }

    private GameObject CreateCircle(Vector2 anchoredposition)
    {
        // anchoredposition += new Vector2(2, 2);
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredposition;
        // gameObject.
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    public void ShowGraph(List<float> valueList, float ymax = 100f)
    {

        if (labelTemplateX == null)
        {
            Debug.Log("X NULL");
        }
        else
        {
            Debug.Log("X GUD");
        }
        if (labelTemplateY == null)
        {
            Debug.Log("Y NULL");
        }
        else
        {
            Debug.Log("Y GUD");
        }

        ClearDataPoints();
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        // ymax = 100f;
        float xsize = graphWidth / valueList.Count;
        GameObject lastCircle = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xpos = i * xsize;
            float ypos = valueList[i] / ymax * graphHeight;
            var circle = CreateCircle(new Vector2(xpos, ypos));
            if (lastCircle != null)
            {
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, circle.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = circle;
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xpos, -2f);
            labelX.GetComponent<Text>().text = i.ToString();
        }
    }
    private void ClearDataPoints()
    {
        var circles = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "circle").ToArray();
        var connections = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "dotConnection").ToArray();
        // var xlabels = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "dotConnection").ToArray();
        // for(int i=0;i < circles.Count; i++){}
        foreach (var c in circles)
        {
            Destroy(c.gameObject);
        }
        foreach (var c in connections)
        {
            Destroy(c.gameObject);
        }
        // var circles = transform.Find("circle");
    }
    private void CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosB - dotPosA).normalized;
        float dist = Vector2.Distance(dotPosA, dotPosB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(dist, 3f);
        rectTransform.anchoredPosition = dotPosA + dir * dist * .5f;
        // rectTransform.an
        rectTransform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI));
    }

    void Update()
    {

    }
}
