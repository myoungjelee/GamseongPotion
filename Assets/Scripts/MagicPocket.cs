using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicPocket : MonoBehaviour
{
    public Transform pocketWorld;
    private HashSet<XRGrabInteractable> itemsInPocket = new HashSet<XRGrabInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
        {
            XRGrabInteractable item = other.gameObject.GetComponent<XRGrabInteractable>();
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();

            

            if (item != null && item.isSelected && !itemsInPocket.Contains(item))
            {
                // HerbGrabInteractable �Ǵ� PotionGrabInteractable ���� Ȯ��
                var pocketHerb = item as HerbGrabInteractable;
                var pocketPotion = item as PotionGrabInteractable;

                if (pocketHerb != null)
                {
                    pocketHerb.isInPocket = true;
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = true;
                }

                if (!rigidbody.isKinematic && (pocketHerb.isInPocket || pocketPotion.isInPocket)) return;

                if (item.gameObject.transform.parent != pocketWorld)
                {
                    item.gameObject.transform.SetParent(pocketWorld);
                }

                item.gameObject.layer = LayerMask.NameToLayer("Stencil");

                itemsInPocket.Add(item);

                //Debug.Log($"{item.gameObject.name}�� Magic Pocket�� �����ϴ�.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
        {
            XRGrabInteractable item = other.gameObject.GetComponent<XRGrabInteractable>();

            if (item != null && item.isSelected && itemsInPocket.Contains(item))
            {

                item.gameObject.transform.SetParent(null);
                item.gameObject.layer = LayerMask.NameToLayer("Default");

                // HerbGrabInteractable �Ǵ� PotionGrabInteractable ���� Ȯ��
                var pocketHerb = item as HerbGrabInteractable;
                var pocketPotion = item as PotionGrabInteractable;

                if (pocketHerb != null)
                {
                    pocketHerb.isInPocket = false;
                    item.gameObject.transform.DOScale(pocketHerb.originScale, 1);
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = false;
                    item.gameObject.transform.DOScale(pocketPotion.originScale, 1);
                }

                itemsInPocket.Remove(item);

                //Debug.Log($"{item.gameObject.name}�� Magic Pocket���� �������ϴ�.");
            }
        }
    }
}
