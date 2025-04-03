// Caro3_4/BLL/BotManager.cs
using Caro3_4.Class;
using Caro3_4.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms; // Để dùng List<List<Button>>

namespace Caro3_4.BLL
{
    public class BotManager
    {
        #region Properties & Fields
        public int Level { get; private set; }

        // AI Scoring Arrays
        private int[] attackScore = { 0 };
        private int[] defenseScore = { 0 };

        // Random generator for breaking ties
        private Random random = new Random();

        // Game state needed for calculation (passed in)
        private List<List<Button>> currentMatrix = new List<List<Button>>();
        private Stack<PlayInfo> currentPlayTimeLine = new Stack<PlayInfo>();
        private List<Player> currentPlayers = new List<Player>();
        private int currentAiPlayerIndex;


        #endregion

        #region Initialize
        public BotManager(int level)
        {
            this.Level = level;
            SetValueForArrayScore();
        }
        #endregion

        #region AI Core Logic

        // Hàm chính để tính nước đi tốt nhất
        public Point CalculateBestMove(List<List<Button>> matrix, Stack<PlayInfo> playTimeLine, List<Player> players, int aiPlayerIndex)
        {
            // Cập nhật trạng thái game hiện tại để các hàm helper sử dụng
            this.currentMatrix = matrix;
            this.currentPlayTimeLine = new Stack<PlayInfo>(new Stack<PlayInfo>(playTimeLine)); // Tạo bản sao để không ảnh hưởng gốc
            this.currentPlayers = players;
            this.currentAiPlayerIndex = aiPlayerIndex;

            Point bestMove = new Point(-1, -1);
            int maxScore = int.MinValue;
            List<Point> potentialMoves = new List<Point>();

            int opponentPlayerIndex = 1 - aiPlayerIndex;

            // Xử lý lượt đi đầu tiên hoặc thứ hai (ưu tiên gần trung tâm hoặc gần đối thủ)
            if (playTimeLine.Count == 0 && players[aiPlayerIndex].Name == "Máy tính") // Máy đi trước
            {
                return GetFirstMove();
            }
            if (playTimeLine.Count == 1 && players[aiPlayerIndex].Name == "Máy tính") // Máy đi sau
            {
                return GetSecondMove(playTimeLine.Peek().Point);
            }


            // Duyệt qua các ô có thể đi
            for (int r = 0; r < Const.CHESS_BOARD_LENGTH; r++) // r = row
            {
                for (int c = 0; c < Const.CHESS_BOARD_LENGTH; c++) // c = col
                {
                    // Chỉ xét ô trống và không bị cắt tỉa (gần các quân đã đánh)
                    if (matrix[r][c].BackgroundImage == null && !ShouldPrune(r, c))
                    {
                        int attackScoreVal = CalculateAttackScore(r, c);
                        int defenseScoreVal = CalculateDefenseScore(r, c);
                        int currentScore = attackScoreVal + defenseScoreVal; // Có thể trọng số hóa điểm phòng thủ/tấn công

                        // Ưu tiên đặc biệt cho nước đi thắng hoặc chặn nước thắng của đối thủ
                        if (attackScoreVal >= attackScore[6]) // Nếu nước này thắng ngay
                            currentScore = int.MaxValue;
                        else if (defenseScoreVal >= defenseScore[6]) // Nếu nước này chặn đối thủ thắng ngay
                            currentScore = int.MaxValue - 1; // Ưu tiên cao thứ nhì


                        if (currentScore > maxScore)
                        {
                            maxScore = currentScore;
                            potentialMoves.Clear();
                            potentialMoves.Add(new Point(c, r)); // X=cột, Y=hàng
                        }
                        else if (currentScore == maxScore)
                        {
                            potentialMoves.Add(new Point(c, r));
                        }
                    }
                }
            }

            // Chọn nước đi từ danh sách điểm cao nhất
            if (potentialMoves.Count > 0)
            {
                int randomIndex = random.Next(potentialMoves.Count);
                bestMove = potentialMoves[randomIndex];
            }
            else
            {
                // Nếu không tìm thấy nước đi nào sau khi tỉa (hiếm) -> tìm ô trống đầu tiên không bị tỉa
                bestMove = FindFallbackMove(matrix);
                if (bestMove.X == -1) // Nếu vẫn ko đc -> tìm ô trống đầu tiên bất kỳ
                {
                    bestMove = FindFirstEmptyCell(matrix);
                }
            }

            // Sanity check - đảm bảo không trả về (-1, -1) nếu bàn cờ chưa đầy
            if (bestMove.X == -1 && playTimeLine.Count < Const.CHESS_BOARD_LENGTH * Const.CHESS_BOARD_LENGTH)
            {
                Console.WriteLine("CRITICAL ERROR: BotManager couldn't find any move!");
                return FindFirstEmptyCell(matrix); // Cố gắng tìm ô trống cuối cùng
            }

            return bestMove;
        }

        // --- Các hàm Helper cho việc tính toán của AI ---

        #region Scoring Calculation Helpers

        // Tính điểm tấn công cho ô (row, col)
        private int CalculateAttackScore(int row, int col)
        {
            Image aiMark = currentPlayers[currentAiPlayerIndex].Mark;
            Image opponentMark = currentPlayers[1 - currentAiPlayerIndex].Mark;
            return CalculateTotalLineScore(row, col, aiMark, opponentMark, attackScore);
        }

        // Tính điểm phòng thủ cho ô (row, col) - Bằng cách giả lập đối thủ đánh vào ô đó
        private int CalculateDefenseScore(int row, int col)
        {
            Image aiMark = currentPlayers[currentAiPlayerIndex].Mark;
            Image opponentMark = currentPlayers[1 - currentAiPlayerIndex].Mark;
            int score = CalculateTotalLineScore(row, col, opponentMark, aiMark, defenseScore);

            // // *Thêm điểm thưởng nếu chặn được nước nguy hiểm của đối thủ*
            // // Ví dụ: Nếu đối thủ có thể tạo 4 không chặn nếu đánh vào ô này
            // bool opponentFormsOpenFour = false;
            // if (CalculateLineScore(row, col, 1, 0, opponentMark, aiMark, defenseScore) == defenseScore[5] ||
            //     CalculateLineScore(row, col, 0, 1, opponentMark, aiMark, defenseScore) == defenseScore[5] ||
            //     CalculateLineScore(row, col, 1, 1, opponentMark, aiMark, defenseScore) == defenseScore[5] ||
            //     CalculateLineScore(row, col, 1, -1, opponentMark, aiMark, defenseScore) == defenseScore[5])
            // {
            //     opponentFormsOpenFour = true;
            // }
            //
            // if(opponentFormsOpenFour)
            // {
            //     score += defenseScore[5] ; // Cộng thêm điểm lớn
            // }

            // *Ưu tiên chặn nước tạo thành 5 của đối thủ*
            if (score >= defenseScore[6]) // Nếu đặt vào đây sẽ chặn đối phương thắng
            {
                score = defenseScore[6]; // Đặt điểm chặn thắng là cao nhất cho phòng thủ
            }


            return score;
        }


        // Tính tổng điểm cho một ô dựa trên 4 đường
        private int CalculateTotalLineScore(int r, int c, Image targetMark, Image enemyMark, int[] scoreArr)
        {
            return CalculateLineScore(r, c, 1, 0, targetMark, enemyMark, scoreArr) +  // Ngang
                   CalculateLineScore(r, c, 0, 1, targetMark, enemyMark, scoreArr) +  // Dọc
                   CalculateLineScore(r, c, 1, 1, targetMark, enemyMark, scoreArr) +  // Chéo chính
                   CalculateLineScore(r, c, 1, -1, targetMark, enemyMark, scoreArr); // Chéo phụ
        }


        // Tính điểm cho một đường cụ thể (giống trong ChessBoardMaganer gốc đã sửa)
        private int CalculateLineScore(int r, int c, int dx, int dy, Image targetMark, Image enemyMark, int[] scoreArr)
        {
            int consecutiveCount1, consecutiveCount2;
            bool blocked1, blocked2;

            // Đếm 2 hướng
            CountConsecutive(r, c, dx, dy, targetMark, enemyMark, out consecutiveCount1, out blocked1);
            CountConsecutive(r, c, -dx, -dy, targetMark, enemyMark, out consecutiveCount2, out blocked2);

            int totalConsecutive = consecutiveCount1 + consecutiveCount2;
            int blockCount = (blocked1 ? 1 : 0) + (blocked2 ? 1 : 0);

            // Kiểm tra chặn 2 đầu
            if (blockCount == 2 && totalConsecutive < 4) // Nếu không phải tạo thành 5 thì chặn 2 đầu là vô dụng
                return 0;


            // Tính điểm dựa trên số quân và số đầu bị chặn
            if (totalConsecutive >= 4) // Đặt vào tạo thành 5 hoặc nhiều hơn
                return scoreArr[6]; // Điểm thắng
            if (totalConsecutive == 3) // Đặt vào tạo thành 4
            {
                if (blockCount == 0) return scoreArr[5]; // 4 không bị chặn
                if (blockCount == 1) return scoreArr[4]; // 4 bị chặn 1 đầu
            }
            else if (totalConsecutive == 2) // Đặt vào tạo thành 3
            {
                if (blockCount == 0) return scoreArr[3]; // 3 không bị chặn
                if (blockCount == 1) return scoreArr[2]; // 3 bị chặn 1 đầu
            }
            else if (totalConsecutive == 1) // Đặt vào tạo thành 2
            {
                if (blockCount == 0) return scoreArr[1]; // 2 không bị chặn
            }

            return 0; // Các trường hợp khác
        }


        // Đếm số quân liên tiếp và chặn đầu (giống trong ChessBoardMaganer gốc đã sửa)
        private void CountConsecutive(int r, int c, int dx, int dy, Image targetMark, Image enemyMark, out int count, out bool blocked)
        {
            count = 0;
            blocked = false;
            for (int i = 1; i <= 5; i++) // Kiểm tra tối đa 5 ô theo 1 hướng
            {
                int nextR = r + i * dy;
                int nextC = c + i * dx;

                if (!IsValid(nextR, nextC))
                {
                    blocked = true; // Chặn bởi biên
                    break;
                }

                Image? nextMark = currentMatrix[nextR][nextC].BackgroundImage;
                if (nextMark == targetMark)
                    count++;
                else
                {
                    if (nextMark == enemyMark || nextMark != null) // Chặn bởi địch hoặc quân khác
                        blocked = true;
                    // Nếu là ô trống (null), không bị chặn (blocked = false)
                    break;
                }
            }
        }

        #endregion

        #region Pruning Helpers

        // Hàm cắt tỉa: Chỉ xem xét các ô trống gần các quân đã đánh
        private bool ShouldPrune(int row, int col, int range = 2)
        {
            if (currentPlayTimeLine.Count < 2) return false; // Không tỉa ở đầu game

            // Kiểm tra xem có quân cờ nào trong phạm vi 'range' không
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
                {
                    if (i == 0 && j == 0) continue; // Bỏ qua ô trung tâm

                    int checkRow = row + i;
                    int checkCol = col + j;

                    if (IsValid(checkRow, checkCol))
                    {
                        if (currentMatrix[checkRow][checkCol].BackgroundImage != null)
                        {
                            return false; // Tìm thấy quân cờ gần đó -> Không tỉa
                        }
                    }
                }
            }
            return true; // Không có quân cờ nào gần -> Nên tỉa
        }

        // Kiểm tra tọa độ hợp lệ
        private bool IsValid(int r, int c)
        {
            return r >= 0 && r < Const.CHESS_BOARD_LENGTH && c >= 0 && c < Const.CHESS_BOARD_LENGTH;
        }


        #endregion

        #region Special Move Helpers (First/Second Move, Fallback)

        private Point GetFirstMove()
        {
            // Đánh vào ô trung tâm hoặc gần đó nếu bị chiếm
            Point center = new Point(Const.CHESS_BOARD_LENGTH / 2, Const.CHESS_BOARD_LENGTH / 2);
            if (currentMatrix[center.Y][center.X].BackgroundImage == null)
                return center;
            else
                return FindRandomNeighbor(center.Y, center.X); // Tìm ô trống lân cận
        }

        private Point GetSecondMove(Point opponentFirstMove)
        {
            // Đánh vào ô trống ngẫu nhiên gần nước đi đầu tiên của đối thủ
            return FindRandomNeighbor(opponentFirstMove.Y, opponentFirstMove.X);
        }

        private Point FindRandomNeighbor(int r, int c)
        {
            List<Point> neighbors = new List<Point>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int nr = r + i;
                    int nc = c + j;
                    if (IsValid(nr, nc) && currentMatrix[nr][nc].BackgroundImage == null)
                    {
                        neighbors.Add(new Point(nc, nr)); // X=col, Y=row
                    }
                }
            }
            if (neighbors.Count > 0)
            {
                return neighbors[random.Next(neighbors.Count)];
            }
            else
            {
                // Nếu không có hàng xóm trống -> tìm ô trống bất kỳ (fallback)
                return FindFirstEmptyCell(currentMatrix);
            }
        }


        private Point FindFallbackMove(List<List<Button>> matrix)
        {
            // Tìm ô trống đầu tiên không bị cắt tỉa
            for (int r = 0; r < Const.CHESS_BOARD_LENGTH; r++)
                for (int c = 0; c < Const.CHESS_BOARD_LENGTH; c++)
                    if (matrix[r][c].BackgroundImage == null && !ShouldPrune(r, c))
                        return new Point(c, r);
            return new Point(-1, -1); // Không tìm thấy
        }
        private Point FindFirstEmptyCell(List<List<Button>> matrix)
        {
            for (int r = 0; r < Const.CHESS_BOARD_LENGTH; r++)
                for (int c = 0; c < Const.CHESS_BOARD_LENGTH; c++)
                    if (matrix[r][c].BackgroundImage == null)
                        return new Point(c, r);
            return new Point(-1, -1); // Bàn cờ đầy
        }

        #endregion


        #region AI Score Initialization
        private void SetValueForArrayScore()
        {
            // Sử dụng mảng điểm như trong ChessBoardMaganer gốc hoặc điều chỉnh
            // Ví dụ giữ nguyên:
            if (Level == 1)
            {
                attackScore = new int[7] { 0, 3, 24, 192, 1536, 12288, 98304 }; // Level 3 cũ
                defenseScore = new int[7] { 0, 1, 9, 81, 729, 6561, 59049 };
            }
            else if (Level == 2)
            {
                attackScore = new int[7] { 0, 9, 54, 162, 1458, 13112, 118008 }; // Level 4 cũ
                defenseScore = new int[7] { 0, 3, 27, 99, 729, 6561, 59049 };
            }
            else if (Level == 3) // Level 3 mới (cân bằng hơn)
            {
                // Tăng nhẹ điểm tấn công/phòng thủ cho các nước thông thường
                attackScore = new int[7] { 0, 10, 60, 350, 2000, 15000, 1000000 }; // Thắng -> rất lớn
                defenseScore = new int[7] { 0, 8, 50, 300, 1800, 14000, 900000 }; // Chặn thắng -> rất lớn
            }
            else if (Level == 4) // Level 4 mới (mạnh hơn)
            {
                attackScore = new int[7] { 0, 12, 70, 400, 2500, 20000, 1000000 };
                defenseScore = new int[7] { 0, 9, 60, 380, 2300, 18000, 900000 };
            }
            else // Level 5 (Mạnh nhất)
            {
                attackScore = new int[7] { 0, 15, 80, 500, 3000, 25000, 1000000 }; // Level 5 cũ
                defenseScore = new int[7] { 0, 10, 70, 450, 2800, 23000, 900000 };
            }

            // Đảm bảo điểm thắng/chặn thắng là lớn nhất
            attackScore[6] = Math.Max(attackScore[6], defenseScore[6]) + 10000; // Attack thắng > Defense chặn thắng
            defenseScore[6] = attackScore[6] - 5000; // Defense chặn thắng phải cực lớn
        }
        #endregion

        #endregion
    }
}