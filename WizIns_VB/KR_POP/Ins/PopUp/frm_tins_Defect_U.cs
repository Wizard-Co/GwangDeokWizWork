using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // DllImport
using WizCommon;

namespace WizIns
{
    public partial class frm_tins_Defect_U : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        string m_sOrderID;                                                      //' OrderID (관리번호)
        int m_sSearchIndex;                                                      //' 넘어온 검색 값
        string m_TextName;                                                       //   ' 넘어온 값
        InsView.TInspect m_tIns = new InsView.TInspect();         //검사 실적
        List<InsView.TInspectSub> list_m_tInsSub = new List<InsView.TInspectSub>();//검사 실적

        List<CB_IDNAME> list_cbx = new List<CB_IDNAME>();
        DataTable dt_DefectType = null;//불량내용 테이블 전역변수 , 대표불량 콤보박스에 추가 할때 가져다 쓴다.
        /// <summary>
        /// 생성
        /// </summary>
        public frm_tins_Defect_U()
        {
            InitializeComponent();
        }
        private void SetScreen()
        {
            tlpMain.SetRowSpan(tlpRight, 2);
            foreach (Control control in this.Controls)//control >> tlpTop, tlpRight, tlpReason
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control ctrl in control.Controls)//button , tlpExamNoQty
                {
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control ctl in ctrl.Controls)
                    {
                        ctl.Dock = DockStyle.Fill;
                        ctl.Margin = new Padding(0, 0, 0, 0);
                    }
                }
            }
        }

        private void InitDemeritButton()
        {
            //            Private Sub InitDemeritButton()

            //    On Error GoTo err_rtn


            //    Select Case g_tBase.BasisID
            //        Case MT_10POINT:
            //            cmdDemerit(0).Caption = " 3 점"
            //            cmdDemerit(1).Caption = " 5 점"
            //            cmdDemerit(2).Caption = " 7 점"
            //            cmdDemerit(3).Caption = "10 점"
            //        Case MT_4POINT:
            //            cmdDemerit(0).Caption = " 1 점"
            //            cmdDemerit(1).Caption = " 2 점"
            //            cmdDemerit(2).Caption = " 3 점"
            //            cmdDemerit(3).Caption = " 4 점"
            //        Case MT_10P_COUNT:
            //            cmdDemerit(0).Caption = " 1 점"
            //            cmdDemerit(1).Caption = " 3 점"
            //            cmdDemerit(2).Caption = " 5 점"
            //            cmdDemerit(3).Caption = "10 점"
            //        Case MT_4P_COUNT:
            //            cmdDemerit(0).Caption = " 1 점"
            //            cmdDemerit(1).Caption = " 2 점"
            //            cmdDemerit(2).Caption = " 3 점"
            //            cmdDemerit(3).Caption = " 4 점"
            //    End Select


            //    Exit Sub


            //err_rtn:

            //            Call ErrorBox(Err.Number, "frm_tins_Defect_U.InitDemeritButton", Err.Description)    
            //End Sub
        }

        private void FillGridDefect()
        {
            try
            {
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sDefectSub", null, false);
                int nPos;//, nRow, nCol;
                int i = 0;
                int Horizontal = Frm_tins_Main.g_tSet.ButtonX;

                foreach (DataRow dr in dt.Rows)
                {
                    nPos = int.Parse(dr["ButtonSeq"].ToString()) - 1;
                    //nRow = int.Parse((float.Parse(nPos.ToString()) / float.Parse(Frm_tins_Main.g_tSet.ButtonX.ToString())).ToString());
                    //nCol = nPos % Frm_tins_Main.g_tSet.ButtonX;

                    InsView.TDefect m_aDefect = new InsView.TDefect();
                    m_aDefect.DefectID = dr["DefectID"].ToString();
                    m_aDefect.Display = dr["Display1"].ToString() + "\r\n" + dr["Display2"].ToString() + dr["Display3"].ToString();
                    m_aDefect.KDefect = dr["KDefect"].ToString();
                    m_aDefect.EDefect = dr["EDefect"].ToString();
                    m_aDefect.TagName = dr["TagName"].ToString();
                    m_aDefect.DefectClss = dr["DefectClss"].ToString();
                    m_aDefect.ButtonSeq = nPos.ToString();
                    m_aDefect.DefectCnt = "0";
                    m_aDefect.nPos = nPos.ToString();


                    RadioButton newRbn = new RadioButton();
                    if (dr["DEFECTCLSS"].ToString() == "1")
                    {
                        newRbn.BackColor = System.Drawing.Color.White;
                    }
                    else if (dr["DEFECTCLSS"].ToString() == "2")
                    {
                        newRbn.BackColor = System.Drawing.Color.LightGray;
                    }
                    newRbn.Appearance = System.Windows.Forms.Appearance.Button;
                    newRbn.Text = m_aDefect.Display;
                    newRbn.Tag = m_aDefect.nPos.ToString();
                    newRbn.Dock = DockStyle.Fill;
                    newRbn.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                    newRbn.Click += new System.EventHandler(this.SelectReasonBtn);

                    Frm_tins_Main.list_g_tDef.Add(m_aDefect);
                    tlpReason.Controls.Add(newRbn, (i % Horizontal), (i / Horizontal));
                    i++;
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }


        public frm_tins_Defect_U(int intDefectQty)
        {
            InitializeComponent();
        }

        private void frm_tins_Defect_U_Load(object sender, EventArgs e)
        {
            SetTlpTop();
            Frm_tins_Main main = (this.Owner).MdiParent as Frm_tins_Main;
            main.setInfo();
            ShowDefect();
            SetDT_DefectType();
            //Call SetComboBox
            if (Frm_tins_Main.g_nEdit == InsView.EEdit.MT_ADDNEW)
            {
                m_sOrderID = Frm_tins_Main.g_tBase.OrderID;
                list_m_tInsSub = Frm_tins_Main.list_g_tInsSub; //필요없어보임
                pnlExamNoRoll.Text = "박스당수량: " + Frm_tins_Main.g_tBase.WorkQty;
                pnlExamNoQty.Text = "수량 : " + Frm_tins_Main.g_tBase.OrderQty;
                pnlCtrlQty.Text = Frm_tins_Main.g_tBase.WorkQty;//' 박스당수량  //'old: g_tBase.WorkQty
            }
            else if (Frm_tins_Main.g_nEdit == InsView.EEdit.MT_UPDATE)
            {
                m_sOrderID = Frm_tins_Main.g_tBase.uOrderID;
                pnlExamNoRoll.Text = Frm_tins_Main.g_tBase.WorkQty;
                pnlExamNoQty.Text = Frm_tins_Main.g_tBase.OrderQty;
                pnlCtrlQty.Text = Frm_tins_Main.g_tBase.WorkQty;//         ' 박스당수량 
            }

        }
        private void SetDT_DefectType()
        {
            
            dt_DefectType  = Lib.GetCode(WizWorkLib.CodeTypeClss.CD_DEFECT);

            //컬럼명 변경 ID와, NAME으로
            foreach (DataColumn dc in dt_DefectType.Columns)
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
        }
        private void InitTlpReason()
        {
            int intHorizontal = Convert.ToInt16(Frm_tins_Main.g_tSet.ButtonX);
            int intVertical = Convert.ToInt16(Frm_tins_Main.g_tSet.ButtonY);
            SetLayout(intHorizontal, intVertical);  // ini 설정값으로 창 변경
        }

        private void ShowDefect()
        {
            InitTlpReason();
            FillGridDefect();

            int i = int.Parse(Frm_tins_Main.g_tBase.DefectCnt);
            if (i > 0)//왜 있는지 모르겠는 로직..
            {

                //If g_tBase.DefectCnt > 0 Then
                //    With grdDefect
                //        For i = 0 To g_tBase.DefectCnt - 1
                //            nPos = g_tInsSub(i).ButtonSeq
                //            nRow = Int(nPos / g_tSet.ButtonX)
                //            nCol = nPos Mod g_tSet.ButtonX


                //            m_aDefect(nPos).DefectCnt = m_aDefect(nPos).DefectCnt + 1
                //            .TextMatrix(nRow, nCol) = Mid(.TextMatrix(nRow, nCol), 1, Len(.TextMatrix(nRow, nCol)) - IIf(m_aDefect(nPos).DefectCnt > 1, 3, 2)) & Format(m_aDefect(nPos).DefectCnt, "@@@")
                //        Next i
                //    grdDefect.Select nRow, nCol
                //    End With
                //End If
            }
        }

        private void SetTlpTop()
        {
            lblOrderID.Text = Frm_tins_Main.g_tBase.OrderID;
            lblOrderNO.Text = Frm_tins_Main.g_tBase.OrderNo;
            lblCustom.Text = Frm_tins_Main.g_tBase.Custom;
            lblArticle.Text = Frm_tins_Main.g_tBase.Article;
            lblOrderQty.Text = Frm_tins_Main.g_tBase.OrderQty;
            lblWorkQty.Text = Frm_tins_Main.g_tBase.WorkQty;
            lblUnitClss.Text = Frm_tins_Main.g_tBase.UnitClss;
            lblOrderSeq.Text = Frm_tins_Main.g_tBase.OrderSeq;
        }
        /// <summary>
        /// DB의 설정값으로 칸 나눔
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        void SetLayout(int Horizontal, int Vertical)
        {
            while (tlpReason.ColumnCount != Horizontal)
            {
                if (tlpReason.ColumnCount > Horizontal)
                {
                    tlpReason.ColumnCount--;
                    tlpReason.ColumnStyles.RemoveAt(tlpReason.ColumnCount);
                }
                else if (tlpReason.ColumnCount < Horizontal)
                {
                    tlpReason.ColumnCount++;
                    tlpReason.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
                }
            }
            foreach (ColumnStyle style in tlpReason.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 100.0f / tlpReason.ColumnCount;
            }

            while (tlpReason.RowCount != Vertical)
            {
                if (tlpReason.RowCount > Vertical)
                {
                    tlpReason.RowCount--;
                    tlpReason.RowStyles.RemoveAt(tlpReason.RowCount);
                }
                else if (tlpReason.RowCount < Vertical)
                {
                    tlpReason.RowCount++;
                    tlpReason.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
                }
            }
            foreach (RowStyle style in tlpReason.RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 100.0f / tlpReason.RowCount;
            }
        }

        private void SelectReasonBtn(object sender, EventArgs e)
        {
            int nDefectQty;
            RadioButton rbtn = ((RadioButton)sender);
            WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad(rbtn.Text, "불량추가");
            FK.Owner = this;

            if (FK.ShowDialog() == DialogResult.OK)
            {
                nDefectQty = int.Parse(FK.tbInputText.Text);
            }
            else
            {
                return;
            }

            foreach (InsView.TDefect defect in Frm_tins_Main.list_g_tDef)
            {
                if (defect.nPos == rbtn.Tag.ToString())
                {
                    defect.DefectCnt = defect.DefectCnt + nDefectQty;
                    rbtn.Text = defect.Display + defect.DefectCnt;

                    InsView.TInspectSub inspectsub = new InsView.TInspectSub();
                    inspectsub.OrderID = Frm_tins_Main.g_tBase.OrderID;
                    inspectsub.DefectID = defect.DefectID;
                    inspectsub.KDefect = defect.KDefect;
                    inspectsub.EDefect = defect.EDefect;
                    inspectsub.TagName = defect.TagName;
                    inspectsub.Demerit = "0";
                    inspectsub.ButtonSeq = defect.ButtonSeq;

                    inspectsub.BonusQty = defect.Loss.ToString();
                    inspectsub.nDefectQty = nDefectQty.ToString();

                    if (nDefectQty > 0)
                    {
                        foreach (DataRow dr in dt_DefectType.Rows)
                        {
                            if (inspectsub.DefectID == dr["ID"].ToString())
                            {
                                CB_IDNAME cb = new CB_IDNAME();
                                cb.cbName = "cboDefect";
                                cb.ID = dr["ID"].ToString();
                                cb.NAME = dr["NAME"].ToString();
                                cb.Idx = list_cbx.Count;
                                list_cbx.Add(cb);
                            }
                        }
                    }
                    else if (nDefectQty == 0)
                    {
                        foreach (CB_IDNAME cb in list_cbx)
                        {
                            if (cb.ID == inspectsub.DefectID)
                            {
                                list_cbx.Remove(cb);
                                break;
                            }
                        }
                    }
                     
                    //cboDefect.AddItem m_aDefect(nPos -1).KDefect & Space(50) & m_aDefect(nPos - 1).DefectID
                    Frm_tins_Main.list_g_tInsSub.Add(inspectsub);

                    Frm_tins_Main.g_tBase.DefectCnt = (int.Parse(Frm_tins_Main.g_tBase.DefectCnt) + 1).ToString();
                    Frm_tins_Main.g_tBase.DLoss = (int.Parse(Frm_tins_Main.g_tBase.DLoss) + int.Parse(defect.Loss)).ToString();

                }
            }
            //tlpRight를 탭버튼으로 만들고 0,1번 탭이 있을때 1번탭에 버튼이 1점,2점,3점,4점 버튼이 있을때 추가할것
            //    Else '난단대표불량 입력
            //With grdDefect
            //    If.Row < 0 Or Len(.TextMatrix(.Row, .Col)) = 0 Then Exit Sub

            //   nRow = .Row
            //    nCol = .Col
            //    nPos = (nRow * g_tSet.ButtonX) + nCol + 1


            //    g_tBase.CutDefectID = m_aDefect(nPos - 1).DefectID
            //    g_tBase.CutDefectClss = m_aDefect(nPos - 1).DefectClss


            //    tabButton.Tab = 0
            //End With
            //End If

        }

        /// <summary>
        /// 나가기 버튼 클릭(UC_Qlt_ReasonMenuCell)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            //for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            //{
            //    if (newRbn[i].Text.Contains("\r\n") == true)
            //    {
            //        newRbn[i].Text = newRbn[i].Text.Substring(0, newRbn[i].Text.IndexOf("\r\n"));
            //    }
            //}   

        }
        private void InitForm()
        {
            //m_nMeter = 0;
            Frm_tins_Main.g_tBase.DefectCnt = "0";
            Frm_tins_Main.g_tBase.BCutQty = "0";
            Frm_tins_Main.g_tBase.FCutQty = "0";
            Frm_tins_Main.g_tBase.BSample = "0";
            Frm_tins_Main.g_tBase.FSample = "0";
            Frm_tins_Main.g_tBase.Loss = "0";
            Frm_tins_Main.g_tBase.DLoss = "0";
            Frm_tins_Main.g_tBase.CutDefectID = "";

            Frm_tins_Main.list_g_tInsSub = null;
            Frm_tins_Main.list_g_tDef = null;

            Frm_tins_Main.list_g_tInsSub = new List<InsView.TInspectSub>();      //Erase g_tInsSub
            Frm_tins_Main.list_g_tDef = new List<InsView.TDefect>();           //Erase m_aDefect
            FillGridDefect();
        }

        private bool SaveData()
        {
            try
            {
                //18.05.08 분할처리로직 사용안함
                ////'분할처리시 분할정보를 넣는다.
                //if (Frm_tins_Main.g_nEdit == InsView.EEdit.MT_ADDNEW || 
                //    Frm_tins_Main.g_nEdit == InsView.EEdit.MT_UPDATE)
                //{
                //    if (Lib.OnlyNumber(Frm_tins_Main.g_tBase.WorkQty) > Lib.OnlyNumber()
                //}
                //'g_tInsSub는 불량클릭 하는 곳에서 데이터가 저장된다.
                if (Frm_tins_Main.g_nEdit == InsView.EEdit.MT_ADDNEW)
                {
                    //m_sOrderID = Frm_tins_Main.g_tBase.OrderID;

                    m_tIns.OrderID = Frm_tins_Main.g_tBase.OrderID;
                    m_tIns.OrderSeq = "1";
                    m_tIns.ExamNo = Frm_tins_Main.g_tBase.ExamNO;               //' 검사 호기
                    m_tIns.ExamDate = DateTime.Today.ToString("yyyyMMdd");      // string.Format(Date, "YYYYMMDD")                  //' 검사 일자
                    m_tIns.ExamTime = DateTime.Now.ToString("HHmm");            //' 검사 시간
                    m_tIns.TeamID = Frm_tins_Main.g_tBase.TeamID;               //' 검사 조
                    m_tIns.PersonID = Frm_tins_Main.g_tBase.PersonID;           //' 검사자 코드
                    m_tIns.RealQty = pnlCtrlQty.Text;                           //  ' 오더수량 (20160322 박스당수량으로 변경)
                    m_tIns.CtrlQty = pnlCtrlQty.Text;                           //  ' 박스당수량  'old: g_tBase.WorkQty
                    m_tIns.SampleQty = "0";                                     //
                    m_tIns.LossQty = "0";                                       //
                    m_tIns.CutQty = "0";                                        //
                    m_tIns.UnitClss = Frm_tins_Main.g_tBase.UnitClss;           //' 단위
                    m_tIns.Density = "0";                                       //
                    //주석 풀어서 수정할것
                    //m_tIns.GradeID = ""; cboGrade.ItemData(cboGrade.ListIndex)      //'등급
                    //m_tIns.LotNo = Frm_tins_Main.g_tBase.QcLot;                 //
                    //주석 풀어서 수정할것
                    m_tIns.BoxID = Frm_tins_Main.g_tBase.LabelID;               //' 박스ID

                    int cnt = int.Parse(Frm_tins_Main.g_tBase.DefectCnt);
                    if (cnt > 0)
                    {
                        //기존 VB소스 수정. 에러날 여지가 있어서 수정
                        //for (int i = 0; i < cnt - 1; i++)
                        //{
                        //    m_tIns.DefectQty = m_tIns.DefectQty + Frm_tins_Main.list_g_tInsSub[i].nDefectQty;
                        //}
                        foreach (InsView.TInspectSub tis in Frm_tins_Main.list_g_tInsSub)
                        {
                            m_tIns.DefectQty = m_tIns.DefectQty + tis.nDefectQty;
                        }
                    }
                    else
                    {
                        m_tIns.DefectQty = "0";
                    }
                    m_tIns.DefectPoint = "0";                       //' 불량 점수
                    //주석 풀어서 수정할것
                    //m_tIns.DefectID = ""; cboGrade.Tag                  //' 대표불량 코드
                    //m_tIns.ReworkClss = "";                          //' 재작업 여부 (사용안함)
                    //주석 풀어서 수정할것
                    m_tIns.InstID = Frm_tins_Main.g_tBase.InstID;
                    m_tIns.CardID = "";
                    m_tIns.SplitID = "";

                    m_tIns.DivideQty = (Lib.OnlyNumber(Frm_tins_Main.g_tBase.WorkQty) - Lib.OnlyNumber(pnlCtrlQty.Text)).ToString();
                    m_tIns.CreateUserID = Frm_tins_Main.g_tBase.PersonID;

                    if (AddNewInspect(m_tIns, Frm_tins_Main.list_g_tInsSub, int.Parse(Frm_tins_Main.g_tBase.DefectCnt), Frm_tins_Main.g_tBase.RollClss,
                        int.Parse(Frm_tins_Main.g_tBase.WorkInspect), Frm_tins_Main.list_g_tBoxtransfer, Frm_tins_Main.g_lnBoxtransferQty))
                    { return true; }
                    else
                    {
                        return false;
                    }
                }
                else if (Frm_tins_Main.g_nEdit == InsView.EEdit.MT_UPDATE)
                {
                    m_tIns.OrderID = Frm_tins_Main.g_tBase.OrderID;
                    m_tIns.OrderSeq = "1";
                    m_tIns.RollSeq = Frm_tins_Main.g_tIns.RollSeq;
                    m_tIns.ExamNo = Frm_tins_Main.g_tBase.ExamNO;               //' 검사 호기
                    m_tIns.ExamDate = DateTime.Today.ToString("yyyyMMdd");      // string.Format(Date, "YYYYMMDD")                  //' 검사 일자
                    m_tIns.ExamTime = DateTime.Now.ToString("HHmm");            //' 검사 시간
                    m_tIns.TeamID = Frm_tins_Main.g_tBase.TeamID;               //' 검사 조
                    m_tIns.PersonID = Frm_tins_Main.g_tBase.PersonID;           //' 검사자 코드
                    m_tIns.RealQty = Frm_tins_Main.g_tBase.OrderQty;                           //  ' 오더수량 (20160322 박스당수량으로 변경)

                    m_tIns.CtrlQty = pnlCtrlQty.Text;                           //  ' 박스당수량  'old: g_tBase.WorkQty

                    m_tIns.SampleQty = "0";                                     //
                    m_tIns.LossQty = "0";                                       //
                    m_tIns.CutQty = "0";                                        //
                    m_tIns.UnitClss = Frm_tins_Main.g_tBase.UnitClss;           //' 단위
                    m_tIns.Density = "0";                                       //
                    //주석 풀어서 수정할것
                    //m_tIns.GradeID = ""; cboGrade.ItemData(cboGrade.ListIndex)      //'등급
                    //m_tIns.LotNo = Frm_tins_Main.g_tBase.QcLot;                 //
                    //주석 풀어서 수정할것
                    m_tIns.BoxID = Frm_tins_Main.g_tBase.BoxID;               //' 박스ID

                    int cnt = int.Parse(Frm_tins_Main.g_tBase.DefectCnt);
                    if (cnt > 0)
                    {
                        //기존 VB소스 수정. 에러날 여지가 있어서 수정
                        //for (int i = 0; i < cnt - 1; i++)
                        //{
                        //    m_tIns.DefectQty = m_tIns.DefectQty + Frm_tins_Main.list_g_tInsSub[i].nDefectQty;
                        //}
                        foreach (InsView.TInspectSub tis in Frm_tins_Main.list_g_tInsSub)
                        {
                            m_tIns.DefectQty = m_tIns.DefectQty + tis.nDefectQty;
                        }
                    }
                    else
                    {
                        m_tIns.DefectQty = "0";
                    }
                    m_tIns.DefectPoint = "0";                       //' 불량 점수
                    //주석 풀어서 수정할것
                    //m_tIns.DefectID = ""; cboGrade.Tag                  //' 대표불량 코드
                    //m_tIns.ReworkClss = "";                          //' 재작업 여부 (사용안함)
                    //주석 풀어서 수정할것
                    m_tIns.InstID = Frm_tins_Main.g_tBase.InstID;
                    m_tIns.CardID = "";
                    m_tIns.SplitID = "";
                    m_tIns.CreateUserID = Frm_tins_Main.g_tBase.PersonID;
                    m_tIns.DivideQty = (Lib.OnlyNumber(Frm_tins_Main.g_tBase.WorkQty) - Lib.OnlyNumber(pnlCtrlQty.Text)).ToString();


                    if (UpdateInspect(m_tIns, Frm_tins_Main.list_g_tInsSub, int.Parse(Frm_tins_Main.g_tBase.DefectCnt), Frm_tins_Main.g_tBase.RollClss,
                        Frm_tins_Main.list_g_tBoxtransfer, Frm_tins_Main.g_lnBoxtransferQty))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private bool AddNewInspect(InsView.TInspect TIns, List<InsView.TInspectSub> list_TInsSub, int DefectCnt, string sRollClss,
                        int WorkSFlag, List<InsView.TBoxTransfer> list_TBoxTransfer, long nTransCount)
        {
            try
            {
                List<Procedure> Prolist = new List<Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                sqlParameter1.Add("@OrderID", TIns.OrderID);
                sqlParameter1.Add("@RollSeq", 0);
                sqlParameter1.Add("@OrderSeq", TIns.OrderSeq);
                sqlParameter1.Add("@ExamNO", TIns.ExamNo);
                sqlParameter1.Add("@ExamDate", TIns.ExamDate);

                sqlParameter1.Add("@ExamTime", TIns.ExamTime);
                sqlParameter1.Add("@TeamID", TIns.TeamID);
                sqlParameter1.Add("@PersonID", TIns.PersonID);
                sqlParameter1.Add("@RealQty", TIns.RealQty);
                sqlParameter1.Add("@CtrlQty", TIns.CtrlQty);

                //sqlParameter.Add("@SampleQty", TIns.SampleQty          );
                //sqlParameter.Add("@LossQty", TIns.LossQty              );
                //sqlParameter.Add("@CutQty",  TIns.CutQty               );
                sqlParameter1.Add("@UnitClss", TIns.UnitClss);
                //sqlParameter.Add("@Density", TIns.Density              );

                sqlParameter1.Add("@GradeID", TIns.GradeID);
                sqlParameter1.Add("@LotNo", TIns.LotNo);
                sqlParameter1.Add("@BoxID", TIns.BoxID);
                sqlParameter1.Add("@DefectQty", TIns.DefectQty);
                sqlParameter1.Add("@DefectPoint", TIns.DefectPoint);

                sqlParameter1.Add("@DefectID", TIns.DefectID);
                //sqlParameter.Add("@CutDefectID", TIns.CutDefectID      );
                sqlParameter1.Add("@DefectClss", TIns.DefectClss);
                //sqlParameter.Add("@CutDefectClss", TIns.CutDefectClss  );
                sqlParameter1.Add("@InstID", TIns.InstID);

                //sqlParameter.Add("@CardID",			TIns.CardID      );
                //sqlParameter.Add("@SplitID",	TIns.SplitID		     );
                sqlParameter1.Add("@CardIDList", TIns.CardIDList);
                sqlParameter1.Add("@CreateUserID", TIns.CreateUserID);

                Procedure pro1 = new Procedure();
                pro1.Name = "xp_WizIns_iInspectFinal";
                pro1.OutputUseYN = "Y";
                pro1.OutputName = "RollSeq";
                pro1.OutputLength = "10";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter1);

                int nSubCount = 0;
                foreach (InsView.TInspectSub TInsSub in list_TInsSub)
                {
                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                    sqlParameter2.Add("@OrderID", TIns.OrderID);
                    sqlParameter2.Add("@RollSeq", TIns.RollSeq);
                    sqlParameter2.Add("@DefectSeq", ++nSubCount);
                    sqlParameter2.Add("@DefectID", TInsSub.DefectID);
                    sqlParameter2.Add("@YPos", TInsSub.YPos);

                    sqlParameter2.Add("@Demerit", TInsSub.Demerit);
                    sqlParameter2.Add("@BonusQty", TInsSub.BonusQty);           //'보상
                    sqlParameter2.Add("@DefectQty", TInsSub.nDefectQty);        //'불량수량
                    sqlParameter2.Add("@PersonID", TIns.PersonID);              //'수정자

                    Procedure pro2 = new Procedure();
                    pro2.Name = "xp_WizIns_iInspectSub";
                    pro2.OutputUseYN = "N";
                    pro2.OutputName = "RollSeq";
                    pro2.OutputLength = "10";

                    Prolist.Add(pro2);
                    ListParameter.Add(sqlParameter2);
                }

                if (nTransCount > 0)
                {
                    foreach (InsView.TBoxTransfer TBoxTransfer in list_TBoxTransfer)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                        sqlParameter3.Add("@BoxID", TBoxTransfer.BoxID);                        //'From Box ID
                        sqlParameter3.Add("@GetBoxID", TBoxTransfer.GetBoxID);                  //'From Box ID
                        sqlParameter3.Add("@DivideBoxID", TBoxTransfer.DivideBoxID);            //'To Box ID
                        sqlParameter3.Add("@WorkDate", TIns.ExamDate);
                        sqlParameter3.Add("@WorkTime", TIns.ExamTime);
                        sqlParameter3.Add("@nBeforeGetBoxQty", TBoxTransfer.nBeforeGetBoxQty);   //'이송처리전의 From Box 의 장입량
                        sqlParameter3.Add("@nTransferQty", TBoxTransfer.nTransferQty);          	//'이송처리하는 수량
                        sqlParameter3.Add("@sUserID", TIns.PersonID);           					//'수정자

                        Procedure pro3 = new Procedure();
                        pro3.Name = "xp_WizIns_iTransferBox";
                        pro3.OutputUseYN = "N";
                        pro3.OutputName = "RollSeq";
                        pro3.OutputLength = "10";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter3);
                    }
                }

                string[] Confirm = new string[2];
                Confirm = DataStore.Instance.ExecuteAllProcedureOutputNew(Prolist, ListParameter);
                if (Confirm[0] == "success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private bool UpdateInspect(InsView.TInspect TIns, List<InsView.TInspectSub> list_TInsSub, int DefectCnt, string sRollClss,
                 List<InsView.TBoxTransfer> list_TBoxTransfer, long nTransCount)
        {
            try
            {
                List<Procedure> Prolist = new List<Procedure>();
                List<Dictionary<string, object>> ListParameter = new List<Dictionary<string, object>>();

                Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                sqlParameter1.Add("@OrderID", TIns.OrderID);
                sqlParameter1.Add("@RollSeq", TIns.RollSeq);
                sqlParameter1.Add("@OrderSeq", TIns.OrderSeq);
                sqlParameter1.Add("@ExamNO", TIns.ExamNo);
                sqlParameter1.Add("@ExamDate", TIns.ExamDate);

                sqlParameter1.Add("@ExamTime", TIns.ExamTime);
                sqlParameter1.Add("@TeamID", TIns.TeamID);
                sqlParameter1.Add("@PersonID", TIns.PersonID);
                sqlParameter1.Add("@StuffQty", TIns.StuffQty);
                sqlParameter1.Add("@RealQty", TIns.RealQty);

                sqlParameter1.Add("@CtrlQty", TIns.CtrlQty);
                //sqlParameter.Add("@SampleQty", TIns.SampleQty          );
                //sqlParameter.Add("@LossQty", TIns.LossQty              );
                //sqlParameter.Add("@CutQty",  TIns.CutQty               );
                sqlParameter1.Add("@UnitClss", TIns.UnitClss);

                sqlParameter1.Add("@StuffWeight", TIns.StuffWeight);
                sqlParameter1.Add("@StuffWeightUnit", TIns.StuffWeightUnit);
                //sqlParameter.Add("@Density", TIns.Density);
                sqlParameter1.Add("@StuffWeight", TIns.StuffWeight);

                sqlParameter1.Add("@GradeID", TIns.GradeID);
                sqlParameter1.Add("@LotNo", TIns.LotNo);
                sqlParameter1.Add("@DefectID", TIns.DefectID);
                sqlParameter1.Add("@DefectClss", TIns.DefectClss);
                //sqlParameter.Add("@CutDefectID", TIns.CutDefectID);
                //sqlParameter.Add("@CutDefectClss", TIns.CutDefectClss);
                sqlParameter1.Add("@DefectQty", TIns.DefectQty);
                sqlParameter1.Add("@DefectPoint", TIns.DefectPoint);

                sqlParameter1.Add("@RollClss", sRollClss);
                sqlParameter1.Add("@DivideQty", TIns.DivideQty);

                sqlParameter1.Add("@CardID", TIns.ReworkClss);       //'재작업 여부

                Procedure pro1 = new Procedure();
                pro1.Name = "xp_WizIns_uInspect";
                pro1.OutputUseYN = "N";
                pro1.OutputName = "RollSeq";
                pro1.OutputLength = "10";

                Prolist.Add(pro1);
                ListParameter.Add(sqlParameter1);

                //'불량 삭제가 성공하면 다시 Insert 

                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add("@OrderID", TIns.OrderID);
                sqlParameter2.Add("@RollSeq", TIns.RollSeq);

                Procedure pro2 = new Procedure();
                pro2.Name = "xp_WizIns_dInspectSub";
                pro2.OutputUseYN = "N";
                pro2.OutputName = "RollSeq";
                pro2.OutputLength = "10";

                Prolist.Add(pro2);
                ListParameter.Add(sqlParameter2);


                int nSubCount = 0;
                foreach (InsView.TInspectSub TInsSub in list_TInsSub)
                {
                    int nDQ = int.Parse(TInsSub.nDefectQty);
                    if (nDQ > 0)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                        sqlParameter3.Add("@OrderID", TIns.OrderID);
                        sqlParameter3.Add("@RollSeq", TIns.RollSeq);
                        sqlParameter3.Add("@DefectSeq", ++nSubCount);
                        sqlParameter3.Add("@DefectID", TInsSub.DefectID);
                        sqlParameter3.Add("@YPos", TInsSub.YPos);

                        sqlParameter3.Add("@Demerit", TInsSub.Demerit);
                        sqlParameter3.Add("@BonusQty", TInsSub.BonusQty);           //'보상
                        sqlParameter3.Add("@DefectQty", TInsSub.nDefectQty);        //'불량수량
                        sqlParameter3.Add("@PersonID", TIns.PersonID);              //'수정자

                        Procedure pro3 = new Procedure();
                        pro3.Name = "xp_WizIns_iInspectSub";
                        pro3.OutputUseYN = "N";
                        pro3.OutputName = "RollSeq";
                        pro3.OutputLength = "10";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter3);
                    }
                }

                if (nTransCount > 0)
                {
                    foreach (InsView.TBoxTransfer TBoxTransfer in list_TBoxTransfer)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();

                        sqlParameter3.Add("@BoxID", TBoxTransfer.BoxID);                            //'From Box ID
                        sqlParameter3.Add("@GetBoxID", TBoxTransfer.GetBoxID);                      //'From Box ID
                        sqlParameter3.Add("@DivideBoxID", TBoxTransfer.DivideBoxID);                //'To Box ID
                        sqlParameter3.Add("@WorkDate", TIns.ExamDate);
                        sqlParameter3.Add("@WorkTime", TIns.ExamTime);
                        sqlParameter3.Add("@nBeforeGetBoxQty", TBoxTransfer.nBeforeGetBoxQty);      //'이송처리전의 From Box 의 장입량
                        sqlParameter3.Add("@nTransferQty", TBoxTransfer.nTransferQty);          	//'이송처리하는 수량
                        sqlParameter3.Add("@sUserID", TIns.PersonID);           					//'수정자

                        Procedure pro3 = new Procedure();
                        pro3.Name = "xp_WizIns_iTransferBox";
                        pro3.OutputUseYN = "N";
                        pro3.OutputName = "RollSeq";
                        pro3.OutputLength = "10";

                        Prolist.Add(pro3);
                        ListParameter.Add(sqlParameter3);
                    }
                }

                string[] Confirm = new string[2];
                Confirm = DataStore.Instance.ExecuteAllProcedureOutputNew(Prolist, ListParameter);
                if (Confirm[0] == "success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        //private bool DeleteInspectSub(string sOrderID, int nRollSeq)
        //{
        //    try
        //    {
        //        string[] strMessage;
        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //        sqlParameter.Add("@OrderID", sOrderID);
        //        sqlParameter.Add("@RollSeq", nRollSeq);

        //        strMessage = DataStore.Instance.ExecuteProcedure("xp_WizIns_dInspectSub", sqlParameter, false);

        //        if (strMessage[0] != "success")//프로시저 실행 실패 시 에러 메세지 발생
        //        {
        //            string strErr = "";

        //            foreach (var str in strMessage)
        //            {
        //                strErr = strErr + "\r\n" + str;
        //            }

        //            throw new Exception(strErr);
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
        //        return false;
        //    }
        //}

        //private void cmdExit_Click(object sender, EventArgs e)
        //{
        //    DialogResult = DialogResult.No;
        //    this.Dispose();
        //    this.Close();
        //}

        private void cmdClose_Click(object sender, EventArgs e)
        {
            string Text = "라벨 발행중입니다. 잠시만 기다려주십시오.";
            WizCommon.Popup.MyMessageBox.ShowBox(Text, "[확인]", 3, 2);


            AutoClosingMessageBox.Show("불량수량이 일치하지 않습니다. 다시 입력하여 주십시오.", "불량수량이 올바르지 않습니다.", 1500);
            this.Close();


        }

        private void ShowDefectType()
        {
            WizCommon.Popup.DefectU.FrmDefect frmDefect = new WizCommon.Popup.DefectU.FrmDefect(list_cbx);

            if (frmDefect.ShowDialog() == DialogResult.OK)
            {
                m_tIns.DefectID = Lib.FindComboBoxID(frmDefect.cboDefect, list_cbx);
            }
            else
            {
                m_tIns.DefectID = "";
            }
        }

        private void cmdQuit_Click(object sender, EventArgs e)
        {

            int nDCnt = int.Parse(Frm_tins_Main.g_tBase.DefectCnt);
            if (nDCnt > 0)
            {
                if (WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._101, "[확인]", 0, 0) == DialogResult.OK)
                {
                    int wsf = int.Parse(Frm_tins_Main.g_tBase.WorkSFlag);
                    if (wsf > 0)
                    {
                        UpdateWorkEnd(Frm_tins_Main.g_tBase.CardID, Frm_tins_Main.g_tBase.SplitID, int.Parse(Frm_tins_Main.g_tBase.WorkSeq), Frm_tins_Main.g_tBase.PersonID);
                    }
                    Frm_tins_Main.g_tBase.WorkSFlag = "0";           //'공정작업 시작 구분(WorkSFlag)을 초기화
                    Frm_tins_Main.g_tBase.WorkSeq = "0";             //'작업순서값을 초기화

                    this.Dispose();
                    this.Close();
                }
            }
            else
            {
                int wsf = int.Parse(Frm_tins_Main.g_tBase.WorkSFlag);
                if (wsf > 0)
                {
                    UpdateWorkEnd(Frm_tins_Main.g_tBase.CardID, Frm_tins_Main.g_tBase.SplitID, int.Parse(Frm_tins_Main.g_tBase.WorkSeq), Frm_tins_Main.g_tBase.PersonID);
                }
                Frm_tins_Main.g_tBase.WorkSFlag = "0";           //'공정작업 시작 구분(WorkSFlag)을 초기화
                Frm_tins_Main.g_tBase.WorkSeq = "0";             //'작업순서값을 초기화

                this.Dispose();
                this.Close();
            }
        }

        private bool UpdateWorkEnd(string sCardID, string sSplitID, int nWorkSeq , string sPersonID )
        {
            try
            {
                //string[] strMessage;
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@CardID", sCardID);
                sqlParameter.Add("@SplitID", sSplitID);
                sqlParameter.Add("@WorkSeq", nWorkSeq);
                sqlParameter.Add("@@LastUpdateUserID", sPersonID);

                //사용못하는 프로시저 테이블 다 없앴음
                //strMessage = DataStore.Instance.ExecuteProcedure("xp_WizIns_uWorkEnd", sqlParameter, false);

                //if (strMessage[0] != "success")//프로시저 실행 실패 시 에러 메세지 발생
                //{
                //    string strErr = "";

                //    foreach (var str in strMessage)
                //    {
                //        strErr = strErr + "\r\n" + str;
                //    }

                //    throw new Exception(strErr);
                //}
                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void cmdInit_Click(object sender, EventArgs e)
        {
            Frm_tins_Main.g_tBase.DLoss = "0";  //'Loss합계 초기화
            int nDCnt = Lib.OnlyNumber(Frm_tins_Main.g_tBase.DefectCnt);

            if (nDCnt > 0)
            {
                if (WizCommon.Popup.MyMessageBox.ShowBox(Properties.Resources._105, "[확인]", 0, 0) == DialogResult.OK)
                {
                    InitForm();
                }
            }
        }
        //주석 풀어서 수정할것
//        private void cmdDefect_Click(object sender, EventArgs e)
//        {
//            int nDCnt = Lib.OnlyNumber(Frm_tins_Main.g_tBase.DefectCnt);
//            if (nDCnt < 1)
//            {
//                return;
//            }
//            string sMsg = "'" + Frm_tins_Main.list_g_tInsSub[nDCnt - 1].KDefect + "' (" + string.Format("{0:no}", Frm_tins_Main.list_g_tInsSub[nDCnt - 1].YPos) +  ")의 불량 선택을 취소하시겠습니까 ?";
//            string sDefectID;
//            sDefectID = Frm_tins_Main.list_g_tInsSub[nDCnt - 1].DefectID;




//            If Len(grdDefect.TextMatrix(grdDefect.Row, grdDefect.Col)) = 0 Then Exit Sub

                
    
//        With grdDefect

//            For nPos = 0 To g_tSet.ButtonX* g_tSet.ButtonY
//               If m_aDefect(nPos).DefectID = sDefectID Then
//                   Exit For
//               End If

//           Next nPos

//           nRow = Int(nPos / g_tSet.ButtonX)
//            nCol = nPos Mod g_tSet.ButtonX



//            m_aDefect(nPos).DefectCnt = m_aDefect(nPos).DefectCnt - g_tInsSub(g_tBase.DefectCnt - 1).nDefectQty         'OLD: m_aDefect(nPos).DefectCnt = m_aDefect(nPos).DefectCnt - 1



//Print
//If m_aDefect(nPos).DefectCnt = 0 Then     ' 불량 개수가 0 일때는 불량명만 뿌려준다.
//                .TextMatrix(nRow, nCol) = Mid(.TextMatrix(nRow, nCol), 1, Len(.TextMatrix(nRow, nCol)) - 5)
//            Else
//                .TextMatrix(nRow, nCol) = Mid(.TextMatrix(nRow, nCol), 1, Len(.TextMatrix(nRow, nCol)) - 5) & Format(m_aDefect(nPos).DefectCnt, "@@@@@")
//            End If
//        End With

//''        g_tBase.DefectCnt = g_tBase.DefectCnt - 1 ' 불량 갯수 (--1감소)


//        'S_201201_조일_07 에 의한 추가-Loss차감
//        g_tBase.DLoss = g_tBase.DLoss - g_tInsSub(g_tBase.DefectCnt - 1).BonusQty



//        'S_201201_조일_09 에 의한 수정-위치이동
//        g_tBase.DefectCnt = g_tBase.DefectCnt - 1 ' 불량 갯수 (--1감소)


//        ReDim Preserve g_tInsSub(g_tBase.DefectCnt)
//        }
    }
}
