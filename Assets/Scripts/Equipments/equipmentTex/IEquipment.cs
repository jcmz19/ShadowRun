using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����װ����ͼ�Ľӿڣ�Ҫ��ÿ��װ����ʰ��֮ǰ���������Ԥ������Ϣ
/// </summary>
public interface IEquipment 
{

    EquipmentInfoStruct GetStruct();
    void SetStruct();


}

/// <summary>
/// ���ڴ��װ��Ԥ�������Ϣ
/// </summary>
public struct EquipmentInfoStruct
{
    
    public string prebPath;

  
}
