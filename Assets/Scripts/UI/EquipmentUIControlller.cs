using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ����װ����Ѫ����UI�ű�
/// �ṩ��̬instance
/// </summary>
public class EquipmentUIController : MonoBehaviour
{
    public static EquipmentUIController _instance;
    public static EquipmentUIController instance
    {
        get
        {
            if (!_instance)
            {
                if (!(_instance = FindAnyObjectByType<EquipmentUIController>()))
                {
                    Debug.LogError("no EquipmentUIController in the scene");
                }
            }
            return _instance;
        }
    }
    
    public Transform equipmentBag;
    private GameObject[] equipmentTabs;
    private GameObject[] equipmentTextures;
    public TextMeshProUGUI equipmentText;
    private int currentHiglight;

    // Start is called before the first frame update
    void Awake()
    {
        equipmentTextures = new GameObject[Constants.MAX_EQUIPMENT_CAP];
        Init();
        currentHiglight = 0;

    }

    /// <summary>
    /// ��ʼ��װ��UI����
    /// </summary>
    public void Init()
    {
        equipmentTabs = new GameObject[Constants.MAX_EQUIPMENT_CAP];
        for (int i = 0; i < Constants.MAX_EQUIPMENT_CAP; i++)
        {
            GameObject equipTab = Instantiate(Resources.Load<GameObject>(Constants.EQUIPMENT_TAB_PATH));
            if (equipTab.CompareTag("EquipmentUI"))
            {
                equipmentTabs[i] = equipTab;
                equipTab.transform.SetParent(equipmentBag);
                equipTab.transform.localPosition = new Vector2((equipTab.transform.GetComponent<Image>().rectTransform.rect.width) * i *20,0);
            }
            else
            {
                Debug.LogError("find object no UI tag");
            }
        }
    }

    /// <summary>
    /// ����װ����ͼƬ����ʾ��װ����
    /// </summary>
    /// <param name="equipmentIndex">װ���±�</param>
    /// <param name="equipemtTex">װ��ͼ</param>
    public void SetEquipmentTex(int equipmentIndex, Sprite equipemtTex)
    {
        GameObject equipment = new GameObject(equipmentIndex.ToString());
        Image image = equipment.AddComponent<Image>();
        Transform newEquipemtTransform = equipmentTabs[equipmentIndex].transform;
        if(image != null)
        {
            image.sprite = equipemtTex;
            equipmentTextures[equipmentIndex] = equipment;
            image.rectTransform.sizeDelta = new Vector2(newEquipemtTransform.GetComponent<Image>().rectTransform.rect.width/2, newEquipemtTransform.GetComponent<Image>().rectTransform.rect.height/2);
            image.transform.localPosition = new Vector2( 0, 0);
            //Debug.Log(image.rectTransform.anchoredPosition);
            equipment.transform.SetParent(newEquipemtTransform);
        }
        else
        {
            Debug.LogError("instanite equip texure fail");
        }
    }

    /// <summary>
    /// װ����ʹ��֮������װ���������ͼ��
    /// </summary>
    /// <param name="equipmentIndex">�����ɾ��װ�����±�</param>
    public void RemoveEquipment(int equipmentIndex)
    {
        GameObject removeObject = equipmentTextures[equipmentIndex];
        Destroy(removeObject);
        equipmentTextures[equipmentIndex] = null;
    }

    /// <summary>
    /// ����װ��
    /// </summary>
    /// <param name="highlightIndex">����װ�����±�</param>
    public void Highlight(int highlightIndex)
    {
        equipmentTabs[currentHiglight].GetComponent<Image>().color = Color.white;
        equipmentTabs[highlightIndex].GetComponent<Image>().color = Color.red;
        currentHiglight = highlightIndex;
    }
    public void SetEquipments(EquipmentInfoStruct[] list)
    {
        equipmentText.text = list.ToString();
    }
}
