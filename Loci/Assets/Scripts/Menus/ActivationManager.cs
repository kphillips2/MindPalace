using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActivationManager {

    private static bool Activated = false;
    private static GameObject ActivatedObject = null;
    public static GameObject ImageMenu;
    public static GameObject objectHeld1 = null;
    public static GameObject objectHeld2 = null;

    public static void ActivateMenu(GameObject NewActivation)
    {
        if (Activated == false || ActivatedObject == null)
        {
            ActivatedObject = NewActivation;
            Activated = true;
        }
        else
        {
            ActivatedObject.GetComponent<SubMenuHandler>().DefaultState();
            ActivatedObject = NewActivation;
            ImageMenu.transform.position = new Vector3(0, -100, 0); //Like Deactivating, but deactivating breaks it
        }
    }

    public static void NoActive()
    {
        Activated = false;
        ActivatedObject.GetComponent<SubMenuHandler>().DefaultState();
    }

    public static void GiveImageMenu(GameObject ImageMenuParam)
    {
        ImageMenu = ImageMenuParam;
    }

    public static GameObject GetActive()
    {
        return ActivatedObject;
    }
}
