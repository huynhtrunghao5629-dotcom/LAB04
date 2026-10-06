using System;
using System.Drawing;
using System.Windows.Forms;

namespace THUCHANH04d
{
    public partial class Form4 : Form
    {
        // Khai báo các Control
        private TextBox txt_HoTen, txt_DiaChi, txt_SoNgay;
        private Label lbl_ThanhTien;
        private RadioButton rdo_Don, rdo_Doi, rdo_Ba;
        private CheckBox chk_Tivi, chk_Internet, chk_NuocNong;
        private CheckBox chk_Karaoke, chk_AnSang;
        private TextBox txt_TongLuot, txt_TongTien;
        private Button btn_ThanhToan, btn_NhapMoi, btn_TongKet, btn_Thoat;

        // Biến lưu trữ tổng kết cuối ngày
        private int tongSoLuot = 0;
        private float tongDoanhThu = 0;

        public Form4()
        {
            this.Text = "Quản lý Khách sạn";
            this.ClientSize = new Size(620, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 10);

            TaoGiaoDien();
            GanSuKien();
            KhoiTaoTrangThai();
        }

        private void TaoGiaoDien()
        {
            Label lbl_Title = new Label() { Text = "KHÁCH SẠN THANH THANH - TRẢ PHÒNG", ForeColor = Color.Red, Font = new Font("Tahoma", 16, FontStyle.Bold), Location = new Point(80, 15), AutoSize = true };
            this.Controls.Add(lbl_Title);

            // ================= THÔNG TIN KHÁCH =================
            this.Controls.Add(new Label() { Text = "Họ và tên:", Location = new Point(20, 60), AutoSize = true });
            txt_HoTen = new TextBox() { Location = new Point(100, 57), Width = 220 };

            this.Controls.Add(new Label() { Text = "Địa chỉ:", Location = new Point(20, 95), AutoSize = true });
            txt_DiaChi = new TextBox() { Location = new Point(100, 92), Width = 220 };

            this.Controls.Add(new Label() { Text = "Số ngày ở:", Location = new Point(20, 130), AutoSize = true });
            txt_SoNgay = new TextBox() { Location = new Point(100, 127), Width = 80 };

            this.Controls.Add(new Label() { Text = "Thành tiền:", Location = new Point(340, 60), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold) });
            lbl_ThanhTien = new Label() { Text = "0 VNĐ", Location = new Point(430, 60), AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold), ForeColor = Color.Red };

            this.Controls.AddRange(new Control[] { txt_HoTen, txt_DiaChi, txt_SoNgay, lbl_ThanhTien });

            // ================= CÁC NÚT BẤM (Bên phải) =================
            btn_ThanhToan = new Button() { Text = "Thanh toán", Location = new Point(480, 100), Width = 100, Height = 30 };
            btn_NhapMoi = new Button() { Text = "Nhập mới", Location = new Point(480, 140), Width = 100, Height = 30 };
            btn_TongKet = new Button() { Text = "Tổng Kết", Location = new Point(480, 290), Width = 100, Height = 30 };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(480, 330), Width = 100, Height = 30 };
            this.Controls.AddRange(new Control[] { btn_ThanhToan, btn_NhapMoi, btn_TongKet, btn_Thoat });

            // ================= LOẠI PHÒNG =================
            GroupBox grp_Phong = new GroupBox() { Text = "Loại phòng", Location = new Point(20, 180), Size = new Size(130, 140) };
            rdo_Don = new RadioButton() { Text = "Phòng đơn", Location = new Point(10, 30), AutoSize = true, Checked = true };
            rdo_Doi = new RadioButton() { Text = "Phòng đôi", Location = new Point(10, 65), AutoSize = true };
            rdo_Ba = new RadioButton() { Text = "Phòng ba", Location = new Point(10, 100), AutoSize = true };
            grp_Phong.Controls.AddRange(new Control[] { rdo_Don, rdo_Doi, rdo_Ba });

            // ================= TIỆN NGHI =================
            GroupBox grp_TienNghi = new GroupBox() { Text = "Tiện nghi", Location = new Point(160, 180), Size = new Size(150, 140) };
            chk_Tivi = new CheckBox() { Text = "Ti Vi", Location = new Point(10, 30), AutoSize = true };
            chk_Internet = new CheckBox() { Text = "Internet", Location = new Point(10, 65), AutoSize = true };
            chk_NuocNong = new CheckBox() { Text = "Máy nước nóng", Location = new Point(10, 100), AutoSize = true };
            grp_TienNghi.Controls.AddRange(new Control[] { chk_Tivi, chk_Internet, chk_NuocNong });

            // ================= DỊCH VỤ =================
            GroupBox grp_DichVu = new GroupBox() { Text = "Dịch vụ", Location = new Point(320, 180), Size = new Size(130, 140) };
            chk_Karaoke = new CheckBox() { Text = "Karaoke", Location = new Point(10, 30), AutoSize = true };
            chk_AnSang = new CheckBox() { Text = "Ăn sáng", Location = new Point(10, 65), AutoSize = true };
            grp_DichVu.Controls.AddRange(new Control[] { chk_Karaoke, chk_AnSang });

            this.Controls.AddRange(new Control[] { grp_Phong, grp_TienNghi, grp_DichVu });

            // ================= THÔNG TIN TỔNG KẾT =================
            GroupBox grp_TongKet = new GroupBox() { Text = "Thông tin tổng kết", Location = new Point(20, 340), Size = new Size(430, 100) };

            Label lbl_Luot = new Label() { Text = "Số lượt người:", Location = new Point(20, 30), AutoSize = true };
            txt_TongLuot = new TextBox() { Location = new Point(140, 27), Width = 100, ReadOnly = true, BackColor = Color.White };

            Label lbl_TongTien = new Label() { Text = "Tổng số tiền:", Location = new Point(20, 65), AutoSize = true };
            txt_TongTien = new TextBox() { Location = new Point(140, 62), Width = 250, ReadOnly = true, BackColor = Color.White };

            // Lần này đã nhét đủ 4 component vào trong GroupBox
            grp_TongKet.Controls.AddRange(new Control[] { lbl_Luot, txt_TongLuot, lbl_TongTien, txt_TongTien });
            this.Controls.Add(grp_TongKet);
        }

        private void KhoiTaoTrangThai()
        {
            txt_HoTen.Focus();
            btn_ThanhToan.Enabled = false;
            btn_NhapMoi.Enabled = false;
            btn_TongKet.Enabled = (tongSoLuot > 0);
        }

        private void GanSuKien()
        {
            btn_Thoat.Click += (s, e) => this.Close();
            this.FormClosing += (s, e) => {
                if (MessageBox.Show("Thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
            };

            txt_SoNgay.KeyPress += (s, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };

            EventHandler kiemTraNhap = (s, e) => {
                btn_ThanhToan.Enabled = !string.IsNullOrWhiteSpace(txt_HoTen.Text) &&
                                        !string.IsNullOrWhiteSpace(txt_DiaChi.Text) &&
                                        !string.IsNullOrWhiteSpace(txt_SoNgay.Text);
            };
            txt_HoTen.TextChanged += kiemTraNhap;
            txt_DiaChi.TextChanged += kiemTraNhap;
            txt_SoNgay.TextChanged += kiemTraNhap;

            // ================= THANH TOÁN =================
            btn_ThanhToan.Click += (s, e) => {
                if (int.TryParse(txt_SoNgay.Text, out int soNgay))
                {
                    float tienPhong = 0;
                    if (rdo_Don.Checked) tienPhong = 300000 * soNgay;
                    else if (rdo_Doi.Checked) tienPhong = 350000 * soNgay;
                    else if (rdo_Ba.Checked) tienPhong = 400000 * soNgay;

                    float tienTienNghi = 0;
                    if (chk_Tivi.Checked) tienTienNghi += 10000;
                    if (chk_Internet.Checked) tienTienNghi += 10000;
                    if (chk_NuocNong.Checked) tienTienNghi += 10000;

                    float tienDichVu = 0;
                    if (chk_Karaoke.Checked) tienDichVu += 50000;
                    if (chk_AnSang.Checked) tienDichVu += (15000 * soNgay);

                    float tienKhachHienTai = tienPhong + tienTienNghi + tienDichVu;
                    lbl_ThanhTien.Text = tienKhachHienTai.ToString("N0") + " VNĐ";

                    // Cộng dồn vào quỹ
                    tongSoLuot++;
                    tongDoanhThu += tienKhachHienTai;

                    // Mở khóa các nút
                    btn_NhapMoi.Enabled = true;
                    btn_TongKet.Enabled = true;
                    btn_ThanhToan.Enabled = false;
                }
            };

            // ================= NHẬP MỚI =================
            btn_NhapMoi.Click += (s, e) => {
                txt_HoTen.Clear(); txt_DiaChi.Clear(); txt_SoNgay.Clear();
                lbl_ThanhTien.Text = "0 VNĐ";
                txt_TongLuot.Clear(); txt_TongTien.Clear(); // Xóa màn hình thống kê cũ nếu có

                rdo_Don.Checked = true;
                chk_Tivi.Checked = false; chk_Internet.Checked = false; chk_NuocNong.Checked = false;
                chk_Karaoke.Checked = false; chk_AnSang.Checked = false;

                KhoiTaoTrangThai();
            };

            // ================= TỔNG KẾT =================
            btn_TongKet.Click += (s, e) => {
                // In ra kết quả
                txt_TongLuot.Text = tongSoLuot.ToString();
                txt_TongTien.Text = tongDoanhThu.ToString("N0") + " VNĐ";

                // Xóa quỹ về 0 để đóng ca
                tongSoLuot = 0;
                tongDoanhThu = 0;

                btn_TongKet.Enabled = false;
            };
        }
    }
}