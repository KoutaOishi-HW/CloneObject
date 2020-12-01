#if UNITY_EDITOR
using UnityEditor;
#endif  
using UnityEngine;
using System.Text.RegularExpressions;

public class CloneObjs : MonoBehaviour
{
    [Header( "左方向の設定" )]
    [SerializeField]
    int leftCopyNum;

    [SerializeField]
    float leftDistance;

    [SerializeField]
    float left_Yup;


    [ Header( "後ろ方向の設定" )]
    [SerializeField]
    int backCopyNum;

    [SerializeField]
    float backDistance;

    [SerializeField]
    float back_Yup;

    /*
    [Header( "上方向へのクローン数" )]
    [SerializeField]
    int upCopyNum;

    [Header( "上方向間隔" )]
    [SerializeField]
    float upDistance;
    */

    void CloneObjects()
    {
        var copyObj = this.gameObject;
        Vector3 objPos = copyObj.transform.position;
        float defX = objPos.x;
        float defY = objPos.y;
        float defZ = objPos.z;

        //for( int h = 0; h < upCopyNum; h++ )
        //{
            for( int i = 0; i < backCopyNum; i++ )
            {
                for( int j = 0; j < leftCopyNum; j++ )
                {

                    if( i == 0 && j == 0 )
                    {
                        objPos.y += left_Yup;
                        continue;
                    }
                    else if( j == 0 )
                    {

                    }
                    else
                    {
                        objPos.x += leftDistance;
                    }

                    GameObject tmpObj = Instantiate(copyObj, objPos, copyObj.transform.rotation);
                    tmpObj.transform.parent = copyObj.transform.parent;
                    tmpObj.name = copyObj.name;

                    objPos.y += left_Yup;
                }

                objPos.z += backDistance;

                objPos.x = defX;
                objPos.y = defY;

                objPos.y += back_Yup * ( i + 1 );
            }

            //objPos.y += upDistance;
            //objPos.x = defX;
            //objPos.z = defZ;
        //}
    }

#if UNITY_EDITOR
    [CustomEditor( typeof( CloneObjs ) )]
    public class ExampleInspector : Editor
    {
        CloneObjs rootClass;

        private void OnEnable()
        {
            rootClass = target as CloneObjs;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if( GUILayout.Button( "オブジェクトのクローンを作成" ) )
            {
                rootClass.CloneObjects();
            }

            serializedObject.Update();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif  
}