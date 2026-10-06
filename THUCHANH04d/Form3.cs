using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04d
{
    public partial class Form3 : Form
    {
        private TextBox txt_TenKH, txt_SoKH;
        private CheckBox chk_SinhVien;
        private RadioButton rdo_Den, rdo_Da, rdo_Sua, rdo_SuaDa, rdo_Kem;
        private CheckBox chk_BmTrung, chk_BmCa, chk_MyTom, chk_MyXao, chk_MyCay;
        private Button btn_TinhTien, btn_NhapLai, btn_ThanhToan, btn_Thoat;
        private TextBox txt_TongKhach, txt_TongTien;

        // Biến lưu trữ doanh thu tổng
        private int tongSoKhach = 0;
        private float tongDoanhThu = 0;
        private float tienKhachHienTai = 0;

        public Form3()
        {
            this.Text = "Thanh toán tiền";
            this.ClientSize = new Size(520, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            TaoGiaoDien();
            GanSuKien();
            KhoiTaoTrangThai();
        }

        private void TaoGiaoDien()
        {
            Label lbl_Title = new Label() { Text = "CAFE SINH VIÊN", ForeColor = Color.Orange, Font = new Font("Tahoma", 16, FontStyle.Bold), Location = new Point(170, 15), AutoSize = true };
            this.Controls.Add(lbl_Title);

            // ================= THÔNG TIN KHÁCH HÀNG =================
            this.Controls.Add(new Label() { Text = "Tên khách hàng", Location = new Point(30, 60), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold) });
            txt_TenKH = new TextBox() { Location = new Point(160, 57), Width = 320 };

            this.Controls.Add(new Label() { Text = "Số khách hàng", Location = new Point(30, 95), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold) });
            txt_SoKH = new TextBox() { Location = new Point(160, 92), Width = 320 };

            chk_SinhVien = new CheckBox() { Text = "Sinh viên ?", Location = new Point(160, 125), AutoSize = true };
            this.Controls.AddRange(new Control[] { txt_TenKH, txt_SoKH, chk_SinhVien });

            // ================= MENU NƯỚC UỐNG (RadioButton) =================
            GroupBox grp_Nuoc = new GroupBox() { Text = "Nước uống", Location = new Point(30, 160), Size = new Size(220, 150) };
            rdo_Den = new RadioButton() { Text = "Cafe đen", Location = new Point(10, 30), AutoSize = true };
            rdo_Da = new RadioButton() { Text = "Cafe đá", Location = new Point(120, 30), AutoSize = true };
            rdo_Sua = new RadioButton() { Text = "Cafe sữa", Location = new Point(10, 70), AutoSize = true };
            rdo_Kem = new RadioButton() { Text = "Cafe kem", Location = new Point(120, 70), AutoSize = true };
            rdo_SuaDa = new RadioButton() { Text = "Cafe sữa đá", Location = new Point(10, 110), AutoSize = true };
            grp_Nuoc.Controls.AddRange(new Control[] { rdo_Den, rdo_Da, rdo_Sua, rdo_Kem, rdo_SuaDa });

            // ================= MENU THỨC ĂN (CheckBox) =================
            GroupBox grp_ThucAn = new GroupBox() { Text = "Thức ăn", Location = new Point(270, 160), Size = new Size(210, 150) };
            chk_BmTrung = new CheckBox() { Text = "Bánh mỳ trứng", Location = new Point(10, 30), AutoSize = true };
            chk_MyXao = new CheckBox() { Text = "Mỳ xào bò", Location = new Point(130, 30), AutoSize = true };
            chk_BmCa = new CheckBox() { Text = "Bánh mỳ cá", Location = new Point(10, 70), AutoSize = true };
            chk_MyCay = new CheckBox() { Text = "Mỳ cay", Location = new Point(130, 70), AutoSize = true };
            chk_MyTom = new CheckBox() { Text = "Mỳ tôm trứng", Location = new Point(10, 110), AutoSize = true };
            grp_ThucAn.Controls.AddRange(new Control[] { chk_BmTrung, chk_MyXao, chk_BmCa, chk_MyCay, chk_MyTom });

            this.Controls.AddRange(new Control[] { grp_Nuoc, grp_ThucAn });

            // ================= CÁC NÚT CHỨC NĂNG =================
            btn_TinhTien = new Button() { Text = "Tính tiền", Location = new Point(40, 330), Width = 90, Height = 35 };
            btn_NhapLai = new Button() { Text = "Nhập lại", Location = new Point(150, 330), Width = 90, Height = 35 };
            btn_ThanhToan = new Button() { Text = "Thanh toán", Location = new Point(260, 330), Width = 100, Height = 35 };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(380, 330), Width = 90, Height = 35 };
            this.Controls.AddRange(new Control[] { btn_TinhTien, btn_NhapLai, btn_ThanhToan, btn_Thoat });

            // ================= THỐNG KÊ TỔNG =================
            this.Controls.Add(new Label() { Text = "Tổng khách hàng", Location = new Point(30, 395), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold) });
            // Đã đẩy TextBox sang X=210 để không bị Label đè lên
            txt_TongKhach = new TextBox() { Location = new Point(210, 392), Width = 270, ReadOnly = true, BackColor = Color.White };

            this.Controls.Add(new Label() { Text = "Tổng tiền thanh toán", Location = new Point(30, 430), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold) });
            // Đã đẩy TextBox sang X=210
            txt_TongTien = new TextBox() { Location = new Point(210, 427), Width = 270, ReadOnly = true, BackColor = Color.White };

            this.Controls.AddRange(new Control[] { txt_TongKhach, txt_TongTien });
        }

        private void KhoiTaoTrangThai()
        {
            txt_TenKH.Focus();
            btn_TinhTien.Enabled = false;
            btn_NhapLai.Enabled = false;
            btn_ThanhToan.Enabled = false;
        }

        private void GanSuKien()
        {
            // Thoát Form
            btn_Thoat.Click += (s, e) => this.Close();
            this.FormClosing += (s, e) => {
                if (MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
            };

            // Chỉ cho nhập số vào ô Số lượng khách
            txt_SoKH.KeyPress += (s, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };

            // Mở khóa nút Tính Tiền khi đã nhập Tên Khách Hàng và Số Khách Hàng
            EventHandler kiemTraNhap = (s, e) => {
                btn_TinhTien.Enabled = !string.IsNullOrWhiteSpace(txt_TenKH.Text) && !string.IsNullOrWhiteSpace(txt_SoKH.Text);
            };
            txt_TenKH.TextChanged += kiemTraNhap;
            txt_SoKH.TextChanged += kiemTraNhap;

            // ================= LOGIC TÍNH TIỀN =================
            btn_TinhTien.Click += (s, e) => {
                float tienNuoc = 0;
                if (rdo_Den.Checked) tienNuoc = 20000;
                else if (rdo_Da.Checked) tienNuoc = 25000;
                else if (rdo_Sua.Checked) tienNuoc = 25000;
                else if (rdo_SuaDa.Checked) tienNuoc = 30000;
                else if (rdo_Kem.Checked) tienNuoc = 35000;

                float tienAn = 0;
                if (chk_BmTrung.Checked) tienAn += 15000;
                if (chk_BmCa.Checked) tienAn += 15000;
                if (chk_MyTom.Checked) tienAn += 20000;
                if (chk_MyXao.Checked) tienAn += 30000;
                if (chk_MyCay.Checked) tienAn += 50000;

                tienKhachHienTai = tienNuoc + tienAn;

                // Giảm giá 20% cho sinh viên
                if (chk_SinhVien.Checked)
                {
                    tienKhachHienTai = tienKhachHienTai * 0.8f;
                }

                MessageBox.Show($"Khách hàng: {txt_TenKH.Text}\nSố tiền phải trả: {tienKhachHienTai:N0} VNĐ", "Hóa Đơn");

                btn_NhapLai.Enabled = true;
                btn_ThanhToan.Enabled = true;
            };

            // ================= LOGIC THANH TOÁN =================
            btn_ThanhToan.Click += (s, e) => {
                if (int.TryParse(txt_SoKH.Text, out int soKhach))
                {
                    tongSoKhach += soKhach;
                    tongDoanhThu += tienKhachHienTai;

                    txt_TongKhach.Text = tongSoKhach.ToString();
                    txt_TongTien.Text = tongDoanhThu.ToString("N0") + " VNĐ";

                    // Chạy hàm Reset để đón khách mới nhưng vẫn giữ lại tổng doanh thu bên dưới
                    btn_NhapLai.PerformClick();
                    btn_ThanhToan.Enabled = false;
                }
            };

            // ================= LOGIC NHẬP LẠI (RESET) =================
            btn_NhapLai.Click += (s, e) => {
                txt_TenKH.Clear();
                txt_SoKH.Clear();
                chk_SinhVien.Checked = false;

                rdo_Den.Checked = false; rdo_Da.Checked = false; rdo_Sua.Checked = false; rdo_SuaDa.Checked = false; rdo_Kem.Checked = false;
                chk_BmTrung.Checked = false; chk_BmCa.Checked = false; chk_MyTom.Checked = false; chk_MyXao.Checked = false; chk_MyCay.Checked = false;

                KhoiTaoTrangThai();
            };
        }
    }
}