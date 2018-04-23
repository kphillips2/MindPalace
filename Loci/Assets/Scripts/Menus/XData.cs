using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XData : MonoBehaviour {

    public PlusData PlusSignThisReplaces;
    public int wallIndex;
    public float wallLoc;
    public bool AssignedToWindow;  //True if on window, false for picture
    public GameObject PlusSign;
    public List<GameObject> ImagePlacedOver;
    public Vector3 offset;

    public void OnDeleteClick()
    {
        if (AssignedToWindow) {
            DeleteWindow(); 
        }
        else {
            DeletePicture();
        }
    }

    private void DeleteWindow()
    {
        print(wallLoc);
        PlusSign.GetComponent<SubMenuHandler>().room.GetComponent<RoomHandler>().RemoveDoorOrWindow(wallIndex,wallLoc);
        RestorePlusSign();
    }

    private void DeletePicture()
    {
        PlusSign.GetComponent<SubMenuHandler>().room.GetComponent<RoomHandler>().RemovePicture(ImagePlacedOver[0].transform.position);
        foreach (GameObject obj in ImagePlacedOver)
        {
            Destroy(obj);
        }
        RestorePlusSign();
    }

    private void RestorePlusSign()
    {
        GameObject component = Instantiate(
              PlusSign,
              Vector3.zero,
              Quaternion.Euler(0, 0, 0)
          ) as GameObject;
        component.transform.position = this.transform.position-offset;
        component.transform.rotation = this.transform.rotation;
        component.SetActive(true);
        component.transform.SetParent(PlusSign.GetComponent<SubMenuHandler>().room.transform);
        component.GetComponent<SubMenuHandler>().InitData(PlusSignThisReplaces);
        Destroy(this.gameObject);
    }
}
