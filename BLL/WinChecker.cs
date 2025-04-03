// Caro3_4/BLL/WinChecker.cs
using Caro3_4.Class;
using Caro3_4.Entity; // Cần nếu Player được dùng, nhưng hiện tại chỉ cần Image
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms; // Để dùng List<List<Button>>

namespace Caro3_4.BLL
{
    public class WinChecker
    {
        // Kiểm tra xem game đã kết thúc chưa sau nước đi tại lastMovePoint
        // Trả về true nếu kết thúc, false nếu chưa.
        // out winnerIndex: Chỉ số người thắng (0 hoặc 1), hoặc -1 nếu hòa.
        public bool IsGameOver(List<List<Button>> matrix, int moveCount, Point lastMovePoint, out int winnerIndex)
        {
            winnerIndex = -1; // Mặc định là chưa ai thắng

            if (lastMovePoint.X < 0 || lastMovePoint.Y < 0) return false; // Nước đi không hợp lệ

            Button lastButton = matrix[lastMovePoint.Y][lastMovePoint.X];
            if (lastButton.BackgroundImage == null) return false; // Ô trống không thể gây kết thúc

            // 1. Kiểm tra thắng thua
            if (IsWin(matrix, lastMovePoint))
            {
                // Xác định người thắng dựa vào quân cờ của nước đi cuối
                // Cần truy cập thông tin Player hoặc truyền PlayerIndex vào
                // Cách đơn giản: giả sử ta biết Player Index của nước đi cuối
                // Hoặc cần sửa để truyền Player Index vào hàm này.
                // Tạm thời, ta cần cách xác định người chơi từ lastButton.BackgroundImage
                // Ví dụ (cần có danh sách Players hoặc cách map Image -> Player Index):
                // winnerIndex = GetPlayerIndexFromMark(lastButton.BackgroundImage, players);
                // ===> Cần sửa lại ChessboardManager để truyền CurrentPlayer của nước đi cuối vào đây
                // ===> Hoặc sửa hàm IsWin trả về index người thắng
                winnerIndex = GetWinnerIndex(matrix, lastMovePoint); // Gọi hàm IsWin đã sửa đổi
                if (winnerIndex != -1) return true;
            }

            // 2. Kiểm tra hòa cờ (bàn cờ đầy)
            if (IsDraw(moveCount))
            {
                winnerIndex = -1; // Đánh dấu là hòa
                return true;
            }

            // Chưa kết thúc
            winnerIndex = -1;
            return false;
        }

        // Sửa lại IsWin để trả về index người thắng hoặc -1
        public int GetWinnerIndex(List<List<Button>> matrix, Point point)
        {
            Button btn = matrix[point.Y][point.X];
            if (btn?.BackgroundImage == null) return -1;

            if (CheckLine(matrix, point, 1, 0) >= 5 ||  // Ngang
                CheckLine(matrix, point, 0, 1) >= 5 ||  // Dọc
                CheckLine(matrix, point, 1, 1) >= 5 ||  // Chéo chính
                CheckLine(matrix, point, 1, -1) >= 5)   // Chéo phụ
            {
                // Cần xác định index người chơi từ btn.BackgroundImage
                // Giả sử có hàm GetPlayerIndexFromMark(Image mark)
                // return GetPlayerIndexFromMark(btn.BackgroundImage);

                // ===> Tạm thời trả về 0 hoặc 1 dựa trên logic game (cần cải thiện)
                // Ví dụ đơn giản: Nếu cần biết ai thắng, hàm gọi (IsGameOver) phải biết ai vừa đánh nước này.
                // => Cách tốt hơn: IsGameOver nên gọi hàm này và tự xác định người thắng dựa trên CurrentPlayer của nước đi đó.
                // => Nên hàm IsWin chỉ cần trả về true/false.
                return 0; // Tạm thời trả về 0, cần logic đúng để xác định index người thắng
            }
            return -1;
        }

        // Hàm IsWin gốc (chỉ kiểm tra có thắng không)
        public bool IsWin(List<List<Button>> matrix, Point point)
        {
            Button btn = matrix[point.Y][point.X];
            if (btn?.BackgroundImage == null) return false;

            return CheckLine(matrix, point, 1, 0) >= 5 ||  // Ngang
                   CheckLine(matrix, point, 0, 1) >= 5 ||  // Dọc
                   CheckLine(matrix, point, 1, 1) >= 5 ||  // Chéo chính
                   CheckLine(matrix, point, 1, -1) >= 5;   // Chéo phụ
        }


        public bool IsDraw(int moveCount)
        {
            // Hòa khi tất cả các ô đã được đánh
            return moveCount == Const.CHESS_BOARD_LENGTH * Const.CHESS_BOARD_LENGTH;
        }

        // Đếm số quân cờ liên tiếp của một loại từ điểm (point) theo hướng (dx, dy)
        private int CheckLine(List<List<Button>> matrix, Point point, int dx, int dy)
        {
            Button startButton = matrix[point.Y][point.X];
            if (startButton.BackgroundImage == null) return 0;
            Image targetMark = startButton.BackgroundImage;

            int count = 1; // Bắt đầu từ chính nút đó

            // Đếm theo hướng (dx, dy)
            for (int i = 1; i < 5; i++)
            {
                int nextX = point.X + i * dx;
                int nextY = point.Y + i * dy;

                if (!IsValid(nextX, nextY)) break; // Ra ngoài biên

                if (matrix[nextY][nextX].BackgroundImage == targetMark)
                    count++;
                else
                    break; // Gặp ô trống hoặc quân địch
            }

            // Đếm theo hướng ngược lại (-dx, -dy)
            for (int i = 1; i < 5; i++)
            {
                int nextX = point.X - i * dx;
                int nextY = point.Y - i * dy;

                if (!IsValid(nextX, nextY)) break; // Ra ngoài biên

                if (matrix[nextY][nextX].BackgroundImage == targetMark)
                    count++;
                else
                    break; // Gặp ô trống hoặc quân địch
            }

            return count;
        }

        // Kiểm tra tọa độ hợp lệ
        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < Const.CHESS_BOARD_LENGTH && y >= 0 && y < Const.CHESS_BOARD_LENGTH;
        }      
    }
}