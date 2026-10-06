using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form5 : Form
    {
        private Label lbl_TieuDe, lbl_Nhap, lbl_KetQua;
        private TextBox txt_Nhap;
        private Button btn_ThucHien, btn_Xoa, btn_Thoat;

        public Form5()
        {
            this.Text = "Tâm Gà - Đọc Chữ Số";
            this.ClientSize = new Size(380, 220);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            TaoGiaoDien();

            txt_Nhap.KeyPress += new KeyPressEventHandler(txt_Nhap_KeyPress);
            btn_ThucHien.Click += new EventHandler(btn_ThucHien_Click);
            btn_Xoa.Click += new EventHandler(btn_Xoa_Click);
            btn_Thoat.Click += new EventHandler(btn_Thoat_Click);
            this.FormClosing += new FormClosingEventHandler(Form5_FormClosing);
        }

        private void TaoGiaoDien()
        {
            lbl_TieuDe = new Label() { Text = "Đọc Số Thành Chữ", Location = new Point(90, 15), AutoSize = true, Font = new Font("Tahoma", 16, FontStyle.Bold), ForeColor = Color.Red };

            lbl_Nhap = new Label() { Text = "Nhập dãy số : (từ 1 đến 999)", Location = new Point(30, 60), AutoSize = true };
            txt_Nhap = new TextBox() { Location = new Point(230, 57), Width = 100 };

            btn_ThucHien = new Button() { Text = "Thực hiện", Location = new Point(30, 100), Width = 100, Height = 30 };
            btn_Xoa = new Button() { Text = "Xóa", Location = new Point(140, 100), Width = 90, Height = 30 };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(240, 100), Width = 90, Height = 30 };

            lbl_KetQua = new Label() { Text = "", Location = new Point(30, 150), AutoSize = true, ForeColor = Color.Blue, Font = new Font("Tahoma", 11, FontStyle.Bold) };

            this.Controls.AddRange(new Control[] { lbl_TieuDe, lbl_Nhap, txt_Nhap, btn_ThucHien, btn_Xoa, btn_Thoat, lbl_KetQua });
        }

        private void txt_Nhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        // THUẬT TOÁN DỊCH SỐ THÀNH CHỮ
        private string DocSoThanhChu(int so)
        {
            if (so < 1 || so > 999) return "Vui lòng nhập số từ 1 đến 999!";

            string[] chu = { "", "Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín" };
            int tram = so / 100;
            int chuc = (so % 100) / 10;
            int donvi = so % 10;

            string kq = "";

            // Xử lý Hàng trăm
            if (tram > 0) kq += chu[tram] + " Trăm ";

            // Xử lý Hàng chục
            if (chuc > 0)
            {
                if (chuc == 1) kq += "Mười ";
                else kq += chu[chuc] + " Mươi ";
            }
            else if (tram > 0 && donvi > 0)
            {
                kq += "Lẻ ";
            }

            // Xử lý Hàng đơn vị (Bắt các trường hợp ngoại lệ: Mốt, Lăm)
            if (donvi > 0)
            {
                if (donvi == 1 && chuc > 1) kq += "Mốt";
                else if (donvi == 5 && chuc > 0) kq += "Lăm";
                else kq += chu[donvi];
            }

            return kq.Trim();
        }

        private void btn_ThucHien_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_Nhap.Text, out int so))
            {
                lbl_KetQua.Text = DocSoThanhChu(so);
            }
            else
            {
                lbl_KetQua.Text = "Dữ liệu không hợp lệ!";
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            txt_Nhap.Clear();
            lbl_KetQua.Text = "";
            txt_Nhap.Focus();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
        }
    }
}