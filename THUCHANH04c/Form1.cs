using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04
{
    public class Form1 : Form
    {
        // 1. Khai báo các công cụ (Controls)
        private Label lbl_a, lbl_b, lbl_KetQua;
        private TextBox txt_a, txt_b, txt_KetQua;
        private Button btn_Cong, btn_Tru, btn_Nhan, btn_Chia;
        private ErrorProvider errorProvider1;

        public Form1()
        {
            // 2. Thiết lập kích thước và tiêu đề cho Form
            this.Text = "Cộng trừ nhân chia";
            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            // 3. Khởi tạo và vẽ giao diện bằng Code
            TaoGiaoDien();

            // 4. Gán sự kiện
            txt_a.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            txt_b.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            txt_a.Leave += new EventHandler(TextBox_Leave);
            txt_b.Leave += new EventHandler(TextBox_Leave);

            btn_Cong.Click += new EventHandler(btn_Cong_Click);
            btn_Tru.Click += new EventHandler(btn_Tru_Click);
            btn_Nhan.Click += new EventHandler(btn_Nhan_Click);
            btn_Chia.Click += new EventHandler(btn_Chia_Click);

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        // HÀM TỰ ĐỘNG VẼ GIAO DIỆN
        private void TaoGiaoDien()
        {
            // Dòng 1: Nhập a và b
            lbl_a = new Label() { Text = "a = ", Location = new Point(30, 30), AutoSize = true };
            txt_a = new TextBox() { Location = new Point(70, 27), Width = 60 };

            lbl_b = new Label() { Text = "b = ", Location = new Point(150, 30), AutoSize = true };
            txt_b = new TextBox() { Location = new Point(190, 27), Width = 60 };

            // Dòng 2: Kết quả
            lbl_KetQua = new Label() { Text = "Kết quả", Location = new Point(30, 70), AutoSize = true };
            txt_KetQua = new TextBox() { Location = new Point(90, 67), Width = 160, ReadOnly = true };

            // Dòng 3: Các nút phép tính
            btn_Cong = new Button() { Text = "+", Location = new Point(30, 110), Width = 40 };
            btn_Tru = new Button() { Text = "-", Location = new Point(80, 110), Width = 40 };
            btn_Nhan = new Button() { Text = "x", Location = new Point(130, 110), Width = 40 };
            btn_Chia = new Button() { Text = "/", Location = new Point(180, 110), Width = 40 };

            errorProvider1 = new ErrorProvider();

            // Đẩy tất cả các công cụ lên Form
            this.Controls.Add(lbl_a); this.Controls.Add(txt_a);
            this.Controls.Add(lbl_b); this.Controls.Add(txt_b);
            this.Controls.Add(lbl_KetQua); this.Controls.Add(txt_KetQua);
            this.Controls.Add(btn_Cong); this.Controls.Add(btn_Tru);
            this.Controls.Add(btn_Nhan); this.Controls.Add(btn_Chia);
        }

        // ================= PHẦN XỬ LÝ LOGIC =================

        // MỨC 1: Bắt lỗi bỏ trống
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
                errorProvider1.SetError(txt, "Vui lòng nhập giá trị!");
            else
                errorProvider1.Clear();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(673, 504);
            this.Name = "Form1";
            this.ResumeLayout(false);
           
        }

        // MỨC 2: Chặn nhập chữ
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lấy giá trị a, b
        private bool LayGiaTri(out float a, out float b)
        {
            a = 0; b = 0;
            if (float.TryParse(txt_a.Text, out a) && float.TryParse(txt_b.Text, out b)) return true;
            MessageBox.Show("Vui lòng nhập đầy đủ a và b!", "Lỗi");
            return false;
        }

        // Xử lý tính toán
        private void btn_Cong_Click(object sender, EventArgs e)
        {
            if (LayGiaTri(out float a, out float b)) txt_KetQua.Text = (a + b).ToString();
        }

        private void btn_Tru_Click(object sender, EventArgs e)
        {
            if (LayGiaTri(out float a, out float b)) txt_KetQua.Text = (a - b).ToString();
        }

        private void btn_Nhan_Click(object sender, EventArgs e)
        {
            if (LayGiaTri(out float a, out float b)) txt_KetQua.Text = (a * b).ToString();
        }

        private void btn_Chia_Click(object sender, EventArgs e)
        {
            if (LayGiaTri(out float a, out float b))
            {
                if (b == 0) MessageBox.Show("Không thể chia cho 0!");
                else txt_KetQua.Text = (a / b).ToString();
            }
        }

        // Hỏi trước khi thoát
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No) e.Cancel = true;
        }
    }
}