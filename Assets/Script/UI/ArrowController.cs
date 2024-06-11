using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : SingletonMonoBehaviour<ArrowController>
{
    public GameObject arrowPool;
    public GameObject arrowObject; // 矢印オブジェクト
    public float distanceFromMouse = 10f;
    private GameObject m_arrowObj;
    private bool isPoped = false;

    public void PopArrow()
    {
        if (isPoped) return;
        m_arrowObj = Instantiate(arrowObject, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        m_arrowObj.transform.SetParent(arrowPool.transform, false);
        isPoped = true;
    }
    public void UnPopArrow()
    {
        if (!isPoped) return;
        if (m_arrowObj != null) Destroy(m_arrowObj);
        m_arrowObj = null;
        isPoped = false;
    }
    void Update()
    {
        if (isPoped)
        {
            // マウスカーソルの位置を取得
            Vector3 mousePosition = Input.mousePosition;

            m_arrowObj.transform.position = mousePosition;

            // マウスの位置に向かう方向ベクトルを計算し、正規化
            Vector3 direction = (mousePosition - transform.position).normalized;

            // マウスの位置から若干距離を離す
            Vector3 targetPosition = mousePosition - direction * distanceFromMouse;

            // Imageを新しい位置に移動
            m_arrowObj.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);

            // Imageをマウスの方向に向ける
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            m_arrowObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //// 画面の中央を基準にして矢印の位置を調整
            //float screenWidth = Screen.width;
            //if (mousePosition.x < screenWidth / 2)
            //{
            //    // 画面の左半分にある場合
            //    m_arrowObj.transform.localPosition -= new Vector3(5f, 0f, 0f);
            //}
            //else
            //{
            //    // 画面の右半分にある場合
            //    m_arrowObj.transform.localPosition -= new Vector3(10f, 0f, 0f);
            //}
        }
    }
}
