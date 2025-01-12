using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//انشاء بطاقات العدو المقلوبة وتدميرها 
public class UIUtils
{
    // TAKEN FROM UMMORPG / vis2k assets (paid) - will update or remove later
    // instantiate/remove enough prefabs to match amount
    //هذه الدالة مسؤلة عن انشاء بطاقات العدو المقلوبة وتدميرها 
    public static void BalancePrefabs(GameObject prefab, int amount, Transform parent)
    {
        // instantiate until amount
        //   مدام عدد البطاقات الموجودة داخال الحاوية اقل من عدد البطاقات الموجودة في يد العدو ابق انشاء ابطاقات الى غاية اكماله
        for (int i = parent.childCount; i < amount; ++i)//i = parent.childCount معناها انه يساوي عدد البطاقات الموجودة داخل الحاوية 
        {
            //انشئ البطاقة المقلوبة 
            GameObject go = GameObject.Instantiate(prefab);
            //جل الكونتينر اب للبطاقة المقلوبة  
            go.transform.SetParent(parent, false);  //يتغير موقعه ليصبح متناسبًا مع الأب الجديد false 
        }

        // delete everything that's too much
        // (backwards loop because Destroy changes childCount)
        for (int i = parent.childCount - 1; i >= amount; --i)
            GameObject.Destroy(parent.GetChild(i).gameObject);
    }
}
