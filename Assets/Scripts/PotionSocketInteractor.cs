using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionSocketInteractor : XRSocketInteractor
{
    public Image progressBar;
    public Soup soup;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // interactableObject�� PotionGrabInteractable Ÿ������ Ȯ���մϴ�.
        if (args.interactableObject is PotionGrabInteractable potion)
        {
            base.OnSelectEntered(args);

            soup.isPotionSpawn = true;
        }
    }
}
