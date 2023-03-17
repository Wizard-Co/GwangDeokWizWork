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
    public partial class frm_tins_NoLabel_Q : Form
    {
        public delegate void TextEventHandler(Ins_Label Ins);            // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler SendEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 
        WizWorkLib Lib = new WizWorkLib();
        int m_LabelPrintQty = 0;
        public frm_tins_NoLabel_Q()
        {
            InitializeComponent();
        }

        private void frm_tins_NoLabel_Q_Load(object sender, EventArgs e)
        {
            SetScreen();
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            txtOrderID.Text = "A1234567890";
            txtInstID.Text = "A1234567890";
            chkInstID.Checked = true;
            InitGrid();
        }
        private void SetScreen()
        {
            tlpForm.SetRowSpan(tlpRight, 2);
            tlp_Search_Date.SetRowSpan(chkInsDate, 2);
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
            grdData.ColumnCount = 23;

            int i = 0;

            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "StartDate";
            grdData.Columns[i].HeaderText = "일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "CustomID";
            grdData.Columns[i].HeaderText = "거래처코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "관리번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "LOTID";
            grdData.Columns[i].HeaderText = "작지번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerModelID";
            grdData.Columns[i].HeaderText = "차종코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            
            grdData.Columns[++i].Name = "WorkEndDate";
            grdData.Columns[i].HeaderText = "생산일자 ";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OrderQty";
            grdData.Columns[i].HeaderText = "오더수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstQty";
            grdData.Columns[i].HeaderText = "지시수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "LabelPrintQty";
            grdData.Columns[i].HeaderText = "발행수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "QtyPerBox";
            grdData.Columns[i].HeaderText = "박스당투입수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstDetSeq";
            grdData.Columns[i].HeaderText = "InstDetSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LabelPrintYN";
            grdData.Columns[i].HeaderText = "LabelPrintYN";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "RemainLabelPrintQty";
            grdData.Columns[i].HeaderText = "발행할수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "LastProdArticleID";
            grdData.Columns[i].HeaderText = "최종품품명코드ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstID";
            grdData.Columns[i].HeaderText = "InstID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ProcessID";
            grdData.Columns[i].HeaderText = "ProcessID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;


            grdData.Columns[++i].Name = "ProdLabelID";
            grdData.Columns[i].HeaderText = "이동전표";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ProdQtyPerBox";
            grdData.Columns[i].HeaderText = "이동전표장입량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


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
                //int nChkProdLabelID = 0;
                //string lsProdLabelID = "";
                int nChkInstID = 0;
                string sInstID = "";

                if (chkInsDate.Checked)
                {
                    nChkExamDate = 1;
                    sExamDate = mtb_From.Text.Replace("-", "");
                    eExamDate = mtb_To.Text.Replace("-", "");
                }
                if (chkOrderID.Checked)
                {
                    nChkOrderID = 1;
                    sOrderID = txtOrderID.Text.Trim();
                }
                
                if (chkInstID.Checked)
                {
                    nChkInstID = 1;
                    sInstID = txtInstID.Text.Trim();
                }

                if ((!chkInsDate.Checked) && (!chkOrderID.Checked) && (!chkInstID.Checked))//!(chkInstID.Checked)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("검색조건 최소한 1가지는 선택하세요.\r\n{0}", "박스가 검은색이 되도록 선택"), "[확인]", 0, 1);
                    return;
                }


                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@FromDate", sExamDate);
                sqlParameter.Add("@ToDate", eExamDate);
                sqlParameter.Add("@OrderID", sOrderID);
                sqlParameter.Add("@InstID", sInstID);//지시번호는 입력받지 않으므로 공백
                sqlParameter.Add("@ProdLotID", "");

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_wkLabelprint_splInputDet", sqlParameter, false);

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
            //IsOK = false;
            WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad("", "관리번호");
            WizCommon.Popup.Frm_CMNumericKeypad.KeypadStr = txtOrderID.Text.Trim();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtOrderID.Text = FK.tbInputText.Text;
                chkOrderID.Focus();
            }
            else
            {
                //IsOK = true;
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 잘못된 관리번호입니다.\r\n{0}", FK.tbInputText.Text), "[오류]", 0, 1);
                txtOrderID.Text = string.Empty;
                txtOrderID.Enabled = false;
                chkOrderID.Checked = false;
                chkOrderID.Focus();
                GetOrderID();
            }
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
                WizCommon.TSCLIB_DLL.openport(g_sPrinterName);
                //SendWindowDllCommand(vData, "002", 1, 0);
                WizCommon.TSCLIB_DLL.closeport();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            //dr["InstID"].ToString(),                //'지시번호(InstID)
            //"",                                     //공정이동전표
            //dr["QtyPerBox"].ToString(),             //'박스당투입수량
            //dr["Model"].ToString(),                 //'바이어모델
            //dr["BuyerModelID"].ToString(),          //'바이어모델ID
            //dr["BuyerArticleNo"].ToString(),        //'품번
            //dr["Article"].ToString(),               //'품명
            //dr["ArticleID"].ToString(),             //'품명코드
            //DateTime.Today.ToString("MM-dd"),       //'생산일자
            //dr["RemainLabelPrintQty"].ToString(),   //'인쇄할 페이지 수
            //dr["LastProdArticleID"].ToString(),     //'마지막제품 품명코드ID
            //dr["InstDetSeq"].ToString(),            //'InstDetSeq
            //dr["OrderID"].ToString(),               //'OrderID
            //DateTime.Today.ToString("MM-dd"),       //'생산일자

            //dr["StartDate"].ToString(),	            //'InspectBasisID
            //dr["CustomID"].ToString(),	            //'ReqID
            //dr["KCUSTOM"].ToString(),	            //'ReqSeq
                                    
                                    
                                    
                                    
            //                        dr["LotID"].ToString(),                 //'LOTID
                                    
                                    
            //                        OrderQty,                               //'오더량
            //                        InstQty,                                //'지시수량
            //                        LabelPrintQty,                          //'발행수량
            //                        QtyPerBox,                              //'박스당투입수량
                                    
            //                        dr["LabelPrintYN"].ToString(),          //'LabelPrintYN
            //                        RemainLabelPrintQty,                    //'인쇄할 페이지 수
            //                        dr["LastProdArticleID"].ToString(),     //'마지막제품 품명코드ID
                                    
            //                        dr["ProcessID"].ToString(),             //'지시번호(공정)
            //                        dr["ProdLabelID"].ToString(),           //'ProdLabelID
            //                        dr["ProdQtyPerBox"].ToString()          //'ProdQtyPerBox
        }

        //public void SendWindowDllCommand(List<string> vData, string sTagID, int nPrintCount, int nDefectCnt)
        //{
        //    try
        //    {
        //        DataTable dt = null;

        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("TagID", sTagID);
        //        dt = Common.DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTag]", sqlParameter, false);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.Rows[0];
        //            Frm_tins_Main.m_tTag.sTagID = dr["TagID"].ToString();
        //            Frm_tins_Main.m_tTag.sTag = dr["Tag"].ToString();
        //            Frm_tins_Main.m_tTag.nWidth = Lib.OnlyNumber(dr["Width"].ToString());
        //            Frm_tins_Main.m_tTag.nHeight = Lib.OnlyNumber(dr["Height"].ToString());
        //            //Sub_m_tTag.sUse_YN = dr["clss"].ToString();

        //            Frm_tins_Main.m_tTag.nDefHeight = Lib.OnlyNumber(dr["DefHeight"].ToString());
        //            Frm_tins_Main.m_tTag.nDefBaseY = Lib.OnlyNumber(dr["DefBaseY"].ToString());
        //            Frm_tins_Main.m_tTag.nDefBaseX1 = Lib.OnlyNumber(dr["DefBaseX1"].ToString());
        //            Frm_tins_Main.m_tTag.nDefBaseX2 = Lib.OnlyNumber(dr["DefBaseX2"].ToString());
        //            Frm_tins_Main.m_tTag.nDefBaseX3 = Lib.OnlyNumber(dr["DefBaseX3"].ToString());

        //            Frm_tins_Main.m_tTag.nDefGapY = Lib.OnlyNumber(dr["DefGapY"].ToString());
        //            Frm_tins_Main.m_tTag.nDefGapX1 = Lib.OnlyNumber(dr["DefGapX1"].ToString());
        //            Frm_tins_Main.m_tTag.nDefGapX2 = Lib.OnlyNumber(dr["DefGapX2"].ToString());
        //            Frm_tins_Main.m_tTag.nDefLength = Lib.OnlyNumber(dr["DefLength"].ToString());
        //            Frm_tins_Main.m_tTag.nDefHCount = Lib.OnlyNumber(dr["DefHCount"].ToString());

        //            Frm_tins_Main.m_tTag.nDefBarClss = Lib.OnlyNumber(dr["DefBarClss"].ToString());
        //            Frm_tins_Main.m_tTag.nGap = Lib.OnlyNumber(dr["Gap"].ToString());
        //            Frm_tins_Main.m_tTag.sDirect = dr["Direct"].ToString();
        //        }

        //        dt = null;
        //        dt = Common.DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sMtTagSub]", sqlParameter, false);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                Common.TTagSub m_tItem = new Common.TTagSub();
        //                m_tItem.sName = dr["Name"].ToString();
        //                m_tItem.nType = Lib.OnlyNumber(dr["Type"].ToString());
        //                m_tItem.nTypeSub = Lib.OnlyNumber(dr["TypeSub"].ToString());
        //                m_tItem.nAlign = Lib.OnlyNumber(dr["Align"].ToString());
        //                m_tItem.x = Lib.OnlyNumber(dr["x"].ToString());
        //                m_tItem.y = Lib.OnlyNumber(dr["y"].ToString());
        //                m_tItem.nFont = Lib.OnlyNumber(dr["Font"].ToString());
        //                m_tItem.nLength = Lib.OnlyNumber(dr["Length"].ToString());
        //                m_tItem.nHMulti = Lib.OnlyNumber(dr["HMulti"].ToString());
        //                m_tItem.nVMulti = Lib.OnlyNumber(dr["VMulti"].ToString());
        //                m_tItem.nRelation = Lib.OnlyNumber(dr["Relation"].ToString());
        //                m_tItem.nRotation = Lib.OnlyNumber(dr["Rotation"].ToString());
        //                m_tItem.nSpace = Lib.OnlyNumber(dr["Space"].ToString());
        //                m_tItem.nPrevItem = Lib.OnlyNumber(dr["PrevItem"].ToString());
        //                m_tItem.nBarType = Lib.OnlyNumber(dr["BarType"].ToString());
        //                m_tItem.nBarHeight = Lib.OnlyNumber(dr["BarHeight"].ToString());
        //                m_tItem.nFigureWidth = Lib.OnlyNumber(dr["FigureWidth"].ToString());
        //                m_tItem.nFigureHeight = Lib.OnlyNumber(dr["FigureHeight"].ToString());
        //                m_tItem.nThickness = Lib.OnlyNumber(dr["Thickness"].ToString());
        //                m_tItem.sImageFile = dr["ImageFile"].ToString();
        //                m_tItem.nWidth = Lib.OnlyNumber(dr["Width"].ToString());
        //                m_tItem.nHeight = Lib.OnlyNumber(dr["Height"].ToString());
        //                m_tItem.nVisible = Lib.OnlyNumber(dr["Visible"].ToString());
        //                m_tItem.sFontName = dr["FontName"].ToString();
        //                m_tItem.sFontStyle = dr["FontStyle"].ToString();
        //                m_tItem.sFontUnderLine = dr["FontUnderLine"].ToString();
        //            }


        //                if (sTagID == "005")
        //                {
        //                    //int a = 0;
        //                    //foreach (string str in lData)
        //                    //{
        //                    //    Console.WriteLine(a++.ToString() + "/////" + str + "///////");
        //                    //}

        //                    //20171011 김종영 수정 type 변경
        //                    //if (list_m_tItem[i].nType == 1 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
        //                    if (list_m_tItem[i].nType < 2 && list_m_tItem[i].sName.Substring(0, 1).ToUpper() == "D")
        //                    {
        //                        if (list_m_tItem[i].nRelation == 0 && list_m_tItem[i].nTypeSub == 1)//바코드
        //                        {
        //                            //list_m_tItem[i].sText = dr["Text"].ToString();

        //                            list_m_tItem[i].sText = vData[4];
        //                        }

        //                        else if (list_m_tItem[i].nRelation > 0 && list_m_tItem[i].nRelation < 5 && list_m_tItem[i].nTypeSub == 1)
        //                        //list_m_tItem[i].sName.Substring(list_m_tItem[i].sName.Length).ToUpper() == "0")
        //                        {
        //                            switch (list_m_tItem[i].nRelation)
        //                            {
        //                                case 1:
        //                                    list_m_tItem[i].sText = vData[0];
        //                                    break;
        //                                case 2:
        //                                    list_m_tItem[i].sText = vData[1];
        //                                    break;
        //                                case 3:
        //                                    list_m_tItem[i].sText = vData[2];
        //                                    break;
        //                                case 4:
        //                                    list_m_tItem[i].sText = vData[3];
        //                                    break;
        //                            }
        //                        }
        //                        else if (list_m_tItem[i].nRelation > 4)
        //                        {
        //                            if (vData.Count > 5 && vData.Count > list_m_tItem[i].nRelation)
        //                            {
        //                                //for (int p = 5; p < vData.Count; p++)
        //                                //{
        //                                //    list_m_tItem[i].sText = vData[p].ToString();
        //                                //}
        //                                list_m_tItem[i].sText = vData[list_m_tItem[i].nRelation].ToString();

        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (list_m_tItem[i].sName == "T_Title" && Frm_tprc_Main.g_tBase.ProcessID == "1201")
        //                        {
        //                            list_m_tItem[i].sText = "레이져커팅전표";
        //                        }
        //                        else
        //                        {
        //                            list_m_tItem[i].sText = dr["Text"].ToString();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    list_m_tItem[i].sText = dr["Text"].ToString();
        //                }



        //        }
        //        for (int a = 0; a < list_m_tItem.Count; a++)
        //        {
        //            Console.WriteLine(list_m_tItem[a].sText + "@@" + a.ToString() + "@@" + list_m_tItem[a].sName + list_m_tItem[a].sText);
        //        }
        //        string strWidth = "";
        //        string strHeight = "";
        //        try
        //        {
        //            if (Lib.CheckNum(Sub_m_tTag.nWidth.ToString()) != "0")
        //            {
        //                strWidth = (Sub_m_tTag.nWidth / 10F).ToString();
        //            }
        //            if (Lib.CheckNum(Sub_m_tTag.nHeight.ToString()) != "0")
        //            {
        //                strHeight = (Sub_m_tTag.nHeight / 10F).ToString();
        //            }
        //        }
        //        catch
        //        {
        //            strWidth = "0";
        //            strHeight = "0";
        //        }

        //        TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "3", "0");//기존소스
        //        //TSCLIB_DLL.setup(strWidth, strHeight, "8", "15", "0", "0", "0");//감열지 테스트용

        //        TSCLIB_DLL.sendcommand("DIRECTION " + Sub_m_tTag.sDirect);

        //        TSCLIB_DLL.clearbuffer();

        //        //PictureBox m_picPrint = new PictureBox();

        //        int m_picPrint = 0;
        //        string sText = "";

        //        int nHMulti = 0;
        //        int nVMulti = 0;
        //        int nFontDot = 0;
        //        int nGapY = 0;

        //        int x = 0;
        //        int j = 0;
        //        int k = 0;
        //        string[] sBarType = new string[2];

        //        // 구방식
        //        if (sTagID == "004")
        //        {
        //            Console.WriteLine(vData[7]);
        //            m_sData = new string[vData.Count];
        //            for (int i = 0; i < vData.Count; i++)
        //            {
        //                m_sData[i] = Lib.CheckNull(vData[i]);
        //            }
        //            Console.WriteLine(m_sData[7]);
        //            for (int a = 0; a < m_sData.Length; a++)
        //            {
        //                Console.WriteLine(m_sData[a] + "@@" + a.ToString() + "번쨰");
        //            }
        //            for (int i = 0; i < list_m_tItem.Count; i++)
        //            {
        //                if (list_m_tItem[i].nVisible > 0)//출력여부
        //                {
        //                    //'바코드
        //                    if (list_m_tItem[i].nType == WTS.EnumItem.IO_BARCODE)
        //                    {
        //                        if (list_m_tItem[i].nPrevItem == 0)
        //                        {
        //                            if (list_m_tItem[i].nBarType == 0)// 1:1 Code
        //                            {
        //                                sBarType[0] = "1";
        //                                sBarType[1] = "1";
        //                            }
        //                            else                            // 2:5 Code
        //                            {
        //                                sBarType[0] = "2";
        //                                sBarType[1] = "5";
        //                            }

        //                            string readable = "1";

        //                            //MessageBox.Show("바코드");

        //                            TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(),
        //                                               list_m_tItem[i].y.ToString(),
        //                                               "128",
        //                                               list_m_tItem[i].nBarHeight.ToString(),
        //                                               "1",
        //                                               list_m_tItem[i].nRotation.ToString(),
        //                                               sBarType[0],
        //                                               sBarType[1],
        //                                               sTagID == "004" ? vData[7] : GetBarCodeItemText(i)
        //                                               //2017.0908  김종영 수정 라벨ID가 나오는구나
        //                                               );
        //                            Console.WriteLine(vData[7]);
        //                            Console.WriteLine(GetBarCodeItemText(i));
        //                        }
        //                    }
        //                    //데이터 OR 문자
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_DATA || list_m_tItem[i].nType == WTS.EnumItem.IO_TEXT)
        //                    {
        //                        //IO_DATA와 IO_TEXT 구분
        //                        if (list_m_tItem[i].nType == WTS.EnumItem.IO_DATA)
        //                        {
        //                            if (sTagID == "005")
        //                            {
        //                                sText = GetItemText(i);
        //                            }
        //                            else//vData 배열에 nRelation 번호순으로 데이터 집어넣어놈. 
        //                            //1. 거래처 2. 품명 3. 라벨id 4.카드id(바코드) 5. 공정명 6. 작업일 7. 작업자 8. 생산수량 9. 비고 및 Y라벨ID(2101공정일때 성형?)
        //                            {
        //                                if (list_m_tItem[i].nRelation > 0 && vData.Count > list_m_tItem[i].nRelation)
        //                                {
        //                                    //if (list_m_tItem[i].nRelation > 4)
        //                                    //{
        //                                    //    if (vData[0] == "0401")//사출
        //                                    //    {
        //                                    //        sText = vData[list_m_tItem[i].nRelation];
        //                                    //    }
        //                                    //    else
        //                                    //    {
        //                                    //        sText = vData[list_m_tItem[i].nRelation];
        //                                    //    }
        //                                    //}
        //                                    //else{
        //                                    sText = vData[list_m_tItem[i].nRelation];
        //                                    //}
        //                                    if (list_m_tItem[i].sName.Contains("생산수량"))
        //                                    {
        //                                        if (Int32.Parse(Lib.CheckNum(sText)) > 0)
        //                                        {
        //                                            sText = string.Format("{0:#,###}", Int32.Parse(sText)) + "EA";
        //                                        }
        //                                    }

        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sText = list_m_tItem[i].sText;
        //                        }

        //                        nHMulti = list_m_tItem[i].nHMulti++;
        //                        nVMulti = list_m_tItem[i].nVMulti++;
        //                        nFontDot = tagPrint.GetCleverFontDot(list_m_tItem[i].nFont);
        //                        if (nVMulti > 0)
        //                        {
        //                            nGapY = tagPrint.GetCleverFontGapY(list_m_tItem[i].nFont) / nVMulti;
        //                        }
        //                        else
        //                        {
        //                            nGapY = 0;
        //                        }

        //                        ////기존VB소스
        //                        //x = TagPrint.GAP_X + Convert.ToInt32(Convert.ToDouble(list_m_tItem[i].x) * TagPrint.DPI_RATIO);
        //                        ////변경소스 20170908 김종영 필요없어보임
        //                        //x = list_m_tItem[i].x;

        //                        //'**************************************************************************************
        //                        //' ※※※※※주의사항 윈도우dll 문제인지 x,y 위치 값이 원형은 integer인데  프로그램에서 CStr 해주지 않으면
        //                        //'                    라벨 발행이 안된다.프린터기 모델에 따라 다를 수 있으므로 꼭 주의
        //                        //'-------------------------------------------------------------------------------------
        //                        int intx = list_m_tItem[i].x;
        //                        int inty = list_m_tItem[i].y;
        //                        int fontheight = int.Parse(Lib.CheckNum((list_m_tItem[i].nFont).ToString()));
        //                        int rotation = list_m_tItem[i].nRotation;

        //                        int fontstyle = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontStyle));
        //                        int fontunderline = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontUnderLine));
        //                        string szFaceName = list_m_tItem[i].sFontName;


        //                        //int fontstyle = tagPrint.g_sLabelFontStyle;
        //                        //int fontunderline = tagPrint.g_sLabelFontUnderLine;
        //                        //string szFaceName = tagPrint.g_sLabelFontName;
        //                        string content = sText.Trim();



        //                        TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);


        //                        //TSCLIB_DLL.windowsfont(list_m_tItem[i].x, list_m_tItem[i].y, int.Parse((list_m_tItem[i].nFont * 10).ToString()),
        //                        //    list_m_tItem[i].nRotation, tagPrint.g_sLabelFontStyle, tagPrint.g_sLabelFontUnderLine, tagPrint.g_sLabelFontName, sText);



        //                    }
        //                    //'선(Line)-5이하
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
        //                    {
        //                        int x1 = 0;
        //                        int x2 = 0;
        //                        int y1 = 0;
        //                        int y2 = 0;
        //                        int nTh = 0;

        //                        //기존VB소스
        //                        if (sTagID == "005")
        //                        {
        //                            x1 = TagPrint.GAP_X + Convert.ToInt32(Convert.ToDouble(list_m_tItem[i].x) * TagPrint.DPI_RATIO);
        //                            y1 = TagPrint.GAP_Y + Convert.ToInt32(Convert.ToDouble(list_m_tItem[i].y) * TagPrint.DPI_RATIO);
        //                            x2 = j + Convert.ToInt32(Convert.ToDouble(list_m_tItem[i].nFigureWidth) * TagPrint.DPI_RATIO);
        //                            y2 = k + Convert.ToInt32(Convert.ToDouble(list_m_tItem[i].nFigureHeight) * TagPrint.DPI_RATIO);
        //                            nTh = list_m_tItem[i].nThickness;
        //                        }
        //                        //변경소스 20170908 김종영
        //                        else
        //                        {
        //                            x1 = list_m_tItem[i].x;
        //                            y1 = list_m_tItem[i].y;
        //                            x2 = list_m_tItem[i].nFigureWidth;
        //                            y2 = list_m_tItem[i].nFigureHeight;
        //                            nTh = list_m_tItem[i].nThickness;
        //                        }

        //                        string IsDllStr = "BOX " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + nTh.ToString();

        //                        TSCLIB_DLL.sendcommand(IsDllStr);

        //                    }
        //                    //'선(Line)-5초과, 다이아몬드, 이미지
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_LINE || list_m_tItem[i].nType == WTS.EnumItem.IO_DIAMOND || list_m_tItem[i].nType == WTS.EnumItem.IO_IMAGE)
        //                    {
        //                        if (list_m_tItem[i].nType == WTS.EnumItem.IO_LINE)
        //                        {
        //                        }
        //                        else if (list_m_tItem[i].nType == WTS.EnumItem.IO_RECT)
        //                        {
        //                        }
        //                        else if (list_m_tItem[i].nType == WTS.EnumItem.IO_DIAMOND)
        //                        {
        //                        }
        //                        else if (list_m_tItem[i].nType == WTS.EnumItem.IO_CIRCLE)
        //                        {
        //                        }
        //                        else if (list_m_tItem[i].nType == WTS.EnumItem.IO_IMAGE)
        //                        {
        //                        }
        //                    }
        //                    //사각형
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_RECT)
        //                    {
        //                        TSCLIB_DLL.sendcommand("BOX " + list_m_tItem[i].x.ToString() + list_m_tItem[i].y.ToString() + list_m_tItem[i].x.ToString() + list_m_tItem[i].y.ToString()
        //                            + "\"" + list_m_tItem[i].nThickness + "\"");
        //                    }
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_QRcode)
        //                    {
        //                        sText = GetItemText(i);

        //                        string IsDllStr = "QRCODE " + (TagPrint.GAP_X + int.Parse((list_m_tItem[i].x * TagPrint.DPI_RATIO).ToString())).ToString() + ", " +
        //                                          (TagPrint.GAP_Y + int.Parse((list_m_tItem[i].y * TagPrint.DPI_RATIO).ToString())).ToString() + ", L" +
        //                                          ", " + list_m_tItem[i].nFont.ToString() + ", A, 0, M2, S1, \"" + sText + "\"\r\n";

        //                        TSCLIB_DLL.sendcommand(IsDllStr);
        //                    }
        //                }
        //            }
        //        }


        //        //김종영
        //        else if (sTagID == "005")
        //        {
        //            for (int i = 0; i < list_m_tItem.Count; i++)
        //            {
        //                if (list_m_tItem[i].nVisible > 0)//출력여부
        //                {
        //                    //'바코드
        //                    if (list_m_tItem[i].nType == WTS.EnumItem.IO_BARCODE)
        //                    {
        //                        if (list_m_tItem[i].nPrevItem == 0)
        //                        {
        //                            if (list_m_tItem[i].nBarType == 0)// 1:1 Code
        //                            {
        //                                sBarType[0] = "1";
        //                                sBarType[1] = "1";
        //                            }
        //                            else                            // 2:5 Code
        //                            {
        //                                sBarType[0] = "2";
        //                                sBarType[1] = "5";
        //                            }

        //                            string readable = "1";

        //                            TSCLIB_DLL.barcode(list_m_tItem[i].x.ToString(),
        //                                               list_m_tItem[i].y.ToString(),
        //                                               "128",
        //                                               list_m_tItem[i].nBarHeight.ToString(),
        //                                               "1",
        //                                               list_m_tItem[i].nRotation.ToString(),
        //                                               sBarType[0],
        //                                               sBarType[1],
        //                                               list_m_tItem[0].sText
        //                                               //list_m_tItem[i].sText
        //                                               //IsTagID == "004" ? vData[4] : GetBarCodeItemText(i)
        //                                               //2017.0908  김종영 수정 라벨ID가 나오는구나

        //                                               );
        //                            Console.WriteLine(list_m_tItem[0].sText + "바코드");
        //                        }
        //                    }
        //                    //데이터 OR 문자
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_DATA || list_m_tItem[i].nType == WTS.EnumItem.IO_TEXT)
        //                    {
        //                        if (sTagID == "005")
        //                        {
        //                            sText = list_m_tItem[i].sText;
        //                            if (list_m_tItem[i].sName.Contains("생산수량"))
        //                            {
        //                                if (Int32.Parse(Lib.CheckNum(sText)) > 0)
        //                                {
        //                                    sText = string.Format("{0:#,###}", Int32.Parse(sText)) + "EA";
        //                                }
        //                            }
        //                        }

        //                        int intx = list_m_tItem[i].x;
        //                        int inty = list_m_tItem[i].y;
        //                        int fontheight = int.Parse((list_m_tItem[i].nFont).ToString());
        //                        int rotation = list_m_tItem[i].nRotation;
        //                        int fontstyle = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontStyle));
        //                        int fontunderline = int.Parse(Lib.CheckNum(list_m_tItem[i].sFontUnderLine));
        //                        string szFaceName = list_m_tItem[i].sFontName;
        //                        string content = sText.Trim();

        //                        TSCLIB_DLL.windowsfont(intx, inty, fontheight, rotation, fontstyle, fontunderline, szFaceName, content);
        //                    }
        //                    //'선(Line)-5이하
        //                    else if (list_m_tItem[i].nType == WTS.EnumItem.IO_LINE)// && (list_m_tItem[i].nFigureHeight <= 5 || list_m_tItem[i].nFigureWidth <= 5))
        //                    {
        //                        int x1 = 0;
        //                        int x2 = 0;
        //                        int y1 = 0;
        //                        int y2 = 0;
        //                        int nTh = 0;

        //                        x1 = list_m_tItem[i].x;
        //                        y1 = list_m_tItem[i].y;
        //                        x2 = list_m_tItem[i].nFigureWidth;
        //                        y2 = list_m_tItem[i].nFigureHeight;
        //                        nTh = list_m_tItem[i].nThickness;

        //                        string IsDllStr = "BOX " + x1.ToString() + ", " + y1.ToString() + ", " + x2.ToString() + ", " + y2.ToString() + ", " + nTh.ToString();

        //                        TSCLIB_DLL.sendcommand(IsDllStr);

        //                    }
        //                }
        //            }
        //        }

        //        TSCLIB_DLL.printlabel("1", nPrintCount.ToString());

        //        list_m_tItem = new List<TTagSub>();
        //        vData = new List<string>();
        //        m_picPrint = 0;
        //    }
        //    catch (Exception excpt)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
        //    }
        //    finally
        //    {
        //        DataStore.Instance.CloseConnection();
        //    }

        //}
    }
}


