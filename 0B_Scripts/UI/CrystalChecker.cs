using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalChecker : MonoBehaviour
{
    public Vector3[] pos = new Vector3[7];

    public Color[] colors = new Color[6];
    public Color bossColor;

    public List<WaveCrystal> crystals = new List<WaveCrystal>(6);
    public WaveCrystal bossCrystal;

    public Image[] progressImage = new Image[6];
    public Image bossImage;

    public List<int> nums = new List<int>();

    public List<WaveCrystal> roomList = new();

    private void Awake()
    {
        int roomIdx = 0;
        foreach (Image img in progressImage)
        {
            //Generate Random Color, in nums
            int randNum = Random.Range(0, nums.Count);

            //Change Image Color
            img.color = colors[nums[randNum]];
            img.gameObject.SetActive(false);

            //Create WaveCrystal(color by randnum)
            WaveCrystal crystal= Instantiate(crystals[randNum]);

            //set waveCrystal position by roomidx
            crystal.transform.position = pos[roomIdx];
            crystal.gameObject.SetActive(false);
            if (roomIdx == 0) crystal.gameObject.SetActive(true);
            roomList.Add(crystal);

            //remove Crystal in list
            crystals.RemoveAt(randNum);
            //remove nums in list
            nums.RemoveAt(randNum);
            roomIdx++;
        }

        WaveCrystal elitecrystal = Instantiate(bossCrystal);
        elitecrystal.transform.position = pos[6];
        elitecrystal.gameObject.SetActive(false);
        elitecrystal.crystalProgressImage = bossImage.gameObject;
        roomList.Add(elitecrystal);
        for (int i = 0; i < 6; i++)
        {
            roomList[i].nextCrystal = roomList[i + 1];
            roomList[i].crystalProgressImage = progressImage[i].gameObject;
        }
        bossImage.color = bossColor;
        bossImage.GetComponentInChildren<Image>().color = bossColor;
        bossImage.gameObject.SetActive(false);
    }
}
