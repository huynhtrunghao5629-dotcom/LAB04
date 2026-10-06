using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace THUCHANH04d
{
    // ================= CLASS XỬ LÝ LOGIC MẢNG =================
    public class MangSoNguyen
    {
        public List<int> arr;

        public MangSoNguyen(string input)
        {
            arr = new List<int>();
            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in parts)
            {
                if (int.TryParse(s.Trim(), out int n)) arr.Add(n);
            }
        }

        public string Xuat() => string.Join(" ", arr);

        public void SapXep(bool tang)
        {
            arr.Sort();
            if (!tang) arr.Reverse();
        }

        public int TimViTri(int giaTri) => arr.IndexOf(giaTri);
        public int TimGiaTri(int viTri) => (viTri >= 0 && viTri < arr.Count) ? arr[viTri] : -1;

        public bool XoaViTri(int viTri)
        {
            if (viTri >= 0 && viTri < arr.Count) { arr.RemoveAt(viTri); return true; }
            return false;
        }

        public bool XoaGiaTri(int giaTri) => arr.Remove(giaTri);

        public void Them(int giaTri, int viTri)
        {
            if (viTri < 0) viTri = 0;
            if (viTri > arr.Count) viTri = arr.Count;
            arr.Insert(viTri, giaTri);
        }

        public int Tong() => arr.Sum();
        public int TongChan() => arr.Where(x => x % 2 == 0).Sum();
        public int TongLe() => arr.Where(x => x % 2 != 0).Sum();

        public int Max() => arr.Count > 0 ? arr.Max() : 0;
        public int Min() => arr.Count > 0 ? arr.Min() : 0;

        public bool ThayThe(int viTri, int giaTriMoi)
        {
            if (viTri >= 0 && viTri < arr.Count) { arr[viTri] = giaTriMoi; return true; }
            return false;
        }
    }

    // ================= GIAO DIỆN VÀ SỰ KIỆN FORM =================
    public partial class Form2 : Form
    {
        private TextBox txt_Nhap, txt_KetQua;
        private TextBox txt_TimGiaTri, txt_TimViTri, txt_KqTim;
        private TextBox txt_XoaGiaTri, txt_XoaViTri;
        private TextBox txt_ThemGiaTri, txt_ThemViTri;
        private TextBox txt_ThayViTri, txt_ThayGiaTri, txt_SoThayThe;
        private TextBox txt_Tong, txt_TongChan, txt_TongLe, txt_Max, txt_Min;

        private RadioButton rdo_Tang, rdo_Giam;
        private RadioButton rdo_TimGiaTri, rdo_TimViTri;
        private RadioButton rdo_XoaGiaTri, rdo_XoaViTri;
        private RadioButton rdo_ThemGiaTri;
        private RadioButton rdo_ThayGiaTri, rdo_ThayViTri;

        private Button btn_ThucHien, btn_Reset, btn_Thoat, btn_Tong, btn_TimMaxMin;

        private MangSoNguyen mang;

        public Form2()
        {
            this.Text = "Mảng Số Nguyên";
            this.ClientSize = new Size(580, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Tahoma", 9);

            TaoGiaoDien();
            GanSuKien();
        }

        private void TaoGiaoDien()
        {
            Label lbl_Title = new Label() { Text = "Mảng Số Nguyên", ForeColor = Color.Red, Font = new Font("Tahoma", 16, FontStyle.Bold), Location = new Point(180, 10), AutoSize = true };
            this.Controls.Add(lbl_Title);

            // Dòng Nhập & Kết quả
            this.Controls.Add(new Label() { Text = "Nhập mảng:", Location = new Point(20, 60), AutoSize = true });
            txt_Nhap = new TextBox() { Location = new Point(100, 57), Width = 300 };
            btn_Reset = new Button() { Text = "Reset", Location = new Point(420, 55), Width = 80 };

            this.Controls.Add(new Label() { Text = "Kết quả:", Location = new Point(20, 95), AutoSize = true });
            txt_KetQua = new TextBox() { Location = new Point(100, 92), Width = 300, ReadOnly = true, BackColor = Color.White };
            btn_Thoat = new Button() { Text = "Thoát", Location = new Point(420, 90), Width = 80 };

            btn_ThucHien = new Button() { Text = "Thực Hiện\n(Nhập Mảng)", Location = new Point(20, 130), Size = new Size(90, 45), BackColor = Color.LightBlue };

            // ================= SẮP XẾP =================
            GroupBox grp_SapXep = new GroupBox() { Text = "Sắp Xếp", Location = new Point(130, 130), Size = new Size(270, 50) };
            rdo_Tang = new RadioButton() { Text = "Sắp xếp Tăng", Location = new Point(20, 20), AutoSize = true, Checked = true };
            rdo_Giam = new RadioButton() { Text = "Sắp xếp Giảm", Location = new Point(140, 20), AutoSize = true };
            grp_SapXep.Controls.AddRange(new Control[] { rdo_Tang, rdo_Giam });

            // ================= TÌM KIẾM =================
            GroupBox grp_Tim = new GroupBox() { Text = "Tìm Kiếm", Location = new Point(20, 190), Size = new Size(250, 110) };
            rdo_TimGiaTri = new RadioButton() { Text = "Tìm giá trị cần tìm", Location = new Point(10, 20), AutoSize = true, Checked = true };
            txt_TimGiaTri = new TextBox() { Location = new Point(140, 18), Width = 50 };
            rdo_TimViTri = new RadioButton() { Text = "Tìm vị trí cần tìm", Location = new Point(10, 50), AutoSize = true };
            txt_TimViTri = new TextBox() { Location = new Point(140, 48), Width = 50, Enabled = false };
            Label lbl_KqTim = new Label() { Text = "Số tìm được là:", Location = new Point(30, 80), AutoSize = true };
            txt_KqTim = new TextBox() { Location = new Point(140, 78), Width = 50, ReadOnly = true, BackColor = Color.White };
            // Lần này đã add đầy đủ Label và txt_KqTim vào GroupBox
            grp_Tim.Controls.AddRange(new Control[] { rdo_TimGiaTri, txt_TimGiaTri, rdo_TimViTri, txt_TimViTri, lbl_KqTim, txt_KqTim });

            // ================= XÓA =================
            GroupBox grp_Xoa = new GroupBox() { Text = "Xóa (Nhấn Enter)", Location = new Point(290, 190), Size = new Size(250, 110) };
            rdo_XoaGiaTri = new RadioButton() { Text = "Tìm giá trị cần xóa", Location = new Point(10, 20), AutoSize = true, Checked = true };
            txt_XoaGiaTri = new TextBox() { Location = new Point(150, 18), Width = 50 };
            rdo_XoaViTri = new RadioButton() { Text = "Tìm vị trí cần xóa", Location = new Point(10, 50), AutoSize = true };
            txt_XoaViTri = new TextBox() { Location = new Point(150, 48), Width = 50, Enabled = false };
            grp_Xoa.Controls.AddRange(new Control[] { rdo_XoaGiaTri, txt_XoaGiaTri, rdo_XoaViTri, txt_XoaViTri });

            // ================= THÊM =================
            GroupBox grp_Them = new GroupBox() { Text = "Thêm (Nhấn Enter)", Location = new Point(20, 310), Size = new Size(250, 110) };
            rdo_ThemGiaTri = new RadioButton() { Text = "Giá trị cần thêm", Location = new Point(10, 20), AutoSize = true, Checked = true };
            txt_ThemGiaTri = new TextBox() { Location = new Point(150, 18), Width = 50 };
            Label lbl_ThemViTri = new Label() { Text = "Tại vị trí cần thêm:", Location = new Point(30, 52), AutoSize = true };
            txt_ThemViTri = new TextBox() { Location = new Point(150, 48), Width = 50 };
            // Lần này đã add txt_ThemViTri vào GroupBox
            grp_Them.Controls.AddRange(new Control[] { rdo_ThemGiaTri, txt_ThemGiaTri, lbl_ThemViTri, txt_ThemViTri });

            // ================= THAY THẾ =================
            GroupBox grp_Thay = new GroupBox() { Text = "Thay Thế (Nhấn Enter)", Location = new Point(290, 310), Size = new Size(250, 110) };
            rdo_ThayGiaTri = new RadioButton() { Text = "Giá trị cần thay thế", Location = new Point(10, 20), AutoSize = true, Checked = true };
            txt_ThayGiaTri = new TextBox() { Location = new Point(150, 18), Width = 50 };
            rdo_ThayViTri = new RadioButton() { Text = "Vị trí cần thay thế", Location = new Point(10, 50), AutoSize = true };
            txt_ThayViTri = new TextBox() { Location = new Point(150, 48), Width = 50, Enabled = false };
            Label lbl_Thay = new Label() { Text = "Số thay thế là:", Location = new Point(30, 82), AutoSize = true };
            txt_SoThayThe = new TextBox() { Location = new Point(150, 78), Width = 50 };
            grp_Thay.Controls.AddRange(new Control[] { rdo_ThayGiaTri, txt_ThayGiaTri, rdo_ThayViTri, txt_ThayViTri, lbl_Thay, txt_SoThayThe });

            // ================= TỔNG =================
            GroupBox grp_Tong = new GroupBox() { Text = "Tổng", Location = new Point(20, 430), Size = new Size(250, 90) };
            Label lbl_Tong = new Label() { Text = "Tổng:", Location = new Point(10, 20), AutoSize = true };
            txt_Tong = new TextBox() { Location = new Point(50, 17), Width = 45, ReadOnly = true, BackColor = Color.White };
            Label lbl_Chan = new Label() { Text = "Chẵn:", Location = new Point(105, 20), AutoSize = true };
            txt_TongChan = new TextBox() { Location = new Point(150, 17), Width = 45, ReadOnly = true, BackColor = Color.White };
            Label lbl_Le = new Label() { Text = "Lẻ:", Location = new Point(10, 50), AutoSize = true };
            txt_TongLe = new TextBox() { Location = new Point(50, 47), Width = 45, ReadOnly = true, BackColor = Color.White };
            btn_Tong = new Button() { Text = "Tính Tổng", Location = new Point(110, 45), Width = 85 };
            // Lần này đã add tất cả TextBox Tổng vào GroupBox
            grp_Tong.Controls.AddRange(new Control[] { lbl_Tong, txt_Tong, lbl_Chan, txt_TongChan, lbl_Le, txt_TongLe, btn_Tong });

            // ================= MAX - MIN =================
            GroupBox grp_Max = new GroupBox() { Text = "Max - Min", Location = new Point(290, 430), Size = new Size(250, 90) };
            Label lbl_Max = new Label() { Text = "Lớn nhất:", Location = new Point(10, 25), AutoSize = true };
            txt_Max = new TextBox() { Location = new Point(80, 22), Width = 50, ReadOnly = true, BackColor = Color.White };
            Label lbl_Min = new Label() { Text = "Nhỏ nhất:", Location = new Point(10, 55), AutoSize = true };
            txt_Min = new TextBox() { Location = new Point(80, 52), Width = 50, ReadOnly = true, BackColor = Color.White };
            btn_TimMaxMin = new Button() { Text = "Tìm", Location = new Point(150, 30), Width = 60, Height = 40 };
            // Lần này đã add Textbox Max/Min vào GroupBox
            grp_Max.Controls.AddRange(new Control[] { lbl_Max, txt_Max, lbl_Min, txt_Min, btn_TimMaxMin });

            this.Controls.AddRange(new Control[] { txt_Nhap, btn_Reset, txt_KetQua, btn_Thoat, btn_ThucHien, grp_SapXep, grp_Tim, grp_Xoa, grp_Them, grp_Thay, grp_Tong, grp_Max });
        }

        private void GanSuKien()
        {
            btn_Thoat.Click += (s, e) => this.Close();
            this.FormClosing += (s, e) => {
                if (MessageBox.Show("Thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
            };

            btn_Reset.Click += (s, e) => {
                txt_Nhap.Clear(); txt_KetQua.Clear(); mang = null;
                txt_KqTim.Clear(); txt_TimGiaTri.Clear(); txt_TimViTri.Clear();
                txt_XoaGiaTri.Clear(); txt_XoaViTri.Clear();
                txt_ThemGiaTri.Clear(); txt_ThemViTri.Clear();
                txt_ThayGiaTri.Clear(); txt_ThayViTri.Clear(); txt_SoThayThe.Clear();
                txt_Tong.Clear(); txt_TongChan.Clear(); txt_TongLe.Clear();
                txt_Max.Clear(); txt_Min.Clear();
            };

            // LOGIC NHẬP MẢNG
            Action NhapMang = () => {
                if (string.IsNullOrWhiteSpace(txt_Nhap.Text)) return;
                mang = new MangSoNguyen(txt_Nhap.Text);
                mang.SapXep(rdo_Tang.Checked);
                txt_KetQua.Text = mang.Xuat();
            };
            btn_ThucHien.Click += (s, e) => NhapMang();
            txt_Nhap.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; NhapMang(); }
            };

            // SẮP XẾP TỰ ĐỘNG KHI CLICK RADIO BUTTON
            rdo_Tang.CheckedChanged += (s, e) => { if (mang != null && rdo_Tang.Checked) { mang.SapXep(true); txt_KetQua.Text = mang.Xuat(); } };
            rdo_Giam.CheckedChanged += (s, e) => { if (mang != null && rdo_Giam.Checked) { mang.SapXep(false); txt_KetQua.Text = mang.Xuat(); } };

            // KHÓA/MỞ TEXTBOX THEO RADIOBUTTON
            rdo_TimGiaTri.CheckedChanged += (s, e) => { txt_TimGiaTri.Enabled = rdo_TimGiaTri.Checked; txt_TimViTri.Enabled = !rdo_TimGiaTri.Checked; txt_TimViTri.Clear(); };
            rdo_TimViTri.CheckedChanged += (s, e) => { txt_TimViTri.Enabled = rdo_TimViTri.Checked; txt_TimGiaTri.Enabled = !rdo_TimViTri.Checked; txt_TimGiaTri.Clear(); };

            rdo_XoaGiaTri.CheckedChanged += (s, e) => { txt_XoaGiaTri.Enabled = rdo_XoaGiaTri.Checked; txt_XoaViTri.Enabled = !rdo_XoaGiaTri.Checked; txt_XoaViTri.Clear(); };
            rdo_XoaViTri.CheckedChanged += (s, e) => { txt_XoaViTri.Enabled = rdo_XoaViTri.Checked; txt_XoaGiaTri.Enabled = !rdo_XoaViTri.Checked; txt_XoaGiaTri.Clear(); };

            rdo_ThayGiaTri.CheckedChanged += (s, e) => { txt_ThayGiaTri.Enabled = rdo_ThayGiaTri.Checked; txt_ThayViTri.Enabled = !rdo_ThayGiaTri.Checked; txt_ThayViTri.Clear(); };
            rdo_ThayViTri.CheckedChanged += (s, e) => { txt_ThayViTri.Enabled = rdo_ThayViTri.Checked; txt_ThayGiaTri.Enabled = !rdo_ThayViTri.Checked; txt_ThayGiaTri.Clear(); };


            // TÌM KIẾM (TỰ ĐỘNG GÕ ĐẾN ĐÂU TÌM ĐẾN ĐÓ)
            txt_TimGiaTri.TextChanged += (s, e) => {
                if (mang != null && rdo_TimGiaTri.Checked)
                {
                    if (int.TryParse(txt_TimGiaTri.Text, out int val))
                    {
                        int vt = mang.TimViTri(val);
                        txt_KqTim.Text = (vt != -1) ? vt.ToString() : "Ko có";
                    }
                    else txt_KqTim.Clear();
                }
            };

            txt_TimViTri.TextChanged += (s, e) => {
                if (mang != null && rdo_TimViTri.Checked)
                {
                    if (int.TryParse(txt_TimViTri.Text, out int pos))
                    {
                        int gt = mang.TimGiaTri(pos);
                        txt_KqTim.Text = (gt != -1) ? gt.ToString() : "Ko có";
                    }
                    else txt_KqTim.Clear();
                }
            };

            // XÓA
            KeyEventHandler xoaHandler = (s, e) => {
                if (e.KeyCode == Keys.Enter && mang != null)
                {
                    e.SuppressKeyPress = true; // Bỏ tiếng kêu bíp
                    if (rdo_XoaGiaTri.Checked && int.TryParse(txt_XoaGiaTri.Text, out int val))
                    {
                        if (mang.XoaGiaTri(val))
                        {
                            txt_KetQua.Text = mang.Xuat();
                            MessageBox.Show("Xóa giá trị thành công!");
                        }
                        else MessageBox.Show("Không tìm thấy giá trị này!");
                    }
                    else if (rdo_XoaViTri.Checked && int.TryParse(txt_XoaViTri.Text, out int pos))
                    {
                        if (mang.XoaViTri(pos))
                        {
                            txt_KetQua.Text = mang.Xuat();
                            MessageBox.Show("Xóa tại vị trí thành công!");
                        }
                        else MessageBox.Show("Vị trí không hợp lệ!");
                    }
                }
            };
            txt_XoaGiaTri.KeyDown += xoaHandler;
            txt_XoaViTri.KeyDown += xoaHandler;

            // THÊM
            KeyEventHandler themHandler = (s, e) => {
                if (e.KeyCode == Keys.Enter && mang != null)
                {
                    e.SuppressKeyPress = true;
                    if (int.TryParse(txt_ThemGiaTri.Text, out int val) && int.TryParse(txt_ThemViTri.Text, out int pos))
                    {
                        mang.Them(val, pos);
                        txt_KetQua.Text = mang.Xuat();
                        MessageBox.Show("Thêm thành công!");
                    }
                    else MessageBox.Show("Vui lòng nhập cả giá trị và vị trí!");
                }
            };
            txt_ThemGiaTri.KeyDown += themHandler;
            txt_ThemViTri.KeyDown += themHandler;

            // THAY THẾ
            KeyEventHandler thayTheHandler = (s, e) => {
                if (e.KeyCode == Keys.Enter && mang != null)
                {
                    e.SuppressKeyPress = true;
                    if (!int.TryParse(txt_SoThayThe.Text, out int newVal))
                    {
                        MessageBox.Show("Vui lòng nhập 'Số thay thế là' (Giá trị mới)!");
                        return;
                    }

                    if (rdo_ThayGiaTri.Checked && int.TryParse(txt_ThayGiaTri.Text, out int oldVal))
                    {
                        int pos = mang.TimViTri(oldVal);
                        if (pos != -1)
                        {
                            mang.ThayThe(pos, newVal);
                            txt_KetQua.Text = mang.Xuat();
                            MessageBox.Show("Thay thế thành công!");
                        }
                        else MessageBox.Show("Không tìm thấy giá trị cũ để thay!");
                    }
                    else if (rdo_ThayViTri.Checked && int.TryParse(txt_ThayViTri.Text, out int pos))
                    {
                        if (mang.ThayThe(pos, newVal))
                        {
                            txt_KetQua.Text = mang.Xuat();
                            MessageBox.Show("Thay thế thành công!");
                        }
                        else MessageBox.Show("Vị trí không hợp lệ!");
                    }
                }
            };
            txt_ThayGiaTri.KeyDown += thayTheHandler;
            txt_ThayViTri.KeyDown += thayTheHandler;
            txt_SoThayThe.KeyDown += thayTheHandler;

            // TỔNG & MAX MIN
            btn_Tong.Click += (s, e) => {
                if (mang != null)
                {
                    txt_Tong.Text = mang.Tong().ToString();
                    txt_TongChan.Text = mang.TongChan().ToString();
                    txt_TongLe.Text = mang.TongLe().ToString();
                }
            };
            btn_TimMaxMin.Click += (s, e) => {
                if (mang != null)
                {
                    txt_Max.Text = mang.Max().ToString();
                    txt_Min.Text = mang.Min().ToString();
                }
            };
        }
    }
}