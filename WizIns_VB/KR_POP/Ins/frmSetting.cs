using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizIns.Properties;
using Microsoft.VisualBasic;
using WizIns.Tools;
using WizCommon;

namespace WizIns
{
    public partial class FrmSetting : Form
    {
        //public static string[] cbPList = null;
        //public static string[] cbNList = null;
        private DataSet ds = null;
        bool DuringGrid = false;
        string sProcess = "";                                    //폼안의 그리드뷰에 체크한 ProcessID를 저장할 변수
        //string sFileName = "C:\\Windows\\wizard.ini";             //Wizard.ini 파일위치
        //string sFileName = "C:\\TestWizardT.ini";             //Wizard.ini 파일위치
        string sFileName = ConnectionInfo.filePath;             //Wizard.ini 파일위치
        INI_GS gs = new INI_GS();
        public FrmSetting()
        {
            InitializeComponent();
            
            
        }

        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        ////섹션명[]. 키값,
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        ////섹션명[], 키값, 기본값, 가져온문자열, 문자열버퍼크기, 파일이름

        //public String GetValue(String section, String key, String default_String)
        //{
        //    StringBuilder temp = new StringBuilder();
        //    if (GetPrivateProfileString(section, key, "(NONE)", temp, 32, sFileName) == 0)
        //    {
        //        if (default_String == null) //디폴트값없을때
        //        { return ""; }
        //        else
        //        { return default_String; }
        //    }
        //    else { return temp.ToString(); }         
        //}
        //public void SetValue(string section, string key, string sValue, string sFileName)
        //{
        //    WritePrivateProfileString(section, key, sValue, sFileName);//섹션,키,값,INI파일경로
        //}
        private void LoadBaseSetting()
        {
            txtServer.Text = gs.GetValue("SQLServer", "server", "WZServer");
            txtServer.Tag = gs.GetValue("SQLServer", "server", "WZServer");
            txtDB.Text = gs.GetValue("SQLServer", "Database", "WizMRP");
            txtDB.Tag = gs.GetValue("SQLServer", "Database", "WizMRP");
        }
        
        //DB에 있는 Process 값 조회해서 그리드뷰에 추가(USECLSS 체크되있는 애들만 가져올듯..??)
        private void proc_Q()
         {
            ds = DataStore.Instance.ProcedureToDataSet("xp_Work_sProcess", null, false);
             DataStore.Instance.CloseConnection();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DuringGrid = true;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    dgvLookupResult.Rows.Add(
                        false,                                  //체크여부
                        i + 1,                                  //No
                        dr["PROCESSID"],            //공정코드
                        dr["PROCESS"]               //공정명
                        );
                }
            }
        }
        //닫기버튼 폼닫기
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //그리드뷰의 셀클릭시 그리드뷰의체크박스 변경해주는 메소드
        private void dgvLookupResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                bool flag = (bool)dgvLookupResult.Rows[e.RowIndex].Cells[0].Value;
                dgvLookupResult.Rows[e.RowIndex].Cells[0].Value = !flag;
            }
        }
        //그리드뷰에서 선택한 PROCESSID들을 sProcess에 넣어주고, 
        private void button2_Click(object sender, EventArgs e)//선택버튼
        {

            if (txtServer.Text.ToString() != txtServer.Tag.ToString() || txtDB.Text.ToString() != txtDB.Tag.ToString())
            {
                if (MessageBox.Show("Server 혹은 DataBase 가 변경 되었습니다. 계속 진행하시겠습니까?", "확인 메세지", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    gs.SetValue("SQLServer", "server", txtServer.Text, ConnectionInfo.filePath);        //Wizard.ini server변경
                    gs.SetValue("SQLServer", "Database", txtDB.Text, ConnectionInfo.filePath);          //Wizard.ini DB변경
                }
                else
                { return; }
            }
            checked_sProcess();
            write_sProcess();                                                                            //Wizard.ini파일수정부분******** ProcessID 변경                          
            this.Close();                                                                               //설정 저장 후 화면 닫기
            
        }
        private void checked_sProcess()
        {
            DataGridViewCell cell;
            DataRow dr;
            bool isChecked;
            for (int h = 0; h < dgvLookupResult.Rows.Count; h++)
            {
                cell = dgvLookupResult.Rows[h].Cells[0];
                isChecked = (bool)cell.EditedFormattedValue;
                if (isChecked)
                {
                    dr = ds.Tables[0].Rows[h];
                    if (sProcess == "")
                    {
                        sProcess = dgvLookupResult.Rows[h].Cells[2].Value.ToString();
                    }
                    else
                    {
                        sProcess = sProcess + "|" + dgvLookupResult.Rows[h].Cells[2].Value.ToString();
                    }

                    //int k = 0;
                    //cbPList[k] = dgvLookupResult.Rows[h].Cells[Work_sProcess.PROCESSID].Value.ToString();
                    //cbNList[k] = dgvLookupResult.Rows[h].Cells[Work_sProcess.PROCESS].Value.ToString();
                    //k++;
                }
            }
        }
        //Wizard.ini파일수정부분******** ProcessID 변경
        private void write_sProcess()
        {
            gs.SetValue("Work", "ProcessID", sProcess, ConnectionInfo.filePath);
        }

        //그리드뷰 체크박스 셋팅(전체체크박스 초기화 후 ProcessID와 그리드뷰의 ProcessID 비교 후 같은 값 체크해준다)
        private void set_Grid()
        {
            
            //string[] sProcessID_S = uProcess.Split('|');
            string[] sProcessID = new string[ds.Tables[0].Rows.Count];
            //sProcessID = gs.GetValue("Work", "ProcessID", "ProcessID");//.Split('|');//배열에 프로세스아이디 넣기
            sProcessID = gs.GetValue("Work", "ProcessID", "ProcessID").Split('|');//배열에 프로세스아이디 넣기
            //그리드뷰 체크박스 초기화
            for (int k = 0; k < dgvLookupResult.Rows.Count; k++)
            {
                dgvLookupResult.Rows[k].Cells[0].Value = false;
            }
            /*****************************************************************************************************/
            //그리드뷰 체크박스, sProcessID배열에 담긴 0번째부터의 값과 그리드뷰값 0번째부터의 값이 일치하는지 체크
            /*****************************************************************************************************/
            for (int i = 0; i < sProcessID.Length; i++)
            {
                for (int j = 0; j < dgvLookupResult.Rows.Count; j++)
                {
                    Console.WriteLine(dgvLookupResult.Rows[j].Cells[2].ToString());
                    if (sProcessID[i].ToString() == dgvLookupResult.Rows[j].Cells[2].Value.ToString())
                    {
                        Console.WriteLine(dgvLookupResult.Rows[j].Cells[0].Value.ToString());
                        dgvLookupResult.Rows[j].Cells[0].Value = true;
                        break;
                    }
                }
            }
        }

        //아래 버튼, 그리드뷰 선택된 셀에서 아래로 이동
        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < dgvLookupResult.SelectedCells.Count; i++)
            {
                iSelRow = dgvLookupResult.SelectedCells[i].RowIndex;
                if (iSelRow == dgvLookupResult.Rows.Count - 1) return;
                dgvLookupResult[0, iSelRow + 1].Selected = true;
                break;
            }
        }
        //위 버튼, 그리드뷰 선택된 셀에서 위로 이동
        private void cmdRowUp_Click_1(object sender, EventArgs e)
        {
            int iSelRow = 0;
            for (int i = 0; i < dgvLookupResult.SelectedCells.Count; i++)
            {
                iSelRow = dgvLookupResult.SelectedCells[i].RowIndex;
                if (iSelRow == 0) return;
                dgvLookupResult[0, iSelRow - 1].Selected = true;
                break;
            }
        }

        private void cmdSQL_0_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtServer.Text, "Server");

            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtServer.Text = FK.tbInputText.Text;
            }
        }

        private void cmdSQL_1_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMKeypad FK = new WizCommon.Popup.Frm_CMKeypad(txtDB.Text, "DB");
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtDB.Text = FK.tbInputText.Text;
            }
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            proc_Q();                                           //DB에 있는 Process 값 조회해서 그리드뷰에 추가
            set_Grid();                                         //그리드뷰 체크박스 셋팅
            LoadBaseSetting();                                  //Server,DB정보 불러오기
        }
    }
}
