using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class PlayerItemUI : MonoBehaviour
{
    public Text nameText;
    //public Text roleText;
    public Image statusIcon; // Xanh: sống, Đỏ: chết
    public Text voteCountText;
    public Text hpText;
    public Text bloodText;

    private Vector3 bloodTextOriginPos;

    private void Awake() {
        if (bloodText != null)
            bloodTextOriginPos = bloodText.transform.localPosition;
    }

    public void SetInfo(PlayerController player, int voteCount = 0)
    {
        nameText.text = player.playerName; //player.name;
        //roleText.text = role ?? "";
        statusIcon.color = player.isAlive ? Color.green : Color.red;
        if (voteCountText != null)
            voteCountText.text = voteCount > 0 ? voteCount.ToString() : "";
        if (hpText != null) hpText.text = player.CurrentHP.ToString();
        if (bloodText != null)
        {
            bloodText.text = "";
            bloodText.gameObject.SetActive(false);
            bloodText.transform.localPosition = bloodTextOriginPos;
            var cg = bloodText.GetComponent<CanvasGroup>();
            if (cg != null) cg.alpha = 1f;
        }
    }

    //public void ShowDamage(int damage)
    //{
    //    if (bloodText != null)
    //    {
    //        bloodText.text = $"-{damage}";
    //        bloodText.gameObject.SetActive(true);

    //        var cg = bloodText.GetComponent<CanvasGroup>();
    //        if (cg == null) cg = bloodText.gameObject.AddComponent<CanvasGroup>();

    //        Sequence bloodSequence = DOTween.Sequence();
    //        bloodSequence.Append(bloodText.transform.DOLocalMoveY(
    //            bloodTextOriginPos.y + 50f, 1f));
    //        bloodSequence.Join(cg.DOFade(0f, 1f));
    //        bloodSequence.OnComplete(() =>
    //        {
    //            bloodText.gameObject.SetActive(false);
    //            bloodText.transform.localPosition = bloodTextOriginPos;
    //            cg.alpha = 1f;
    //        });
    //    }
    //}

    public void ClearVoteCount()
    {
        if (voteCountText != null)
            voteCountText.text = string.Empty;
    }
} 