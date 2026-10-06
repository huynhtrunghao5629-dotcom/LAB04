using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04d
{
    // CLASS XỬ LÝ TOÁN HỌC (Tách rời khỏi Form)
    public class PhuongTrinhBacHai
    {
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }

        public string GiaiBacNhat()
        {
            if (A == 0) return B == 0 ? "Vô số nghiệm" : "Vô nghiệm";
            return $"Phương trình có nghiệm x = {-B / A:F2}";
        }

        public string GiaiBacHai()
        {
            if (A == 0)
            {
                float tempA = A; A = B; B = C;
                string res = GiaiBacNhat();
                A = tempA; B = tempA;
                return res;
            }

            float delta = B * B - 4 * A * C;
            if (delta < 0) return "Vô nghiệm";
            if (delta == 0) return $"Phương trình có nghiệm kép x1 = x2 = {-B / (2 * A):F2}";

            float x1 = (float)((-B + Math.Sqrt(delta)) / (2 * A));
            float x2 = (float)((-B - Math.Sqrt(delta)) / (2 * A));
            return $"Phương trình có 2 nghiệm: x1 = {x1:F2}, x2 = {x2:F2}";
        }
    }

    public partial class Form1 : Form
    {
        private Label lbl_TieuDe, lbl_a, lbl_b, lbl_c, lbl_KetQua;
        private TextBox txt_a, txt_b, txt_c, txt_KetQua;
        private RadioButton rdo_Bac1, rdo_Bac2;
        private GroupBox grp_Chon;
        private Button btn_Giai, btn_Thoat;

        public Form1()
        {
            this.Text = "Giải phương trình bậc 1-2";
            this.ClientSize = new Size(380, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            TaoGiaoDien();

            rdo_Bac1.CheckedChanged += Rdo_CheckedChanged;
            rdo_Bac2.CheckedChanged += Rdo_CheckedChanged;

            txt_a.TextChanged += KiemTraNhapLieu;
            txt_b.TextChanged += KiemTraNhapLieu;
            txt_c.TextChanged += KiemTraNhapLieu;

            txt_a.KeyPress += ChiNhapSo;
            txt_b.KeyPress += ChiNhapSo;
            txt_c.KeyPress += ChiNhapSo;

            btn_Giai.Click += Btn_Giai_Click;
            btn_Thoat.Click += (s, e) => this.Close();
            this.FormClosing += Form1_FormClosing;

            rdo_Bac1.Checked = true;
        }

        private void TaoGiaoDien()
        {
            lbl_TieuDe = new Label() { Text = "GIẢI PHƯƠNG TRÌNH", ForeColor = Color.Red, Location = new Point(70, 15), AutoSize = true, Font = new Font("Tahoma", 16, FontStyle.Bold) };

            grp_Chon = new GroupBox() { Text = "Bạn vui lòng chọn", Location = new Point(20, 50), Size = new Size(340, 80) };
            rdo_Bac1 = new RadioButton() { Text = "Phương trình bậc nhất", Location = new Point(20, 25), AutoSize = true };
            rdo_Bac2 = new RadioButton() { Text = "Phương trình bậc hai", Location = new Point(20, 50), AutoSize = true };
            grp_Chon.Controls.AddRange(new Control[] { rdo_Bac1, rdo_Bac2 });

            lbl_a = new Label() { Text = "Nhập a", Location = new Point(20, 150), AutoSize = true };
            txt_a = new TextBox() { Location = new Point(90, 147), Width = 120 };

            lbl_b = new Label() { Text = "Nhập b", Location = new Point(20, 190), AutoSize = true };
            txt_b = new TextBox() { Location = new Point(90, 187), Width = 120 };

            lbl_c = new Label() { Text = "Nhập c", Location = new Point(20, 230), AutoSize = true };
            txt_c = new TextBox() { Location = new Point(90, 227), Width = 120 };

            btn_Giai = new Button() { Text = "Giải", Location = new Point(240, 147), Size = new Size(100, 45), Enabled = false };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(240, 207), Size = new Size(100, 45) };

            lbl_KetQua = new Label() { Text = "Kết quả", Location = new Point(20, 290), AutoSize = true };
            txt_KetQua = new TextBox() { Location = new Point(90, 290), Size = new Size(250, 80), Multiline = true, ReadOnly = true, BackColor = Color.White };

            this.Controls.AddRange(new Control[] { lbl_TieuDe, grp_Chon, lbl_a, txt_a, lbl_b, txt_b, lbl_c, txt_c, btn_Giai, btn_Thoat, lbl_KetQua, txt_KetQua });
        }

        // Tự động mờ/sáng ô C tùy theo loại phương trình được chọn
        private void Rdo_CheckedChanged(object sender, EventArgs e)
        {
            bool laBac2 = rdo_Bac2.Checked;
            lbl_c.Enabled = laBac2;
            txt_c.Enabled = laBac2;
            if (!laBac2) txt_c.Clear();
            KiemTraNhapLieu(null, null);
        }

        private void ChiNhapSo(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.') e.Handled = true;
        }

        // Bật/tắt nút Giải theo điều kiện nhập liệu
        private void KiemTraNhapLieu(object sender, EventArgs e)
        {
            if (rdo_Bac1.Checked)
                btn_Giai.Enabled = !string.IsNullOrWhiteSpace(txt_a.Text) && !string.IsNullOrWhiteSpace(txt_b.Text);
            else
                btn_Giai.Enabled = !string.IsNullOrWhiteSpace(txt_a.Text) && !string.IsNullOrWhiteSpace(txt_b.Text) && !string.IsNullOrWhiteSpace(txt_c.Text);
        }

        private void Btn_Giai_Click(object sender, EventArgs e)
        {
            PhuongTrinhBacHai pt = new PhuongTrinhBacHai();
            float.TryParse(txt_a.Text, out float a);
            float.TryParse(txt_b.Text, out float b);
            pt.A = a; pt.B = b;

            if (rdo_Bac1.Checked)
            {
                txt_KetQua.Text = pt.GiaiBacNhat();
            }
            else
            {
                float.TryParse(txt_c.Text, out float c);
                pt.C = c;
                txt_KetQua.Text = pt.GiaiBacHai();
            }

            btn_Giai.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }
    }
}