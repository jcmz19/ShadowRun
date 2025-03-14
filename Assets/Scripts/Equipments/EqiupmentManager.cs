using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ص��ɲٿؽ�ɫ�ϣ�
/// ���ṩ��̬��
/// ��һ����̬�ṹ��Arr��¼װ����Ϣ
/// </summary>
public class EqiupmentManager : MonoBehaviour
{

    private EquipmentInfoStruct[] equipmentArr;
    public int currentEquipNum;
    private int currentEquipCapcity;
    // Start is called before the first frame update
    void Start()
    {
        equipmentArr = new EquipmentInfoStruct[Constants.MAX_EQUIPMENT_CAP];
        currentEquipNum = 0;
        currentEquipCapcity = 0;
    }

    // Update is called once per frame
    void Update()
    {
     /*   DetectEquipmentUse();
        DetectEquipmentChoose();*/
    }

    /// <summary>
    /// ���װ����ײ
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentEquipCapcity + 1 < Constants.MAX_EQUIPMENT_CAP)
        {
            if (collision.CompareTag("equip"))
            {
                //Debug.Log("pickup");
                PickUp(collision);
            }
        }
      
    }
    
    /// <summary>
    /// ʰȡװ��
    /// </summary>
    /// <param name="collision">����ײ��װ��</param>
    private void PickUp(Collider2D collision)
    {
      
        GameObject collidedObject = collision.gameObject;

        // ���GameObject�Ƿ�ʵ����Equipments�ӿ�
        IEquipment equipment = collidedObject.GetComponent<IEquipment>();
        if (equipment != null)
        {
      
           //����һ���ǿ�Ԫ�ظ�ֵ
          for(int i = 0; i < Constants.MAX_EQUIPMENT_CAP; i++)
            {             
                if (equipmentArr[i].prebPath == null)
                {
                    equipmentArr[i] = equipment.GetStruct();
                    currentEquipCapcity++;
                    //����װ����
                    EquipmentUIController.instance.SetEquipmentTex(i, collidedObject.GetComponent<SpriteRenderer>().sprite);
                    break;
                }
                //Debug.Log(equipmentArr[i].prebPath);
            }
          //����UI
            EquipmentUIController.instance.SetEquipments(equipmentArr);
            Destroy(collidedObject);
        }
        else
        {
            Debug.LogError("no Script��");
        }


    }


    /// <summary>
    /// ���װ��ʹ��
    /// ��ʹ�ã�ʵ����װ������ʹ��use����
    /// ����currentEquipmentCap��UIManager
    /// </summary>
    private void DetectEquipmentUse()
    {
        if (Input.GetAxis("Fire1") > 0.3)
        {
            //Debug.Log("getDown");
            if(equipmentArr[currentEquipNum].prebPath!=null )
            {
                //���ݽṹ�����Ԥ����
                GameObject equip = Instantiate(Resources.Load<GameObject>(equipmentArr[currentEquipNum].prebPath));
                //�õ�Ԥ����ű�
                var equipImpl = equip.GetComponent<IEquipmentPerb>();
                if(equipImpl == null)
                {
                    Debug.LogError("no script on preb");
                    return;
                }
                StartCoroutine(equipImpl.Use(transform));
                equipmentArr[currentEquipNum].prebPath = null;
                currentEquipCapcity--;

                //����UI
                EquipmentUIController.instance.SetEquipments(equipmentArr);
                EquipmentUIController.instance.RemoveEquipment(currentEquipNum);
            }
                  
           
        }

    }

    /// <summary>
    /// ���Ŀǰѡ��װ��
    /// </summary>
    private void DetectEquipmentChoose()
    {
        int num = currentEquipNum;
        if(Input.GetAxis("Equipment1") > 0.3)
        {
            currentEquipNum = 0;
        }
        else if(Input.GetAxis("Equipment2") > 0.3)
        {
            currentEquipNum = 1;
        }
        else if (Input.GetAxis("Equipment3") > 0.3)
        {
            currentEquipNum = 2;
        }
        else if (Input.GetAxis("Equipment4") > 0.3)
        {
            currentEquipNum = 3;
        }

        if(currentEquipNum >= Constants.MAX_EQUIPMENT_CAP)
        {
            currentEquipNum = num;
        }

        EquipmentUIController.instance.Highlight(currentEquipNum);
    }
}
