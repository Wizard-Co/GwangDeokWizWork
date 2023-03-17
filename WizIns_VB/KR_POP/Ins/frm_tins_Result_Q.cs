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
    public partial class frm_tins_Result_Q : Form
    {
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();//콤보박스 리스트
        WizWorkLib Lib = new WizWorkLib();//통합라이브러리
        DataTable dteBox = new DataTable();//grdData 합계 dt
        DataTable dteLot = new DataTable();//grdLotNo 합계 dt
        bool IsOK = false;//GotFocus때문에 필요

        /// <summary>
        /// 생성
        /// </summary>
        public frm_tins_Result_Q()
        {
            InitializeComponent();
        }
        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            tlp_Main.Dock = DockStyle.Fill;
            foreach (Control con in tlp_Main.Controls)//con = tlp 상위에서 2번째
            {
                con.Dock = DockStyle.Fill;
                if (con is Panel)
                {
                    Panel pnl = con as Panel;
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                }
                foreach (Control ctl in con.Controls)//tlp 상위에서 3번째
                {
                    ctl.Dock = DockStyle.Fill;
                    foreach (Control ct in ctl.Controls)
                    {
                        ct.Dock = DockStyle.Fill;
                    }
                }
            }

            //Main tlp 세팅
            tlp_Search_Date.SetRowSpan(chkInsDate, 2);
            tlp_Main.SetColumnSpan(tabdgv, 2);
            //TabControl 셋팅
            tabdgv.Appearance = TabAppearance.Normal;//바꾸지말것. Normal이 아니면 그리드뷰 안나오는 현상 생김
            tabdgv.Alignment = TabAlignment.Bottom;
            tabdgv.Dock = DockStyle.Fill;
            tabdgv.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold);
            tabdgv.TabPages["BOX"].Text = "BOX별 검사실적";
            tabdgv.TabPages["LOT"].Text = "LOT별 검사실적";
            foreach (TabPage tp in tabdgv.TabPages)
            {
                tp.Font = new Font("맑은 고딕", 10F, FontStyle.Regular);
                foreach (Control con in tp.Controls)
                {
                    con.Dock = DockStyle.Fill;
                    foreach (Control ctl in con.Controls)
                    {
                        ctl.Dock = DockStyle.Fill;
                    }
                }
            }
        }
        #endregion
        #region 그리드뷰 컬럼 셋팅
        private void InitGrid()
        {
            ///BOX별 검사실적 grdData///
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 17;

            int i = 0;

            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "관리번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "입고처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품  명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품  번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BoxID";
            grdData.Columns[i].HeaderText = "BoxID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "RollNo";
            grdData.Columns[i].HeaderText = "절번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LotNo";
            grdData.Columns[i].HeaderText = "LotNo";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "CtrlQty";
            grdData.Columns[i].HeaderText = "박스당수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].DefaultCellStyle.Format = "N0";
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OrderQty";
            grdData.Columns[i].HeaderText = "오더량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].DefaultCellStyle.Format = "N0";
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "UnitClssName";
            grdData.Columns[i].HeaderText = "단위";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Grade";
            grdData.Columns[i].HeaderText = "등급";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "TagName";
            grdData.Columns[i].HeaderText = "Tag";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "DefectQty";
            grdData.Columns[i].HeaderText = "불량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].DefaultCellStyle.Format = "N0";
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "RollSeq";
            grdData.Columns[i].HeaderText = "RollID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderSeq";
            grdData.Columns[i].HeaderText = "OrderSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "ExamNO";
            grdData.Columns[i].HeaderText = "ExamNO";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
            }

            //grdDefect
            grdDefect.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdDefect.ColumnCount = 8;

            int d = 0;

            grdDefect.Columns[d].Name = "IDX";
            grdDefect.Columns[d].HeaderText = "";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = true;

            grdDefect.Columns[++d].Name = "KDefect";
            grdDefect.Columns[d].HeaderText = "불량명";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = true;

            grdDefect.Columns[++d].Name = "EDefect";
            grdDefect.Columns[d].HeaderText = "영문명";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = false;

            grdDefect.Columns[++d].Name = "TagName";
            grdDefect.Columns[d].HeaderText = "Tag";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = false;

            grdDefect.Columns[++d].Name = "YPos";
            grdDefect.Columns[d].HeaderText = "위치";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].DefaultCellStyle.Format = "N0";
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = false;

            grdDefect.Columns[++d].Name = "Demerit";
            grdDefect.Columns[d].HeaderText = "벌점";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].DefaultCellStyle.Format = "N0";
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = false;

            grdDefect.Columns[++d].Name = "BonusQty";
            grdDefect.Columns[d].HeaderText = "보상";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].DefaultCellStyle.Format = "N0";
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = false;

            grdDefect.Columns[++d].Name = "DefectQty";
            grdDefect.Columns[d].HeaderText = "수량";
            grdDefect.Columns[d].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdDefect.Columns[d].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdDefect.Columns[d].DefaultCellStyle.Format = "N0";
            grdDefect.Columns[d].ReadOnly = true;
            grdDefect.Columns[d].Visible = true;

            grdDefect.Font = new Font("맑은 고딕", 8, FontStyle.Bold);
            grdDefect.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 8F);
            grdDefect.RowTemplate.Height = 30;
            grdDefect.ColumnHeadersHeight = 35;
            grdDefect.ScrollBars = ScrollBars.Both;
            grdDefect.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdDefect.MultiSelect = false;
            grdDefect.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdDefect.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdDefect.Dock = DockStyle.Fill;
            grdDefect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in grdDefect.Columns)
            {
                col.DataPropertyName = col.Name;
            }
            //LOT별 검사실적 grdLotNo

            grdLotNo.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdLotNo.ColumnCount = 19;

            int k = 0;

            grdLotNo.Columns[k].Name = "IDX";
            grdLotNo.Columns[k].HeaderText = "";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "OrderID";
            grdLotNo.Columns[k].HeaderText = "관리번호";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "Order NO";
            grdLotNo.Columns[k].HeaderText = "OrderNO";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++i].Name = "KCustom";
            grdLotNo.Columns[k].HeaderText = "입고처";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++i].Name = "Article";
            grdLotNo.Columns[k].HeaderText = "품  명";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "BuyerArticleNo";
            grdLotNo.Columns[k].HeaderText = "품  번";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "OrderSeq";
            grdLotNo.Columns[k].HeaderText = "색상코드";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "색상명";
            grdLotNo.Columns[k].HeaderText = "Color";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "LotNo";
            grdLotNo.Columns[k].HeaderText = "LotNo";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "CtrlQty";
            grdLotNo.Columns[k].HeaderText = "박스당수량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "ColorQty";
            grdLotNo.Columns[k].HeaderText = "오더량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "UnitClss";
            grdLotNo.Columns[k].HeaderText = "단위";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "StuffQty";
            grdLotNo.Columns[k].HeaderText = "입고량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "InspectRoll";
            grdLotNo.Columns[k].HeaderText = "검사절";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "InspectQty";
            grdLotNo.Columns[k].HeaderText = "검사량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "PassRoll";
            grdLotNo.Columns[k].HeaderText = "합격절수";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "PassQty";
            grdLotNo.Columns[k].HeaderText = "합격량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "DefectRoll";
            grdLotNo.Columns[k].HeaderText = "불량절수";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "DefectQty";
            grdLotNo.Columns[k].HeaderText = "불량";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = true;

            grdLotNo.Columns[++k].Name = "LossQty";
            grdLotNo.Columns[k].HeaderText = "보상";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Columns[++k].Name = "CutQty";
            grdLotNo.Columns[k].HeaderText = "난단";
            grdLotNo.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdLotNo.Columns[k].DefaultCellStyle.Format = "N0";
            grdLotNo.Columns[k].ReadOnly = true;
            grdLotNo.Columns[k].Visible = false;

            grdLotNo.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            grdLotNo.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdLotNo.RowTemplate.Height = 30;
            grdLotNo.ColumnHeadersHeight = 35;
            grdLotNo.ScrollBars = ScrollBars.Both;
            grdLotNo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdLotNo.MultiSelect = false;
            grdLotNo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdLotNo.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);


            foreach (DataGridViewColumn col in grdLotNo.Columns)
            {
                col.DataPropertyName = col.Name;
            }
            //BOX별 검사실적 grdRollSum 합계

            grdRollSum.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.

            grdRollSum.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdRollSum.RowTemplate.Height = 40;
            grdRollSum.ScrollBars = ScrollBars.None;
            grdRollSum.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdRollSum.MultiSelect = false;
            grdRollSum.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdRollSum.ColumnHeadersVisible = false;
            grdRollSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dteBox.Columns.Add("WorkSumText".ToString());
            dteBox.Columns.Add("WorkCount".ToString());
            dteBox.Columns.Add("WorkSum".ToString());

            DataRow dr = dteBox.NewRow();
            dr["WorkSumText"] = "합  계";
            dr["WorkCount"] = "0 Box";
            dr["WorkSum"] = "0";
            dteBox.Rows.Add(dr);
            grdRollSum.DataSource = dteBox;

            grdRollSum.CurrentCell.Selected = false;
            grdRollSum.ClearSelection();
            grdRollSum.Enabled = false;

            grdRollSum.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdRollSum.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdRollSum.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //BOX별 검사실적 grdLotNoSum 합계

            grdLotNoSum.Columns.Clear();
            grdLotNoSum.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdLotNoSum.RowTemplate.Height = 40;
            grdLotNoSum.ScrollBars = ScrollBars.None;
            grdLotNoSum.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdLotNoSum.MultiSelect = false;
            grdLotNoSum.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdLotNoSum.ColumnHeadersVisible = false;
            grdLotNoSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //dteLot dt 셋팅
            dteLot.Columns.Add("InspectSumText".ToString());
            dteLot.Columns.Add("InspectCount".ToString());
            dteLot.Columns.Add("InspectSum".ToString());
            dteLot.Columns.Add("PassSumText".ToString());
            dteLot.Columns.Add("PassCount".ToString());
            dteLot.Columns.Add("PassSum".ToString());
            dteLot.Columns.Add("DefectSumText".ToString());
            dteLot.Columns.Add("DefectCount".ToString());
            dteLot.Columns.Add("DefectSum".ToString());

            //dteLot 초기값 세팅
            DataRow drw = dteLot.NewRow();
            drw["InspectSumText"] = "전 체";
            drw["InspectCount"] = "0 Box";
            drw["InspectSum"] = "0";
            drw["PassSumText"] = "합격량";
            drw["PassCount"] = "0 Box";
            drw["PassSum"] = "0";
            drw["DefectSumText"] = "불합격";
            drw["DefectCount"] = "0 Box";
            drw["DefectSum"] = "0";

            dteLot.Rows.Add(drw);
            grdLotNoSum.DataSource = dteLot;

            tabdgv.SelectedIndex = 1;

            grdLotNoSum.CurrentCell.Selected = false;
            grdLotNoSum.ClearSelection();
            grdLotNoSum.Enabled = false;

            tabdgv.SelectedIndex = 0;

            //grdLotNoSum 세팅
            grdLotNoSum.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdLotNoSum.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdLotNoSum.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdLotNoSum.Columns[3].DefaultCellStyle.BackColor = Color.PaleGreen;
            grdLotNoSum.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdLotNoSum.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdLotNoSum.Columns[6].DefaultCellStyle.BackColor = Color.OrangeRed;
            grdLotNoSum.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdLotNoSum.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        #endregion
        #region 폼 로드 시의 작업
        private void frm_tins_Result_Q_Load(object sender, EventArgs e)
        {
            InitGrid();
            SetScreen();
            SetComboBox();

            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

            txtOrderID.GotFocus += txtOrderID_GotFocus;
            txtBoxID.GotFocus += txtBoxID_GotFocus;
            tabdgv_Click(null, null);
            chkInsDate_Click(null, null);
            chkMachine_Click(null, null);
            chkBoxID_Click(null, null);
            chkOrderID_Click(null, null);
        }
        #endregion
        #region 콤보박스에 검사호기 셋팅
        private void SetComboBox()
        {
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    if (i == 0)
                    {
                        cboMachine.Items.Add("전체");
                    }
                    else
                    {
                        cboMachine.Items.Add(i.ToString());
                    }
                }

                //패널크기에 따른 콤보박스 사이즈 및 폰트사이즈 변경
                Lib.SetComboBox(cboMachine, null, null);
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
            finally
            {
                //임시
                //Frm_tins_Main.g_tBase.DepartID = "";

                //전역변수에 Depart가 없을 시 0번째 Index 선택
                if (Frm_tins_Main.g_tBase.DepartID == "")
                {
                    cboMachine.SelectedIndex = 0;
                }
                //전역변수에 Depart가 있을 시 해당 값을 가진 Index 선택
                else
                {
                    cboMachine.SelectedIndex = Convert.ToInt32(Lib.FindComboBoxIdx(cboMachine, list_cbx, Frm_tins_Main.g_tBase.DepartID));
                }
            }

        }
        #endregion
        #region grdData 조회 //박스별 검사실적 탭의 그리드뷰
        private void FillGridData()
        {
            try
            {
                int nChkExamDate = 0;
                string sExamDate = "";
                string eExamDate = "";
                int nChkExamNO = 0;
                string sExamNo = "";
                int nChkOrderID = 0;
                string sOrderID = "";
                int nChkRollID = 0;
                string sRollID = "";
                if (chkInsDate.Checked)
                {
                    nChkExamDate = 1;
                    sExamDate = mtb_From.Text.Replace("-", "");
                    eExamDate = mtb_To.Text.Replace("-", "");
                }
                if (chkMachine.Checked)
                {
                    if (cboMachine.SelectedIndex == 0)
                    {
                        nChkExamNO = 0;
                        sExamNo = cboMachine.SelectedIndex.ToString();
                    }
                    else
                    {
                        nChkExamNO = 1;
                        sExamNo = cboMachine.SelectedIndex.ToString();
                    }
                }
                if (chkOrderID.Checked)
                {
                    nChkOrderID = 1;
                    sOrderID = txtOrderID.Text.Trim();
                }
                if (chkBoxID.Checked)
                {
                    nChkRollID = 1;
                    sRollID = txtBoxID.Text.Trim();
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@ChkExamDate", nChkExamDate);
                sqlParameter.Add("@sExamDate", sExamDate);
                sqlParameter.Add("@eExamDate", eExamDate);
                sqlParameter.Add("@ChkExamNo", nChkExamNO);
                sqlParameter.Add("@ExamNo", sExamNo);
                sqlParameter.Add("@ChkOrderID", nChkOrderID);
                sqlParameter.Add("@OrderID", sOrderID);
                sqlParameter.Add("@ChkBoxID", nChkRollID);
                sqlParameter.Add("@BoxID", sRollID);

                DataTable dt = WizCommon.DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspect", sqlParameter, false);

                grdData.Rows.Clear();
                int i = 0;
                int CtrlQty = 0;
                int OrderQty = 0;
                int DefectQty = 0;
                int TotalCtrlQty = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    CtrlQty = int.Parse(dr["CtrlQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    OrderQty = int.Parse(dr["OrderQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    TotalCtrlQty = TotalCtrlQty + CtrlQty;
                    grdData.Rows.Add(++i,
                                    dr["OrderID"].ToString(),               //1)관리번호
                                    dr["KCUSTOM"].ToString(),               //2)입고처
                                    dr["Article"].ToString(),               //3)품명
                                    dr["BuyerArticleNo"].ToString(),        //4)품번
                                    dr["BoxID"].ToString(),                 //5)BoxID
                                    dr["RollNo"].ToString(),                //6)절번호
                                    dr["LotNo"].ToString(),                 //7)LotNo
                                    string.Format("{0:n0}", CtrlQty),       //8)박스당수량
                                    string.Format("{0:n0}", OrderQty),      //9)오더량
                                    dr["UnitClssName"].ToString(),          //10)단위
                                    dr["Grade"].ToString(),                 //11)등급
                                    dr["TagName"].ToString(),               //12)Tag

                                    string.Format("{0:n0}", DefectQty),     //13)불량
                                                                            //
                                    dr["RollSeq"].ToString(),               //14)RollID
                                    dr["OrderSeq"].ToString(),              //15)색상코드
                                    dr["ExamNO"].ToString()                 //16)검사호기
                                    );
                }
                string Box = string.Format("{0:n0}", dt.Rows.Count) + " Box";
                string Qty = string.Format("{0:n0}", TotalCtrlQty) + " 개";

                dteBox.Rows.Clear();
                DataRow drw = dteBox.NewRow();
                drw["WorkSumText"] = "합  계";
                drw["WorkCount"] = Box;
                drw["WorkSum"] = Qty;
                dteBox.Rows.Add(drw);

                grdRollSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                grdRollSum.CurrentCell.Selected = false;
                grdRollSum.ClearSelection();
                grdRollSum.Enabled = false;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }
        #endregion
        #region grdLotNo 조회 // LOT별 검사실적 탭의 그리드뷰
        private void FillGridLotNo()
        {
            try
            {
                int nChkExamDate = 0;
                string sExamDate = "";
                string eExamDate = "";
                int nChkExamNO = 0;
                string sExamNo = "";
                int nChkOrderID = 0;
                string sOrderID = "";
                int nChkRollID = 0;
                string sRollID = "";
                if (chkInsDate.Checked)
                {
                    nChkExamDate = 1;
                    sExamDate = mtb_From.Text.Replace("-", "");
                    eExamDate = mtb_To.Text.Replace("-", "");
                }
                if (chkMachine.Checked)
                {
                    nChkExamNO = 1;
                    sExamNo = cboMachine.SelectedIndex.ToString();
                    //sExamNo = Lib.FindComboBoxID(cboMachine, list_cbx);
                }
                if (chkOrderID.Checked)
                {
                    nChkOrderID = 1;
                    sOrderID = txtOrderID.Text.Trim();
                }
                if (chkBoxID.Checked)
                {
                    nChkRollID = 1;
                    sRollID = txtBoxID.Text.Trim();
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@ChkExamDate", nChkExamDate);
                sqlParameter.Add("@sExamDate", sExamDate);
                sqlParameter.Add("@eExamDate", eExamDate);
                sqlParameter.Add("@ChkExamNo", nChkExamNO);
                sqlParameter.Add("@ExamNo", sExamNo);
                sqlParameter.Add("@ChkOrder", nChkOrderID);
                sqlParameter.Add("@Order", sOrderID);
                sqlParameter.Add("@ChkOrderSeq", 0);
                sqlParameter.Add("@OrderSeq", 0);
                sqlParameter.Add("@ChkLotNo", 0);
                sqlParameter.Add("@LotNo", "");

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectByLotNo", sqlParameter, false);

                grdLotNo.Rows.Clear();
                int i = 0;
                int ColorQty = 0;
                int InspectQty = 0;
                int PassQty = 0;
                int DefectQty = 0;
                int InspectRoll = 0;
                int PassRoll = 0;
                int DefectRoll = 0;
                int TotalInspectQty = 0;
                int TotalPassQty = 0;
                int TotalDefectQty = 0;
                int TotalInspectRoll = 0;
                int TotalPassRoll = 0;
                int TotalDefectRoll = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    ColorQty = int.Parse(dr["ColorQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    InspectQty = int.Parse(dr["InspectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    PassQty = int.Parse(dr["PassQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    InspectRoll = int.Parse(dr["InspectRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    PassRoll = int.Parse(dr["PassRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    DefectRoll = int.Parse(dr["DefectRoll"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    TotalInspectQty = TotalInspectQty + InspectQty;
                    TotalPassQty = TotalPassQty + PassQty;
                    TotalDefectQty = TotalDefectQty + DefectQty;
                    TotalInspectRoll = TotalInspectRoll + InspectRoll;
                    TotalPassRoll = TotalPassRoll + PassRoll;
                    TotalDefectRoll = TotalDefectRoll + DefectRoll;

                    grdLotNo.Rows.Add(++i,                                  //0)Row 수
                                    dr["OrderID"].ToString(),               //1)관리번호
                                    dr["OrderNo"].ToString(),               //2)Order NO
                                    dr["KCUSTOM"].ToString(),               //3)입고처
                                    dr["Article"].ToString(),               //4)품  명
                                    dr["BuyerArticleNo"].ToString(),        //5)품  번
                                    dr["OrderSeq"].ToString(),              //6)색상코드
                                    dr["Color"].ToString(),                 //7)색상명
                                    dr["LotNo"].ToString(),                 //8)LotNo
                                    string.Format("{0:n0}", ColorQty),      //9)수주량
                                    dr["UnitClssName"].ToString(),          //10)단위
                                    "0",                                    //11)입고량
                                    string.Format("{0:n0}", InspectRoll),   //12)검사절수
                                    string.Format("{0:n0}", InspectQty),    //13)검사량
                                    string.Format("{0:n0}", PassRoll),      //14)합격절수
                                    string.Format("{0:n0}", PassQty),       //15)합격량
                                    string.Format("{0:n0}", DefectRoll),    //16)불량절수
                                    string.Format("{0:n0}", DefectQty)      //17)불량수량
                                                                            //dr["LossQty"].ToString(),  	        //18)보상
                                                                            //dr["CutQty"].ToString()		        //19)난단
                                    );
                }
                dt = null;


                dteLot.Rows.Clear();
                DataRow drw = dteLot.NewRow();
                drw["InspectSumText"] = "전 체";
                drw["InspectCount"] = string.Format("{0:n0}", InspectRoll) + " Box";
                drw["InspectSum"] = string.Format("{0:n0}", InspectQty);
                drw["PassSumText"] = "합격량";
                drw["PassCount"] = string.Format("{0:n0}", PassRoll) + " Box";
                drw["PassSum"] = string.Format("{0:n0}", PassQty);
                drw["DefectSumText"] = "불합격";
                drw["DefectCount"] = string.Format("{0:n0}", DefectRoll) + " Box";
                drw["DefectSum"] = string.Format("{0:n0}", DefectQty);

                dteLot.Rows.Add(drw);
                grdLotNoSum.DataSource = dteLot;

                grdLotNoSum.CurrentCell.Selected = false;
                grdLotNoSum.ClearSelection();
                grdLotNoSum.Enabled = false;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }
        #endregion
        #region grdDefect 조회 // 박스별 검사실적의 Row의 불량
        private void FillGridDefect()
        {
            if (grdData.Rows.Count == 0)
            { return; }
            if (!(pnlDefect.Enabled))
            {
                return;
            }
            try
            {
                int rowIndex = grdData.SelectedRows[0].Index;
                DataGridViewRow dgvr = grdData.Rows[rowIndex];

                string sOrderID = dgvr.Cells["OrderID"].Value.ToString();
                string nRollSeq = dgvr.Cells["RollSeq"].Value.ToString();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@OrderID", sOrderID);
                sqlParameter.Add("@RollSeq", nRollSeq); ;

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectSub", sqlParameter, false);

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                grdDefect.Rows.Clear();

                int i = 0;
                int BonusQty = 0;
                int DefectQty = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    BonusQty = int.Parse(dr["BonusQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);
                    DefectQty = int.Parse(dr["DefectQty"].ToString(), System.Globalization.NumberStyles.AllowThousands);

                    grdDefect.Rows.Add(++i,                                  //'0)Row 수
                                    dr["KDefect"].ToString(),                //'1)불량명
                                    dr["EDefect"].ToString(),                //'2)영문명
                                    dr["TagName"].ToString(),                //'3)Tag
                                    dr["YPos"].ToString(),                   //'4)위치
                                    dr["Demerit"].ToString(),                //'5)벌점
                                    string.Format("{0:n0}", BonusQty),       //'6)보상
                                    string.Format("{0:n0}", DefectQty)       //'7)불량수량
                                    );
                }
                grdDefect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }
        #endregion
        #region 검색버튼 클릭 시 // 탭에 따라 다르게 검색
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int i = tabdgv.SelectedIndex;
            switch (i)
            {
                case 0:
                    FillGridData();
                    break;
                case 1:
                    FillGridLotNo();
                    break;
            }            
        }
        #endregion
        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            if (btnName == mtb_From.Name)
            {
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");
            }
            else if (btnName == mtb_To.Name)
            {
                mtb_To.Text = dateTime.ToString("yyyy-MM-dd");
            }

        }
        #endregion
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_tins_Calendar calendar = new WizCommon.Popup.Frm_tins_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_tins_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region BoxID 체크박스 클릭 시 BoxID 텍스트박스 Enable 변경
        private void chkBoxID_Click(object sender, EventArgs e)
        {
            if (chkBoxID.Checked)
            {
                IsOK = true;
                txtBoxID.Enabled = true;
                txtBoxID.Text = "";
                txtBoxID.Focus();
            }
            else
            {
                IsOK = false;
                chkBoxID.Focus();
                txtBoxID.Enabled = false;
            }           
        }
        #endregion
        #region Machine 체크박스 클릭 시 검사호기 콤보박스 Enable 변경
        private void chkMachine_Click(object sender, EventArgs e)
        {
            if (chkMachine.Checked)
            {
                cboMachine.Enabled = true;
            }
            else
            {
                cboMachine.SelectedIndex = 0;
                cboMachine.Enabled = false;
            }
        }
        #endregion
        #region Machine 체크박스 클릭 시 관리번호 텍스트박스 Enable 변경
        private void chkOrderID_Click(object sender, EventArgs e)
        {
            if (chkOrderID.Checked)
            {
                IsOK = true;
                txtOrderID.Enabled = true;
                txtOrderID.Text = "";   
                txtOrderID.Focus();
            }
            else
            {
                IsOK = false;
                txtOrderID.Text = "전체";
                txtOrderID.Enabled = false;
            }
        }
        #endregion
        #region 현재 그리드뷰의 row up
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (tabdgv.SelectedIndex == 0)
            {
                Lib.btnRowUp(grdData);
            }
            else if((tabdgv.SelectedIndex == 1))
            {
                Lib.btnRowUp(grdLotNo);
            }
        }
        #endregion
        #region 현재 그리드뷰의 row down
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (tabdgv.SelectedIndex == 0)
            {
                Lib.btnRowDown(grdData);
            }
            else if ((tabdgv.SelectedIndex == 1))
            {
                Lib.btnRowDown(grdLotNo);
            }
        }
        #endregion
        #region grdData의 row 삭제 // 박스별 검사실적 그리드뷰
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                //선택된 행이 없을때
                if (grdData.SelectedRows.Count == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._202, "", 0, 1);
                    return;
                }
                //선택한 행을 삭제할지 물을때
                if (WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._211, "[확인]", 0, 0) == DialogResult.No)
                {
                    return;
                }
                int i = grdData.SelectedRows[0].Index;
                DataGridViewRow dgvr = grdData.Rows[i];
                string sOrderID = dgvr.Cells["OrderID"].Value.ToString();           //관리번호
                string nOrderSeq = dgvr.Cells["OrderSeq"].Value.ToString();         //OrderSeq-색상순위
                string nRollID = dgvr.Cells["RollSeq"].Value.ToString();            //RollID
                string sExamNo = dgvr.Cells["ExamNO"].Value.ToString();             //검사호기

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@OrderID", sOrderID);
                sqlParameter.Add("@RollSeq", nRollID);

                DataStore.Instance.ExecuteProcedure("xp_WizIns_dInspect", sqlParameter, true);

                FillGridData();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
        #endregion
        #region 탭컨트롤의 탭이 선택될때 마다의 이벤트
        private void tabdgv_Click(object sender, EventArgs e)
        {
            if (tabdgv.SelectedIndex == 0)
            {
                chkBoxID.Enabled = true;
                txtBoxID.Enabled = true;
                btnEdit.Visible = false;
                btnDelete.Visible = true;
                pnlDefect.Visible = false;
                pnlDefect.Enabled = true;

                FillGridData();
            }
            else if (tabdgv.SelectedIndex == 1)
            {
                chkBoxID.Enabled = false;
                txtBoxID.Enabled = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                pnlDefect.Visible = false;
                pnlDefect.Enabled = false;

                FillGridLotNo();
            }
        }
        #endregion
        #region 관리번호 텍스트박스에 포커스가 왔을때 숫자 키패드 띄움
        private void txtOrderID_GotFocus(object sender, EventArgs e)
        {
            if (!IsOK)
            { return; }
            if (chkOrderID.Checked)
            {
                IsOK = false;
                WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad("", "관리번호");
                WizCommon.Popup.Frm_CMNumericKeypad.KeypadStr = txtOrderID.Text.Trim();
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    txtOrderID.Text = FK.tbInputText.Text;
                    chkOrderID.Focus();
                }
                else
                {
                    IsOK = true;

                    txtOrderID.Text = string.Empty;
                    txtOrderID.Enabled = false;
                    chkOrderID.Checked = false;
                    chkOrderID.Focus();
                }
            }
        }
        #endregion
        #region BoxID 텍스트박스에 포커스가 왔을때 문자 키패드 띄움
        private void txtBoxID_GotFocus(object sender, EventArgs e)
        {
            if (!IsOK)
            { return; }
            if (chkBoxID.Checked)
            {
                IsOK = false;
                WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad("BOX ID 검색", "BOX ID");
                WizCommon.Popup.Frm_CMKeypad.KeypadStr = txtBoxID.Text.Trim();
                if (FK.ShowDialog() == DialogResult.OK)
                {
                    txtBoxID.Text = FK.tbInputText.Text;
                    //ScanLotNo();
                    //ScahBarcode();
                    chkBoxID.Focus();
                }
                else
                {
                    IsOK = true;

                    chkBoxID.Focus();
                    txtBoxID.Text = string.Empty;
                    chkBoxID.Checked = false;
                    txtBoxID.Enabled = false;
                }
            }
            else
            {
                chkBoxID.Focus();
            }
        }
        #endregion
        #region 검사일자 체크박스 클릭 시 From,To 텍스트박스, 달력버튼 Enable 변경
        private void chkInsDate_Click(object sender, EventArgs e)
        {
            if (chkInsDate.Checked)
            {
                mtb_From.Enabled = true;
                btnCal_From.Enabled = true;
                mtb_To.Enabled = true;
                btnCal_To.Enabled = true;
            }
            else
            {
                mtb_From.Enabled = false;
                btnCal_From.Enabled = false;
                mtb_To.Enabled = false;
                btnCal_To.Enabled = false;
            }
        }
        #endregion
        #region 현재 전역변수의 OrderID 기준으로 전역변수 m_tIns, m_tInsSub 채움 왜있는지 모르겠....
        private void LoadInspectData()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                string sOrderID = Frm_tins_Main.g_tBase.uOrderID;
                string nRollID = Frm_tins_Main.g_tBase.uRollSeq;
                sqlParameter.Add("@sOrderID", sOrderID);
                sqlParameter.Add("@nRollSeq", nRollID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectOne", sqlParameter, false);

                if (dt.Rows.Count == 0)
                { return; }
                else if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    Frm_tins_Main.g_tIns.OrderID = dr["OrderID"].ToString();//      ' 관리 번호
                    Frm_tins_Main.g_tIns.RollSeq = dr["RollSeq"].ToString();//        ' 일련 순위
                    Frm_tins_Main.g_tIns.OrderSeq = dr["OrderSeq"].ToString();//       ' 색상 번호
                    Frm_tins_Main.g_tIns.ExamNo = dr["ExamNO"].ToString();//       ' 검사 호기
                    Frm_tins_Main.g_tIns.ExamDate = dr["ExamDate"].ToString();//     ' 검사 일자
                    Frm_tins_Main.g_tIns.ExamTime = dr["ExamTime"].ToString();//     ' 검사 시간
                    Frm_tins_Main.g_tIns.TeamID = dr["TeamID"].ToString();//   ' 검사 조
                    Frm_tins_Main.g_tIns.PersonID = dr["PersonID"].ToString();//   ' 검사자 코드
                    Frm_tins_Main.g_tIns.RealQty = dr["RealQty"].ToString();//      ' 실제검사 수량
                    Frm_tins_Main.g_tIns.CtrlQty = dr["CtrlQty"].ToString();//      ' 조정검사 수량
                    //Frm_tins_Main.g_tIns.SampleQty = dr["SampleQty"].ToString();//    ' 견본 수량
                    //Frm_tins_Main.g_tIns.LossQty = dr["LossQty"].ToString();//      ' 보상 수량
                    //Frm_tins_Main.g_tIns.CutQty = dr["CutQty"].ToString();//       ' 난단 수량
                    Frm_tins_Main.g_tIns.UnitClss = dr["UnitClss"].ToString();//       ' 검사 단위
                    //Frm_tins_Main.g_tIns.Density = dr["Density"].ToString();//      ' 원단 밀도
                    Frm_tins_Main.g_tIns.GradeID = dr["GradeID"].ToString();// 등급
                    Frm_tins_Main.g_tIns.LotNo = dr["LotNo"].ToString();//        ' Lot NO
                    Frm_tins_Main.g_tIns.DefectID = dr["DefectID"].ToString();//   ' 대표불량 코드
                    Frm_tins_Main.g_tIns.Defect = dr["KDefect"].ToString();//      ' 대표불량명
                    Frm_tins_Main.g_tIns.DefectClss = dr["DefectClss"].ToString();//      ' 불량 종류
                    //Frm_tins_Main.g_tIns.CutDefectID = dr["CutDefectID"].ToString();// ' 난단 대표불량 코드
                    //Frm_tins_Main.g_tIns.CutDefectClss = dr["CutDefectClss"].ToString();//     ' 난단 대표불량 불량 종류
                    Frm_tins_Main.g_tIns.DefectQty = dr["DefectQty"].ToString();//    ' 불량 갯수
                    Frm_tins_Main.g_tIns.DefectPoint = dr["DefectPoint"].ToString();//  ' 불량 점수
                    Frm_tins_Main.g_tIns.ReworkClss = dr["ReworkClss"].ToString();//   ' 재작업 여부,  S_201110_조일_02 에 따른 추가
                    Frm_tins_Main.g_tIns.BoxID = dr["BoxID"].ToString();//
                    Frm_tins_Main.g_tIns.InstID = dr["InstID"].ToString();//
                }
                else
                {
                    return;
                }

                DataTable dtSub = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspectSub", sqlParameter, false);
                if (int.Parse(Frm_tins_Main.g_tIns.DefectQty) > 0)
                {
                    foreach (DataRow dr in dtSub.Rows)
                    {
                        InsView.TInspectSub m_tInsSub = new InsView.TInspectSub();
                        m_tInsSub.OrderID = dr["OrderID"].ToString();                                           // ' 관리 번호
                        m_tInsSub.DefectSeq = dr["DefectSeq"].ToString();                                       // ' 불량 순위
                        m_tInsSub.DefectID = dr["DefectID"].ToString();                                         // ' 불량 코드
                        m_tInsSub.KDefect = dr["KDefect"].ToString();                                           //  ' 불량 명(한글)
                        m_tInsSub.EDefect = dr["EDefect"].ToString();                                           //	' 불량 명(영문)
                        m_tInsSub.TagName = dr["TagName"].ToString();                                           // ' Tag Name
                        m_tInsSub.YPos = dr["YPos"].ToString();                                                 // '수직위치
                        m_tInsSub.Demerit = dr["Demerit"].ToString();                                           // ' 감점
                        m_tInsSub.BonusQty = string.Format("{0:n1}", int.Parse(dr["BonusQty"].ToString()));		// "#,#00.0")
                        m_tInsSub.nDefectQty = string.Format("{0:n0}", int.Parse(dr["BonusQty"].ToString()));   // "#,#00")
                        Frm_tins_Main.list_g_tInsSub.Add(m_tInsSub);
                    }
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }

        }
        #endregion
        #region 
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //선택된 행이 없을때
            if (grdData.SelectedRows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._202, "", 0, 1);
                return;
            }
            //WizWorkLib.
        }
        #endregion 
        #region BOX별 검사실적 row변경시마다 불량개수가 1개 이상이고, 조회 결과 1개이상인 경우 해당 row 위에 그리드뷰 띄움 
        private void grdData_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdData.Rows.Count == 0)
                {
                    pnlDefect.Visible = false;
                    return;
                }
                if (grdData.SelectedRows.Count == 0 || grdData.SelectedRows.Count > 1)
                {
                    pnlDefect.Visible = false;
                    return;
                }
                                
                int rowIndex = grdData.SelectedRows[0].Index;
                int colIndex = grdData.Columns["CtrlQty"].Index;
                int DefectQty = int.Parse(grdData.Rows[rowIndex].Cells["DefectQty"].Value.ToString());
                if (DefectQty == 0)
                {
                    pnlDefect.Visible = false;
                    return;
                }
                else
                {
                    pnlDefect.Visible = true;
                }
                FillGridDefect();
                if (grdDefect.Rows.Count == 0)
                {
                    pnlDefect.Visible = false;
                    return;
                }
                else
                {
                    pnlDefect.Location = grdData.GetCellDisplayRectangle(colIndex, rowIndex, true).Location;
                    pnlDefect.BringToFront();
                    grdDefect.Visible = true;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #endregion
    }
}

