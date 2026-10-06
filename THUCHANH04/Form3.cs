using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public partial class Form3 : Form
    {
        // 1. Khai báo công cụ
        private Label lbl_TieuDe, lbl_a, lbl_b, lbl_UCLN, lbl_BCNN;
        private TextBox txt_a, txt_b, txt_UCLN, txt_BCNN;
        private Button btn_ThucHien, btn_TiepTuc, btn_Thoat;
        private ErrorProvider errorProvider1;

        public Form3()
        {
            // 2. Cài đặt khung Form
            this.Text = "Ước Số - Bội Số";
            this.ClientSize = new Size(370, 260); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            // 3. Vẽ giao diện
            TaoGiaoDien();

            // 4. Đăng ký sự kiện
            txt_a.KeyPress += new KeyPressEventHandler(ChiNhapSo);
            txt_b.KeyPress += new KeyPressEventHandler(ChiNhapSo);

            btn_ThucHien.Click += new EventHandler(btn_ThucHien_Click);
            btn_TiepTuc.Click += new EventHandler(btn_TiepTuc_Click);
            btn_Thoat.Click += new EventHandler(btn_Thoat_Click);
            this.FormClosing += new FormClosingEventHandler(Form3_FormClosing);
        }

        private void TaoGiaoDien()
        {
            lbl_TieuDe = new Label() { Text = "Ước Số Chung - Bội Số Chung", Location = new Point(40, 15), AutoSize = true, Font = new Font("Tahoma", 14, FontStyle.Bold), ForeColor = Color.Red };

            lbl_a = new Label() { Text = "Nhập số a :", Location = new Point(30, 60), AutoSize = true };
            txt_a = new TextBox() { Location = new Point(190, 57), Width = 120 };

            lbl_b = new Label() { Text = "Nhập số b :", Location = new Point(30, 100), AutoSize = true };
            txt_b = new TextBox() { Location = new Point(190, 97), Width = 120 };

            lbl_UCLN = new Label() { Text = "Ước số chung lớn nhất :", Location = new Point(30, 140), AutoSize = true };
            txt_UCLN = new TextBox() { Location = new Point(190, 137), Width = 120, ReadOnly = true, BackColor = Color.White };

            lbl_BCNN = new Label() { Text = "Bội số chung nhỏ nhất :", Location = new Point(30, 180), AutoSize = true };
            txt_BCNN = new TextBox() { Location = new Point(190, 177), Width = 120, ReadOnly = true, BackColor = Color.White };

            btn_ThucHien = new Button() { Text = "Thực Hiện", Location = new Point(30, 220), Width = 90, Height = 30 };
            btn_TiepTuc = new Button() { Text = "Tiếp Tục", Location = new Point(140, 220), Width = 80, Height = 30 };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(230, 220), Width = 80, Height = 30 };

            errorProvider1 = new ErrorProvider();

            this.Controls.AddRange(new Control[] {
                lbl_TieuDe, lbl_a, txt_a, lbl_b, txt_b,
                lbl_UCLN, txt_UCLN, lbl_BCNN, txt_BCNN,
                btn_ThucHien, btn_TiepTuc, btn_Thoat
            });
        }

        private void ChiNhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                errorProvider1.SetError((Control)sender, "Chỉ được nhập số nguyên!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        // THUẬT TOÁN TÌM ƯỚC SỐ CHUNG LỚN NHẤT (UCLN)
        private int TimUCLN(int a, int b)
        {
            a = Math.Abs(a); // Lấy trị tuyệt đối để tránh lỗi với số âm
            b = Math.Abs(b);
            if (a == 0 || b == 0) return a + b;

            while (a != b)
            {
                if (a > b) a = a - b;
                else b = b - a;
            }
            return a;
        }

        // NÚT THỰC HIỆN
        private void btn_ThucHien_Click(object sender, EventArgs e)
        {
            int a, b;
            // Kiểm tra xem dữ liệu nhập vào có phải số nguyên (int) không
            if (int.TryParse(txt_a.Text, out a) && int.TryParse(txt_b.Text, out b))
            {
                int ucln = TimUCLN(a, b);
                int bcnn = (a == 0 || b == 0) ? 0 : (Math.Abs(a * b) / ucln); // Công thức BCNN

                txt_UCLN.Text = ucln.ToString();
                txt_BCNN.Text = bcnn.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số nguyên hợp lệ vào a và b!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NÚT TIẾP TỤC (Reset Form)
        private void btn_TiepTuc_Click(object sender, EventArgs e)
        {
            txt_a.Clear();
            txt_b.Clear();
            txt_UCLN.Clear();
            txt_BCNN.Clear();
            txt_a.Focus(); // Đưa con trỏ nháy quay lại ô a
        }

        // NÚT THOÁT
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close(); // Sẽ gọi đến FormClosing ở dưới
        }

        // Xác nhận khi đóng Form
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
        }
    }
}