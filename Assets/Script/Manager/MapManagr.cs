using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;
using UniRx;


public class MapManagr : MonoBehaviour
{
    //マップ仕様
    //マップごとに特色があって進たびにアイテム貰えるが持ち越せないなど


    private RaycastHit2D _hit;
    private Vector2 _hitPos;
    [SerializeField]
    private GameObject cursorObj;

    [SerializeField]
    Tilemap blockTilemap;

    private void Start()
    {
        

        //毎フレーム呼ばれる
        this.UpdateAsObservable().Subscribe(_ =>
        {
            Action();
        }).AddTo(gameObject);
    }
    private void Update()
    {
        //画面をクリックしたら呼ばれる
        if (Input.GetMouseButton(0))
        {
            //クリックするか、クリックを離すと反応
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
            {    //クリック話したときはマウスの位置が0で来る
                return;
            }

            var position = gameObject.transform.position;
            if (Camera.main == null) return;

            Vector3 diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - position).normalized;
            _hitPos = diff;
            _hit = Physics2D.Raycast(position, diff/*方向*/, 1/*距離*/, LayerMask.GetMask("Block"));
        }
    }
    private void Action()
    {
        if (_hit.collider == null) return;
        var tilePos = blockTilemap.WorldToCell(_hit.point + _hitPos);
        blockTilemap.SetTile(tilePos, null);    //消去
    }
}
