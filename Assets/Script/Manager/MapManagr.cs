using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;
using UniRx;


public class MapManagr : MonoBehaviour
{
    //�}�b�v�d�l
    //�}�b�v���Ƃɓ��F�������Đi���тɃA�C�e���Ⴆ�邪�����z���Ȃ��Ȃ�


    private RaycastHit2D _hit;
    private Vector2 _hitPos;
    [SerializeField]
    private GameObject cursorObj;

    [SerializeField]
    Tilemap blockTilemap;

    private void Start()
    {
        

        //���t���[���Ă΂��
        this.UpdateAsObservable().Subscribe(_ =>
        {
            Action();
        }).AddTo(gameObject);
    }
    private void Update()
    {
        //��ʂ��N���b�N������Ă΂��
        if (Input.GetMouseButton(0))
        {
            //�N���b�N���邩�A�N���b�N�𗣂��Ɣ���
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
            {    //�N���b�N�b�����Ƃ��̓}�E�X�̈ʒu��0�ŗ���
                return;
            }

            var position = gameObject.transform.position;
            if (Camera.main == null) return;

            Vector3 diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - position).normalized;
            _hitPos = diff;
            _hit = Physics2D.Raycast(position, diff/*����*/, 1/*����*/, LayerMask.GetMask("Block"));
        }
    }
    private void Action()
    {
        if (_hit.collider == null) return;
        var tilePos = blockTilemap.WorldToCell(_hit.point + _hitPos);
        blockTilemap.SetTile(tilePos, null);    //����
    }
}
