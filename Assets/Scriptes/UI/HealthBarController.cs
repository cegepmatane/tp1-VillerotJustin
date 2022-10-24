/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {
    
    
    
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    
    private void Start()
    {
        /* Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)PlayerStatManagment.instance.getMaxTotalHealth];
        heartFills = new Image[(int)PlayerStatManagment.instance.getMaxTotalHealth];*/

        InstantiateHeartContainers();
        PlayerStatManagment.instance.onHealthChangedCallback += UpdateHeartsHUD;
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD() {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers() {
        int counter = 0;
        foreach (HeartContainer heartContainer in GetComponentsInChildren<HeartContainer>()) {
            if (counter < PlayerStatManagment.instance.getMaxHealth) {
                heartContainer.gameObject.SetActive(true);
            }   
            else {
                heartContainer.gameObject.SetActive(false);
            }
            counter++;
        }
    }

    void SetFilledHearts() {
        int counter = 0;
        foreach (HeartContainer heartContainer in GetComponentsInChildren<HeartContainer>()) {
            if (counter < PlayerStatManagment.instance.getHealth) {
                heartContainer.fill(1);
            }
            else {
                heartContainer.fill(0);
            }
            counter++;
        }

        if (PlayerStatManagment.instance.getHealth % 1 != 0) {
            int lastPos = Mathf.FloorToInt(PlayerStatManagment.instance.getHealth);

            ((HeartContainer)heartsParent.gameObject.GetComponentsInChildren<HeartContainer>().GetValue(lastPos)).fill(PlayerStatManagment.instance.getHealth % 1);
        }
    }

    void InstantiateHeartContainers() {
        for (int i = 0; i < PlayerStatManagment.instance.getMaxTotalHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            /*
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();*/
        }
    }
}
