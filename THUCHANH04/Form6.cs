using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form6 : Form
    {
        private Label lbl_ManAnh, lbl_ThanhTien;
        private TextBox txt_ThanhTien;
        private Button btn_Chon, btn_Huy, btn_KetThuc;

        // Dùng mảng để quản lý 15 cái ghế thay vì tạo từng biến lẻ
        private Button[] btn_Ghe = new Button[15];

        public Form6()
        {
            this.Text = "BÁN VÉ RẠP CHIẾU BÓNG";
            this.ClientSize = new Size(380, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10, FontStyle.Bold);

            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            lbl_ManAnh = new Label() { Text = "MÀN ẢNH", ForeColor = Color.Orange, Location = new Point(140, 20), AutoSize = true, Font = new Font("Tahoma", 14, FontStyle.Bold) };
            this.Controls.Add(lbl_ManAnh);

            // Vòng lặp vẽ 15 cái ghế 
            int toaDoX_BatDau = 30, toaDoY_BatDau = 70;
            int chieuRongGhe = 55, chieuCaoGhe = 55, khoangCach = 10;

            for (int i = 0; i < 15; i++)
            {
                btn_Ghe[i] = new Button();
                btn_Ghe[i].Text = (i + 1).ToString();
                btn_Ghe[i].Size = new Size(chieuRongGhe, chieuCaoGhe);

                // Toán học cơ bản để xếp ghế thành lưới 3x5
                int hang = i / 5;
                int cot = i % 5;

                btn_Ghe[i].Location = new Point(toaDoX_BatDau + cot * (chieuRongGhe + khoangCach), toaDoY_BatDau + hang * (chieuCaoGhe + khoangCach));
                btn_Ghe[i].BackColor = Color.White; // Ghế trống mặc định màu trắng

                // Gán chung 1 sự kiện Click cho cả 15 cái ghế
                btn_Ghe[i].Click += new EventHandler(Ghe_Click);

                this.Controls.Add(btn_Ghe[i]);
            }

            // Các nút chức năng bên dưới
            int toaDoY_Duoi = 290;
            lbl_ThanhTien = new Label() { Text = "Thành Tiền:", Location = new Point(30, toaDoY_Duoi), AutoSize = true, Font = new Font("Tahoma", 10) };
            txt_ThanhTien = new TextBox() { Location = new Point(130, toaDoY_Duoi - 3), Width = 215, ReadOnly = true, BackColor = Color.White };

            btn_Chon = new Button() { Text = "Chọn", Location = new Point(30, 340), Width = 90, Height = 35 };
            btn_Huy = new Button() { Text = "Hủy bỏ", Location = new Point(145, 340), Width = 90, Height = 35 };
            btn_KetThuc = new Button() { Text = "Kết thúc", Location = new Point(255, 340), Width = 90, Height = 35 };

            btn_Chon.Click += new EventHandler(btn_Chon_Click);
            btn_Huy.Click += new EventHandler(btn_Huy_Click);
            btn_KetThuc.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lbl_ThanhTien, txt_ThanhTien, btn_Chon, btn_Huy, btn_KetThuc });
        }


        // Khi click vào bất kỳ ghế nào
        private void Ghe_Click(object sender, EventArgs e)
        {
            Button gheDuocChon = sender as Button;

            if (gheDuocChon.BackColor == Color.White)
            {
                gheDuocChon.BackColor = Color.Blue; // Trắng -> Xanh (Đang chọn)
            }
            else if (gheDuocChon.BackColor == Color.Blue)
            {
                gheDuocChon.BackColor = Color.White; // Xanh -> Trắng (Hủy chọn)
            }
            else if (gheDuocChon.BackColor == Color.Yellow)
            {
                MessageBox.Show("Ghế này đã được bán. Vui lòng chọn ghế khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Bấm nút CHỌN (Thanh toán)
        private void btn_Chon_Click(object sender, EventArgs e)
        {
            int tongTien = 0;
            for (int i = 0; i < 15; i++)
            {
                if (btn_Ghe[i].BackColor == Color.Blue)
                {
                    btn_Ghe[i].BackColor = Color.Yellow; // Đổi sang Vàng (Đã bán)

                    // Tính giá tiền dựa theo vị trí ghế (Lô A, B, C)
                    if (i < 5) tongTien += 1000;           // Ghế 1-5 (Lô A)
                    else if (i < 10) tongTien += 1500;     // Ghế 6-10 (Lô B)
                    else tongTien += 2000;                 // Ghế 11-15 (Lô C)
                }
            }

            // Hiển thị tổng tiền hoặc thông báo nếu chưa chọn ghế nào
            if (tongTien > 0)
                txt_ThanhTien.Text = tongTien.ToString();
            else
                MessageBox.Show("Vui lòng chọn ít nhất 1 ghế màu trắng!", "Thông báo");
        }

        // Bấm nút HỦY BỎ
        private void btn_Huy_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                if (btn_Ghe[i].BackColor == Color.Blue)
                {
                    btn_Ghe[i].BackColor = Color.White; // Trả lại ghế trống
                }
            }
            txt_ThanhTien.Text = "0"; // Trả tiền về 0
        }
    }
}