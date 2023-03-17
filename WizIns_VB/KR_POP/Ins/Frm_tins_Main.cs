using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WizIns.Properties;
using WizIns.Tools;
using Microsoft.Win32;
using WizCommon;

namespace WizIns
{
    public partial class Frm_tins_Main : Form
    {
        private int childFormNumber = 0;

        //string sOrderID = string.Empty;
        string sOrderID = string.Empty;

        public static string pl_GP_Input { get; set; }//plinput_q에서 가져온 변수,GP생산과 공정작업 두메뉴에서 함께 사용하므로 
        //각 폼에서 호출할때 어느 폼에서 호출하는지 알려주는 변수 0을 받으면 GP생산 1을 받으면 공정작업

        public static string NowProcessID { get; set; }      //Main폼에서 저장하고있는 현재 프로세스ID 변수
        public static string NowProcessName { get; set; }    //Main폼에서 저장하고있는 현재 프로세스 이름 변수
        public static string NowInstID { get; set; }         //Main폼에서 저장하고있는 현재 지시번호 변수
        public static string NowPersonID { get; set; }       //Main폼에서 저장하고 있는 현재 사원 번호

        public static TMold g_tMold = new TMold(); //금형 선택 정보

        public static List<TMold> list_tMold = new List<TMold>();

        public static TBaseSpec g_tBase = new TBaseSpec(); //기본 정보

        //public static TWkResultDefect g_tInsSub = new TWkResultDefect();

        //public static List<TWkResultDefect> list_g_tInsSub = new List<TWkResultDefect>();

        public static TTerminalSet g_tSet = new TTerminalSet(); //검사기준 정보
        
        public static string ComboBox_Process { get; set; }
        //public static WizIns.Mold[] g_tMold = new WizIns.Mold[1];// 추후에 수정해야한다. 레지스트리에 등록되어있는 Mold정보를 가지고 클래스배열의 갯수를 정해야한다.
        /// <summary>
        /// WizIns용 전역변수 선언
        /// </summary>
        public static InsView.TInspect g_tIns = new InsView.TInspect();
        public static List<InsView.TInspectSub> list_g_tInsSub = new List<InsView.TInspectSub>();
        public static InsView.TDefect g_tDef = new InsView.TDefect();
        public static List<InsView.TDefect> list_g_tDef = new List<InsView.TDefect>();
        public static List<InsView.TBoxTransfer> list_g_tBoxtransfer = new List<InsView.TBoxTransfer>();
        public static InsView.TWkLabelPrint TWkLabelPrint = new InsView.TWkLabelPrint();
        public static List<InsView.TWkLabelPrint> list_TWkLabelPrint = new List<InsView.TWkLabelPrint>();
        public static TTag m_tTag = new TTag();
        public static TTagSub list_m_tItem = new TTagSub();

        public static long g_lnBoxtransferQty = 0;

        public static InsView.EEdit g_nEdit = new InsView.EEdit();
        public static InsView.ESearch g_nSearch = new InsView.ESearch();                     //' 검사 작업, 검사 조회 구분
        public static InsView.EWorkState g_nWorkState = new InsView.EWorkState();            //' 처음검사 , 계속검사 구분
        public static InsView.EDefectSelect g_bDefectSelect = new InsView.EDefectSelect();   //'대표불량 선택여부 : 검사수정일때 폼로드시 선택되지 않게 한다
        public static bool g_VBMode = new bool();                                            //'VB 개발 Mode에서 실행하는지 Check   
        public static bool g_bRework = new bool();                                           //'재작업 선택 여부,                   

        public Frm_tins_Main()
        {
            InitializeComponent();

            //timer1.Tick += timer1_Tick;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "창 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Overlap()
        {
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_tprc_Work_U" || openForm.Name == "Frm_tprc_Work_U_GP")
                {
                    
                    if (MessageBox.Show("생산작업을 진행중이였습니다. 작업을 종료하시겠습니까?", "WizWork", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        openForm.Close();
                        return;
                    }
                    else
                    {
                        openForm.Activate();
                        return;
                    }
                }
            }
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            //Overlap();
            this.Text = "생산정보시스템 - 공지사항";
            Frm_Info child0 = new Frm_Info();
            child0.MdiParent = this;
            child0.Dock = DockStyle.Fill;
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_Info")
                {
                    openForm.Activate();
                    return;
                }
            }
            child0.Show();

           // Form1 f1 = new Form1();
           // f1.MdiParent = this;
           // f1.Show();
           // f1.Location = new Point(0, 89);
            
        }

        private void cmdWorkQ_Click(object sender, EventArgs e)
        {
            //Overlap();
            this.Text = "생산정보시스템 - 생산실적 조회";

            frm_tins_Result_Q child4 = new frm_tins_Result_Q();

            child4.MdiParent = this;
            child4.Dock = DockStyle.Fill;
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_tprc_Result")
                {
                    openForm.Activate();
                    return;
                }
            }
            child4.Show();
        }

        private void cmdNoWork_Click(object sender, EventArgs e)
        {
            //Overlap();
            //this.Text = "생산정보시스템 - 무작업입력"; 

            // foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    //18.01.12 frm_tprc_NoWork_U1 -> frm_tprc_NoWork_U 로 수정
            //    if (openForm.Name == "frm_tprc_NoWork_U")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}

            // WizIns.Frm_tprc_NoWork_U set_ps = new WizIns.Frm_tprc_NoWork_U();
            //set_ps.MdiParent = this;
            //set_ps.Dock = DockStyle.Fill;
            //set_ps.Show();             
          
            //return;
        }

     

        private void cmdWork_Click(object sender, EventArgs e)
        {
            //Overlap();

            //this.Text = "생산정보시스템 - 공정작업";
            //pl_GP_Input = "1";//GP생산 구분용 변수 GP생산이므로 0을 넣어줌.

            //WizIns.Frm_tprc_PlanInput_Q child2 = new WizIns.Frm_tprc_PlanInput_Q();
            //child2.MdiParent = this;
            ////18.01.12 닫고 여는게 아니라 열린게 있으면 활성화
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "Frm_tprc_PlanInput_Q")
            //    {
            //        //openForm.Close();
            //        //child2.Dock = DockStyle.Fill;
            //        //child2.Show();
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child2.Dock = DockStyle.Fill;
            //child2.Show();            
        }


        private void cmdGP_Click(object sender, EventArgs e)
        {
            ////Overlap();
            //pl_GP_Input = "0";//GP생산 구분용 변수 GP생산이므로 0을 넣어줌.
            //Frm_tprc_PlanInput_Q child1 = new Frm_tprc_PlanInput_Q();
            //child1.MdiParent = this;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    //18.01.12 닫고 여는게 아니라 열린게 있으면 활성화
            //    if (openForm.Name == "Frm_tprc_PlanInput")
            //    {
            //        //openForm.Close();
            //        //child1.Dock = DockStyle.Fill;
            //        //child1.Show();
            //        openForm.Activate();
            //        return;
            //    }

            //}
            //child1.Dock = DockStyle.Fill;
            //child1.Show();            
        }

        private void cmdMissWorkQ_Click(object sender, EventArgs e)
        {
            ////Overlap();
            //this.Text = "생산정보시스템 - 생선 미달성 지시";
            //Frm_tprc_MissingWorkQty child5 = new Frm_tprc_MissingWorkQty();
            //child5.MdiParent = this;
            //child5.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "Frm_tprc_MissingWorkQty")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child5.Show();            
        }

        private void cmdDefectQ_Click(object sender, EventArgs e)
        {
            ////Overlap();
            //this.Text = "생산정보시스템 - 불량이력";
            //frm_tprc_WorkDefect_Q child6 = new frm_tprc_WorkDefect_Q();
            //child6.ShowDialog();
        }

        private void cmdMtrLotStock_Click(object sender, EventArgs e)
        {
            ////Overlap();
            //this.Text = "생산정보시스템 - 자재 LOT별 재고 조회";
            //Frm_tprc_MtrLotStock_Q child7 = new Frm_tprc_MtrLotStock_Q();
            //child7.MdiParent = this;
            //child7.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "Frm_tprc_MtrLotStock_Q")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child7.Show();            
        }

        private void cmdMoldQ_Click(object sender, EventArgs e)
        {
            ////Overlap();
            //this.Text = "생산정보시스템 - 금형정보";
            //Frm_tprc_Mold_Q child8 = new Frm_tprc_Mold_Q();
            //child8.ShowDialog();
        }
        
        private void cmdOrderDetailQ_Click(object sender, EventArgs e)
        {
            //Overlap();
            this.Text = "생산정보시스템 - 오더상세";
            frm_tins_OrderPopUp child9 = new frm_tins_OrderPopUp();
            child9.ShowDialog();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("시스템을 종료하시겠습니까", "시스템 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    System.Diagnostics.Process.Start("ShutDown", "-s"); //shutdown 종료
            //}
            //else//No 이벤트처리
            //{
 
            //}

            this.Close();
            //System.Diagnostics.Process.Start("ShutDown", "-s"); //shutdown 종료
            //System.Diagnostics.Process.Start("ShutDown", "-r"); //restart 재시작
        }


        private void MDIParent1_Load(object sender, EventArgs e)
        {
            LoadRegistry();
            cmdInfo_Click(null, null);
        }

        private void cmdSetting_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMNumericKeypad keypad = new WizCommon.Popup.Frm_CMNumericKeypad("", "비밀번호");

            string password = string.Empty;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                password = keypad.tbInputText.Text;
                if (password == "0000")
                {
                    FrmSetting menu3 = new FrmSetting();
                    menu3.ShowDialog();
                }
                else
                {
                    AutoClosingMessageBox.Show("비밀번호가 일치하지 않습니다", "Password Incorrect Error", 1000);
                }
            }
        }

        private void cmdInfo_Click(object sender, KeyEventArgs e)
        {
            //Overlap();
            Frm_Info child0 = new Frm_Info();
            child0.MdiParent = this;
            child0.Dock = DockStyle.Fill;
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_Info")
                {
                    openForm.Activate();
                    return;
                }
            }
            child0.Show();            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            stbNowTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Text = "생산정보시스템 - 설비점검 조회";

            //Frm_tprc_DailMachineCheck_Q child10 = new Frm_tprc_DailMachineCheck_Q();
            //child10.MdiParent = this;
            //child10.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "Frm_tprc_DailMachineCheck_Q")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child10.Show();            
        }

        private void cmdDailyCheck_Click(object sender, EventArgs e)
        {
            //this.Text = "생산정보시스템 - 설비점검";

            //Frm_tprc_DailMachineCheck child11 = new Frm_tprc_DailMachineCheck();
            //child11.MdiParent = this;
            //child11.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "Frm_tprc_DailMachineCheck")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child11.Show();
        }

        /// <summary>
        /// 자주 검사 메뉴 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTinsInspectAuto_Click(object sender, EventArgs e)
        {
            
            //this.Text = "생산정보시스템 - 자주검사";

            //frm_tins_InspectAuto_U child11 = new frm_tins_InspectAuto_U();
            //child11.MdiParent = this;
            //child11.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "frm_tins_InspectAuto_U")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child11.Show();
            
        }

        /// <summary>
        /// 자주검사 실적 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTinsInspectAutoQ_Click(object sender, EventArgs e)
        {
            //this.Text = "생산정보시스템 - 자주검사 실적 조회";
                
            //frm_tins_InspectAutoResult_Q child11 = new frm_tins_InspectAutoResult_Q();
            //child11.MdiParent = this;
            //child11.Dock = DockStyle.Fill;
            
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "frm_tins_InspectAutoResult_Q")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child11.Show();            
        }

        private void cmdTprcCardRePrint_Click(object sender, EventArgs e)
        {
            //this.Text = "생산정보시스템 - 이동전표 재발행";
       
            //frm_tprc_CardRePrint_U child11 = new frm_tprc_CardRePrint_U();
            //child11.MdiParent = this;
            //child11.Dock = DockStyle.Fill;
            //foreach (Form openForm in Application.OpenForms)//중복실행방지
            //{
            //    if (openForm.Name == "frm_tprc_CardRePrint_U")
            //    {
            //        openForm.Activate();
            //        return;
            //    }
            //}
            //child11.Show();
            
        }

        private void Frm_tins_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRegistry();
        }

        private void LoadRegistry()
        {
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizIns_sInspect", null, false);

            //Frm_tins_Main.g_tSet.EnCoderPort =
            //Frm_tins_Main.g_tSet.PrinterPort =
            //Frm_tins_Main.g_tSet.IndicatorPort =
            //Frm_tins_Main.g_tSet.EncoderClss =
            //Frm_tins_Main.g_tSet.VerticalPort =
            //Frm_tins_Main.g_tSet.WaverPort =
            //Frm_tins_Main.g_tSet.ExternalPort =
            //Frm_tins_Main.g_tSet.InternalPort =
            //Frm_tins_Main.g_tSet.RoundnessPort =
            //Frm_tins_Main.g_tSet.YNbtnPort =
            //Frm_tins_Main.g_tSet.WeightPort =
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                //Frm_tins_Main.g_tSet.TagShift = CInt(GetValue("TagPrinter", "TagShift", 0))
                //'-------------------------------------------
                Frm_tins_Main.g_tSet.GradeClss =    dr["GradeClss"].ToString();    //   ' 등급결정 방법 (0: 사용않함, 1: 검사자지정, 2:예외)
                Frm_tins_Main.g_tSet.DemeritClss =  dr["DemeritClss"].ToString();  //   ' 벌점적용 방법 (0: 사용않함, 1: 불량보상-지정감점사용, 2: 검사기준적용, 3: 수동입력적용)
                Frm_tins_Main.g_tSet.LossClss =     dr["LossClss"].ToString();     //   ' 보상적용 방법 (0: 사용않함, 1: 보상-지정보상사용)
                Frm_tins_Main.g_tSet.DefectClss =   dr["DefectClss"].ToString();   //   ' 대표불량 선택 등급기준 (어느 등급(Index)부터 대표불량을 적용할것인지 ? > 0)
                Frm_tins_Main.g_tSet.CutDefect =    dr["CutDefect"].ToString();    //   ' 난단불량 사용여부 (0 : 사용않함, ~ )
                //'---------------------------------//
                Frm_tins_Main.g_tSet.ButtonX =      int.Parse(dr["ButtonX"].ToString());      // '= 5
                Frm_tins_Main.g_tSet.ButtonY =      int.Parse(dr["ButtonY"].ToString());      // '= 4
                Frm_tins_Main.g_tSet.ColorCnt =     int.Parse(dr["ColorCnt"].ToString());     // '= 2 색상수
                Frm_tins_Main.g_tSet.RepeatCnt =    int.Parse(dr["RepeatCnt"].ToString());    // '= 2 반복수
                Frm_tins_Main.g_tSet.FontSize =     int.Parse(dr["FontSize"].ToString());     // '불량명 폰트 크기
                //'---------------------------------//
                Frm_tins_Main.g_tSet.RoundClss =    dr["RoundClss"].ToString();    // ' 소숫점 관리
                Frm_tins_Main.g_tSet.RollClss = int.Parse(dr["RollClss"].ToString());     // ' Roll NO 부여방법
            }

            if (Application.UserAppDataRegistry.GetValue("Person") != null)
            {
                //키 로드 
                Frm_tins_Main.g_tBase.OrderID = (string)Application.UserAppDataRegistry.GetValue("OrderID", "");
                Frm_tins_Main.g_tBase.OrderNo = (string)Application.UserAppDataRegistry.GetValue("OrderNO", "");
                Frm_tins_Main.g_tBase.Custom = (string)Application.UserAppDataRegistry.GetValue("Custom", "");
                Frm_tins_Main.g_tBase.OrderQty = (string)Application.UserAppDataRegistry.GetValue("OrderQty", "0");
                Frm_tins_Main.g_tBase.OrderUnit = (string)Application.UserAppDataRegistry.GetValue("OrderUnit", "");
                //--------------------------------------------------------------------------------------------------------------------------------------------
                Frm_tins_Main.g_tBase.ProcessID = (string)Application.UserAppDataRegistry.GetValue("ProcessID", "");
                Frm_tins_Main.g_tBase.Process = (string)Application.UserAppDataRegistry.GetValue("Process", "");
                Frm_tins_Main.g_tBase.ProcessCode = (string)Application.UserAppDataRegistry.GetValue("ProcessCode", "");
                //--------------------------------------------------------------------------------------------------------------------------------------------
                Frm_tins_Main.g_tBase.CardID = (string)Application.UserAppDataRegistry.GetValue("CardID", "");
                Frm_tins_Main.g_tBase.WorkSeq = (string)Application.UserAppDataRegistry.GetValue("WorkSeq", "");
                Frm_tins_Main.g_tBase.MachineID = (string)Application.UserAppDataRegistry.GetValue("MachineID", "");
                //--------------------------------------------------------------------------------------------------------------------------------------------
                Frm_tins_Main.g_tBase.ColorID = (string)Application.UserAppDataRegistry.GetValue("ColorID", "");
                Frm_tins_Main.g_tBase.Color = (string)Application.UserAppDataRegistry.GetValue("Color", "");
                Frm_tins_Main.g_tBase.ColorQty = (string)Application.UserAppDataRegistry.GetValue("ColorQty", "0");
                //--------------------------------------------------------------------------------------------------------------------------------------------
                Frm_tins_Main.g_tBase.TeamID = (string)Application.UserAppDataRegistry.GetValue("TeamID", "00");
                Frm_tins_Main.g_tBase.Team = (string)Application.UserAppDataRegistry.GetValue("Team", "");
                Frm_tins_Main.g_tBase.PersonID = (string)Application.UserAppDataRegistry.GetValue("PersonID", "0000");
                Frm_tins_Main.g_tBase.Person = (string)Application.UserAppDataRegistry.GetValue("Person", "");
                Frm_tins_Main.g_tBase.TagID = (string)Application.UserAppDataRegistry.GetValue("TagID", "000");

                stbMachine.Text = (string)Application.UserAppDataRegistry.GetValue("NowMachineName");
                stbMachine.Tag = (string)Application.UserAppDataRegistry.GetValue("NowMachineID");
                stbTeam.Text = (string)Application.UserAppDataRegistry.GetValue("NowTeam");
                stbTeam.Tag = (string)Application.UserAppDataRegistry.GetValue("NowTeamID");
                stbPerson.Tag = (string)Application.UserAppDataRegistry.GetValue("NowPersonID");
                NowPersonID = (string)Application.UserAppDataRegistry.GetValue("NowPersonID");
                stbPerson.Text = "작업자 : " + (string)Application.UserAppDataRegistry.GetValue("NowPersonName");
                stbMold.Tag = (string)Application.UserAppDataRegistry.GetValue("NowMoldID");
                stbMold.Text = (string)Application.UserAppDataRegistry.GetValue("NowMoldName");
                NowProcessID = (string)Application.UserAppDataRegistry.GetValue("NowProcessID");
                NowProcessName = (string)Application.UserAppDataRegistry.GetValue("NowProcessName");
                NowInstID = (string)Application.UserAppDataRegistry.GetValue("NowInstID");
            }            


            //RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);

            ////값 가져오기
            //if (regKey.GetValue("NowMachineName").ToString() != null)  {stbMachine.Text = regKey.GetValue("NowMachineName").ToString(); }
            //if (regKey.GetValue("NowMachineID").ToString() != null) {   stbMachine.Tag = regKey.GetValue("NowMachineID").ToString(); }
            //if (regKey.GetValue("NowTeam").ToString() != null) {        stbTeam.Text = regKey.GetValue("NowTeam").ToString(); }
            //if (regKey.GetValue("NowTeamID").ToString() != null) {      stbTeam.Tag = regKey.GetValue("NowTeamID").ToString(); }
            //if (regKey.GetValue("NowPersonID").ToString() != null) {    stbPerson.Tag = regKey.GetValue("NowPersonID").ToString(); }
            //if (regKey.GetValue("NowPersonID").ToString() != null) {    NowPersonID = regKey.GetValue("NowPersonID").ToString(); }
            //if (regKey.GetValue("NowPersonName").ToString() != null) {  stbPerson.Text = "작업자 : " + regKey.GetValue("NowPersonName").ToString(); }
            //if (regKey.GetValue("NowMoldID").ToString() != null) {      stbMold.Tag = regKey.GetValue("NowMoldID").ToString(); }
            //if (regKey.GetValue("NowMoldName").ToString() != null) {    stbMold.Text = regKey.GetValue("NowMoldName").ToString(); }
            //if (regKey.GetValue("NowProcessID").ToString() != null) {   NowProcessID = regKey.GetValue("NowProcessID").ToString(); }
            //if (regKey.GetValue("NowProcessName").ToString() != null) { NowProcessName = regKey.GetValue("NowProcessName").ToString(); }
            //if (regKey.GetValue("NowInstID").ToString() != null) {      NowInstID = regKey.GetValue("NowInstID").ToString(); } 
        }

        private void SaveRegistry()
        {
            //키 생성하기
            Application.UserAppDataRegistry.SetValue("SOFTWARE\\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);


            Application.UserAppDataRegistry.SetValue("NowProcessID", NowProcessID, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowProcessName", NowProcessName, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowInstID", NowInstID, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowPersonID", stbPerson.Tag, RegistryValueKind.String);
            if (stbPerson.Text.Length > 0)
            { Application.UserAppDataRegistry.SetValue("NowPersonName", stbPerson.Text, RegistryValueKind.String); }
            
            Application.UserAppDataRegistry.SetValue("NowMachineName", stbMachine.Text, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMachineID", stbMachine.Tag,    RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMoldName", stbMold.Text,       RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMoldID", stbMold.Tag,          RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowTeam", stbTeam.Text,           RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowTeamID", stbTeam.Tag,          RegistryValueKind.String);

            Application.UserAppDataRegistry.SetValue("OrderID", g_tBase.OrderID     ,RegistryValueKind.String);          //' 관리번호
            Application.UserAppDataRegistry.SetValue("OrderNO", g_tBase.OrderNo     ,RegistryValueKind.String);          //' Order NO
            Application.UserAppDataRegistry.SetValue("Custom", g_tBase.Custom       ,RegistryValueKind.String);          //' 거래처 명
            Application.UserAppDataRegistry.SetValue("OrderQty", g_tBase.OrderQty   ,RegistryValueKind.String);          //' 수주수량
            Application.UserAppDataRegistry.SetValue("OrderUnit", g_tBase.OrderUnit ,RegistryValueKind.String);          //' 수주 단위

            Application.UserAppDataRegistry.SetValue("ProcessID",   g_tBase.ProcessID   ,RegistryValueKind.String);      //' 공정 ID
            Application.UserAppDataRegistry.SetValue("Process",     g_tBase.Process     ,RegistryValueKind.String);      //' 공정명
            Application.UserAppDataRegistry.SetValue("ProcessCode", g_tBase.ProcessCode ,RegistryValueKind.String);      //' 공정명
                                                                                         
            Application.UserAppDataRegistry.SetValue("CardID",     g_tBase.CardID      ,RegistryValueKind.String);        //' 카드ID
            Application.UserAppDataRegistry.SetValue("WorkSeq",    g_tBase.WorkSeq     ,RegistryValueKind.String);        // ' 가공순위
            Application.UserAppDataRegistry.SetValue("MachineID",   g_tBase.MachineID   ,RegistryValueKind.String);        // ' 가공순위

            Application.UserAppDataRegistry.SetValue("ColorID",     g_tBase.ColorID     ,RegistryValueKind.String);         //' 색상코드
            Application.UserAppDataRegistry.SetValue("Color",       g_tBase.Color       ,RegistryValueKind.String);         //' 색상명
            Application.UserAppDataRegistry.SetValue("ColorQty",    g_tBase.ColorQty    ,RegistryValueKind.String);         //' 색상수량

            Application.UserAppDataRegistry.SetValue("TeamID",     g_tBase.TeamID   ,RegistryValueKind.String);            //' 작업조 코드
            Application.UserAppDataRegistry.SetValue("Team",       g_tBase.Team     ,RegistryValueKind.String);            //' 작업조
            Application.UserAppDataRegistry.SetValue("PersonID",    g_tBase.PersonID ,RegistryValueKind.String);            //' 작업자 코드
            Application.UserAppDataRegistry.SetValue("Person",      g_tBase.Person   ,RegistryValueKind.String);            //' 작업자 코드
            //Frm_tins_Main.g_tBase.OrderID = 

            //Application.UserAppDataRegistry.SetValue("SOFTWARE\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);
            //Application.UserAppDataRegistry.SetValue("SOFTWARE\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);

            //  Application.UserAppDataRegistry.SetValue("SOFTWARE\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);

            //RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\TemporaryKey", RegistryKeyPermissionCheck.ReadWriteSubTree);
            ////값 저장하기
            //if (NowProcessID != null) { regKey.SetValue("NowProcessID", NowProcessID, RegistryValueKind.String); }
            //if (NowProcessName != null) {   regKey.SetValue("NowProcessName", NowProcessName, RegistryValueKind.String); }
            //if (NowInstID != null) {        regKey.SetValue("NowInstID", NowInstID, RegistryValueKind.String); }
            //if (stbPerson.Tag != null) {    regKey.SetValue("NowPersonID", stbPerson.Tag, RegistryValueKind.String); }
            //if (stbPerson.Text != null) {   regKey.SetValue("NowPersonName", stbPerson.Text, RegistryValueKind.String); }
            //if (stbMachine.Text != null) {  regKey.SetValue("NowMachineName", stbMachine.Text, RegistryValueKind.String); }
            //if (stbMachine.Tag != null) {   regKey.SetValue("NowMachineID", stbMachine.Tag, RegistryValueKind.String); }
            //if (stbMold.Text != null) {     regKey.SetValue("NowMoldName", stbMold.Text, RegistryValueKind.String); }
            //if (stbMold.Tag != null) {      regKey.SetValue("NowMoldID", stbMold.Tag, RegistryValueKind.String); }
            //if (stbTeam.Text != null) {     regKey.SetValue("NowTeam", stbTeam.Text, RegistryValueKind.String); }
            //if (stbTeam.Tag != null) {      regKey.SetValue("NowTeamID", stbTeam.Tag, RegistryValueKind.String); }
        }

        public void Set_stbInfo(string g_TeamID, string g_TeamName, string sPersonID, string sPersonName, string sMachineID, string sMachineName,
                                string MoldID, string MoldName, string sProcessID, string sProcessName, string strInstID)
        {
            this.stbTeam.Text = g_TeamName;
            this.stbTeam.Tag = g_TeamID;
            this.stbPerson.Tag = sPersonID;
            NowPersonID = sPersonID;
            NowInstID = strInstID;
            this.stbPerson.Text = sPersonName;
            this.stbMachine.Text = sProcessName + " " + sMachineName;
            this.stbMachine.Tag = sMachineID;
            if (MoldName != "null" && MoldID != "null")
            {
                stbMold.Text = MoldName;
                stbMold.Tag = MoldID;
            }
            else
            {
                stbMold.Text = "";
                stbMold.Tag = "";
            }
            

           
            Frm_tins_Main.NowProcessID = sProcessID;
            Frm_tins_Main.NowProcessName = sProcessName;
            SaveRegistry();

        }
        public void SetProcess_stbInfo(string sProcessID, string sProcessName, string sMachineName, string sMachineID)
        {
            
            this.stbMachine.Text = sProcessName + " " + sMachineName;
            this.stbMachine.Tag = sMachineID;


            Frm_tins_Main.NowProcessID = sProcessID;
            Frm_tins_Main.NowProcessName = sProcessName;

            Application.UserAppDataRegistry.SetValue("NowProcessID", NowProcessID, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowProcessName", NowProcessName, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMachineName", stbMachine.Text, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMachineID", stbMachine.Tag, RegistryValueKind.String);


        }
        public void SetPerson_stbInfo(string sPersonID, string sName, string sTeamID, string sTeam)
        {

            this.stbTeam.Text = sTeam;
            this.stbTeam.Tag = sTeamID;
            this.stbPerson.Tag = sPersonID;

            NowPersonID = sPersonID;
            this.stbPerson.Text = sName;

            //키 생성하기
            Application.UserAppDataRegistry.SetValue("NowPersonID", stbPerson.Tag, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowPersonName", stbPerson.Text, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowMachineName", stbMachine.Text, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowTeam", stbTeam.Text, RegistryValueKind.String);
            Application.UserAppDataRegistry.SetValue("NowTeamID", stbTeam.Tag, RegistryValueKind.String);


        }
        public void Set_stbInfo_Mold(string MoldID, string MoldName)
        {
            this.stbMold.Text = MoldName;
            this.stbMold.Tag = MoldID;
        }


        public void SetstbLookUp(string strstbLookUp)
        {

            this.stbLookUp.Text = strstbLookUp;
            return;
        }
        public string  GetPersonID()
        {
            return this.stbPerson.Tag.ToString(); ;
        }

        public string GetPersonName()
        {
            return this.stbPerson.Text.ToString(); ;
        }

        public void SetFromCClose()
        {
            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "frm_tprc_Work_U")
                {
                    openForm.Close();

                }
                if (openForm.Name == "frm_tprc_Working_U")
                {
                    openForm.Close();

                }
            }

            return;
        }

        public void setInfo(string Message = "")
        {
            stbLookUp.Text = Message;
            stbMachine.Text = "검사 " + Frm_tins_Main.g_tBase.ExamNO + "호기";
            stbTeam.Text = "작업조 : " + Frm_tins_Main.g_tBase.Team;
            stbPerson.Text = "검사자 : " + Frm_tins_Main.g_tBase.Person;
            stbMold.Text = "검사기준 : " + Frm_tins_Main.g_tBase.Basis;
        }
    }
}
