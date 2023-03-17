namespace WizIns
{
    partial class FrmSetting
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            this.dgvLookupResult = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSQL_0 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdRowDown = new System.Windows.Forms.Button();
            this.cmdRowUp = new System.Windows.Forms.Button();
            this.cmdSQL_1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLookupResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLookupResult
            // 
            this.dgvLookupResult.AllowUserToAddRows = false;
            this.dgvLookupResult.AllowUserToDeleteRows = false;
            this.dgvLookupResult.AllowUserToResizeRows = false;
            this.dgvLookupResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLookupResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLookupResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLookupResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLookupResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLookupResult.Location = new System.Drawing.Point(7, 90);
            this.dgvLookupResult.MultiSelect = false;
            this.dgvLookupResult.Name = "dgvLookupResult";
            this.dgvLookupResult.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dgvLookupResult.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLookupResult.RowTemplate.Height = 23;
            this.dgvLookupResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLookupResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLookupResult.Size = new System.Drawing.Size(416, 334);
            this.dgvLookupResult.TabIndex = 144;
            this.dgvLookupResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLookupResult_CellClick);
            // 
            // Check
            // 
            this.Check.FillWeight = 166.4975F;
            this.Check.HeaderText = "선택";
            this.Check.Name = "Check";
            this.Check.Width = 80;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 77.83418F;
            this.Column2.HeaderText = "번호";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 111;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 77.83418F;
            this.Column3.HeaderText = "공정코드";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 77.83418F;
            this.Column4.HeaderText = "공정명";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 111;
            // 
            // cmdSQL_0
            // 
            this.cmdSQL_0.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdSQL_0.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSQL_0.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSQL_0.Location = new System.Drawing.Point(7, 8);
            this.cmdSQL_0.Name = "cmdSQL_0";
            this.cmdSQL_0.Size = new System.Drawing.Size(109, 35);
            this.cmdSQL_0.TabIndex = 145;
            this.cmdSQL_0.Text = "Server";
            this.cmdSQL_0.UseVisualStyleBackColor = true;
            this.cmdSQL_0.Click += new System.EventHandler(this.cmdSQL_0_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(429, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 63);
            this.button2.TabIndex = 162;
            this.button2.Text = "선택  ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdRowDown
            // 
            this.cmdRowDown.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdRowDown.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdRowDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdRowDown.Image")));
            this.cmdRowDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowDown.Location = new System.Drawing.Point(429, 159);
            this.cmdRowDown.Name = "cmdRowDown";
            this.cmdRowDown.Size = new System.Drawing.Size(105, 63);
            this.cmdRowDown.TabIndex = 160;
            this.cmdRowDown.Text = "아래";
            this.cmdRowDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowDown.UseVisualStyleBackColor = true;
            this.cmdRowDown.Click += new System.EventHandler(this.cmdRowDown_Click);
            // 
            // cmdRowUp
            // 
            this.cmdRowUp.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdRowUp.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.cmdRowUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdRowUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdRowUp.Image")));
            this.cmdRowUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRowUp.Location = new System.Drawing.Point(429, 90);
            this.cmdRowUp.Name = "cmdRowUp";
            this.cmdRowUp.Size = new System.Drawing.Size(105, 63);
            this.cmdRowUp.TabIndex = 161;
            this.cmdRowUp.Text = "위  ";
            this.cmdRowUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRowUp.UseVisualStyleBackColor = true;
            this.cmdRowUp.Click += new System.EventHandler(this.cmdRowUp_Click_1);
            // 
            // cmdSQL_1
            // 
            this.cmdSQL_1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdSQL_1.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.cmdSQL_1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSQL_1.Location = new System.Drawing.Point(7, 49);
            this.cmdSQL_1.Name = "cmdSQL_1";
            this.cmdSQL_1.Size = new System.Drawing.Size(109, 35);
            this.cmdSQL_1.TabIndex = 164;
            this.cmdSQL_1.Text = "Database";
            this.cmdSQL_1.UseVisualStyleBackColor = true;
            this.cmdSQL_1.Click += new System.EventHandler(this.cmdSQL_1_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.LightSalmon;
            this.button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button6.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(429, 360);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 63);
            this.button6.TabIndex = 166;
            this.button6.Text = "닫기  ";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.txtServer.Location = new System.Drawing.Point(122, 10);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(408, 34);
            this.txtServer.TabIndex = 167;
            // 
            // txtDB
            // 
            this.txtDB.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.txtDB.Location = new System.Drawing.Point(122, 50);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(408, 34);
            this.txtDB.TabIndex = 168;
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(544, 430);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.cmdSQL_1);
            this.Controls.Add(this.dgvLookupResult);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmdSQL_0);
            this.Controls.Add(this.cmdRowDown);
            this.Controls.Add(this.cmdRowUp);
            this.Name = "FrmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "공정 및 호기 설정";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLookupResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLookupResult;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button cmdSQL_0;
        
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdRowDown;
        private System.Windows.Forms.Button cmdRowUp;
        private System.Windows.Forms.Button cmdSQL_1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;

    }
}

