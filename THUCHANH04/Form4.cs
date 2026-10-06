using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form4 : Form
    {
        // 1. Khai báo công cụ
        private Label lbl_TieuDe, lbl_Nhap, lbl_DaySo, lbl_Tong, lbl_TongChan, lbl_TongLe;
        private TextBox txt_NhapSo, txt_DayVuaNhap, txt_Tong, txt_TongChan, txt_TongLe;
        private Button btn_Nhap, btn_TiepTuc, btn_Thoat;
        private ErrorProvider errorProvider1;

        // Các biến lưu trữ giá trị cộng dồn
        private int tong = 0;
        private int tongChan = 0;
        private int tongLe = 0;

        public Form4()
        {
            // 2. Cài đặt khung Form
            this.Text = "Dãy số và Tính Tổng";
            this.ClientSize = new Size(420, 260);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            // 3. Vẽ giao diện
            TaoGiaoDien();

            // 4. Đăng ký sự kiện
            txt_NhapSo.KeyPress += new KeyPressEventHandler(ChonNhapSo);

            // Bắt sự kiện khi nhấn phím Enter trong ô nhập số (thay vì phải bấm nút Nhập bằng chuột)
            txt_NhapSo.KeyDown += new KeyEventHandler(O_NhapSo_KeyDown);

            btn_Nhap.Click += new EventHandler(btn_Nhap_Click);
            btn_TiepTuc.Click += new EventHandler(btn_TiepTuc_Click);
            btn_Thoat.Click += new EventHandler(btn_Thoat_Click);
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
        }

        private void TaoGiaoDien()
        {
            lbl_TieuDe = new Label() { Text = "Nhập Dãy Số và Tính Tổng", Location = new Point(50, 15), AutoSize = true, Font = new Font("Tahoma", 16, FontStyle.Bold), ForeColor = Color.Red };

            lbl_Nhap = new Label() { Text = "Nhập số :", Location = new Point(30, 60), AutoSize = true };
            txt_NhapSo = new TextBox() { Location = new Point(110, 57), Width = 100 };
            btn_Nhap = new Button() { Text = "Nhập", Location = new Point(230, 55), Width = 80, Height = 28 };

            lbl_DaySo = new Label() { Text = "Dãy vừa nhập :", Location = new Point(30, 100), AutoSize = true };
            txt_DayVuaNhap = new TextBox() { Location = new Point(140, 97), Width = 230, ReadOnly = true, BackColor = Color.White };

            lbl_Tong = new Label() { Text = "Tổng các phần tử trong dãy :", Location = new Point(30, 140), AutoSize = true };
            txt_Tong = new TextBox() { Location = new Point(230, 137), Width = 140, ReadOnly = true, BackColor = Color.White };

            lbl_TongChan = new Label() { Text = "Tổng Chẵn :", Location = new Point(30, 180), AutoSize = true };
            txt_TongChan = new TextBox() { Location = new Point(120, 177), Width = 60, ReadOnly = true, BackColor = Color.White };

            lbl_TongLe = new Label() { Text = "Tổng Lẻ :", Location = new Point(210, 180), AutoSize = true };
            txt_TongLe = new TextBox() { Location = new Point(290, 177), Width = 80, ReadOnly = true, BackColor = Color.White };

            btn_TiepTuc = new Button() { Text = "Tiếp Tục", Location = new Point(100, 220), Width = 80, Height = 30 };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(230, 220), Width = 80, Height = 30 };

            errorProvider1 = new ErrorProvider();

            this.Controls.AddRange(new Control[] {
                lbl_TieuDe, lbl_Nhap, txt_NhapSo, btn_Nhap,
                lbl_DaySo, txt_DayVuaNhap,
                lbl_Tong, txt_Tong,
                lbl_TongChan, txt_TongChan, lbl_TongLe, txt_TongLe,
                btn_TiepTuc, btn_Thoat
            });
        }


        // Chặn nhập chữ
        private void ChonNhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                errorProvider1.SetError(txt_NhapSo, "Vui lòng nhập số!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        // Logic thêm số vào dãy
        private void ThemSoVaoDay()
        {
            if (int.TryParse(txt_NhapSo.Text, out int so))
            {
                // Nối chuỗi hiển thị
                txt_DayVuaNhap.Text += so.ToString() + "  ";

                // Tính toán
                tong += so;
                if (so % 2 == 0) tongChan += so;
                else tongLe += so;

                // Xuất kết quả ra các textbox
                txt_Tong.Text = tong.ToString();
                txt_TongChan.Text = tongChan.ToString();
                txt_TongLe.Text = tongLe.ToString();

                // Reset ô nhập để người dùng nhập số tiếp theo
                txt_NhapSo.Clear();
                txt_NhapSo.Focus();
                errorProvider1.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ!", "Lỗi");
            }
        }

        // Bấm nút Nhập
        private void btn_Nhap_Click(object sender, EventArgs e)
        {
            ThemSoVaoDay();
        }

        // Bấm phím Enter để thao tác nhanh hơn
        private void O_NhapSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThemSoVaoDay();
                e.SuppressKeyPress = true; // Tắt âm thanh "tít" mặc định của Windows khi ấn Enter
            }
        }

        // Bấm Tiếp tục: Xóa trắng toàn bộ, reset biến về 0
        private void btn_TiepTuc_Click(object sender, EventArgs e)
        {
            tong = 0;
            tongChan = 0;
            tongLe = 0;

            txt_DayVuaNhap.Clear();
            txt_Tong.Clear();
            txt_TongChan.Clear();
            txt_TongLe.Clear();

            txt_NhapSo.Clear();
            txt_NhapSo.Focus();
        }

        // Nút thoát và xác nhận
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
        }
    }
}