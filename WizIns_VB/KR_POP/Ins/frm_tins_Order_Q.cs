using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using WizIns.Common.ControlEX;
using WizIns.Properties;
using WizCommon;
using WizIns.Tools;

namespace WizIns
{
    public partial class frm_tins_Order_Q : Form
    {
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();//콤보박스 리스트
        WizWorkLib Lib = new WizWorkLib();//통합라이브러리
        DataTable dteBox = new DataTable();//grdData 합계 dt
        DataTable dteLot = new DataTable();//grdLotNo 합계 dt
        int m_sSearchIndex;
        string m_TextName;

        bool IsOK = false;//GotFocus때문에 필요

        /// <summary>
        /// 생성
        /// </summary>
        public frm_tins_Order_Q()
        {
            InitializeComponent();
        }

        private void SetTableLayout()
        {
            tlp_Main.SetColumnSpan(tlpTop, 2);
            tlp_Main.SetRowSpan(tlpRight, 2);
            tlp_Main.SetRowSpan(grdOrder, 2);
            tlp_Main.Dock = DockStyle.Fill;
            
            foreach (Control con in tlp_Main.Controls)//tlpTop,tlpRight
            {
                con.Dock = DockStyle.Fill;
                con.Margin = new Padding(0, 0, 0, 0);
                foreach (Control ctl in con.Controls)//tab(탭컨트롤),tlpDate
                {
                    ctl.Dock = DockStyle.Fill;
                    ctl.Margin = new Padding(0, 0, 0, 0);
                    if (ctl is TabControl)
                    {
                        TabControl tab = ctl as TabControl;
                        foreach (TabPage tp in tab.TabPages)
                        {
                            foreach(Control co in tp.Controls)//tlpMON, tlpCustom
                            {
                                co.Dock = DockStyle.Fill;
                                co.Margin = new Padding(0, 0, 0, 0);
                                foreach (Control coc in co.Controls)//Panel
                                {
                                    coc.Margin = new Padding(0, 0, 0, 0);
                                    coc.Dock = DockStyle.Fill;
                                    if (coc is RadioButton)
                                    {
                                        coc.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
                                    }
                                    foreach (Control cocc in coc.Controls)//TextBox
                                    {
                                        cocc.Dock = DockStyle.Fill;
                                    }
                                }
                            }
                        }
                    }
                    foreach (Control ct in ctl.Controls)//tlpMON,
                    {
                        ct.Dock = DockStyle.Fill;
                        ct.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control cl in ct.Controls)//
                        {
                            cl.Dock = DockStyle.Fill;
                            cl.Margin = new Padding(0, 0, 0, 0);

                        }
                    }
                }
            }

            //tlpMON
        }

        //#region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        //private void SetScreen()
        //{
        //    tlp_Main.Dock = DockStyle.Fill;
        //    foreach (Control con in tlp_Main.Controls)//con = tlp 상위에서 2번째
        //    {
        //        con.Dock = DockStyle.Fill;
        //        if (con is Panel)
        //        {
        //            Panel pnl = con as Panel;
        //            pnl.BorderStyle = BorderStyle.FixedSingle;
        //        }
        //        foreach (Control ctl in con.Controls)//tlp 상위에서 3번째
        //        {
        //            ctl.Dock = DockStyle.Fill;
        //            foreach (Control ct in ctl.Controls)
        //            {
        //                ct.Dock = DockStyle.Fill;
        //            }
        //        }
        //    }

        //    //Main tlp 세팅
        //    tlp_Search_Date.SetRowSpan(chkInsDate, 2);
        //    tlp_Main.SetColumnSpan(tabdgv, 2);
        //    //TabControl 셋팅
        //    tabdgv.Appearance = TabAppearance.Normal;//바꾸지말것. Normal이 아니면 그리드뷰 안나오는 현상 생김
        //    tabdgv.Alignment = TabAlignment.Bottom;
        //    tabdgv.Dock = DockStyle.Fill;
        //    tabdgv.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
        //    tabdgv.TabPages["BOX"].Text = "BOX별 검사실적";
        //    tabdgv.TabPages["LOT"].Text = "LOT별 검사실적";
        //    foreach (TabPage tp in tabdgv.TabPages)
        //    {
        //        tp.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
        //        foreach (Control con in tp.Controls)
        //        {
        //            con.Dock = DockStyle.Fill;
        //            foreach (Control ctl in con.Controls)
        //            {
        //                ctl.Dock = DockStyle.Fill;
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region 그리드뷰 컬럼 셋팅
        private void InitGrid()
        {
            ///BOX별 검사실적 grdData///
            grdOrder.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdOrder.ColumnCount = 19;

            int i = 0;

            grdOrder.Columns[i].Name = "Idx";
            grdOrder.Columns[i].HeaderText = "";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "OrderID";
            grdOrder.Columns[i].HeaderText = "관리번호";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "OrderNO";
            grdOrder.Columns[i].HeaderText = "수주번호";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "LabelID";
            grdOrder.Columns[i].HeaderText = "박스번호";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "CustomID";
            grdOrder.Columns[i].HeaderText = "거래처코드";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "KCustom";
            grdOrder.Columns[i].HeaderText = "거래처";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "ArticleID";
            grdOrder.Columns[i].HeaderText = "품명코드";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "Article";
            grdOrder.Columns[i].HeaderText = "품명";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "BuyerArticleNo";
            grdOrder.Columns[i].HeaderText = "품번";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "BuyerModelID";
            grdOrder.Columns[i].HeaderText = "차종코드";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "Model";
            grdOrder.Columns[i].HeaderText = "차종";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "OrderQty";
            grdOrder.Columns[i].HeaderText = "지시수량";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            //grdOrder.Columns[++i].Name = "CardID";
            //grdOrder.Columns[i].HeaderText = "공정이동전표번호";
            //grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdOrder.Columns[i].ReadOnly = true;
            //grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "WorkQty";
            grdOrder.Columns[i].HeaderText = "생산량";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            //grdOrder.Columns[++i].Name = "TransferQty";
            //grdOrder.Columns[i].HeaderText = "장입량이동수";
            //grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdOrder.Columns[i].ReadOnly = true;
            //grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "ExpectDate";
            grdOrder.Columns[i].HeaderText = "생산시작일";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = true;

            grdOrder.Columns[++i].Name = "InstID";
            grdOrder.Columns[i].HeaderText = "InstID";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;

            grdOrder.Columns[++i].Name = "LabelGubun";
            grdOrder.Columns[i].HeaderText = "LabelGubun";
            grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdOrder.Columns[i].ReadOnly = true;
            grdOrder.Columns[i].Visible = false;


            //grdOrder.Columns[++i].Name = "LotID";
            //grdOrder.Columns[i].HeaderText = "LotNo";
            //grdOrder.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdOrder.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdOrder.Columns[i].ReadOnly = true;
            //grdOrder.Columns[i].Visible = true;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            chkCol.HeaderText = "C";
            chkCol.Name = "Chk";
            chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            chkCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdOrder.Columns.Insert(1, chkCol);


            grdOrder.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdOrder.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdOrder.RowTemplate.Height = 30;
            grdOrder.ColumnHeadersHeight = 35;
            grdOrder.ScrollBars = ScrollBars.Both;
            grdOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdOrder.MultiSelect = false;
            grdOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdOrder.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdOrder.MultiSelect = true;
            grdOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            

            foreach (DataGridViewColumn col in grdOrder.Columns)
            {
                col.DataPropertyName = col.Name;
            }


        }
        #endregion

        private void frm_tins_LabelPrint_U_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1004, 620); // 화면 사이즈 조정
            InitGrid();
            SetTableLayout();
            mtbDate.Text = DateTime.Today.ToString("yyMMdd");
            mtbTime.Text = DateTime.Now.ToString("HHmmss");
            cbxName.Items.Add("박스번호");
            cbxName.Items.Add("관리번호");
            cbxName.Items.Add("수주번호");
            cbxName.Items.Add("거래처");
            Lib.SetComboBox(cbxName, null, null);
            float sz = cbxName.Font.Size;
            sz -= 2;// cbxName.Font.Size;
            txtName.Font = new Font(cbxName.Font.Name, sz, cbxName.Font.Style);

            if (cbxName.Items.Count > 0)
            {
                cbxName.SelectedIndex = 0;//박스번호 조회 클릭
            }
            
        }

        private void SearchData()
        {
            if (cbxName.Items.Count == 0)
            {
                return;
            }
            if (cbxName.SelectedItem is null)
            {
                return;
            }

            int i = cbxName.SelectedIndex;

            switch (i)
            {
                case 0://박스번호
                    SearchBoxNo();
                    break;
                case 1://관리번호
                    SearchOrderID();
                    break;
                case 2://수주번호
                    SearchOrderNo();
                    break;
                case 3://거래처
                    SearchCustom();
                    break;
            }
            void SearchOrderID()
            {
                WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad("", "관리번호");
                WizCommon.Popup.Frm_CMNumericKeypad.KeypadStr = txtName.Text.Trim();
                m_sSearchIndex = 1;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    if (FK.tbInputText.Text.Trim().Length == 10)
                    {
                        txtName.Text = FK.tbInputText.Text.Replace("-", "");
                        txtName.Text = FK.tbInputText.Text;
                        txtName.SelectionStart = txtName.Text.Length;

                        FillGrid(1, 1, txtName.Text);
                    }
                    else
                    {
                        string Text = "잘못된 관리번호입니다. - '" + FK.tbInputText.Text + "'";
                        WizCommon.Popup.MyMessageBox.ShowBox(Text, "[확인]", 0, 0);
                        txtName.Text = "";
                    }
                }
            }
            void SearchOrderNo()
            {
                WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtName.Text.Trim(), "수주번호");
                m_sSearchIndex = 2;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    if (FK.tbInputText.Text.Trim().Length > 0 )
                    {
                        txtName.Text = FK.tbInputText.Text;
                        txtName.SelectionStart = txtName.Text.Length;

                        FillGrid(2, 1, txtName.Text);
                    }
                    else
                    {
                        string Text = "잘못된 수주번호입니다. - '" + FK.tbInputText.Text + "'";
                        WizCommon.Popup.MyMessageBox.ShowBox(Text, "[확인]", 0, 0);
                        txtName.Text = "";
                    }
                }
            }
            void SearchBoxNo()
            {
                WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtName.Text.Trim(), "박스번호");
                m_sSearchIndex = 4;
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    if (FK.tbInputText.Text.Trim().Length == 10 && FK.tbInputText.Text.Trim().Substring(0, 1).ToLower() == "b")
                    {
                        txtName.Text = FK.tbInputText.Text.Replace("-", "");
                        txtName.Text = FK.tbInputText.Text;
                        txtName.SelectionStart = txtName.Text.Length;

                        FillGrid(4, 1, txtName.Text);
                    }
                    else
                    {
                        string Text = "잘못된 박스번호입니다. - 박스번호 길이 10자리 아님'" + FK.tbInputText.Text + "' ";
                        WizCommon.Popup.MyMessageBox.ShowBox(Text, "[확인]", 0, 0);
                        txtName.Text = "";
                    }
                }
            }
            void SearchCustom()
            {
                m_sSearchIndex = 3;
            }
        }

        private void FillGrid(int idx, int nChkOrder = 0, string sOrder = "")
        {
            int nChkCustom = 0;
            string sFirst = "";
            string sSecond = "";
            DataTable dt = null;
            //거래처 검색
            if (nChkOrder == 0)
            {
                foreach (Control con in tlpCustom.Controls)
                {
                    if (idx == Lib.OnlyNumber(con.Name))
                    {
                        sFirst = con.Text;
                        break;
                    }
                }
                if (idx == 15)
                {
                    sFirst = "가";
                    sSecond = "힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁";
                }
                else if (idx == 14)
                {
                    sSecond = "힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁힁";
                }
                else
                {
                    foreach (Control con in tlpCustom.Controls)
                    {
                        if (idx + 1 == Lib.OnlyNumber(con.Name))
                        {
                            sSecond = con.Text;
                            break;
                        }
                    }
                }
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@ChkCustom", nChkCustom);
                sqlParameter.Add("@StartName", sFirst);
                sqlParameter.Add("@EndName", sSecond);
                sqlParameter.Add("@ChkOrder", nChkOrder);
                sqlParameter.Add("@Order", sOrder);

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_swkResultCustom", sqlParameter, false);


            }
            else
            {
                string LabelID = "";
                string OrderID = "";
                string OrderNo = "";
                if (idx == 4)
                {
                    LabelID = sOrder;
                }
                else if (idx == 1)
                {
                    OrderID = sOrder.Replace("-","");
                }
                else if (idx == 2)
                {
                    OrderNo = sOrder;
                }

                if (OrderID == "" && OrderNo == "" && LabelID == "")
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("@OrderID", 1);
                    sqlParameter.Add("@OrderNo", 1);
                    sqlParameter.Add("@LabelID", 1);

                    dt = WizCommon.DataStore.Instance.ProcedureToDataTable("xp_WizIns_swkResult", sqlParameter, false);
                }
                else
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("@OrderID", OrderID);
                    sqlParameter.Add("@OrderNo", OrderNo);
                    sqlParameter.Add("@LabelID", LabelID);

                    dt = WizCommon.DataStore.Instance.ProcedureToDataTable("xp_WizIns_swkResult", sqlParameter, false);
                }
            }

            if (idx == 4)
            {
                if (sOrder == "")
                {
                    grdOrder.Rows.Clear();
                    //InitGrid();
                }
                else if (sOrder != "" && idx == 4)
                {
                    
                }
            }
            if (dt is null || dt.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("이미 검사완료 하였거나, 존재하지 않는 관리번호, 오더번호 입니다.", "[확인]", 0, 0);
            }

            grdOrder.Rows.Clear();
            int i = 0;
            int WorkQty = 0;
            int OrderQty = 0;
            foreach (DataRow dr in dt.Rows)
            {

                OrderQty = int.Parse(dr["OrderQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                WorkQty = int.Parse(dr["WorkQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);

                grdOrder.Rows.Add(++i,                                //'0)Row 수
                                false,                                //'1)Check
                                dr["OrderID"].ToString(),             //'2)관리번호 
                                dr["OrderNo"].ToString(),             //'3)Order NO 
                                dr["LabelID"].ToString(),             //'4)박스번호 
                                dr["CustomID"].ToString(),            //'5)거래처
                                dr["KCUSTOM"].ToString(),             //'6)거래처명 
                                dr["ArticleID"].ToString(),           //'7)품명코드 
                                dr["Article"].ToString(),             //'8)품명 
                                dr["BuyerArticleNo"].ToString(),      //'9)품번 
                                dr["BuyerModelID"].ToString(),        //'10)차종코드 
                                dr["Model"].ToString(),               //'11)차종 
                                string.Format("{0:n0}", OrderQty),    //'12)지시수량 
                                string.Format("{0:n0}", WorkQty),     //'13)박스당수량 
                                dr["ExpectDate"].ToString(),          //'15)작업시작일
                                dr["InstID"].ToString(),              //'16)InstID
                                dr["LabelGubun"].ToString()           //'17)LabelGubun
                                );
            }
            if (idx == 4)
            {
                txtName.Focus();
            }











            if (cbxName.SelectedText == "오더번호")
            {

            }
            else if (cbxName.SelectedText == "박스번호")
            {

            }
            else if (cbxName.SelectedText == "수주번호")
            {

            }
            else if (cbxName.SelectedText == "거래처")
            {
                
                //int nChkOrder = 0;
                //string sOrder = "";

                //foreach (Control con in tlpCustom.Controls)
                //{
                //    if (con is RadioButton)
                //    {
                //        RadioButton rbn = con as RadioButton;
                //        if (rbn.Checked)
                //        {

                //        }
                //        con.Name
                //    }
                //}    
                

               

                
            }
        }
        
        private void tlp_Main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tlpCustom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbn_CheckedChanged(object sender, EventArgs e)
        {
            //sender가 체크 되있을때
            if (((RadioButton)(sender)).Checked)
            {
                RadioButton rbn = sender as RadioButton;
                int idx = 0;
                idx = Lib.OnlyNumber(rbn.Name);
                FillGrid(idx, 0);
            }
        }

        private void cbxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //"박스번호" = 0, "오더번호" = 1, "수주번호" = 2, "거래처" = 3
            SearchData();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdOrder);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdOrder);
        }
        #region 달력 검사날짜 입력 // 달력 창 띄우기
        private void tlpDate_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtbDate.Text.Replace("-", ""), mtbDate.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Calendar.Value -> mtbDate.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            if (btnName == mtbDate.Name)
            {
                mtbDate.Text = dateTime.ToString("yyyy-MM-dd");
            }
        }
        #endregion
        private void tlpTime_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad("", "");
            WizCommon.Popup.Frm_CMNumericKeypad.KeypadStr = txtName.Text.Trim();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                mtbTime.Text = FK.tbInputText.Text;
            }
            else
            {
                mtbTime.Text = "0000";
            }
        }

        private void chkAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in grdOrder.Rows)
            {
                if ((bool)dgvr.Cells["Chk"].Value)
                {
                    dgvr.Cells["Chk"].Value = false;
                }
                else
                {
                    dgvr.Cells["Chk"].Value = true;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtName.Tag = "";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //VB INDEX 4:박스번호,1:관리번호,2: oRDER nO, 3:거래처 코드
            if (cbxName.Items.Count == 0)
            {
                return;
            }
            if (cbxName.SelectedItem is null)
            {
                return;
            }

            int i = cbxName.SelectedIndex;

            switch (i)
            {
                case 0://박스번호
                    FillGrid(1, 1, txtName.Text);
                    break;
                case 1://관리번호
                    FillGrid(2, 1, txtName.Text);
                    break;
                case 2://수주번호
                    FillGrid(4, 1, txtName.Text);
                    break;
            }
        }

        private void btnGridClear_Click(object sender, EventArgs e)
        {
            grdOrder.Rows.Clear();
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_tins_LabelRegister ins_lblreg = new WizCommon.Popup.Frm_tins_LabelRegister();
            ins_lblreg.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (grdOrder.Rows.Count == 0)
            {
                return;
            }

            if (grdOrder.SelectedRows.Count == 0 || grdOrder.SelectedRows.Count > 1)
            {
                return;
            }

            int rowIndex = grdOrder.SelectedRows[0].Index;
            DataGridViewRow dgvr = grdOrder.Rows[rowIndex];            
            frm_tins_OrderPopUp.sOrderID = dgvr.Cells["OrderID"].Value.ToString().Trim();
            frm_tins_OrderPopUp Opu = new frm_tins_OrderPopUp();
            Opu.ShowDialog();
        }

        private void btnInsStart_Click(object sender, EventArgs e)
        {
            string sOrderID = "";

            if (grdOrder.Rows.Count == 0)
            { return; }
            if (grdOrder.SelectedRows.Count == 0 || grdOrder.SelectedRows.Count > 1)
            {
                return;
            }
            DataTable dt = null;
            int rowIndex = grdOrder.SelectedRows[0].Index;
            DataGridViewRow dgvr = grdOrder.Rows[rowIndex];
            sOrderID = dgvr.Cells["OrderID"].Value.ToString().Trim();

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@OrderID", sOrderID);
            dt = DataStore.Instance.ProcedureToDataTable("xp_Order_sOrderOne", sqlParameter, false);

            if (dt.Rows.Count == 0)
            { return; }
            else if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                Frm_tins_Main.g_tBase.OrderID = dr["OrderID"].ToString();                           //'OrderID
                Frm_tins_Main.g_tBase.OrderNo = dr["OrderNo"].ToString();                           //'OrderNo
                Frm_tins_Main.g_tBase.Custom = dr["KCUSTOM"].ToString();                            //'거래처코드
                Frm_tins_Main.g_tBase.Article = dr["Article"].ToString();                                           //'품명코드
                Frm_tins_Main.g_tBase.OrderQty = dr["OrderQty"].ToString();                         //'오더량
                Frm_tins_Main.g_tBase.UnitClss = dr["UnitClss"].ToString();                         //'단위
                Frm_tins_Main.g_tBase.LabelID = dr["LabelID"].ToString();                                           //'LabelID
                Frm_tins_Main.g_tBase.WorkQty = int.Parse(dr["WorkQty"].ToString()).ToString();     //'박스당수량+장입량

                Frm_tins_Main.g_tBase.ExpectDate = dr["ExpectDate"].ToString();                     //'검사시작일
                Frm_tins_Main.g_tBase.ModelID = dr["BuyerModelID"].ToString();                      //'차종
                Frm_tins_Main.g_tBase.InstID = dr["InstID"].ToString();                             //'InstID

                Frm_tins_Main.g_tBase.BasisID = int.Parse(dr["BasisID"].ToString()).ToString();     //
                Frm_tins_Main.g_tBase.BasisUnit = dr["BasisUnit"].ToString();                       //
                Frm_tins_Main.g_tBase.BCutQty = "0";                                                 //'난단 수량 초기화  
                Frm_tins_Main.g_tBase.FCutQty = "0";                                                 //
                Frm_tins_Main.g_tBase.BSample = "0";                                                 //
                Frm_tins_Main.g_tBase.FSample = "0";                                                  //
                Frm_tins_Main.g_tBase.Loss = "0";                                                     //
                Frm_tins_Main.g_tBase.RollClss = Frm_tins_Main.g_tSet.RollClss.ToString();            //
                Frm_tins_Main.g_tBase.ExamDatePrint = "0";            //
            }
            Frm_tins_Main.g_tBase.CardID = "";
            Frm_tins_Main.g_tBase.SplitID = "";
            Frm_tins_Main.g_tBase.WorkInspect = "0";

            dt = null;
            sqlParameter = null;
            sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@OrderID", sOrderID);
            dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectQtyByExamNo", sqlParameter, false);

            if (dt.Rows.Count == 0)
            {
                Frm_tins_Main.g_tBase.ExamNoPassRoll = "0";
                Frm_tins_Main.g_tBase.ExamNoPassQty = "0";
            }
            else if(dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                Frm_tins_Main.g_tBase.ExamNoPassRoll = dr["PassRoll"].ToString();
                Frm_tins_Main.g_tBase.ExamNoPassQty = dr["PassQty"].ToString();
            }
            int PassSubQty = int.Parse(Frm_tins_Main.g_tBase.PassSubQty);
            int ColorQty = int.Parse(Frm_tins_Main.g_tBase.ColorQty);
            if (PassSubQty > ColorQty)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사수량이 색상별 수주량을 넘어섰습니다.!!!\r\n수주량을 다시한번 확인하십시오!!!"
                    , "[확인]", 0, 0);
            }
            //frm_tins_Defect_U.m_sSearchIndex =  m_sSearchIndex
            //frm_tins_Defect_U.m_TextName =      m_TextName
            
            frm_tins_Defect_U ftdu = new frm_tins_Defect_U();
            ftdu.Owner = this;
            ftdu.ShowDialog();
        }


        //#region 폼 로드 시의 작업
        //private void frm_tins_LabelPrint_U_Load(object sender, EventArgs e)
        //{
        //    InitGrid();
        //    SetScreen();
        //    SetComboBox();

        //    mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
        //    mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

        //    txtOrderID.GotFocus += txtOrderID_GotFocus;
        //    txtBoxID.GotFocus += txtBoxID_GotFocus;
        //    tabdgv_Click(null, null);
        //    chkInsDate_Click(null, null);
        //    chkMachine_Click(null, null);
        //    chkBoxID_Click(null, null);
        //    chkOrderID_Click(null, null);
        //}
        //#endregion
        //#region 콤보박스에 검사호기 셋팅
        //private void SetComboBox()
        //{
        //    try
        //    {
        //        for (int i = 0; i < 12; i++)
        //        {
        //            if (i == 0)
        //            {
        //                cboMachine.Items.Add("전체");
        //            }
        //            else
        //            {
        //                cboMachine.Items.Add(i.ToString());
        //            }
        //        }

        //        //패널크기에 따른 콤보박스 사이즈 및 폰트사이즈 변경
        //        Lib.SetComboBox(cboMachine, null, null);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message));
        //    }
        //    finally
        //    {
        //        //임시
        //        //Frm_tins_Main.g_tBase.DepartID = "";

        //        //전역변수에 Depart가 없을 시 0번째 Index 선택
        //        if (Frm_tins_Main.g_tBase.DepartID == "")
        //        {
        //            cboMachine.SelectedIndex = 0;
        //        }
        //        //전역변수에 Depart가 있을 시 해당 값을 가진 Index 선택
        //        else
        //        {
        //            cboMachine.SelectedIndex = Convert.ToInt32(Lib.FindComboBoxIdx(cboMachine, list_cbx, Frm_tins_Main.g_tBase.DepartID));
        //        }
        //    }

        //}
        //#endregion
        //#region grdData 조회 //박스별 검사실적 탭의 그리드뷰
        //private void FillGridData()
        //{
        //    try
        //    {
        //        btnName.Text = 

        //        int nChkExamDate = 0;
        //        string sExamDate = "";
        //        string eExamDate = "";
        //        int nChkExamNO = 0;
        //        string sExamNo = "";
        //        int nChkOrderID = 0;
        //        string sOrderID = "";
        //        int nChkRollID = 0;
        //        string sRollID = "";
        //        if (chkInsDate.Checked)
        //        {
        //            nChkExamDate = 1;
        //            sExamDate = mtb_From.Text.Replace("-", "");
        //            eExamDate = mtb_To.Text.Replace("-", "");
        //        }
        //        if (chkMachine.Checked)
        //        {
        //            if (cboMachine.SelectedIndex == 0)
        //            {
        //                nChkExamNO = 0;
        //                sExamNo = cboMachine.SelectedIndex.ToString();
        //            }
        //            else
        //            {
        //                nChkExamNO = 1;
        //                sExamNo = cboMachine.SelectedIndex.ToString();
        //            }
        //        }
        //        if (chkOrderID.Checked)
        //        {
        //            nChkOrderID = 1;
        //            sOrderID = txtOrderID.Text.Trim();
        //        }
        //        if (chkBoxID.Checked)
        //        {
        //            nChkRollID = 1;
        //            sRollID = txtBoxID.Text.Trim();
        //        }

        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("@ChkExamDate", nChkExamDate);
        //        sqlParameter.Add("@sExamDate", sExamDate);
        //        sqlParameter.Add("@eExamDate", eExamDate);
        //        sqlParameter.Add("@ChkExamNo", nChkExamNO);
        //        sqlParameter.Add("@ExamNo", sExamNo);
        //        sqlParameter.Add("@ChkOrderID", nChkOrderID);
        //        sqlParameter.Add("@OrderID", sOrderID);
        //        sqlParameter.Add("@ChkBoxID", nChkRollID);
        //        sqlParameter.Add("@BoxID", sRollID);

        //        DataTable dt = Common.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspect", sqlParameter, false);

        //        grdOrder.Rows.Clear();
        //        int i = 0;
        //        int CtrlQty = 0;
        //        int OrderQty = 0;
        //        int DefectQty = 0;
        //        int TotalCtrlQty = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            CtrlQty = int.Parse(dr["CtrlQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            OrderQty = int.Parse(dr["OrderQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            TotalCtrlQty = TotalCtrlQty + CtrlQty;
        //            grdOrder.Rows.Add(++i,
        //                            dr["OrderID"].ToString(),               //1)관리번호
        //                            dr["KCUSTOM"].ToString(),               //2)입고처
        //                            dr["Article"].ToString(),               //3)품명
        //                            dr["BuyerArticleNo"].ToString(),        //4)품번
        //                            dr["BoxID"].ToString(),                 //5)BoxID
        //                            dr["RollNo"].ToString(),                //6)절번호
        //                            dr["LotNo"].ToString(),                 //7)LotNo
        //                            string.Format("{0:n0}", CtrlQty),       //8)박스당수량
        //                            string.Format("{0:n0}", OrderQty),      //9)오더량
        //                            dr["UnitClssName"].ToString(),          //10)단위
        //                            dr["Grade"].ToString(),                 //11)등급
        //                            dr["TagName"].ToString(),               //12)Tag

        //                            string.Format("{0:n0}", DefectQty),     //13)불량
        //                                                                    //
        //                            dr["RollSeq"].ToString(),               //14)RollID
        //                            dr["OrderSeq"].ToString(),              //15)색상코드
        //                            dr["ExamNO"].ToString()                 //16)검사호기
        //                            );
        //        }
        //        string Box = string.Format("{0:n0}", dt.Rows.Count) + " Box";
        //        string Qty = string.Format("{0:n0}", TotalCtrlQty) + " 개";

        //        dteBox.Rows.Clear();
        //        DataRow drw = dteBox.NewRow();
        //        drw["WorkSumText"] = "합  계";
        //        drw["WorkCount"] = Box;
        //        drw["WorkSum"] = Qty;
        //        dteBox.Rows.Add(drw);

        //        grdRollSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //        grdRollSum.CurrentCell.Selected = false;
        //        grdRollSum.ClearSelection();
        //        grdRollSum.Enabled = false;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message));
        //    }
        //}
        //#endregion
        //#region grdLotNo 조회 // LOT별 검사실적 탭의 그리드뷰
        //private void FillGridLotNo()
        //{
        //    try
        //    {
        //        int nChkExamDate = 0;
        //        string sExamDate = "";
        //        string eExamDate = "";
        //        int nChkExamNO = 0;
        //        string sExamNo = "";
        //        int nChkOrderID = 0;
        //        string sOrderID = "";
        //        int nChkRollID = 0;
        //        string sRollID = "";
        //        if (chkInsDate.Checked)
        //        {
        //            nChkExamDate = 1;
        //            sExamDate = mtb_From.Text.Replace("-", "");
        //            eExamDate = mtb_To.Text.Replace("-", "");
        //        }
        //        if (chkMachine.Checked)
        //        {
        //            nChkExamNO = 1;
        //            sExamNo = cboMachine.SelectedIndex.ToString();
        //            //sExamNo = Lib.FindComboBoxID(cboMachine, list_cbx);
        //        }
        //        if (chkOrderID.Checked)
        //        {
        //            nChkOrderID = 1;
        //            sOrderID = txtOrderID.Text.Trim();
        //        }
        //        if (chkBoxID.Checked)
        //        {
        //            nChkRollID = 1;
        //            sRollID = txtBoxID.Text.Trim();
        //        }

        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("@ChkExamDate", nChkExamDate);
        //        sqlParameter.Add("@sExamDate", sExamDate);
        //        sqlParameter.Add("@eExamDate", eExamDate);
        //        sqlParameter.Add("@ChkExamNo", nChkExamNO);
        //        sqlParameter.Add("@ExamNo", sExamNo);
        //        sqlParameter.Add("@ChkOrder", nChkOrderID);
        //        sqlParameter.Add("@Order", sOrderID);
        //        sqlParameter.Add("@ChkOrderSeq", 0);
        //        sqlParameter.Add("@OrderSeq", 0);
        //        sqlParameter.Add("@ChkLotNo", 0);
        //        sqlParameter.Add("@LotNo", "");

        //        DataTable dt = Common.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectByLotNo", sqlParameter, false);

        //        grdLotNo.Rows.Clear();
        //        int i = 0;
        //        int ColorQty = 0;
        //        int InspectQty = 0;
        //        int PassQty = 0;
        //        int DefectQty = 0;
        //        int InspectRoll = 0;
        //        int PassRoll = 0;
        //        int DefectRoll = 0;
        //        int TotalInspectQty = 0;
        //        int TotalPassQty = 0;
        //        int TotalDefectQty = 0;
        //        int TotalInspectRoll = 0;
        //        int TotalPassRoll = 0;
        //        int TotalDefectRoll = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            ColorQty = int.Parse(dr["ColorQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            InspectQty = int.Parse(dr["InspectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            PassQty = int.Parse(dr["PassQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            InspectRoll = int.Parse(dr["InspectRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            PassRoll = int.Parse(dr["PassRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            DefectRoll = int.Parse(dr["DefectRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            TotalInspectQty = TotalInspectQty + InspectQty;
        //            TotalPassQty = TotalPassQty + PassQty;
        //            TotalDefectQty = TotalDefectQty + DefectQty;
        //            TotalInspectRoll = TotalInspectRoll + InspectRoll;
        //            TotalPassRoll = TotalPassRoll + PassRoll;
        //            TotalDefectRoll = TotalDefectRoll + DefectRoll;

        //            grdLotNo.Rows.Add(++i,                                  //0)Row 수
        //                            dr["OrderID"].ToString(),               //1)관리번호
        //                            dr["OrderNo"].ToString(),               //2)Order NO
        //                            dr["KCUSTOM"].ToString(),               //3)입고처
        //                            dr["Article"].ToString(),               //4)품  명
        //                            dr["BuyerArticleNo"].ToString(),        //5)품  번
        //                            dr["OrderSeq"].ToString(),              //6)색상코드
        //                            dr["Color"].ToString(),                 //7)색상명
        //                            dr["LotNo"].ToString(),                 //8)LotNo
        //                            string.Format("{0:n0}", ColorQty),      //9)수주량
        //                            dr["UnitClssName"].ToString(),          //10)단위
        //                            "0",                                    //11)입고량
        //                            string.Format("{0:n0}", InspectRoll),   //12)검사절수
        //                            string.Format("{0:n0}", InspectQty),    //13)검사량
        //                            string.Format("{0:n0}", PassRoll),      //14)합격절수
        //                            string.Format("{0:n0}", PassQty),       //15)합격량
        //                            string.Format("{0:n0}", DefectRoll),    //16)불량절수
        //                            string.Format("{0:n0}", DefectQty)      //17)불량수량
        //                                                                    //dr["LossQty"].ToString(),  	        //18)보상
        //                                                                    //dr["CutQty"].ToString()		        //19)난단
        //                            );
        //        }
        //        dt = null;


        //        dteLot.Rows.Clear();
        //        DataRow drw = dteLot.NewRow();
        //        drw["InspectSumText"] = "전 체";
        //        drw["InspectCount"] = string.Format("{0:n0}", InspectRoll) + " Box";
        //        drw["InspectSum"] = string.Format("{0:n0}", InspectQty);
        //        drw["PassSumText"] = "합격량";
        //        drw["PassCount"] = string.Format("{0:n0}", PassRoll) + " Box";
        //        drw["PassSum"] = string.Format("{0:n0}", PassQty);
        //        drw["DefectSumText"] = "불합격";
        //        drw["DefectCount"] = string.Format("{0:n0}", DefectRoll) + " Box";
        //        drw["DefectSum"] = string.Format("{0:n0}", DefectQty);

        //        dteLot.Rows.Add(drw);
        //        grdLotNoSum.DataSource = dteLot;

        //        grdLotNoSum.CurrentCell.Selected = false;
        //        grdLotNoSum.ClearSelection();
        //        grdLotNoSum.Enabled = false;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message));
        //    }
        //}
        //#endregion
        //#region grdDefect 조회 // 박스별 검사실적의 Row의 불량
        //private void FillGridDefect()
        //{
        //    if (grdOrder.Rows.Count == 0)
        //    { return; }
        //    if (!(pnlDefect.Enabled))
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        int rowIndex = grdOrder.SelectedRows[0].Index;
        //        DataGridViewRow dgvr = grdOrder.Rows[rowIndex];

        //        string sOrderID = dgvr.Cells["OrderID"].Value.ToString();
        //        string nRollSeq = dgvr.Cells["RollSeq"].Value.ToString();

        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("@OrderID", sOrderID);
        //        sqlParameter.Add("@RollSeq", nRollSeq); ;

        //        DataTable dt = Common.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectSub", sqlParameter, false);

        //        if (dt.Rows.Count == 0)
        //        {
        //            return;
        //        }

        //        grdDefect.Rows.Clear();

        //        int i = 0;
        //        int BonusQty = 0;
        //        int DefectQty = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            BonusQty = int.Parse(dr["BonusQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
        //            DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);

        //            grdDefect.Rows.Add(++i,                                  //'0)Row 수
        //                            dr["KDefect"].ToString(),                //'1)불량명
        //                            dr["EDefect"].ToString(),                //'2)영문명
        //                            dr["TagName"].ToString(),                //'3)Tag
        //                            dr["YPos"].ToString(),                   //'4)위치
        //                            dr["Demerit"].ToString(),                //'5)벌점
        //                            string.Format("{0:n0}", BonusQty),       //'6)보상
        //                            string.Format("{0:n0}", DefectQty)       //'7)불량수량
        //                            );
        //        }
        //        grdDefect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message));
        //    }
        //}
        //#endregion
        //#region 검색버튼 클릭 시 // 탭에 따라 다르게 검색
        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    int i = tabdgv.SelectedIndex;
        //    switch (i)
        //    {
        //        case 0:
        //            FillGridData();
        //            break;
        //        case 1:
        //            FillGridLotNo();
        //            break;
        //    }            
        //}
        //#endregion
        //#region 달력 From값 입력 // 달력 창 띄우기
        //private void mtb_From_Click(object sender, EventArgs e)
        //{
        //    Common.Popup.Frm_tins_Calendar calendar = new Common.Popup.Frm_tins_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
        //    calendar.WriteDateTextEvent += new Common.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
        //    calendar.Owner = this;
        //    calendar.ShowDialog();
        //}
        //#endregion
        //#region Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        //private void GetDate(string strDate, string btnName)
        //{
        //    DateTime dateTime = new DateTime();
        //    dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
        //    if (btnName == mtb_From.Name)
        //    {
        //        mtb_From.Text = dateTime.ToString("yyyy-MM-dd");
        //    }
        //    else if (btnName == mtb_To.Name)
        //    {
        //        mtb_To.Text = dateTime.ToString("yyyy-MM-dd");
        //    }

        //}
        //#endregion
        //#region 달력 To값 입력 // 달력 창 띄우기
        //private void mtb_To_Click(object sender, EventArgs e)
        //{
        //    Common.Popup.Frm_tins_Calendar calendar = new Common.Popup.Frm_tins_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
        //    calendar.WriteDateTextEvent += new Common.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
        //    calendar.Owner = this;
        //    calendar.ShowDialog();
        //}
        //#endregion
        //#region BoxID 체크박스 클릭 시 BoxID 텍스트박스 Enable 변경
        //private void chkBoxID_Click(object sender, EventArgs e)
        //{
        //    if (chkBoxID.Checked)
        //    {
        //        IsOK = true;
        //        txtBoxID.Enabled = true;
        //        txtBoxID.Text = "";
        //        txtBoxID.Focus();
        //    }
        //    else
        //    {
        //        IsOK = false;
        //        chkBoxID.Focus();
        //        txtBoxID.Enabled = false;
        //    }           
        //}
        //#endregion
        //#region Machine 체크박스 클릭 시 검사호기 콤보박스 Enable 변경
        //private void chkMachine_Click(object sender, EventArgs e)
        //{
        //    if (chkMachine.Checked)
        //    {
        //        cboMachine.Enabled = true;
        //    }
        //    else
        //    {
        //        cboMachine.SelectedIndex = 0;
        //        cboMachine.Enabled = false;
        //    }
        //}
        //#endregion
        //#region Machine 체크박스 클릭 시 관리번호 텍스트박스 Enable 변경
        //private void chkOrderID_Click(object sender, EventArgs e)
        //{
        //    if (chkOrderID.Checked)
        //    {
        //        IsOK = true;
        //        txtOrderID.Enabled = true;
        //        txtOrderID.Text = "";   
        //        txtOrderID.Focus();
        //    }
        //    else
        //    {
        //        IsOK = false;
        //        txtOrderID.Text = "전체";
        //        txtOrderID.Enabled = false;
        //    }
        //}
        //#endregion
        //#region 현재 그리드뷰의 row up
        //private void btnUp_Click(object sender, EventArgs e)
        //{
        //    if (tabdgv.SelectedIndex == 0)
        //    {
        //        Lib.btnRowUp(grdOrder);
        //    }
        //    else if((tabdgv.SelectedIndex == 1))
        //    {
        //        Lib.btnRowUp(grdLotNo);
        //    }
        //}
        //#endregion
        //#region 현재 그리드뷰의 row down
        //private void btnDown_Click(object sender, EventArgs e)
        //{
        //    if (tabdgv.SelectedIndex == 0)
        //    {
        //        Lib.btnRowDown(grdOrder);
        //    }
        //    else if ((tabdgv.SelectedIndex == 1))
        //    {
        //        Lib.btnRowDown(grdLotNo);
        //    }
        //}
        //#endregion
        //#region grdData의 row 삭제 // 박스별 검사실적 그리드뷰
        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        //선택된 행이 없을때
        //        if (grdOrder.SelectedRows.Count == 0)
        //        {
        //            Common.Popup.MyMessageBox.ShowBox(Properties.Resources._202, "", 0, 1);
        //            return;
        //        }
        //        //선택한 행을 삭제할지 물을때
        //        if (Common.Popup.MyMessageBox.ShowBox(Properties.Resources._211, "[확인]", 0, 0) == DialogResult.No)
        //        {
        //            return;
        //        }
        //        int i = grdOrder.SelectedRows[0].Index;
        //        DataGridViewRow dgvr = grdOrder.Rows[i];
        //        string sOrderID = dgvr.Cells["OrderID"].Value.ToString();           //관리번호
        //        string nOrderSeq = dgvr.Cells["OrderSeq"].Value.ToString();         //OrderSeq-색상순위
        //        string nRollID = dgvr.Cells["RollSeq"].Value.ToString();            //RollID
        //        string sExamNo = dgvr.Cells["ExamNO"].Value.ToString();             //검사호기

        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("@OrderID", sOrderID);
        //        sqlParameter.Add("@RollSeq", nRollID);

        //        Common.DataStore.Instance.ExecuteProcedure("xp_WizIns_dInspect", sqlParameter, true);

        //        FillGridData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message));
        //    }
        //}
        //#endregion
        //#region 탭컨트롤의 탭이 선택될때 마다의 이벤트
        //private void tabdgv_Click(object sender, EventArgs e)
        //{
        //    if (tabdgv.SelectedIndex == 0)
        //    {
        //        chkBoxID.Enabled = true;
        //        txtBoxID.Enabled = true;
        //        btnEdit.Visible = false;
        //        btnDelete.Visible = true;
        //        pnlDefect.Visible = false;
        //        pnlDefect.Enabled = true;

        //        FillGridData();
        //    }
        //    else if (tabdgv.SelectedIndex == 1)
        //    {
        //        chkBoxID.Enabled = false;
        //        txtBoxID.Enabled = false;
        //        btnEdit.Visible = false;
        //        btnDelete.Visible = false;
        //        pnlDefect.Visible = false;
        //        pnlDefect.Enabled = false;

        //        FillGridLotNo();
        //    }
        //}
        //#endregion
        //#region 관리번호 텍스트박스에 포커스가 왔을때 숫자 키패드 띄움
        //private void txtOrderID_GotFocus(object sender, EventArgs e)
        //{
        //    if (!IsOK)
        //    { return; }
        //    if (chkOrderID.Checked)
        //    {
        //        IsOK = false;
        //        Common.Popup.Frm_CMNumericKeypad FK = new Common.Popup.Frm_CMNumericKeypad("", "관리번호");
        //        Common.Popup.Frm_CMNumericKeypad.KeypadStr = txtOrderID.Text.Trim();
        //        if (FK.ShowDialog() == DialogResult.OK)
        //        {
        //            txtOrderID.Text = FK.tbInputText.Text;
        //            chkOrderID.Focus();
        //        }
        //        else
        //        {
        //            IsOK = true;

        //            txtOrderID.Text = string.Empty;
        //            txtOrderID.Enabled = false;
        //            chkOrderID.Checked = false;
        //            chkOrderID.Focus();
        //        }
        //    }
        //}
        //#endregion
        //#region BoxID 텍스트박스에 포커스가 왔을때 문자 키패드 띄움
        //private void txtBoxID_GotFocus(object sender, EventArgs e)
        //{
        //    if (!IsOK)
        //    { return; }
        //    if (chkBoxID.Checked)
        //    {
        //        IsOK = false;
        //        Common.Popup.Frm_CMKeypad FK = new Common.Popup.Frm_CMKeypad("BOX ID 검색", "BOX ID");
        //        Common.Popup.Frm_CMKeypad.KeypadStr = txtBoxID.Text.Trim();
        //        if (FK.ShowDialog() == DialogResult.OK)
        //        {
        //            txtBoxID.Text = FK.tbInputText.Text;
        //            //ScanLotNo();
        //            //ScahBarcode();
        //            chkBoxID.Focus();
        //        }
        //        else
        //        {
        //            IsOK = true;

        //            chkBoxID.Focus();
        //            txtBoxID.Text = string.Empty;
        //            chkBoxID.Checked = false;
        //            txtBoxID.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        chkBoxID.Focus();
        //    }
        //}
        //#endregion
        //#region 검사일자 체크박스 클릭 시 From,To 텍스트박스, 달력버튼 Enable 변경
        //private void chkInsDate_Click(object sender, EventArgs e)
        //{
        //    if (chkInsDate.Checked)
        //    {
        //        mtb_From.Enabled = true;
        //        btnCal_From.Enabled = true;
        //        mtb_To.Enabled = true;
        //        btnCal_To.Enabled = true;
        //    }
        //    else
        //    {
        //        mtb_From.Enabled = false;
        //        btnCal_From.Enabled = false;
        //        mtb_To.Enabled = false;
        //        btnCal_To.Enabled = false;
        //    }
        //}
        //#endregion
        //#region 현재 전역변수의 OrderID 기준으로 전역변수 m_tIns, m_tInsSub 채움 왜있는지 모르겠....
        //private void LoadInspectData()
        //{
        //    try
        //    {
        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        string sOrderID = Frm_tins_Main.g_tBase.uOrderID;
        //        string nRollID = Frm_tins_Main.g_tBase.uRollSeq;
        //        sqlParameter.Add("@sOrderID", sOrderID);
        //        sqlParameter.Add("@nRollSeq", nRollID);

        //        DataTable dt = Common.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectOne", sqlParameter, false);

        //        if (dt.Rows.Count == 0)
        //        { return; }
        //        else if (dt.Rows.Count == 1)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            Frm_tins_Main.m_tIns.OrderID = dr["OrderID"].ToString();//      ' 관리 번호
        //            Frm_tins_Main.m_tIns.RollSeq = dr["RollSeq"].ToString();//        ' 일련 순위
        //            Frm_tins_Main.m_tIns.OrderSeq = dr["OrderSeq"].ToString();//       ' 색상 번호
        //            Frm_tins_Main.m_tIns.ExamNo = dr["ExamNO"].ToString();//       ' 검사 호기
        //            Frm_tins_Main.m_tIns.ExamDate = dr["ExamDate"].ToString();//     ' 검사 일자
        //            Frm_tins_Main.m_tIns.ExamTime = dr["ExamTime"].ToString();//     ' 검사 시간
        //            Frm_tins_Main.m_tIns.TeamID = dr["TeamID"].ToString();//   ' 검사 조
        //            Frm_tins_Main.m_tIns.PersonID = dr["PersonID"].ToString();//   ' 검사자 코드
        //            Frm_tins_Main.m_tIns.RealQty = dr["RealQty"].ToString();//      ' 실제검사 수량
        //            Frm_tins_Main.m_tIns.CtrlQty = dr["CtrlQty"].ToString();//      ' 조정검사 수량
        //            //Frm_tins_Main.m_tIns.SampleQty = dr["SampleQty"].ToString();//    ' 견본 수량
        //            //Frm_tins_Main.m_tIns.LossQty = dr["LossQty"].ToString();//      ' 보상 수량
        //            //Frm_tins_Main.m_tIns.CutQty = dr["CutQty"].ToString();//       ' 난단 수량
        //            Frm_tins_Main.m_tIns.UnitClss = dr["UnitClss"].ToString();//       ' 검사 단위
        //            //Frm_tins_Main.m_tIns.Density = dr["Density"].ToString();//      ' 원단 밀도
        //            Frm_tins_Main.m_tIns.GradeID = dr["GradeID"].ToString();// 등급
        //            Frm_tins_Main.m_tIns.LotNo = dr["LotNo"].ToString();//        ' Lot NO
        //            Frm_tins_Main.m_tIns.DefectID = dr["DefectID"].ToString();//   ' 대표불량 코드
        //            Frm_tins_Main.m_tIns.Defect = dr["KDefect"].ToString();//      ' 대표불량명
        //            Frm_tins_Main.m_tIns.DefectClss = dr["DefectClss"].ToString();//      ' 불량 종류
        //            //Frm_tins_Main.m_tIns.CutDefectID = dr["CutDefectID"].ToString();// ' 난단 대표불량 코드
        //            //Frm_tins_Main.m_tIns.CutDefectClss = dr["CutDefectClss"].ToString();//     ' 난단 대표불량 불량 종류
        //            Frm_tins_Main.m_tIns.DefectQty = dr["DefectQty"].ToString();//    ' 불량 갯수
        //            Frm_tins_Main.m_tIns.DefectPoint = dr["DefectPoint"].ToString();//  ' 불량 점수
        //            Frm_tins_Main.m_tIns.ReworkClss = dr["ReworkClss"].ToString();//   ' 재작업 여부,  S_201110_조일_02 에 따른 추가
        //            Frm_tins_Main.m_tIns.BoxID = dr["BoxID"].ToString();//
        //            Frm_tins_Main.m_tIns.InstID = dr["InstID"].ToString();//
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        DataTable dtSub = Common.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectSub", sqlParameter, false);
        //        if (int.Parse(Frm_tins_Main.m_tIns.DefectQty) > 0)
        //        {
        //            foreach (DataRow dr in dtSub.Rows)
        //            {
        //                Common.InsView.TInspectSub m_tInsSub = new Common.InsView.TInspectSub();
        //                m_tInsSub.OrderID = dr["OrderID"].ToString();                                           // ' 관리 번호
        //                m_tInsSub.DefectSeq = dr["DefectSeq"].ToString();                                       // ' 불량 순위
        //                m_tInsSub.DefectID = dr["DefectID"].ToString();                                         // ' 불량 코드
        //                m_tInsSub.KDefect = dr["KDefect"].ToString();                                           //  ' 불량 명(한글)
        //                m_tInsSub.EDefect = dr["EDefect"].ToString();                                           //	' 불량 명(영문)
        //                m_tInsSub.TagName = dr["TagName"].ToString();                                           // ' Tag Name
        //                m_tInsSub.YPos = dr["YPos"].ToString();                                                 // '수직위치
        //                m_tInsSub.Demerit = dr["Demerit"].ToString();                                           // ' 감점
        //                m_tInsSub.BonusQty = string.Format("{0:n1}", int.Parse(dr["BonusQty"].ToString()));		// "#,#00.0")
        //                m_tInsSub.nDefectQty = string.Format("{0:n0}", int.Parse(dr["BonusQty"].ToString()));   // "#,#00")
        //                Frm_tins_Main.list_m_tInsSub.Add(m_tInsSub);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message));
        //    }

        //}
        //#endregion
        //#region 
        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    //선택된 행이 없을때
        //    if (grdOrder.SelectedRows.Count == 0)
        //    {
        //        Common.Popup.MyMessageBox.ShowBox(Properties.Resources._202, "", 0, 1);
        //        return;
        //    }
        //    //WizWorkLib.
        //}
        //#endregion 
        //#region BOX별 검사실적 row변경시마다 불량개수가 1개 이상이고, 조회 결과 1개이상인 경우 해당 row 위에 그리드뷰 띄움 
        //private void grdData_SelectionChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (grdOrder.Rows.Count == 0)
        //        {
        //            pnlDefect.Visible = false;
        //            return;
        //        }
        //        if (grdOrder.SelectedRows.Count == 0 || grdOrder.SelectedRows.Count > 1)
        //        {
        //            pnlDefect.Visible = false;
        //            return;
        //        }

        //        int rowIndex = grdOrder.SelectedRows[0].Index;
        //        int colIndex = grdOrder.Columns["CtrlQty"].Index;
        //        int DefectQty = int.Parse(grdOrder.Rows[rowIndex].Cells["DefectQty"].Value.ToString());
        //        if (DefectQty == 0)
        //        {
        //            pnlDefect.Visible = false;
        //            return;
        //        }
        //        else
        //        {
        //            pnlDefect.Visible = true;
        //        }
        //        FillGridDefect();
        //        if (grdDefect.Rows.Count == 0)
        //        {
        //            pnlDefect.Visible = false;
        //            return;
        //        }
        //        else
        //        {
        //            pnlDefect.Location = grdOrder.GetCellDisplayRectangle(colIndex, rowIndex, true).Location;
        //            pnlDefect.BringToFront();
        //            grdDefect.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message));
        //    }
        //}
        //#endregion
    }
}

