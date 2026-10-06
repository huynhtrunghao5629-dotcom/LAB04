using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form2 : Form
    {
        // 1. Khai báo các công cụ
        private Label lbl_Ten, lbl_Email, lbl_MK, lbl_XacNhan, lbl_Sao1, lbl_Sao2, lbl_Sao3;
        private TextBox txt_TenDangNhap, txt_Email, txt_MatKhau, txt_XacNhan;
        private Button btn_DangKy;
        private ErrorProvider errorProvider1;

        public Form2()
        {
            // 2. Cài đặt khung Form
            this.Text = "Đăng ký tài khoản";
            this.Size = new Size(380, 280);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            // 3. Gọi hàm tự vẽ giao diện
            TaoGiaoDien();

            // 4. Đăng ký sự kiện
            txt_Email.Leave += new EventHandler(txt_Email_Leave);
            btn_DangKy.Click += new EventHandler(btn_DangKy_Click);
            txt_XacNhan.KeyDown += new KeyEventHandler(txt_XacNhan_KeyDown);
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
            txt_TenDangNhap.KeyPress += new KeyPressEventHandler(txt_TenDangNhap_KeyPress);
        }

        private void TaoGiaoDien()
        {
            int mTrai = 30; // Lề trái

            // Dòng 1: Tên đăng nhập
            lbl_Ten = new Label() { Text = "Tên đăng nhập", Location = new Point(mTrai, 30), AutoSize = true };
            txt_TenDangNhap = new TextBox() { Location = new Point(160, 27), Width = 150 };
            lbl_Sao1 = new Label() { Text = "(*)", Location = new Point(315, 30), AutoSize = true, ForeColor = Color.Red };

            // Dòng 2: Email
            lbl_Email = new Label() { Text = "Địa chỉ email", Location = new Point(mTrai, 70), AutoSize = true };
            txt_Email = new TextBox() { Location = new Point(160, 67), Width = 150 };
            lbl_Sao2 = new Label() { Text = "(*)", Location = new Point(315, 70), AutoSize = true, ForeColor = Color.Red };

            // Dòng 3: Mật khẩu
            lbl_MK = new Label() { Text = "Mật khẩu", Location = new Point(mTrai, 110), AutoSize = true };
            txt_MatKhau = new TextBox() { Location = new Point(160, 107), Width = 150, PasswordChar = '*' };
            lbl_Sao3 = new Label() { Text = "(*)", Location = new Point(315, 110), AutoSize = true, ForeColor = Color.Red };

            // Dòng 4: Xác nhận mật khẩu
            lbl_XacNhan = new Label() { Text = "Xác nhận mật khẩu", Location = new Point(mTrai, 150), AutoSize = true };
            txt_XacNhan = new TextBox() { Location = new Point(160, 147), Width = 150, PasswordChar = '*' };

            // Nút Đăng ký
            btn_DangKy = new Button() { Text = "Đăng ký", Location = new Point(160, 190), Width = 150, Height = 30 };

            errorProvider1 = new ErrorProvider();

            // Đưa toàn bộ vào Form
            this.Controls.AddRange(new Control[] {
                lbl_Ten, txt_TenDangNhap, lbl_Sao1,
                lbl_Email, txt_Email, lbl_Sao2,
                lbl_MK, txt_MatKhau, lbl_Sao3,
                lbl_XacNhan, txt_XacNhan,
                btn_DangKy
            });
        }


        // Kiểm tra Email khi click chuột ra chỗ khác
        private void txt_Email_Leave(object sender, EventArgs e)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!string.IsNullOrWhiteSpace(txt_Email.Text) && !Regex.IsMatch(txt_Email.Text, emailPattern))
            {
                errorProvider1.SetError(txt_Email, "Email không đúng định dạng!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        // Kiểm tra chặn nhập số ở ô Tên đăng nhập
        private void txt_TenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nếu ký tự người dùng gõ vào là một con số
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Hủy bỏ không cho nhập con số đó vào ô
                errorProvider1.SetError(txt_TenDangNhap, "Tên đăng nhập không được chứa số!");
            }
            else
            {
                errorProvider1.Clear(); // Tắt cảnh báo đỏ nếu gõ chữ bình thường
            }
        }

        // Hàm gom chung logic Đăng ký
        private void ThucHienDangKy()
        {
            if (string.IsNullOrWhiteSpace(txt_TenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txt_Email.Text) ||
                string.IsNullOrWhiteSpace(txt_MatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các thông tin bắt buộc (*)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txt_MatKhau.Text != txt_XacNhan.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string thongTin = $"Đăng ký thành công!\n\nTên: {txt_TenDangNhap.Text}\nEmail: {txt_Email.Text}";
            MessageBox.Show(thongTin, "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Khi bấm nút
        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            ThucHienDangKy();
        }

        // Khi bấm phím Enter tại ô xác nhận mật khẩu
        private void txt_XacNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThucHienDangKy();
            }
        }

        // Thoát
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
        }
    }
}