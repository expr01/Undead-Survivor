using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // 아이템 관리 필요한 변수들 선언
    public ItemData data;
    public int level;
    public Weapon weapon;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);    
    }

    public void OnClick() 
    {
        switch (data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            
            case ItemData.ItemType.Glove:

                break;
            case ItemData.ItemType.Shoe:

                break;
            case ItemData.ItemType.Heal:

                break;
        }

        level++;
        // 스크립트블 오브젝트에 작성한 레벨 데이터 개수를 넘기지 않게함
        if (level == data.damages.Length) {
            GetComponent<Button>().interactable = false;
        }
    }
}
