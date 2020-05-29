using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICONTROl : MonoBehaviour
{
    public Text scrolls;
    // Start is called before the first frame update
    public SimpleHealthBar LifeBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string s = string.Format("Scrolls: {0}/2", GetComponent<PlayerController2>().getScrolls());
        scrolls.text = s;
        LifeBar.UpdateBar(GetComponent<PlayerController2>().getLife(), 5.0f);
    }
}
