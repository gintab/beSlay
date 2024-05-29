using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : SingletonMonoBehaviour<ArrowController>
{
    public GameObject arrowPool;
    public GameObject arrowObject; // ���I�u�W�F�N�g
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
            // �}�E�X�J�[�\���̈ʒu���擾
            Vector3 mousePosition = Input.mousePosition;

            m_arrowObj.transform.position = mousePosition;

            // �}�E�X�̈ʒu�Ɍ����������x�N�g�����v�Z���A���K��
            Vector3 direction = (mousePosition - transform.position).normalized;

            // �}�E�X�̈ʒu����኱�����𗣂�
            Vector3 targetPosition = mousePosition - direction * distanceFromMouse;

            // Image��V�����ʒu�Ɉړ�
            m_arrowObj.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);

            // Image���}�E�X�̕����Ɍ�����
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            m_arrowObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //// ��ʂ̒�������ɂ��Ė��̈ʒu�𒲐�
            //float screenWidth = Screen.width;
            //if (mousePosition.x < screenWidth / 2)
            //{
            //    // ��ʂ̍������ɂ���ꍇ
            //    m_arrowObj.transform.localPosition -= new Vector3(5f, 0f, 0f);
            //}
            //else
            //{
            //    // ��ʂ̉E�����ɂ���ꍇ
            //    m_arrowObj.transform.localPosition -= new Vector3(10f, 0f, 0f);
            //}
        }
    }
}
