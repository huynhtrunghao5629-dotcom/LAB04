using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form7 : Form
    {
        private TextBox txt_HienThi;
        private Label lbl_TieuDe;

        // Biến lưu trữ trạng thái tính toán
        private float giaTri1 = 0;
        private string phepToan = "";
        private bool nhapMoi = true; // Cờ đánh dấu xem người dùng đang nhập số mới hay nối tiếp số cũ

        public Form7()
        {
            this.Text = "Máy Tính Bỏ Túi";
            this.ClientSize = new Size(290, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 12, FontStyle.Bold);

            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            lbl_TieuDe = new Label() { Text = "Máy Tính Bỏ Túi", ForeColor = Color.Red, Location = new Point(50, 15), AutoSize = true, Font = new Font("Tahoma", 16, FontStyle.Bold) };

            // Ô hiển thị kết quả (chữ căn phải giống máy tính thật)
            txt_HienThi = new TextBox() { Location = new Point(20, 60), Width = 250, TextAlign = HorizontalAlignment.Right, ReadOnly = true, BackColor = Color.White };

            this.Controls.Add(lbl_TieuDe);
            this.Controls.Add(txt_HienThi);

            // Mảng chứa tên các nút trên lưới 4x4
            string[] nutBam = {
                "1", "2", "3", "4",
                "5", "6", "7", "8",
                "9", "0", "=", "C",
                "+", "-", "*", "/"
            };

            int toaDoX = 20, toaDoY = 100;
            int kichThuoc = 55, khoangCach = 10;

            for (int i = 0; i < 16; i++)
            {
                Button btn = new Button();
                btn.Text = nutBam[i];
                btn.Size = new Size(kichThuoc, kichThuoc);

                int hang = i / 4;
                int cot = i % 4;
                btn.Location = new Point(toaDoX + cot * (kichThuoc + khoangCach), toaDoY + hang * (kichThuoc + khoangCach));

                // Phân loại nút để gán sự kiện tương ứng
                if (int.TryParse(btn.Text, out _)) // Nếu là số (0-9)
                {
                    btn.Click += new EventHandler(NhapSo_Click);
                }
                else if (btn.Text == "C")
                {
                    btn.Click += new EventHandler(Xoa_Click);
                }
                else if (btn.Text == "=")
                {
                    btn.Click += new EventHandler(Bang_Click);
                }
                else // Nếu là phép toán (+, -, *, /)
                {
                    btn.Click += new EventHandler(PhepToan_Click);
                }

                this.Controls.Add(btn);
            }
        }


        // Khi bấm các nút số (0-9)
        private void NhapSo_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (nhapMoi)
            {
                txt_HienThi.Text = btn.Text; // Thay thế số cũ
                nhapMoi = false;
            }
            else
            {
                txt_HienThi.Text += btn.Text; // Nối thêm số mới vào sau
            }
        }

        // Khi bấm các dấu (+, -, *, /)
        private void PhepToan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (float.TryParse(txt_HienThi.Text, out float giaTri))
            {
                giaTri1 = giaTri;       // Lưu số vừa nhập lại
                phepToan = btn.Text;    // Lưu dấu phép toán lại
                nhapMoi = true;         // Chờ nhập số thứ 2
            }
        }

        // Khi bấm dấu Bằng (=)
        private void Bang_Click(object sender, EventArgs e)
        {
            if (phepToan != "" && float.TryParse(txt_HienThi.Text, out float giaTri2))
            {
                float ketQua = 0;
                switch (phepToan)
                {
                    case "+": ketQua = giaTri1 + giaTri2; break;
                    case "-": ketQua = giaTri1 - giaTri2; break;
                    case "*": ketQua = giaTri1 * giaTri2; break;
                    case "/":
                        if (giaTri2 == 0)
                        {
                            MessageBox.Show("Không thể chia cho 0!");
                            return;
                        }
                        ketQua = giaTri1 / giaTri2;
                        break;
                }
                txt_HienThi.Text = ketQua.ToString();
                phepToan = ""; // Reset phép toán
                nhapMoi = true; // Kết quả hiện ra xong, nếu bấm số mới sẽ ghi đè
            }
        }

        // Khi bấm chữ C (Clear)
        private void Xoa_Click(object sender, EventArgs e)
        {
            txt_HienThi.Text = "";
            giaTri1 = 0;
            phepToan = "";
            nhapMoi = true;
        }
    }
}