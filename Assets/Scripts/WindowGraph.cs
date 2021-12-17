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
    private List<RectTransform> labelList = new List<RectTransform>();
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
    public void ShowGraph(List<float> valueList, List<float> labels, float ymax = 100f)
    {
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
            labelX.GetComponent<Text>().text = labels[i].ToString("0.0");
            labelList.Add(labelX);
        }
        int separatorCount = 10;
        for (int i = 0; i < separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateX);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedVal = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-10f, normalizedVal * graphHeight + 7f);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedVal * ymax).ToString();
            labelList.Add(labelY);
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
        foreach (var c in labelList)
        {
            // Destroy(c.gameObject);
            c.gameObject.SetActive(false);
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
