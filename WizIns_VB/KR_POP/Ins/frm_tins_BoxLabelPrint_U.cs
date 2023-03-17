using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Reflection;
using WizCommon;

namespace WizIns
{
    public partial class frm_tins_BoxLabelPrint_U : Form
    {
        WizCommon.Popup.Frm_CMNumericKeypad FKN = null;
        WizCommon.Popup.Frm_CMKeypad FK = null;
        WizCommon.Popup.Frm_tins_Calendar calendar = null;
        WizWorkLib Lib = new WizWorkLib();
        int m_LabelPrintQty = 0;
        public frm_tins_BoxLabelPrint_U()
        {
            InitializeComponent();
        }

        private void frm_tins_BoxLabelPrint_U_Load(object sender, EventArgs e)
        {
            SetScreen();
            //mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            //mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //txtOrderID.Text = "A1234567890";
            //txtProdLabelID.Text = "A1234567890";
            chkInspectAuto.Checked = true;
            InitGrid();
            lblPerson.Text = Frm_tins_Main.g_tBase.Person;
            lblPerson.Tag = Frm_tins_Main.g_tBase.PersonID;
        }
        private void SetScreen()
        {
            tlpForm.SetRowSpan(tlpRight, 2);
            //tlp_Search_Date.SetRowSpan(chkInsDate, 2);
            //패널 배치 및 조정          
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                        }
                    }
                }
            }




        }
         
        #region 그리드뷰 컬럼 셋팅
        private void InitGrid()
        {
            ///BOX별 검사실적 grdData///
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 17;

            int i = 0;
            //idx, 검사완료YN, 작지번호, 공정이동전표, 부품번호,제품수, 발행수, 부품명, 차대번호, 생산일자, ARTICLEID
            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            //grdData.Columns[++i].Name = "ChkIns";
            //grdData.Columns[i].HeaderText = "검사C";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstID";
            grdData.Columns[i].HeaderText = "작지번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "labelID";
            grdData.Columns[i].HeaderText = "공정이동전표";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OutQtyPerBox";
            grdData.Columns[i].HeaderText = "박스당제품수";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerModelID";
            grdData.Columns[i].HeaderText = "BuyerModelID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "부품번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "부품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "WorkEndDate";
            grdData.Columns[i].HeaderText = "생산일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "RemainLabelPrintQty";
            grdData.Columns[i].HeaderText = "발행할수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LastProdArticleID";
            grdData.Columns[i].HeaderText = "최종품품명코드ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "InstDetSeq";
            grdData.Columns[i].HeaderText = "InstDetSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;


            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "OrderID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.HeaderText = "제품수";
            btnCol.Name = "WorkQty";
            btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            btnCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns.Insert(3, btnCol);

            DataGridViewButtonColumn btnCol2 = new DataGridViewButtonColumn();
            btnCol2.HeaderText = "발행수";
            btnCol2.Name = "PrintQty";
            btnCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            btnCol2.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns.Insert(4, btnCol2);

            DataGridViewButtonColumn btnCol3 = new DataGridViewButtonColumn();
            btnCol3.HeaderText = "발행일자";
            btnCol3.Name = "NowDate";
            btnCol3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            btnCol3.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns.Insert(5, btnCol3);


            //grdData.Columns[++i].Name = "LabelPrintQty";
            //grdData.Columns[i].HeaderText = "발행수";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            

            //grdData.Columns[++i].Name = "PersonID";
            //grdData.Columns[i].HeaderText = "PersonID";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            //grdData.Columns[++i].Name = "LabelPrintYN";
            //grdData.Columns[i].HeaderText = "LabelPrintYN";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = false;

            //DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            //chkCol.HeaderText = "C";
            //chkCol.Name = "Chk";
            //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //chkCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns.Insert(1, chkCol);


            //grdData.Columns[++i].Name = "ProcessID";
            //grdData.Columns[i].HeaderText = "ProcessID";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = false;

            //grdData.Columns[++i].Name = "ProdLabelID";
            //grdData.Columns[i].HeaderText = "이동전표";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            //grdData.Columns[++i].Name = "ProdQtyPerBox";
            //grdData.Columns[i].HeaderText = "이동전표장입량";
            //grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //grdData.Columns[i].ReadOnly = true;
            //grdData.Columns[i].Visible = true;

            grdData.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdData.MultiSelect = true;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
            }


        }
        #endregion

        private bool CheckData()
        {
            try
            {
                //선택된 행이 없을때
                if (grdData.SelectedRows.Count == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._996, "", 0, 1);
                    return false;
                }
                if (grdData.SelectedRows[0].Cells["LabelPrintYN"].Value.ToString().ToUpper() == "Y")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("품목에 프린터권한이 없습니다.", "[오류]", 0, 1);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }

        }

        private void FillGridData()
        {
            try
            {
                int nChkExamDate = 0;
                string sExamDate = "";
                string eExamDate = "";
                int nChkOrderID = 0;
                string sOrderID = "";
                int nChkProdLabelID = 0;
                string lsProdLabelID = "";

                //if (chkInsDate.Checked)
                //{
                //    nChkExamDate = 1;
                //    sExamDate = mtb_From.Text.Replace("-", "");
                //    eExamDate = mtb_To.Text.Replace("-", "");
                //}
                //if (chkOrderID.Checked)
                //{
                //    nChkOrderID = 1;
                //    sOrderID = txtOrderID.Text.Trim();
                //}
                //if (chkProdLabel.Checked)
                //{
                //    nChkProdLabelID = 1;
                //    lsProdLabelID = txtProdLabelID.Text.Trim();
                //}
                ////if (chkInstID.Checked)
                ////{
                ////    nChkInstID = 1;
                ////    sInstID = txtInstID.Text.Trim();
                ////}

                //if ((!chkInsDate.Checked) && (!chkOrderID.Checked) && (!chkProdLabel.Checked))//!(chkInstID.Checked)
                //{
                //    Common.Popup.MyMessageBox.ShowBox(string.Format("검색조건 최소한 1가지는 선택하세요.\r\n{0}", "박스가 검은색이 되도록 선택"), "[확인]", 0, 1);
                //    return;
                //}


                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@FromDate", sExamDate);
                sqlParameter.Add("@ToDate", eExamDate);
                sqlParameter.Add("@OrderID", sOrderID);
                sqlParameter.Add("@InstID", "");//지시번호는 입력받지 않으므로 공백
                sqlParameter.Add("@ProdLotID", lsProdLabelID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspect", sqlParameter, false);

                grdData.Rows.Clear();
                int i = 0;
                int OrderQty = 0;
                int InstQty = 0;
                int LabelPrintQty = 0;
                int QtyPerBox = 0;
                int RemainLabelPrintQty = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    OrderQty = Lib.OnlyNumber(dr["OrderQty"].ToString());
                    InstQty = Lib.OnlyNumber(dr["InstQty"].ToString());
                    LabelPrintQty = Lib.OnlyNumber(dr["LabelPrintQty"].ToString());
                    RemainLabelPrintQty = Lib.OnlyNumber(dr["RemainLabelPrintQty"].ToString());
                    grdData.Rows.Add(++i,						            //'Index
                                    dr["StartDate"].ToString(),	            //'InspectBasisID
                                    dr["CustomID"].ToString(),	            //'ReqID
                                    dr["KCUSTOM"].ToString(),	            //'ReqSeq
                                    dr["ArticleID"].ToString(),             //'품명코드
                                    dr["Article"].ToString(),               //'품명
                                    dr["BuyerArticleNo"].ToString(),        //'품번
                                    dr["OrderID"].ToString(),               //'OrderID
                                    dr["LotID"].ToString(),                 //'LOTID
                                    dr["BuyerModelID"].ToString(),          //'바이어모델ID
                                    dr["Model"].ToString(),                 //'바이어모델
                                    OrderQty,                               //'오더량
                                    InstQty,                                //'지시수량
                                    LabelPrintQty,                          //'발행수량
                                    QtyPerBox,                              //'박스당투입수량
                                    dr["InstDetSeq"].ToString(),            //'InstDetSeq
                                    dr["LabelPrintYN"].ToString(),          //'LabelPrintYN
                                    RemainLabelPrintQty,                    //'인쇄할 페이지 수
                                    dr["LastProdArticleID"].ToString(),     //'마지막제품 품명코드ID
                                    dr["InstID"].ToString(),                //'지시번호(InstID)
                                    dr["ProcessID"].ToString(),             //'지시번호(공정)
                                    dr["ProdLabelID"].ToString(),           //'ProdLabelID
                                    dr["ProdQtyPerBox"].ToString()          //'ProdQtyPerBox
                                    );
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private void GetOrderID()
        {
            ////IsOK = false;
            //Common.Popup.Frm_CMNumericKeypad FK = new Common.Popup.Frm_CMNumericKeypad("", "관리번호");
            //Common.Popup.Frm_CMNumericKeypad.KeypadStr = txtOrderID.Text.Trim();
            //if (FK.ShowDialog() == DialogResult.OK)
            //{
            //    txtOrderID.Text = FK.tbInputText.Text;
            //    chkOrderID.Focus();
            //}
            //else
            //{
            //    //IsOK = true;
            //    Common.Popup.MyMessageBox.ShowBox(string.Format("오류! 잘못된 관리번호입니다.\r\n{0}", FK.tbInputText.Text), "[오류]", 0, 1);
            //    txtOrderID.Text = string.Empty;
            //    txtOrderID.Enabled = false;
            //    chkOrderID.Checked = false;
            //    chkOrderID.Focus();
            //    GetOrderID();
            //}
        }

        private bool SaveData()
        {
            try
            {
                string sLastProdArticleID = "";
                int nLabelPrintQty = 0;
                if (grdData.Rows.Count == 0)
                {
                    return false;
                }

                if (grdData.SelectedRows.Count == 0 || grdData.SelectedRows.Count > 1)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._996, "[확인]", 0, 1);
                    return false;
                }

                int rowIndex = grdData.SelectedRows[0].Index;
                DataGridViewRow dgvr = grdData.Rows[rowIndex];


                Frm_tins_Main.TWkLabelPrint.sLabelID = "";
                Frm_tins_Main.TWkLabelPrint.sLabelGubun = "2";
                Frm_tins_Main.TWkLabelPrint.sProcessID = Frm_tins_Main.g_tBase.ProcessID;//                 '공정ID
                Frm_tins_Main.TWkLabelPrint.sArticleID = dgvr.Cells["ArticleID"].Value.ToString();
                Frm_tins_Main.TWkLabelPrint.sPrintDate = Frm_tins_Main.g_tBase.ProductDate; //'MakeDate(MT_SHORT, Date)

                Frm_tins_Main.TWkLabelPrint.sReprintDate = "";
                Frm_tins_Main.TWkLabelPrint.nReprintQty = 0;
                Frm_tins_Main.TWkLabelPrint.sInstID = dgvr.Cells["InstID"].Value.ToString(); 
                Frm_tins_Main.TWkLabelPrint.nInstDetSeq = int.Parse(dgvr.Cells["InstDetSeq"].Value.ToString()); 
                Frm_tins_Main.TWkLabelPrint.sOrderID = dgvr.Cells["OrderID"].Value.ToString();

                //Frm_tins_Main.TWkLabelPrint.nPrintQty = Lib.OnlyNumber(lblTagNo.Text);
                //Frm_tins_Main.TWkLabelPrint.nQtyPerBox = Lib.OnlyNumber(lblItemQty.Text);//        '박스당 수량 == 생산수량
                Frm_tins_Main.TWkLabelPrint.sCreateuserID = Frm_tins_Main.g_tBase.PersonID;
                Frm_tins_Main.TWkLabelPrint.sLastUpdateUserID = Frm_tins_Main.g_tBase.PersonID;
                sLastProdArticleID = dgvr.Cells["LastProdArticleID"].Value.ToString();

                //nLabelPrintQty = m_LabelPrintQty + Lib.OnlyNumber(lblTagNo.Text);

                string g_sPrinterName = Lib.GetDefaultPrinter();
                if (g_sPrinterName == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("라벨프린터가 연결되어 있지 않거나 설치되어 있지 않습니다. 라벨프린터를 확인해주세요.", "[확인]", 0, 1);
                    return false;
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@LabelGubun", Frm_tins_Main.TWkLabelPrint.sLabelGubun);
                sqlParameter.Add("@ProcessID", Frm_tins_Main.TWkLabelPrint.sProcessID);
                sqlParameter.Add("@ArticleID", Frm_tins_Main.TWkLabelPrint.sArticleID);
                sqlParameter.Add("@PrintDate", Frm_tins_Main.TWkLabelPrint.sPrintDate);
                sqlParameter.Add("@ReprintDate", Frm_tins_Main.TWkLabelPrint.sReprintDate);
                sqlParameter.Add("@ReprintQty", Frm_tins_Main.TWkLabelPrint.nReprintQty);
                sqlParameter.Add("@InstID", Frm_tins_Main.TWkLabelPrint.sInstID);
                sqlParameter.Add("@InstDetSeq", Frm_tins_Main.TWkLabelPrint.nInstDetSeq);
                sqlParameter.Add("@OrderID", Frm_tins_Main.TWkLabelPrint.sOrderID);
                sqlParameter.Add("@PrintQty", Frm_tins_Main.TWkLabelPrint.nPrintQty);
                sqlParameter.Add("@LabelPrintQty", nLabelPrintQty);
                sqlParameter.Add("@nQtyPerBox", Frm_tins_Main.TWkLabelPrint.nQtyPerBox);
                sqlParameter.Add("@CreateUserID", Frm_tins_Main.TWkLabelPrint.sCreateuserID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_wkLabelPrint_iwkLabelPrint", sqlParameter, false);

                if (dt is null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                {
                    return false;
                }

                DataRow dr = dt.Rows[0];
                List<string> vData = new List<string>();
                vData.Add(dr["OrderNo"].ToString());
                vData.Add(dr["Model"].ToString());
                vData.Add(dr["Article"].ToString());
                vData.Add(dr["OrderSpec"].ToString());
                vData.Add(dr["PrintDate"].ToString());
                vData.Add(dr["QtyPerBox"].ToString());
                vData.Add(dr["LabelID"].ToString());
                vData.Add(dr["LabelID"].ToString());
                vData.Add("");
                //vData.Add(dr["(rs!Article), 25)
                //vData(2) = Left(CheckNull(rs!Article), 24)
                //vData(8) = Mid(CheckNull(rs!Article), 25)

                if (sLastProdArticleID != Frm_tins_Main.TWkLabelPrint.sArticleID)
                {
                    vData.Add("");
                }

                WizCommon.Popup.MyMessageBox.ShowBox("라벨 발행중입니다. 잠시만 기다려주십시오.", "라벨 발행중.....", 3, 2);

                //sPrint = MakeCleverTagPrintString(vData, "002", , , 1, 0)

                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private string MakeCleverTagPrintString(List<string> vData, string sTagID, int nPrintCount = 1, int nDefectCnt = 1)
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@LabelGubun", Frm_tins_Main.TWkLabelPrint.sLabelGubun);
            sqlParameter.Add("@ProcessID", Frm_tins_Main.TWkLabelPrint.sProcessID);
            sqlParameter.Add("@ArticleID", Frm_tins_Main.TWkLabelPrint.sArticleID);
            sqlParameter.Add("@PrintDate", Frm_tins_Main.TWkLabelPrint.sPrintDate);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_wkLabelPrint_iwkLabelPrint", sqlParameter, false);

            if (dt is null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
            {
                return "";
            }
            return "";
        }

        private void txtProdLabelID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtProdLabelID.Text.Substring(0, 1).ToUpper() == "C" && txtProdLabelID.Text.Trim().Length == 10)
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("@ProdLotID", txtProdLabelID.Text.Trim());
                    DataTable dt = WizCommon.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sProdLabelID", sqlParameter, false);

                    grdData.Rows.Clear();

                    if (dt.Rows.Count != 1)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("올바른 공정이동전표가 아닙니다. 라벨을 다시 확인해주십시오.", "[오류]", 0, 1);
                        return;
                    }
                    else
                    {
                        DataRow dr = dt.Rows[0];
                        grdData.Rows.Add(grdData.Rows.Count + 1,                //index
                                         dr["InstID"].ToString(),               //작업지시번호
                                         dr["labelID"].ToString(),              //공정이동전표 번호
                                         dr["WorkQty"].ToString(),              //마지막공정의 생산수(제품수)
                                         dr["PrintQty"].ToString(),             //라벨발행수 : 마지막공정 생산수 / 출하박스당 장입량  
                                         dr["OutQtyPerBox"].ToString(),         //박스당 장입량
                                         dr["Model"].ToString(),                //고객모델명
                                         dr["BuyerModelID"].ToString(),         //고객모델ID
                                         dr["BuyerArticleNo"].ToString(),       //고객품번(부품번호)
                                         dr["Article"].ToString(),              //품명(부품명)
                                         dr["WorkEndDate"].ToString(),          //생산일자(성형공정에서 작업종료 버튼 눌렀을때)
                                         dr["ArticleID"].ToString(),            //품목코드
                                         dr["NowDate"].ToString(),              //포장일자
                                         dr["RemainLabelPrintQty"].ToString(),  //지시수량 - sum(이미 인쇄한  box 당 수량 ) / 박스당 수량
                                         dr["LastProdArticleID"].ToString(),    //마지막 품명
                                         dr["InstDetSeq"].ToString(),           //지시순서
                                         dr["OrderID"].ToString()               //관리번호

                                          );
                    }
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("공정이동전표가 아닙니다. 라벨을 다시 확인해주십시오.", "[오류]", 0, 1);
                    return;
                }
            }
        }

        private void tlpPerson_Click(object sender, EventArgs e)
        {
            Frm_PopUpSel PopSel1 = new Frm_PopUpSel();
            PopSel1.WriteTextEvent += new Frm_PopUpSel.TextEventHandler(GetData);
            PopSel1.Owner = this;
            PopSel1.Show();
            
            void GetData()
            {
                lblPerson.Text = Frm_tins_Main.g_tBase.Person;
                lblPerson.Tag = Frm_tins_Main.g_tBase.PersonID;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0)
            { return; }

            int i = grdData.SelectedCells[0].RowIndex;

            grdData.Rows.RemoveAt(i);
        }

        private void btnInstIDSearch_Click(object sender, EventArgs e)
        {
            grdData.Rows.Add(grdData.Rows.Count + 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            frm_tins_NoLabel_Q ftnq = new frm_tins_NoLabel_Q();

            if (ftnq.ShowDialog() == DialogResult.OK)
            {

            }

            //txtProdLabelID.Focus();

            //DataGridViewRow dgvr = new DataGridViewRow();
            //dgvr.CreateCells(grdData, grdData.Rows.Count + 1);

            //Common.Popup.Frm_tins_Calendar calendar = new Common.Popup.Frm_tins_Calendar
            //    (dgvr.Cells["NowDate"].Value.ToString(), grdData.Name);
            //calendar.WriteDateTextEvent += new Common.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
        }

        private void btnProdLabelID_Click(object sender, EventArgs e)
        {
            FK = new WizCommon.Popup.Frm_CMKeypad(txtProdLabelID.Text.Trim(), "공정이동전표");
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtProdLabelID.Text = FK.tbInputText.Text;
                FillGridData();                
            }
            else
            {
                txtProdLabelID.Text = string.Empty;
            }
            txtProdLabelID.Focus();
        }
        private void GetDate(string strDate, string btnName)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("ko-KR", true);
            DateTime dt = new DateTime();
            if (strDate.Length == 14)
            {
                dt = DateTime.ParseExact(strDate.Substring(0, 8), "yyyyMMdd", culture);
            }
            else if (strDate.Length == 8)
            {
                dt = DateTime.ParseExact(strDate, "yyyyMMdd", culture);
            }
            grdData.SelectedRows[0].Cells["NowDate"].Value = dt.ToString("MM-dd");
            grdData.SelectedRows[0].Cells["NowDate"].Tag = dt.ToString("yyyyMMdd");
            calendar = null;
        }
        private void grdData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.SelectedRows.Count == 0)
            { return; }
            if (dgv.Rows.Count > 0 && e.RowIndex == dgv.SelectedRows[0].Index)
            {
                DataGridViewRow dgvr = dgv.SelectedRows[0];
                int i = 0;
                switch (e.ColumnIndex)
                {
                    case 3:
                        if (dgvr.Cells["WorkQty"].Value is null)
                        { break; }
                        FKN = new WizCommon.Popup.Frm_CMNumericKeypad(Lib.OnlyNumber(dgvr.Cells["WorkQty"].Value.ToString()).ToString(), "제품수");
                        if (FKN.ShowDialog() == DialogResult.OK)
                        {
                            i = Lib.OnlyNumber(FKN.tbInputText.Text.Trim());
                            dgvr.Cells["WorkQty"].Value = string.Format("{0:n0}", i);
                        }
                        break;
                    case 4:
                        if (dgvr.Cells["PrintQty"].Value is null)
                        { break; }
                        FKN = new WizCommon.Popup.Frm_CMNumericKeypad(Lib.OnlyNumber(dgvr.Cells["PrintQty"].Value.ToString()).ToString(), "발행수");
                        if (FKN.ShowDialog() == DialogResult.OK)
                        {
                            i = Lib.OnlyNumber(FKN.tbInputText.Text.Trim());
                            dgvr.Cells["PrintQty"].Value = string.Format("{0:n0}", i);
                        }
                        break;
                    case 5:
                        calendar = new WizCommon.Popup.Frm_tins_Calendar
                        //(dgvr.Cells["NowDate"].Value.ToString().Replace("-", ""), grdData.Name);
                        ("20181107", grdData.Name);
                        calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
                        calendar.Owner = this;
                        calendar.Show();

                        break;
                }
            }
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("스캔된 공정이동전표가 없습니다.\r\n 공정이동전표를 스캔해주세요.", "[SCAN]", 0, 1);
                return;
            }
            string sLastProdArticleID = "";
            List<Ins_Label> list_InsLabel = new List<Ins_Label>();
            foreach (DataGridViewRow dgvr in grdData.Rows)
            {
                Ins_Label Ins_Label = new Ins_Label();
                Ins_Label.Article = dgvr.Cells["Article"].Value.ToString();
                Ins_Label.ArticleID = dgvr.Cells["ArticleID"].Value.ToString();
                Ins_Label.BuyerArticleNo = dgvr.Cells["BuyerArticleNo"].Value.ToString();
                Ins_Label.BuyerModelID = dgvr.Cells["BuyerModelID"].Value.ToString();
                Ins_Label.InsLabelID = "";// dgvr.Cells["InsLabelID"].Value.ToString();
                Ins_Label.InstID = dgvr.Cells["InstID"].Value.ToString();
                Ins_Label.Model = dgvr.Cells["Model"].Value.ToString();
                Ins_Label.MoveLabelID = dgvr.Cells["MoveLabelID"].Value.ToString();
                Ins_Label.NowDate = dgvr.Cells["NowDate"].Value.ToString();
                Ins_Label.OutQtyPerBox = dgvr.Cells["OutQtyPerBox"].Value.ToString();
                Ins_Label.PrintQty = dgvr.Cells["PrintQty"].Value.ToString();
                Ins_Label.WorkEndDate = dgvr.Cells["WorkEndDate"].Value.ToString();
                Ins_Label.WorkQty = dgvr.Cells["WorkQty"].Value.ToString();
                Ins_Label.sLastProdArticleID = dgvr.Cells["LastProdArticleID"].Value.ToString();

                WizCommon.InsView.TWkLabelPrint TWkLabelPrint = new WizCommon.InsView.TWkLabelPrint();
                TWkLabelPrint.sLabelID = "";
                TWkLabelPrint.sLabelGubun = "2";
                TWkLabelPrint.sProcessID = Frm_tins_Main.g_tBase.ProcessID;
                TWkLabelPrint.sArticleID = dgvr.Cells["ArticleID"].Value.ToString();
                TWkLabelPrint.sPrintDate = Frm_tins_Main.g_tBase.ProductDate;

                TWkLabelPrint.sReprintDate = "";
                TWkLabelPrint.nReprintQty = 0;
                TWkLabelPrint.sInstID = dgvr.Cells["InstID"].Value.ToString();
                TWkLabelPrint.nInstDetSeq = Lib.OnlyNumber(dgvr.Cells["InstDetSeq"].Value.ToString());
                TWkLabelPrint.sOrderID = dgvr.Cells["OrderID"].Value.ToString();

                TWkLabelPrint.nPrintQty = Lib.OnlyNumber(dgvr.Cells["PrintQty"].Value.ToString());
                TWkLabelPrint.nQtyPerBox = Lib.OnlyNumber(dgvr.Cells["OutQtyPerBox"].Value.ToString());
                TWkLabelPrint.sCreateuserID = Frm_tins_Main.g_tBase.PersonID;
                TWkLabelPrint.sLastUpdateUserID = Frm_tins_Main.g_tBase.PersonID;

                Frm_tins_Main.list_TWkLabelPrint.Add(TWkLabelPrint);
                list_InsLabel.Add(Ins_Label);
            }
            
            
        }
    }
}



