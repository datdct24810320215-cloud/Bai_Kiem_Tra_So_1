using System.ComponentModel;

namespace TechMartProductManager
{
    // Class Sản phẩm
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string AvatarPath { get; set; }
    }

    public partial class Form1 : Form
    {
        // Sử dụng BindingList và BindingSource như yêu cầu
        private BindingList<Product> productList = new BindingList<Product>();
        private BindingSource bindingSource = new BindingSource();
        private string currentImagePath = "";

        public Form1()
        {
            InitializeComponent();
            SetupDataBinding();
        }

        private void SetupDataBinding()
        {
            bindingSource.DataSource = productList;
            dgvProducts.DataSource = bindingSource;

            // Cấu hình cột thủ công
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên SP" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Danh Mục" });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá", DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số Lượng" });
        }

        // Hàm Validation
        private bool ValidateInput()
        {
            errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider1.SetError(txtProductName, "Tên SP không được để trống!");
                isValid = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal price) || price <= 0)
            {
                errorProvider1.SetError(txtUnitPrice, "Đơn giá phải > 0!");
                isValid = false;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty < 0)
            {
                errorProvider1.SetError(txtQuantity, "Số lượng phải >= 0!");
                isValid = false;
            }

            return isValid;
        }

        // Nút Thêm mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Product p = new Product
            {
                ProductId = txtProductId.Text,
                ProductName = txtProductName.Text,
                Category = cboCategory.Text,
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                Quantity = int.Parse(txtQuantity.Text),
                AvatarPath = currentImagePath
            };

            productList.Add(p);
            UpdateStatus();
            ClearInputs();
        }

        // Nút Cập nhật
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            if (!ValidateInput()) return;

            Product p = (Product)dgvProducts.CurrentRow.DataBoundItem;
            p.ProductId = txtProductId.Text;
            p.ProductName = txtProductName.Text;
            p.Category = cboCategory.Text;
            p.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            p.Quantity = int.Parse(txtQuantity.Text);
            p.AvatarPath = currentImagePath;

            bindingSource.ResetBindings(false);
            UpdateStatus();
        }

        // Nút Xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                productList.Remove((Product)dgvProducts.CurrentRow.DataBoundItem);
                UpdateStatus();
                ClearInputs();
            }
        }

        // Click chọn dòng trên Grid -> Nạp ngược lên ô nhập
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Product p = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                txtProductId.Text = p.ProductId;
                txtProductName.Text = p.ProductName;
                cboCategory.Text = p.Category;
                txtUnitPrice.Text = p.UnitPrice.ToString();
                txtQuantity.Text = p.Quantity.ToString();
                currentImagePath = p.AvatarPath;
                if (!string.IsNullOrEmpty(p.AvatarPath) && File.Exists(p.AvatarPath))
                {
                    picAvatar.Image = Image.FromFile(p.AvatarPath);
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
        }

        // Nút chọn ảnh
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentImagePath = ofd.FileName;
                    picAvatar.Image = Image.FromFile(currentImagePath);
                }
            }
        }

        // Live Search
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            var filtered = productList.Where(p => p.ProductName.ToLower().Contains(keyword)).ToList();
            bindingSource.DataSource = new BindingList<Product>(filtered);
        }

        // Xuất CSV
        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV Files|*.csv" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("Mã SP,Tên SP,Danh Mục,Đơn Giá,Số Lượng");
                        foreach (var p in productList)
                        {
                            sw.WriteLine($"{p.ProductId},{p.ProductName},{p.Category},{p.UnitPrice},{p.Quantity}");
                        }
                    }
                    MessageBox.Show("Xuất file thành công!");
                }
            }
        }

        // Các hàm hỗ trợ
        private void UpdateStatus()
        {
            lblTotal.Text = $"Tổng số sản phẩm: {productList.Count}";
        }

        private void ClearInputs()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtUnitPrice.Text = "";
            txtQuantity.Text = "";
            cboCategory.SelectedIndex = -1;
            currentImagePath = "";
            picAvatar.Image = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}