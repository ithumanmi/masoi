# Ma Sói Online 2D Unity

## 1. Tổng Quan
- **Thể loại:** Board Game, Multiplayer Online, 2D
- **Mục tiêu:** Người chơi vào vai các nhân vật trong làng, cố gắng tìm ra Ma Sói hoặc sống sót đến cuối cùng.
- **Số người chơi:** 6–16 (có thể mở rộng)
- **Vai trò:** Dân làng, Ma Sói, Tiên tri, Bảo vệ, Phù thủy, v.v.

---

## 2. Gameplay Loop
1. **Chia vai trò:** Khi bắt đầu game, mỗi người chơi được gán ngẫu nhiên một vai trò.
2. **Ban đêm:**
   - Ma Sói chọn nạn nhân.
   - Tiên tri chọn người để kiểm tra.
   - Bảo vệ chọn người để bảo vệ.
   - Phù thủy có thể cứu hoặc giết ai đó.
3. **Ban ngày:**
   - Thảo luận, bỏ phiếu loại người chơi nghi là Ma Sói.
   - Công bố kết quả bỏ phiếu.
4. **Kết thúc:**
   - Game kết thúc khi Ma Sói bị loại hết hoặc số Ma Sói >= số dân làng.

---

## 3. Scene
### 3.1. Main Menu
- Chọn chế độ chơi, số người, luật chơi.
- Đăng nhập/đăng ký tài khoản (nếu cần).

### 3.2. Lobby
- Danh sách người chơi trong phòng.
- Chat phòng chờ.
- Chia vai trò khi đủ người.

### 3.3. Game Scene
- Bàn chơi, giao diện ban ngày/ban đêm.
- Chat chung, chat riêng (Ma Sói).
- Bỏ phiếu, chọn mục tiêu.
- Hiển thị trạng thái từng người chơi.

### 3.4. End Scene
- Thông báo thắng/thua, thống kê vai trò, lịch sử game.

---

## 4. Prefab & Đối Tượng
### 4.1. Player
- Thông tin: tên, vai trò (ẩn/hiện), trạng thái sống/chết, avatar.
- Network sync trạng thái.

### 4.2. Card
- Hiển thị vai trò (chỉ hiện khi cần).

### 4.3. Button
- Bỏ phiếu, chọn mục tiêu, chat, xác nhận hành động.

### 4.4. Popup
- Thông báo, xác nhận, kết quả.

---

## 5. UI
- **Danh sách người chơi:** Tên, trạng thái, avatar, icon vai trò (nếu lộ).
- **Khung chat:** Thảo luận, nhắn tin (phân biệt chat chung/riêng).
- **Thông báo:** Sự kiện ban đêm, kết quả bỏ phiếu, ai bị loại.
- **Bảng vai trò:** (Chỉ cho GM hoặc khi kết thúc game)
- **Timer:** Đếm ngược thời gian thảo luận, bỏ phiếu.

---

## 6. Luồng Dữ Liệu & Mạng
- **Chia vai:** Random server-side, gửi về client.
- **Giao tiếp:** Sử dụng Photon PUN/Netcode for GameObjects để đồng bộ trạng thái, chat, hành động.
- **Lưu trạng thái:**
  - Số lượt, ai đã chết, ai còn sống, lịch sử bỏ phiếu, lịch sử chat.
  - Server quản lý logic chính, client chỉ gửi yêu cầu.
- **Bảo mật:** Không để lộ vai trò từ client, chỉ server biết toàn bộ.

---

## 7. Kịch Bản Chi Tiết
### 7.1. Ban Đêm
- Hiển thị giao diện riêng cho từng vai trò.
- Ma Sói chat riêng, chọn nạn nhân (nếu nhiều sói, phải đồng thuận).
- Tiên tri chọn người kiểm tra, nhận kết quả từ server.
- Bảo vệ chọn người bảo vệ, không được bảo vệ 1 người 2 đêm liên tiếp.
- Phù thủy nhận thông báo ai bị sói cắn, chọn cứu hoặc giết.
- Server tổng hợp kết quả, gửi về client.

### 7.2. Ban Ngày
- Hiển thị kết quả đêm (ai chết, ai sống).
- Cho phép thảo luận (chat chung).
- Bỏ phiếu loại người chơi.
- Server xử lý phiếu, công bố kết quả.
- Nếu game chưa kết thúc, chuyển sang đêm tiếp theo.

### 7.3. Kết Thúc
- Kiểm tra điều kiện thắng/thua sau mỗi lượt.
- Hiển thị bảng kết quả, vai trò từng người, lịch sử game.

---

## 8. Công Nghệ Đề Xuất
- **Unity 2D**
- **Photon PUN** hoặc **Netcode for GameObjects** (Multiplayer)
- **ScriptableObject** để quản lý vai trò, luật chơi.
- **Canvas UI** cho giao diện.
- **Server Authority:** Logic game xử lý phía server, client chỉ gửi lệnh.

---

## 9. Mở Rộng
- Thêm nhiều vai trò đặc biệt, luật chơi mới.
- Thêm hiệu ứng âm thanh, animation, biểu cảm.
- Hệ thống nhiệm vụ, thành tích, bảng xếp hạng.
- Chơi với AI hoặc bot khi thiếu người.
- Hệ thống bạn bè, mời phòng, chat voice.

---

## 10. Ghi chú
- Ưu tiên bảo mật vai trò, tránh hack/cheat.
- Thiết kế code dễ mở rộng, tách biệt logic game và UI.
- Có thể tích hợp backend riêng để lưu lịch sử, tài khoản, thống kê. 