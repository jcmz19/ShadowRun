using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingImpl : MonoBehaviour,IEquipmentPerb
{
    private Rigidbody2D rb;
   
    /// <summary>
    /// ��ʼ������λ�ú�Ͷ������
    /// </summary>
    /// <param name="playerTransform">��ɫ����</param>
    public void Init(Transform playerTransform)
    {
        transform.position = playerTransform.position;
        Vector3 mousePotion = Input.mousePosition;
        if(mousePotion == null)
        {
            rb.AddForce(new Vector2(1,0) * Constants.EQUIP_THROW_FORCE, ForceMode2D.Impulse);
        }
 
        rb.AddForce(new Vector2(mousePotion.x, mousePotion.y).normalized * Constants.EQUIP_THROW_FORCE, ForceMode2D.Impulse);

    }

    /// <summary>
    /// ����ʹ�þ���ʵ��
    /// </summary>
    /// <param name="playerTransform">��ɫ����</param>
    /// <returns>���ص���ʹ�õ�Э��</returns>
    public IEnumerator Use(Transform playerTransform)
    {
        Init(playerTransform);
        Debug.Log("use");
        // ��� Light2D ���
       
        yield return new WaitForSeconds(Constants.LIGHTING_EXIT_TIME);
        //ʵ����Ԥ����2d����
        GameObject light = Instantiate(Resources.Load<GameObject>(Constants.LightingEquipName));
        if(light == null)
        {
            Debug.LogError("the equipment light preb not found");
        }
        else
        {
            light.transform.position = transform.position;
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
