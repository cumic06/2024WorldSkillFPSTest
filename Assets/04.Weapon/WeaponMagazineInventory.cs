using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagazineStoreData
{
    public WeaponType Type;
    public int MaxCount = 0;
    public bool IsInfiniteCount = false;

    public int Count { get; set; } = 0;

    public MagazineStoreData(WeaponType magazineType)
    {
        Type = magazineType;
    }
}

public class WeaponMagazineInventory : MonoBehaviour
{
    [SerializeField] private List<MagazineStoreData> magazineStoreDatas = new();

    private void Start()
    {
        ResetCount();
    }

    public void ResetCount()
    {
        for (int i = 0; i < magazineStoreDatas.Count; i++)
        {
            magazineStoreDatas[i].Count = magazineStoreDatas[i].MaxCount;
        }
    }

    public void RemoveMagazine(WeaponType weaponType, int count)
    {
        MagazineStoreData magazineStoreData = GetMagazineData(weaponType);
        if (magazineStoreData != null)
        {
            if (magazineStoreData.Count <= 0) return;
            magazineStoreData.Count -= count;
        }
    }

    public bool AddMagazine(WeaponType weaponType, int value)
    {
        MagazineStoreData magazineStoreData = GetMagazineData(weaponType);

        if (magazineStoreData != null)
        {
            if (IsMagazineInfinite(magazineStoreData.Type)) return true;

            if (magazineStoreData.Count + value >= 0 && magazineStoreData.Count + value <= magazineStoreData.MaxCount)
            {
                magazineStoreData.Count += value;
                magazineStoreData.Count = Mathf.Clamp(magazineStoreData.Count, 0, magazineStoreData.MaxCount);
            }
        }
        return false;

    }

    public int GetMagazineCount(WeaponType weaponType)
    {
        MagazineStoreData magazineStoreData = GetMagazineData(weaponType);

        if (magazineStoreData == null) return 0;

        return magazineStoreData.Count;
    }

    public int GetMagazineMaxCount(WeaponType weaponType)
    {
        MagazineStoreData magazineStoreData = GetMagazineData(weaponType);

        if (magazineStoreData == null) return 0;

        return magazineStoreData.MaxCount;
    }

    public bool IsMagazineInfinite(WeaponType weaponType)
    {
        MagazineStoreData magazineStoreData = GetMagazineData(weaponType);

        if (magazineStoreData == null) return false;

        return magazineStoreData.IsInfiniteCount;
    }

    public MagazineStoreData GetMagazineData(WeaponType weaponType)
    {
        MagazineStoreData magazineStoreData = magazineStoreDatas.Find(data => data.Type == weaponType);

        if (magazineStoreData == null)
        {
            Debug.LogError($"[WeaponInventory]{weaponType}의 저장 데이터가 존재하지 않습니다");
        }

        return magazineStoreData;
    }
}