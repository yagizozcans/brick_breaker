using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapCreatorManager : MonoBehaviour
{
    public GameObject boxPrefab;

    public float spacing;
    public float boxPrefabSize;

    public float topGap;
    public float bottomGap;

    public GameObject boxParent;
    public GameObject boxLineParent;

    public int boxI;
    public int boxJ;

    public ijBox[] ijBoxArray;

    public Slider spacingSlider;
    public Slider sizeSlider;
    public Slider brushSize;

    public GameObject blockButtonsParent;

    int blockType;

    public Color hardBlockColor;

    public GameObject mainMenu;

    public GameObject firstBall;
    public class ijBox
    {
        int[] iBox;
        int[] jBox;
    }

    private void Start()
    {
            ChangeBlock(0);    
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            CheckRays();
        }
        sizeSlider.onValueChanged.AddListener(delegate { ResizeBall(); });
    }

    public void CreatingBackgroundBoxes()
    {
        Vector2 screenXnY = (Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)));

        boxParent = new GameObject();

        spacing = spacingSlider.value;
        boxPrefabSize = sizeSlider.value;

        foreach(Transform obj in boxLineParent.transform)
        {
            Destroy(obj.gameObject);
        }

        boxI = 0;
        boxJ = 0;

        boxPrefab.transform.localScale = Vector2.one * boxPrefabSize;

        boxParent.transform.position = screenXnY;

        float width = (float)(Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height);
        transform.localScale = new Vector3(width / 9, width / 9, width / 9);

        float counterX = -screenXnY.x;
        float counterY = screenXnY.y - topGap;

        while(counterX + spacing + boxPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.x < screenXnY.x)
        {
            counterX += spacing + boxPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.x;
            GameObject box = Instantiate(boxPrefab, new Vector2(counterX, screenXnY.y), Quaternion.identity,boxParent.transform);
            box.GetComponent<MapBoxIdentifier>().thisI = boxI;
            boxI++;
        }
        while (counterY - spacing - boxPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.y > -screenXnY.y + bottomGap)
        {
            counterY -= spacing + boxPrefab.transform.GetComponent<SpriteRenderer>().bounds.size.y;
            GameObject boxLine = Instantiate(boxParent, new Vector2(boxParent.transform.position.x, counterY), Quaternion.identity, boxLineParent.transform);
            for(int i = 0; i < boxI; i++)
            {
                boxLine.transform.GetChild(i).transform.GetComponent<MapBoxIdentifier>().thisJ = boxJ;
            }
            boxJ++;
        }
        Destroy(boxParent.gameObject);
    }

    public void ChangeBlock(int index)
    {
        for(int i = 0; i < blockButtonsParent.transform.childCount; i++)
        {
            blockButtonsParent.transform.GetChild(i).GetComponent<Image>().color = blockButtonsParent.transform.GetChild(i).GetComponent<Button>().colors.normalColor;
        }

        blockButtonsParent.transform.GetChild(index).GetComponent<Image>().color = blockButtonsParent.transform.GetChild(index).GetComponent<Button>().colors.selectedColor;

        blockType = index;
    }

    void CheckRays()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),brushSize.value,Vector2.zero);


        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (blockType == 0)
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.white;
                    hit.transform.GetComponent<MapBoxIdentifier>().blockType = 0;
                }
                else if (blockType == 1)
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = hardBlockColor;
                    hit.transform.GetComponent<MapBoxIdentifier>().blockType = 1;
                }
            }
        }
    }

    public void StartPlay()
    {
        mainMenu.SetActive(false);
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("block");
        foreach(GameObject box in allBoxes)
        {
            if(box.GetComponent<MapBoxIdentifier>().blockType == 2)
            {
                Destroy(box);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void ResizeBall()
    {
        firstBall.transform.localScale = Vector2.one * sizeSlider.value;
    }
}
