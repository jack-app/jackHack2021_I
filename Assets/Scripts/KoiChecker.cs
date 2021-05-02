using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KoiChecker : MonoBehaviour
{
    public List<GameObject> koi = new List<GameObject>();

    [SerializeField]
    List<LineRenderer> amidaLineList = new List<LineRenderer>();

    [SerializeField]
    Sprite clearKoiSprite;
    [SerializeField]
    Sprite normalKoiSprite;

    public List<int> koiSetList = new List<int>();

    //[SerializeField]
    //private List<bool> fuFallCheck = new List<bool>();
    [SerializeField]
    private List<bool> koiCatchCheck = new List<bool>();

    [SerializeField]
    OfuMover ofuMover;

    private bool waitReset = false;

    // Start is called before the first frame update
    void Start()
    {
        //fuFallCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
        koiCatchCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
    }

    public void ResetKoiChecker()
    {
        foreach(var k in koi)
        {
            k.GetComponent<SpriteRenderer>().sprite = normalKoiSprite;
        }

        koi = new List<GameObject>();
        koiSetList = new List<int>();
        waitReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitReset)
        {
            if (ofuMover.ofuStop.All(x => x))
            {
                if (koiSetList.All(x => koiCatchCheck[x]))
                {
                    Debug.Log("clear");
                }
                else
                {
                    Debug.Log("failed");
                }
                waitReset = true;
                koiCatchCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
            }
        }
    }

    public bool CheckFu(int fuIndex)
    {
        //fuFallCheck[fuIndex] = true;
        Debug.Log(fuIndex);
        if(koiSetList.Any(x => x == fuIndex))
        {
            koiCatchCheck[fuIndex] = true;
            koi[koiSetList.IndexOf(fuIndex)].GetComponent<SpriteRenderer>().sprite = clearKoiSprite;
            return true;
        }
        else
        {
            return false;
        }
    }
}
