namespace WizIns
{
    partial class Frm_tins_Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_tins_Main));
            this.cmdWork = new System.Windows.Forms.Button();
            this.cmdNoWork = new System.Windows.Forms.Button();
            this.cmdWorkQ = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdTinsInspectAutoQ = new System.Windows.Forms.Button();
            this.cmdTprcCardRePrint = new System.Windows.Forms.Button();
            this.cmdTinsInspectAuto = new System.Windows.Forms.Button();
            this.cmdDailyCheck_Q = new System.Windows.Forms.Button();
            this.cmdDailyCheck = new System.Windows.Forms.Button();
            this.cmdSetting = new System.Windows.Forms.Button();
            this.cmdMissWorkQ = new System.Windows.Forms.Button();
            this.cmdMoldQ = new System.Windows.Forms.Button();
            this.cmdOrderDetailQ = new System.Windows.Forms.Button();
            this.cmdMtrLotStock = new System.Windows.Forms.Button();
            this.cmdDefectQ = new System.Windows.Forms.Button();
            this.cmdInfo = new System.Windows.Forms.Button();
            this.cmdGP = new System.Windows.Forms.Button();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.stbLookUp = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbMachine = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbTeam = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbPerson = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbMold = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbNowTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1.SuspendLayout();
            this.statusInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdWork
            // 
            this.cmdWork.BackColor = System.Drawing.Color.LightPink;
            this.cmdWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWork.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdWork.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdWork.Image = ((System.Drawing.Image)(resources.GetObject("cmdWork.Image")));
            this.cmdWork.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWork.Location = new System.Drawing.Point(212, 4);
            this.cmdWork.Name = "cmdWork";
            this.cmdWork.Size = new System.Drawing.Size(63, 84);
            this.cmdWork.TabIndex = 9;
            this.cmdWork.Text = "공정작업";
            this.cmdWork.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWork.UseVisualStyleBackColor = false;
            this.cmdWork.Click += new System.EventHandler(this.cmdWork_Click);
            // 
            // cmdNoWork
            // 
            this.cmdNoWork.BackColor = System.Drawing.Color.LightPink;
            this.cmdNoWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdNoWork.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdNoWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdNoWork.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdNoWork.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdNoWork.Location = new System.Drawing.Point(276, 4);
            this.cmdNoWork.Name = "cmdNoWork";
            this.cmdNoWork.Size = new System.Drawing.Size(63, 84);
            this.cmdNoWork.TabIndex = 8;
            this.cmdNoWork.Text = "무작업\r\n입   력";
            this.cmdNoWork.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdNoWork.UseVisualStyleBackColor = false;
            this.cmdNoWork.Click += new System.EventHandler(this.cmdNoWork_Click);
            // 
            // cmdWorkQ
            // 
            this.cmdWorkQ.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdWorkQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWorkQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdWorkQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdWorkQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdWorkQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdWorkQ.Image")));
            this.cmdWorkQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWorkQ.Location = new System.Drawing.Point(407, 4);
            this.cmdWorkQ.Name = "cmdWorkQ";
            this.cmdWorkQ.Size = new System.Drawing.Size(63, 84);
            this.cmdWorkQ.TabIndex = 7;
            this.cmdWorkQ.Text = "생산실적\r\n조      회";
            this.cmdWorkQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWorkQ.UseVisualStyleBackColor = false;
            this.cmdWorkQ.Click += new System.EventHandler(this.cmdWorkQ_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdTinsInspectAutoQ);
            this.panel1.Controls.Add(this.cmdTprcCardRePrint);
            this.panel1.Controls.Add(this.cmdTinsInspectAuto);
            this.panel1.Controls.Add(this.cmdDailyCheck_Q);
            this.panel1.Controls.Add(this.cmdDailyCheck);
            this.panel1.Controls.Add(this.cmdSetting);
            this.panel1.Controls.Add(this.cmdWork);
            this.panel1.Controls.Add(this.cmdNoWork);
            this.panel1.Controls.Add(this.cmdWorkQ);
            this.panel1.Controls.Add(this.cmdMissWorkQ);
            this.panel1.Controls.Add(this.cmdMoldQ);
            this.panel1.Controls.Add(this.cmdOrderDetailQ);
            this.panel1.Controls.Add(this.cmdMtrLotStock);
            this.panel1.Controls.Add(this.cmdDefectQ);
            this.panel1.Controls.Add(this.cmdInfo);
            this.panel1.Controls.Add(this.cmdGP);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 94);
            this.panel1.TabIndex = 4;
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.Silver;
            this.cmdExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdExit.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExit.Location = new System.Drawing.Point(926, 4);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(63, 84);
            this.cmdExit.TabIndex = 3;
            this.cmdExit.Text = "작업종료";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdTinsInspectAutoQ
            // 
            this.cmdTinsInspectAutoQ.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdTinsInspectAutoQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdTinsInspectAutoQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdTinsInspectAutoQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdTinsInspectAutoQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdTinsInspectAutoQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdTinsInspectAutoQ.Image")));
            this.cmdTinsInspectAutoQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTinsInspectAutoQ.Location = new System.Drawing.Point(471, 4);
            this.cmdTinsInspectAutoQ.Name = "cmdTinsInspectAutoQ";
            this.cmdTinsInspectAutoQ.Size = new System.Drawing.Size(63, 84);
            this.cmdTinsInspectAutoQ.TabIndex = 14;
            this.cmdTinsInspectAutoQ.Text = "자주검사\r\n실적조회";
            this.cmdTinsInspectAutoQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTinsInspectAutoQ.UseVisualStyleBackColor = false;
            this.cmdTinsInspectAutoQ.Click += new System.EventHandler(this.cmdTinsInspectAutoQ_Click);
            // 
            // cmdTprcCardRePrint
            // 
            this.cmdTprcCardRePrint.BackColor = System.Drawing.Color.LightPink;
            this.cmdTprcCardRePrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdTprcCardRePrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdTprcCardRePrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdTprcCardRePrint.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdTprcCardRePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTprcCardRePrint.Location = new System.Drawing.Point(340, 4);
            this.cmdTprcCardRePrint.Name = "cmdTprcCardRePrint";
            this.cmdTprcCardRePrint.Size = new System.Drawing.Size(63, 84);
            this.cmdTprcCardRePrint.TabIndex = 13;
            this.cmdTprcCardRePrint.Text = "이동전표\r\n재  발 행";
            this.cmdTprcCardRePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTprcCardRePrint.UseVisualStyleBackColor = false;
            this.cmdTprcCardRePrint.Click += new System.EventHandler(this.cmdTprcCardRePrint_Click);
            // 
            // cmdTinsInspectAuto
            // 
            this.cmdTinsInspectAuto.BackColor = System.Drawing.Color.LightPink;
            this.cmdTinsInspectAuto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdTinsInspectAuto.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdTinsInspectAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdTinsInspectAuto.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdTinsInspectAuto.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTinsInspectAuto.Location = new System.Drawing.Point(148, 4);
            this.cmdTinsInspectAuto.Name = "cmdTinsInspectAuto";
            this.cmdTinsInspectAuto.Size = new System.Drawing.Size(63, 84);
            this.cmdTinsInspectAuto.TabIndex = 12;
            this.cmdTinsInspectAuto.Text = "자주검사";
            this.cmdTinsInspectAuto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTinsInspectAuto.UseVisualStyleBackColor = false;
            this.cmdTinsInspectAuto.Click += new System.EventHandler(this.cmdTinsInspectAuto_Click);
            // 
            // cmdDailyCheck_Q
            // 
            this.cmdDailyCheck_Q.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdDailyCheck_Q.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdDailyCheck_Q.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdDailyCheck_Q.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdDailyCheck_Q.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdDailyCheck_Q.Image = ((System.Drawing.Image)(resources.GetObject("cmdDailyCheck_Q.Image")));
            this.cmdDailyCheck_Q.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDailyCheck_Q.Location = new System.Drawing.Point(599, 4);
            this.cmdDailyCheck_Q.Name = "cmdDailyCheck_Q";
            this.cmdDailyCheck_Q.Size = new System.Drawing.Size(63, 84);
            this.cmdDailyCheck_Q.TabIndex = 11;
            this.cmdDailyCheck_Q.Text = "설비점검조회";
            this.cmdDailyCheck_Q.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDailyCheck_Q.UseVisualStyleBackColor = false;
            this.cmdDailyCheck_Q.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdDailyCheck
            // 
            this.cmdDailyCheck.BackColor = System.Drawing.Color.LightPink;
            this.cmdDailyCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdDailyCheck.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdDailyCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdDailyCheck.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdDailyCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDailyCheck.Location = new System.Drawing.Point(84, 4);
            this.cmdDailyCheck.Name = "cmdDailyCheck";
            this.cmdDailyCheck.Size = new System.Drawing.Size(63, 84);
            this.cmdDailyCheck.TabIndex = 11;
            this.cmdDailyCheck.Text = "설비점검";
            this.cmdDailyCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDailyCheck.UseVisualStyleBackColor = false;
            this.cmdDailyCheck.Click += new System.EventHandler(this.cmdDailyCheck_Click);
            // 
            // cmdSetting
            // 
            this.cmdSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSetting.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.cmdSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSetting.Location = new System.Drawing.Point(987, 5);
            this.cmdSetting.Name = "cmdSetting";
            this.cmdSetting.Size = new System.Drawing.Size(20, 81);
            this.cmdSetting.TabIndex = 10;
            this.cmdSetting.UseVisualStyleBackColor = true;
            this.cmdSetting.Click += new System.EventHandler(this.cmdSetting_Click);
            // 
            // cmdMissWorkQ
            // 
            this.cmdMissWorkQ.BackColor = System.Drawing.Color.LightSkyBlue;
            this.cmdMissWorkQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdMissWorkQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdMissWorkQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdMissWorkQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdMissWorkQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdMissWorkQ.Image")));
            this.cmdMissWorkQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdMissWorkQ.Location = new System.Drawing.Point(535, 4);
            this.cmdMissWorkQ.Name = "cmdMissWorkQ";
            this.cmdMissWorkQ.Size = new System.Drawing.Size(63, 84);
            this.cmdMissWorkQ.TabIndex = 6;
            this.cmdMissWorkQ.Text = "생산\r\n미달성\r\n지   시";
            this.cmdMissWorkQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdMissWorkQ.UseVisualStyleBackColor = false;
            this.cmdMissWorkQ.Click += new System.EventHandler(this.cmdMissWorkQ_Click);
            // 
            // cmdMoldQ
            // 
            this.cmdMoldQ.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdMoldQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdMoldQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdMoldQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdMoldQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdMoldQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoldQ.Image")));
            this.cmdMoldQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdMoldQ.Location = new System.Drawing.Point(794, 4);
            this.cmdMoldQ.Name = "cmdMoldQ";
            this.cmdMoldQ.Size = new System.Drawing.Size(63, 84);
            this.cmdMoldQ.TabIndex = 5;
            this.cmdMoldQ.Text = "금형정보";
            this.cmdMoldQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdMoldQ.UseVisualStyleBackColor = false;
            this.cmdMoldQ.Click += new System.EventHandler(this.cmdMoldQ_Click);
            // 
            // cmdOrderDetailQ
            // 
            this.cmdOrderDetailQ.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdOrderDetailQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdOrderDetailQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdOrderDetailQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdOrderDetailQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdOrderDetailQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdOrderDetailQ.Image")));
            this.cmdOrderDetailQ.Location = new System.Drawing.Point(858, 4);
            this.cmdOrderDetailQ.Name = "cmdOrderDetailQ";
            this.cmdOrderDetailQ.Size = new System.Drawing.Size(63, 84);
            this.cmdOrderDetailQ.TabIndex = 4;
            this.cmdOrderDetailQ.Text = "오더상세";
            this.cmdOrderDetailQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdOrderDetailQ.UseVisualStyleBackColor = false;
            this.cmdOrderDetailQ.Click += new System.EventHandler(this.cmdOrderDetailQ_Click);
            // 
            // cmdMtrLotStock
            // 
            this.cmdMtrLotStock.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdMtrLotStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdMtrLotStock.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdMtrLotStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdMtrLotStock.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdMtrLotStock.Image = ((System.Drawing.Image)(resources.GetObject("cmdMtrLotStock.Image")));
            this.cmdMtrLotStock.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdMtrLotStock.Location = new System.Drawing.Point(730, 4);
            this.cmdMtrLotStock.Name = "cmdMtrLotStock";
            this.cmdMtrLotStock.Size = new System.Drawing.Size(63, 84);
            this.cmdMtrLotStock.TabIndex = 2;
            this.cmdMtrLotStock.Text = "자      재\r\nLOT   별\r\n재고조회";
            this.cmdMtrLotStock.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdMtrLotStock.UseVisualStyleBackColor = false;
            this.cmdMtrLotStock.Click += new System.EventHandler(this.cmdMtrLotStock_Click);
            // 
            // cmdDefectQ
            // 
            this.cmdDefectQ.BackColor = System.Drawing.Color.LightSalmon;
            this.cmdDefectQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdDefectQ.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdDefectQ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdDefectQ.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdDefectQ.Image = ((System.Drawing.Image)(resources.GetObject("cmdDefectQ.Image")));
            this.cmdDefectQ.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDefectQ.Location = new System.Drawing.Point(666, 4);
            this.cmdDefectQ.Name = "cmdDefectQ";
            this.cmdDefectQ.Size = new System.Drawing.Size(63, 84);
            this.cmdDefectQ.TabIndex = 1;
            this.cmdDefectQ.Text = "불량이력";
            this.cmdDefectQ.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDefectQ.UseVisualStyleBackColor = false;
            this.cmdDefectQ.Click += new System.EventHandler(this.cmdDefectQ_Click);
            // 
            // cmdInfo
            // 
            this.cmdInfo.BackColor = System.Drawing.Color.LightGray;
            this.cmdInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdInfo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdInfo.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdInfo.Image = ((System.Drawing.Image)(resources.GetObject("cmdInfo.Image")));
            this.cmdInfo.Location = new System.Drawing.Point(16, 4);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Size = new System.Drawing.Size(63, 84);
            this.cmdInfo.TabIndex = 0;
            this.cmdInfo.Text = "공지사항";
            this.cmdInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdInfo.UseVisualStyleBackColor = false;
            this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
            this.cmdInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmdInfo_Click);
            // 
            // cmdGP
            // 
            this.cmdGP.BackColor = System.Drawing.Color.LightPink;
            this.cmdGP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdGP.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmdGP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdGP.Font = new System.Drawing.Font("맑은 고딕", 12.7F, System.Drawing.FontStyle.Bold);
            this.cmdGP.Image = ((System.Drawing.Image)(resources.GetObject("cmdGP.Image")));
            this.cmdGP.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdGP.Location = new System.Drawing.Point(820, 68);
            this.cmdGP.Name = "cmdGP";
            this.cmdGP.Size = new System.Drawing.Size(117, 18);
            this.cmdGP.TabIndex = 10;
            this.cmdGP.Text = "GP 생산";
            this.cmdGP.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdGP.UseVisualStyleBackColor = false;
            this.cmdGP.Visible = false;
            this.cmdGP.Click += new System.EventHandler(this.cmdGP_Click);
            // 
            // statusInfo
            // 
            this.statusInfo.AutoSize = false;
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbLookUp,
            this.stbMachine,
            this.stbTeam,
            this.stbPerson,
            this.stbMold,
            this.stbNowTime});
            this.statusInfo.Location = new System.Drawing.Point(0, 707);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(998, 22);
            this.statusInfo.SizingGrip = false;
            this.statusInfo.TabIndex = 6;
            this.statusInfo.Text = "statusStrip1";
            // 
            // stbLookUp
            // 
            this.stbLookUp.AutoSize = false;
            this.stbLookUp.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbLookUp.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbLookUp.Name = "stbLookUp";
            this.stbLookUp.Size = new System.Drawing.Size(307, 17);
            this.stbLookUp.Spring = true;
            // 
            // stbMachine
            // 
            this.stbMachine.AutoSize = false;
            this.stbMachine.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbMachine.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbMachine.Name = "stbMachine";
            this.stbMachine.Size = new System.Drawing.Size(141, 17);
            // 
            // stbTeam
            // 
            this.stbTeam.AutoSize = false;
            this.stbTeam.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbTeam.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbTeam.Name = "stbTeam";
            this.stbTeam.Size = new System.Drawing.Size(141, 17);
            // 
            // stbPerson
            // 
            this.stbPerson.AutoSize = false;
            this.stbPerson.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbPerson.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbPerson.Name = "stbPerson";
            this.stbPerson.Size = new System.Drawing.Size(121, 17);
            // 
            // stbMold
            // 
            this.stbMold.AutoSize = false;
            this.stbMold.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbMold.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbMold.Name = "stbMold";
            this.stbMold.Size = new System.Drawing.Size(121, 17);
            // 
            // stbNowTime
            // 
            this.stbNowTime.AutoSize = false;
            this.stbNowTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.stbNowTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.stbNowTime.Name = "stbNowTime";
            this.stbNowTime.Size = new System.Drawing.Size(121, 17);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(998, 96);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Frm_tins_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 729);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1014, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1014, 768);
            this.Name = "Frm_tins_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "생산정보시스템";
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.panel1.ResumeLayout(false);
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button cmdWork;
        private System.Windows.Forms.Button cmdNoWork;
        private System.Windows.Forms.Button cmdWorkQ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdMissWorkQ;
        private System.Windows.Forms.Button cmdMoldQ;
        private System.Windows.Forms.Button cmdOrderDetailQ;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdMtrLotStock;
        private System.Windows.Forms.Button cmdDefectQ;
        private System.Windows.Forms.Button cmdInfo;
        private System.Windows.Forms.Button cmdSetting;
        private System.Windows.Forms.Button cmdGP;
        public System.Windows.Forms.StatusStrip statusInfo;
        public System.Windows.Forms.ToolStripStatusLabel stbLookUp;
        public System.Windows.Forms.ToolStripStatusLabel stbMachine;
        public System.Windows.Forms.ToolStripStatusLabel stbTeam;
        public System.Windows.Forms.ToolStripStatusLabel stbPerson;
        public System.Windows.Forms.ToolStripStatusLabel stbMold;
        public System.Windows.Forms.ToolStripStatusLabel stbNowTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdDailyCheck_Q;
        private System.Windows.Forms.Button cmdDailyCheck;
        private System.Windows.Forms.Button cmdTinsInspectAuto;
        private System.Windows.Forms.Button cmdTprcCardRePrint;
        private System.Windows.Forms.Button cmdTinsInspectAutoQ;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}



