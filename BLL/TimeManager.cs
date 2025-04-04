using Caro3_4.Class;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Caro3_4.BLL
{
    public class TimeManager : IDisposable
    {
        private System.Windows.Forms.Timer timer;
        private Stopwatch stopwatch; // Sử dụng Stopwatch để đo thời gian chính xác

        // Thuộc tính trả về thời gian đã trôi qua bằng mili giây
        public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;
        public bool IsRunning => timer.Enabled;

        public event EventHandler? TimeChanged;
        public event EventHandler? TimeExpired;

        public TimeManager()
        {
            stopwatch = new Stopwatch();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = Const.TimerInterval; // Giảm Interval để cập nhật thường xuyên hơn
            timer.Tick += Timer_Tick!;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnTimeChanged(); // Thông báo thay đổi để cập nhật UI

            // Kiểm tra nếu hết giờ
            if (stopwatch.ElapsedMilliseconds >= Const.TimeLimitMilliseconds)
            {
                StopTimer(); // Dừng stopwatch và timer
                             // Đảm bảo ElapsedMilliseconds không vượt quá giới hạn khi báo sự kiện
                if (stopwatch.IsRunning) // Kiểm tra lại để tránh lỗi nếu đã stop
                    stopwatch.Stop();

                OnTimeExpired(); // Thông báo đã hết giờ
            }
        }

        public void StartTimer()
        {
            stopwatch.Restart(); // Đặt lại và bắt đầu Stopwatch
            timer.Start();      // Bắt đầu Timer
            OnTimeChanged();    // Cập nhật UI ngay lập tức
        }

        public void StopTimer()
        {
            timer.Stop();
            stopwatch.Stop();
        }

        public void ResetTimer()
        {
            StopTimer();         // Dừng trước
            stopwatch.Reset();   // Đặt lại Stopwatch về 0
            OnTimeChanged();     // Cập nhật UI về trạng thái reset (0)
        }

        protected virtual void OnTimeChanged()
        {
            TimeChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTimeExpired()
        {
            TimeExpired?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            timer.Dispose();
            // Stopwatch không cần Dispose
            GC.SuppressFinalize(this);
        }

        ~TimeManager()
        {
            Dispose();
        }
    }
}