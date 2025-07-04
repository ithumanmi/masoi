using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class VotePanel : PopupPanel
{
    public Transform content; // Kéo thả Content của ScrollView/List vào đây
    public VoteItemUI voteItemPrefab; // Prefab cho từng dòng vote
    public float timeoutSeconds = 10f; // Thời gian chờ vote
    public Button buttonAgree;
    public Button buttonClose;
    private List<PlayerController> currentPlayers = new List<PlayerController>();
    private RoleAction voteActionType = RoleAction.VoteKick;
    private Coroutine timeoutCoroutine;
    private List<VoteItemUI> voteItemUIs = new List<VoteItemUI>();
    private VoteItemUI selectedVoteItem = null;
    private PlayerController selectedPlayer = null;
    private PlayerController voterPlayer = null;

    private void OnEnable()
    {
        buttonAgree.onClick.RemoveAllListeners();
        buttonAgree.onClick.AddListener(OnAgreeVote);
        buttonAgree.gameObject.SetActive(false); // Ẩn khi mở panel
        if (buttonClose != null)
        {
            buttonClose.onClick.RemoveAllListeners();
            buttonClose.onClick.AddListener(Hide);
            buttonClose.gameObject.SetActive(true); // Hiện close khi mở panel
        }
    }

    public void Show(List<PlayerController> players, RoleAction voteType = RoleAction.VoteKick)
    {
        Debug.Log("Show vote panel");
        gameObject.SetActive(true);
        currentPlayers = players;
        voteActionType = voteType;
        voteItemUIs.Clear();
        selectedVoteItem = null;
        selectedPlayer = null;
        foreach (Transform child in content)
            Destroy(child.gameObject);
        foreach (var player in players)
        {
            if (!player.isAlive) continue;
            var item = Instantiate(voteItemPrefab, content);
            voteItemUIs.Add(item);
            item.SetInfo(player, (voter) => OnVote(item, player, voter));
            item.SetVoted(false);
        }
        EnableVotingUI(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (timeoutCoroutine != null) StopCoroutine(timeoutCoroutine);
        if (buttonClose != null)
            buttonClose.gameObject.SetActive(true); // Reset trạng thái close khi panel bị ẩn
    }

    private void OnVote(VoteItemUI item, PlayerController player, PlayerController voter)
    {
        // Bỏ chọn tất cả item khác
        foreach (var voteItem in voteItemUIs)
        {
            voteItem.SetVoted(false);
        }
        // Chọn item hiện tại
        item.SetVoted(true);
        selectedVoteItem = item;
        selectedPlayer = player;
        voterPlayer = voter;
        buttonAgree.gameObject.SetActive(true); // Hiện nút xác nhận khi đã chọn
        if (buttonClose != null)
            buttonClose.gameObject.SetActive(false); // Ẩn nút close khi đã vote
        // Không gửi action ngay, chỉ gửi khi xác nhận hoặc hết giờ
    }

    private void OnSkipVote()
    {
        // Bỏ chọn tất cả
        foreach (var voteItem in voteItemUIs)
        {
            voteItem.SetVoted(false);
        }
        selectedVoteItem = null;
        selectedPlayer = null;
        buttonAgree.gameObject.SetActive(false); // Ẩn nút xác nhận khi skip
        if (buttonClose != null)
            buttonClose.gameObject.SetActive(true); // Hiện close khi không vote
        var actionData = new RoleActionData(RoleAction.VoteSkip, null, null);
        ActionResolver.Instance.CollectAction(actionData);
        Hide();
    }

    private void OnAgreeVote()
    {
        if (selectedPlayer != null && voterPlayer != null)
        {
            // Tăng số vote cho player được vote
            if (GameManager.Instance.voteCounts.ContainsKey(selectedPlayer))
                GameManager.Instance.voteCounts[selectedPlayer]++;
            else
                GameManager.Instance.voteCounts[selectedPlayer] = 1;

            var actionData = new RoleActionData(voteActionType, voterPlayer, selectedPlayer);
            ActionResolver.Instance.CollectAction(actionData);
        }
        else
        {
            var actionData = new RoleActionData(RoleAction.VoteSkip, null, null);
            ActionResolver.Instance.CollectAction(actionData);
        }
        // Cập nhật lại danh sách player để hiển thị số vote
        UIManager.Instance.UpdatePlayerList(GameManager.Instance.players);
        Hide();
    }

    public void EnableVotingUI(bool enable)
    {
        foreach (var item in voteItemUIs)
            item.voteButton.interactable = enable;
        if (buttonAgree != null)
            buttonAgree.interactable = enable;
        if (buttonClose != null)
            buttonClose.interactable = enable;
    }
} 