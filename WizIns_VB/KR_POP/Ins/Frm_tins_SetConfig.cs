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
using DevAge.Windows.Forms;
using WizCommon;

namespace WizIns
{
    public partial class Frm_tins_SetConfig : Form
    {
        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();
        WizWorkLib Lib = new WizWorkLib();

        public Frm_tins_SetConfig()
        {
            InitializeComponent();
        }

        private void Frm_tins_SetConfig_Load(object sender, EventArgs e)
        {
            InitGrid();
            SetScreen();
            //    Call SetInfo(1, Me)

            //전역변수 TagID가 없을때
            if (Frm_tins_Main.g_tBase.TagID == "")
            {
                foreach (Control con in tlp_sLabel.Controls)
                {
                    if (con is RadioButton)
                    {
                        RadioButton rbn = con as RadioButton;
                        rbn.Checked = true;
                        break;
                    }
                }
            }
            //전역변수 TagID가 있을때
            else
            {
                foreach (Control con in tlp_sLabel.Controls)
                {
                    if (con is RadioButton)
                    {
                        if (con.Tag.ToString() == Frm_tins_Main.g_tBase.TagID)
                        {
                            RadioButton rbn = con as RadioButton;
                            rbn.Checked = true;
                            break;
                        }
                    }
                }
            }

            //전역변수 TeamID가 없을때
            if (Frm_tins_Main.g_tBase.TeamID == "")
            {
                foreach (Control con in tlp_sTeam.Controls)
                {
                    if (con is RadioButton)
                    {
                        RadioButton rbn = con as RadioButton;
                        rbn.Checked = true;
                        break;
                    }
                }
            }
            //전역변수 TeamID가 있을때
            else
            {
                foreach (Control con in tlp_sTeam.Controls)
                {
                    if (con is RadioButton)
                    {
                        if (con.Tag.ToString() == Frm_tins_Main.g_tBase.TeamID)
                        {
                            RadioButton rbn = con as RadioButton;
                            rbn.Checked = true;
                            break;
                        }
                    }
                }
            }

            //전역변수 검사방법이 없을때
            if (Frm_tins_Main.g_tBase.InspectType == "")
            {
                foreach (Control con in tlp_sInspect.Controls)
                {
                    if (con is RadioButton)
                    {
                        RadioButton rbn = con as RadioButton;
                        rbn.Checked = true;
                        break;
                    }
                }
            }
            //전역변수 검사방법이 있을때
            else
            {
                foreach (Control con in tlp_sInspect.Controls)
                {
                    if (con is RadioButton)
                    {
                        if (con.Tag.ToString() == Frm_tins_Main.g_tBase.InspectType)
                        {
                            RadioButton rbn = con as RadioButton;
                            rbn.Checked = true;
                            break;
                        }
                    }
                }
            }


            //Call FillGrid


        }
        private void SetScreen()
        {
            tlp_lblSetPos.Visible = false; // 필요할때 화면 만들고나서 사용
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.SetRowSpan(tlp_sLabel, 2);//라벨종류 밑에칸 전체 다 합치기
            SetRadioTag();//라벨종류 세팅
            SetRadioTeam();//작업조 세팅
            SetRadioInsepct();//검사방법 세팅


            foreach (Control ct in tlp_Main.Controls)//메인 tlp
            {
                ct.Dock = DockStyle.Fill;//패널 or tlp dockstyle.fill
                if (ct is Panel)
                {
                    Panel pnl = ct as Panel;
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                }
                if (ct is TableLayoutPanel)
                {
                    foreach (Control con in ct.Controls)
                    {
                        if (con is Button)
                        {
                            Button btn = con as Button;
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (con is RadioButton)
                        {
                            RadioButton btn = con as RadioButton;
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.CheckedBackColor = Color.MediumSeaGreen;
                        }
                        con.Font = new Font("맑은 고딕", 15f, FontStyle.Regular);
                        con.Dock = DockStyle.Fill;

                        if (ct.Name.ToLower().Contains("label"))
                        {
                            con.BackColor = Color.DeepSkyBlue;
                        }
                        else if (ct.Name.ToLower().Contains("inspect"))
                        {
                            con.BackColor = Color.MediumSlateBlue;
                        }
                        else if (ct.Name.ToLower().Contains("lbl"))
                        {
                            con.BackColor = Color.OrangeRed;
                        }
                        else if (ct.Name.ToLower().Contains("team"))
                        {
                            con.BackColor = Color.Fuchsia;
                        }


                    }
                }
                foreach (Control ctl in ct.Controls)//메인 tlp 밑의 tlp 밑의 컨트롤들
                {
                    ctl.Dock = DockStyle.Fill;

                    tlp_sPerson.SetColumnSpan(pnlCombo, 2);
                    tlp_sPerson.SetColumnSpan(pnlgrdData, 2);
                    if (ctl is Panel)//대부분의 경우 패널
                    {
                        Panel pnl = ctl as Panel;
                        pnl.BorderStyle = BorderStyle.FixedSingle;


                        foreach (Control con in ctl.Controls)//패널 밑의 컨트롤
                        {
                            con.Dock = DockStyle.Fill;
                        }

                    }

                }
            }
            //콤보박스세팅
            SetComboBox();
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 3;

            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PersonID";
            grdData.Columns[i].HeaderText = "코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Name";
            grdData.Columns[i].HeaderText = "검사자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            //grdData.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 10F);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
            }
        }
        //그리드에 콤보박스 인덱스(부서)에 따른 사용자 세팅
        private void FillGrid()
        {
            string DepartID = Lib.FindComboBoxID(cboDepart, list_cbx);
            //DB에서 사용자 불러와서 DT로 넣음
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@DepartID", DepartID);
            sqlParameter.Add("@ChkPersonID", "0");
            sqlParameter.Add("@PersonID", "");
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizTerm_sPerson", sqlParameter, false);

            grdData.DataSource = dt;
            int i = 1;
            foreach (DataGridViewRow dgvr in grdData.Rows)
            {
                dgvr.Cells["RowSeq"].Value = i.ToString();
                i++;
            }
        }

        //라벨종류 라디오버튼 세팅
        private void SetRadioTag()
        {
            //사용가능 Tag 조회
            //DB에서 사용자 불러와서 DT로 넣음
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@nChkUseAll", "1");
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sBarCodeTag", sqlParameter, false);
            //
            int i = 6;
            if (i < dt.Rows.Count)
            {
                i = dt.Rows.Count;
            }
            Lib.SetLayout(tlp_sLabel, 1, i);

            int c = 0;
            foreach (DataRow dr in dt.Rows)
            {
                RadioButton rbn = new RadioButton();
                rbn.Appearance = Appearance.Button;
                rbn.Tag = dr["TagID"].ToString();
                rbn.Text = dr["Tag"].ToString();
                rbn.Dock = DockStyle.Fill;
                tlp_sLabel.Controls.Add(rbn, 0, c);
                c++;
            }
        }

        //작업조 라디오버튼 세팅
        private void SetRadioTeam()
        {
            //사용가능 Team 조회
            //DB에서 사용자 불러와서 DT로 넣음

            DataTable dt = Lib.GetCode(WizWorkLib.CodeTypeClss.CD_TEAM);
            int i = 6;
            if (i < dt.Rows.Count)
            {
                i = dt.Rows.Count;
            }
            Lib.SetLayout(tlp_sTeam, 1, i);

            int c = 0;
            foreach (DataRow dr in dt.Rows)
            {

                RadioButton rbn = new RadioButton();
                rbn.Appearance = Appearance.Button;
                rbn.Tag = dr["TeamID"].ToString();
                rbn.Text = dr["Team"].ToString();
                rbn.Dock = DockStyle.Fill;
                tlp_sTeam.Controls.Add(rbn, 0, c);
                c++;
            }
        }

        //검사방법 라디오버튼 세팅
        private void SetRadioInsepct()
        {
            Lib.SetLayout(tlp_sInspect, 1, 6);
            
            string[] InsName = { "단순 검사", "불량 검사" };
            string[] InsID = { "0", "1" };

            for (int i = 0; i < InsID.Length; i++)
            {
                RadioButton rbn = new RadioButton();
                rbn.Appearance = Appearance.Button;
                rbn.Tag = InsID[i];
                rbn.Text = InsName[i];
                rbn.Dock = DockStyle.Fill;
                tlp_sInspect.Controls.Add(rbn, 0, i);
            }
        }


        //콤보박스에 Depart 부서 셋팅
        private void SetComboBox()
        {
            try
            {
                DataTable dt = Lib.GetCode(WizWorkLib.CodeTypeClss.CD_DEPART);

                //컬럼명 변경 ID와, NAME으로
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.ToUpper().Contains("ID"))
                    {
                        dc.ColumnName = "ID";
                    }
                    else //if (dc.ColumnName.ToUpper().Contains("NAME"))
                    {
                        dc.ColumnName = "NAME";
                    }
                }

                //패널크기에 따른 콤보박스 사이즈 및 폰트사이즈 변경
                Lib.SetComboBox(cboDepart, dt, list_cbx);
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
                    cboDepart.SelectedIndex = 0;
                }
                //전역변수에 Depart가 있을 시 해당 값을 가진 Index 선택
                else
                {
                    cboDepart.SelectedIndex = Convert.ToInt32(Lib.FindComboBoxIdx(cboDepart, list_cbx, Frm_tins_Main.g_tBase.DepartID));
                }              
            }
            
        }
        //폼 닫기
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //콤보 인덱스 변경 시 재조회
        private void cboDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        //그리드뷰 위로 버튼
        private void btnUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }
        //그리드뷰 아래 버튼
        private void btnDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }
        //선택버튼 클릭 시(전역변수에 값 할당)
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //콤보박스의 부서를 선택안했을때
            if (Lib.FindComboBoxID(cboDepart, list_cbx) == "")
            {
                MessageBox.Show("부서를 선택해주세요.");
                return;
            }
            if (grdData.Rows.Count == 0 || grdData.CurrentRow is null)
            {
                MessageBox.Show("선택된 사용자가 없습니다. 사용자를 선택해주세요.");
                return;
            }

            //전역변수에 라벨종류 저장
            foreach (Control con in tlp_sLabel.Controls)
            {
                if (con is RadioButton)
                {
                    RadioButton rbn = con as RadioButton;
                    if (rbn.Checked)
                    {
                        Frm_tins_Main.g_tBase.TagID = rbn.Tag.ToString();
                        Frm_tins_Main.g_tBase.TagName = rbn.Text.ToString();
                        break;
                    }
                }
            }
            //전역변수에 검사방법 저장
            foreach (Control con in tlp_sInspect.Controls)
            {
                if (con is RadioButton)
                {
                    RadioButton rbn = con as RadioButton;
                    if (rbn.Checked)
                    {
                        Frm_tins_Main.g_tBase.InspectType = rbn.Tag.ToString();
                        Frm_tins_Main.g_tBase.InspectTypeName = rbn.Text.ToString();
                        break;
                    }
                }
            }
            //전역변수에 작업조 저장
            foreach (Control con in tlp_sTeam.Controls)
            {
                if (con is RadioButton)
                {
                    RadioButton rbn = con as RadioButton;
                    if (rbn.Checked)
                    {
                        Frm_tins_Main.g_tBase.TeamID = rbn.Tag.ToString();
                        Frm_tins_Main.g_tBase.Team = rbn.Text.ToString();
                        break;
                    }
                }
            }

            //전역변수에 DepartID 저장
            Frm_tins_Main.g_tBase.DepartID = Lib.FindComboBoxID(cboDepart, list_cbx);
            //전역변수에 사용자 저장
            Frm_tins_Main.g_tBase.PersonID = grdData.SelectedRows[0].Cells["PersonID"].Value.ToString();
            Frm_tins_Main.g_tBase.Person = grdData.SelectedRows[0].Cells["Name"].Value.ToString();

            this.Close();
        }
    }
}

