public enum RoleAction
{
    None,
    Kill,         // Sói cắn, Thợ săn bắn
    Protect,      // Bảo vệ
    Inspect,      // Tiên tri soi
    Save,         // Phù thủy cứu
    Poison,       // Phù thủy giết
    ChooseLovers, // Cupid chọn cặp đôi
    Spy,          // Cô bé lén xem
    Die,          // Chết (cho Lover, OldMan...)
    // Các action liên quan đến vote
    Vote,         // Vote bình thường
    VoteKick,     // Vote loại người chơi
    VoteSave,     // Vote cứu người chơi
    VoteSkip      // Vote bỏ qua
}

public class RoleActionData
{
    public RoleAction action;
    public PlayerController source;
    public PlayerController target;
    public object extraData;

    public RoleActionData(RoleAction action, PlayerController source, PlayerController target = null, object extraData = null)
    {
        this.action = action;
        this.source = source;
        this.target = target;
        this.extraData = extraData;
    }
} 