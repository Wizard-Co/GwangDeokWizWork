using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
//using IWshRuntimeLibrary;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Threading;
using WizCommon;

namespace WizIns
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        
        {
            ///아래는 머신에서 오직 한 프로세스만 사용하기 위한 예제이다. 
            ///머신 전체적으로 한 프로세스만 뜨게 하기 위해, 고유의 Mutex명을 체크할 필요가 있는데, 이를 위해 GUID (globally unique identifier)를 사용하였다. 
            ///처음 프로세스가 먼저 Mutex를 획득한 후에는, 그 프로세스가 죽기 전에는 머신 전체적으로 다른 프로세스가 Mutex를 획득할 수 없다는 점을 이용하여 잠시 
            ///(1초) 체크해 보고 이미 다른 프로세스가 Mutex를 가지고 있으면, 프로세스를 중지하게 된다. 
            //뮤텍스명 지정
            string mtxName = "WizardInformationSystem-WizWork-WorldTrend";
            Mutex mtx = new Mutex(true, mtxName);

            //1초 동안 뮤텍스를 획득하려 대기
            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);

            //실패하면 프로그램 종료
            if (!success)
            {
                return;
            }
            ///
            ///
            ///


            //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            //bool IP_Match_Line = false;
            //bool IP_Match_Monitor = false;

            //17.04.14 김종영 알파용
            // DataStore.Instance.SetConnectionString(ConnectionInfo.POPServerIPAddress_APS, ConnectionInfo.POPServerLoginID_AS, ConnectionInfo.POPServerPassword_AS, ConnectionInfo.DBCatalog_AS);

            // 2017.06.19 월드드랜드
            DataStore.Instance.SetConnectionString(ConnectionInfo.POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, ConnectionInfo.DBCatalog);//, ConnectionInfo.DBCatalog_LOG);
            //DataStore.Log_Instance.L_SetConnectionString(ConnectionInfo.POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, ConnectionInfo.DBCatalog_LOG);
            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //sqlParameter.Add(com_InspectWorkShop.USEYN, "Y");
            //DataSet ds = DataStore.Instance.ProcedureToDataSet("xp_com_InspectWorkShop_s", sqlParameter, false);
            //DataStore.Instance.CloseConnection();

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i ++)
            //{
            //    DataRow dr = ds.Tables[0].Rows[i];

            //    foreach (IPAddress IP_Adress in host.AddressList)
            //    {
            //        if (IP_Adress.ToString() == dr[com_InspectWorkShop.ZIGIP].ToString())//"192.168.21.181")
            //        {
            //            IP_Match_Line = true;
            //            break;
            //        }
            //    }
            //}

            //Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
            //sqlParameter2.Add(com_Code.LCODE, "MONITORIP");
            //DataSet ds2 = DataStore.Instance.ProcedureToDataSet("xp_com_Code_LCode_s", sqlParameter2, false);
            //DataStore.Instance.CloseConnection();

            //if (ds2 != null && ds.Tables[0].Rows.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = ds2.Tables[0].Rows[0];

            //    foreach (IPAddress IP_Adress in host.AddressList)
            //    {
            //        if (IP_Adress.ToString() == dr[com_Code.CODENAME].ToString())
            //        {
            //            IP_Match_Monitor = true;
            //            break;
            //        }
            //    }
            //}


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (IP_Match_Line == true)      // 현장 IP와 일치 시 최종검정 프로그램 실행
            //{
            //    //Application.Run(new Frm_Qul_Monitoring_S());
            //    Application.Run(new Frm_Qlt_Main());
            //}
            //else if (IP_Match_Monitor == true)   // 모니터링 IP와 일치 시 모니터링 프로그램 실행
            //{
            //    Application.Run(new Frm_Qul_Monitoring_S());
            //}
            //else                            // 기존 프로그램 실행
            //{
            //Globals.InitializeMainForm();
            //Application.DoEvents();
            //Application.Run(Globals.MainFormSingleMode);

            //Application.Run(new Frm_Qlt_Main());

            Application.Run(new WizIns.Frm_tins_Main());
            

        }

    }
}

