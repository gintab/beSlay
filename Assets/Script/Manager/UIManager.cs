using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : SingletonMonoBehaviour<UImanager>
{
    public Text damageText = null;
    public Canvas m_canvas = null;

    public void PopDamageText(Transform character,float damage)
    {
        if (damageText == null || character == null) return;
        Text dmg_txt;
        dmg_txt = Instantiate(damageText, new Vector3(0, 0, 0), Quaternion.identity);
        dmg_txt.text = damage.ToString();
        dmg_txt.transform.SetParent(character.transform, false);
        dmg_txt.transform.localPosition = new Vector3(0, 0, 0);
    }
}
