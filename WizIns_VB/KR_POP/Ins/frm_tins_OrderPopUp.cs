using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizIns.Properties;
using System.Runtime.InteropServices;
using WizCommon;
using System.Net;
using WizIns.Tools;

using System.Text.RegularExpressions;

namespace WizIns
{
    public partial class frm_tins_OrderPopUp : Form
    {
        private string m_sOrderID = "";
        private string m_sInstID = "";
        INI_GS gs = new INI_GS();

        FTP_EX _ftp = null;

        private DataSet ds = null;

        public static string sOrderID { get; set; }

        public frm_tins_OrderPopUp()
        {
            InitializeComponent();
        }


        [DllImport("kernel32", CharSet = CharSet.Auto)]
        public static extern Int32 GetWindowsDirectory(String Buffer, Int32 BufferLength);


        private void FDownload()
        {

            if (txtOrderNo.Text != "" || txtArticle.Tag != null)
            {
                // FTP Info
                string FTP_ADDRESS = "ftp://" + gs.GetValue("FTPINFO", "FileSvr", "wizis.iptime.org") + ":" + gs.GetValue("FTPINFO", "FTPPort", "25000");
                string FTP_ID = "wizuser";
                string FTP_PASS = "wiz9999";

                _ftp = new FTP_EX(FTP_ADDRESS, FTP_ID, FTP_PASS);
                string LocalDirPath = Application.StartupPath + "\\" + "#Temp" + "\\" + txtArticle.Tag.ToString() + "\\"; //FTP서버내의 폴더명과 같은 폴더명을 LOCAL에서 사용하자;

                string FtpFolderPath = gs.GetValue("FTPINFO", "FTPIMAGEPATH", "/ImageData") + "/" + txtArticle.Tag.ToString(); // ex)/ImageData/00065
                string[] fileListSimple;

                string[] Local_File = new string[3];
                PictureBox[] Img = { imgSajin_1, imgSajin_2, imgSajin_3 };//도면,작업표준서,공정검사기준서
                Label[] ImgLbl = { lblImageName_1, lblImageName_2, lblImageName_3 };//text가 파일명 , tag 폴더명

                try
                {
                    fileListSimple = _ftp.directoryListSimple(FtpFolderPath, Encoding.Default);

                    if (fileListSimple.Length > 0)
                    {
                        if (fileListSimple[0] == "")
                        {
                            AutoClosingMessageBox.Show("이미지가 등록되어 있지 않습니다.", "[Image Is Null]", 1500);
                            return;
                        }
                    }


                    DirectoryInfo dir = new DirectoryInfo(LocalDirPath);//로컬
                    if (dir.Exists == false)//로컬 폴더 존재 유무 확인 후 없을 시 생성
                    { dir.Create(); }

                    for (int i = 0; i < 3; i++)
                    {
                        bool ftpExistFile = false;
                        Img[i].Tag = ImgLbl[i].Tag.ToString() + "/" + ImgLbl[i].Text;//각 픽쳐박스의 태그속성에 라벨에 저장해놓은 파일경로(폴더명{tag}및파일명{text})
                        Local_File[i] = LocalDirPath + "\\" + ImgLbl[i].Text;


                        foreach (string filename in fileListSimple)//파일 존재 유무 확인 있을때 ftpExistFile변수 True 없을때 False
                        {
                            if (string.Compare(filename, ImgLbl[i].Text) == 0)
                            { ftpExistFile = true; break; }
                        }

                        if (ftpExistFile == false)
                        { AutoClosingMessageBox.Show("[FTP] " + ImgLbl[i].Text + " 이미지가 존재하지 않습니다.", "File Not Found", 15000); }

                        else if (_ftp.GetFileSize(Img[i].Tag.ToString()) == 0)//파일사이즈가 0일때
                        {
                            AutoClosingMessageBox.Show("[FTP] " + ImgLbl[i].Text + " 이미지의 파일사이즈가 0입니다. 사무실프로그램에서 파일을 다시 업로드 해주시기 바랍니다.",
                                "File Size is Null", 15000);
                        }
                        else//파일 사이즈가 0이 아닐때 기존폴더안의 파일들 삭제 후 다운로드
                        {
                            //FTP 다운로드 부분
                            FileInfo file = new FileInfo(Local_File[i]);
                            if (file.Exists == true)//로컬 품명코드 폴더안의 파일 삭제
                            { file.Delete(); }
                            _ftp.download(Img[i].Tag.ToString(), Local_File[i].ToString());

                            FileStream fs = new FileStream(Local_File[i].ToString(), FileMode.Open, FileAccess.Read);
                            Img[i].Image = System.Drawing.Image.FromStream(fs);
                            fs.Close();
                            Img[i].SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
                catch (Exception e)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                }

            }
        }
        public bool procQuery()
        {
            bool IsExist = false;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@OrderID", m_sOrderID);
                ds = DataStore.Instance.ProcedureToDataSet("xp_Order_sOrderOne", sqlParameter, false);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    txtOrderNo.Text = dr["OrderNo"].ToString();   //오더번호 
                    txtDvlyPlace.Text = dr["DvlyPlace"].ToString();     //납품장소
                    txtCustom.Text = dr["CustomName"].ToString();       //거래처
                    txtArticle.Text = dr["Article"].ToString();      //품명
                    txtArticle.Tag = dr["ArticleID"].ToString(); //품명코드 ==>>>>>>>>>>>>>>>>>>> 품명코드=FTP서버내의 폴더명

                    txtOrderQty.Text = dr["OrderQty"].ToString();    //총주문량
                    txtOrderSpec.Text = dr["OrderSpec"].ToString();  //Spec
                                                                                   //txtPoNo.Text = dr[Order_sOrderOne.PONO].ToString();            //P/O NO
                    txtAcptDate.Text = dr["AcptDate"].ToString();    //접수일자
                    txtDvlyDate.Text = dr["DvlyDate"].ToString();    //납기일자
                    txtWorkID.Text = dr["WorkID"].ToString();        //가공구분
                    txtRemark.Text = dr["Remark"].ToString();        //비고사항

                    lblImageName_1.Text = dr["AttFile"].ToString();                 //도면파일
                    lblImageName_1.Tag = dr["AttPath"].ToString();                  //도면폴더
                    lblImageName_2.Text = dr["WorkSheetFile"].ToString();           //작업표준서파일
                    lblImageName_2.Tag = dr["WorkSheetPath"].ToString();            //작업표준서폴더
                    lblImageName_3.Text = dr["InsReportFile"].ToString();           //공정 검사 기준서 파일
                    lblImageName_3.Tag = dr["InsReportPath"].ToString();            //공정 검사 기준서 경로

                    IsExist = true;
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("입력된 관리번호에 해당하는 제품이 없습니다.");
                    IsExist = false;
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
            return IsExist;
        } 
    

        //public bool procQuery()
        //{
        //    bool IsExist = true;
        //    try
        //    {
        //        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

        //        sqlParameter.Add("ORDERID", m_sOrderID); //주석풀어야함
        //        //sqlParameter.Add(Order_sOrderOne.ORDERID, "2017100005"); //테스트용

        //        ds = DataStore.Instance.ProcedureToDataSet("xp_Order_sOrderOne", sqlParameter, false);


        //        if (ds.Tables[0].Rows.Count == 1)
        //        {
        //            DataRow dr = ds.Tables[0].Rows[0];

        //            txtOrderNo.Text = dr["ORDERNO"].ToString();   //오더번호 
        //                                                                        //txtCustom.Text = dr[Order_sOrderOne.KCUSTOM].ToString();       //거래처
        //            txtArticle.Text =   dr["ARTICLE"].ToString();      //품명
        //            txtArticle.Tag =    dr["ARTICLEID"].ToString(); //품명코드 ==>>>>>>>>>>>>>>>>>>> 품명코드=FTP서버내의 폴더명
        //            txtOrderQty.Text =  dr["ORDERQTY"].ToString();    //총주문량
        //            txtOrderSpec.Text = dr["ORDERSPEC"].ToString();  //Spec
        //            //txtPoNo.Text = dr["PONO"].ToString();            //P/O NO
        //            txtAcptDate.Text =  dr["ACPTDATE"].ToString();    //접수일자
        //            txtDvlyDate.Text =  dr["DVLYDATE"].ToString();    //납기일자
        //            txtWorkID.Text =    dr["WORKID"].ToString();        //가공구분
        //            txtRemark.Text =    dr["REMARK"].ToString();        //비고사항

        //            lblImageName_1.Text = dr["DrawFileName"].ToString();                //도면파일
        //            lblImageName_1.Tag = dr["DrawFilePath"].ToString();                 //도면폴더
        //            lblImageName_2.Text = dr["StandardWorkSheetFileName"].ToString();   //작업표준서파일
        //            lblImageName_2.Tag = dr["StandardWorkSheetFilePath"].ToString();    //작업표준서폴더
        //            lblImageName_3.Text = dr["CompleteFileName"].ToString();            //(반)제품사진파일
        //            lblImageName_3.Tag = dr["CompleteFilePath"].ToString();             //(반)제품사진폴더

        //            IsExist = true;
        //        }
        //        else if (ds.Tables[0].Rows.Count == 0)
        //        {
        //            MessageBox.Show("입력된 관리번호에 해당하는 제품이 없습니다.");
        //            IsExist = false;
        //        }
                
        //    }
        //    catch (Exception excpt)
        //    {
        //        MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
        //        IsExist = false;
        //    }
            
        //    return IsExist;
        //}
        private void ImageView_FTP(string sServerPath, string sServerFile, string sLocalPath, string sLocalFile, int iIndex)
        {//서버경로, 서버파일명, 로컬경로, 로컬파일명, FTP파일리스트의 카운트
            if(sServerPath.Equals(string.Empty) || sServerFile.Equals(string.Empty))//FTP 서버 경로 및 파일이 없을때
            {
                return;
            }
            else 
            {
                //string winDir = "C:\\TEMP";
                string winDir = Application.StartupPath + "#Temp";
                Console.WriteLine(Application.StartupPath + "#Temp");
                DirectoryInfo dir = new DirectoryInfo(winDir);
                if (dir.Exists == false)
                {
                    dir.Create();
                }
                
                _ftp.directoryListSimple("ImageData");

                Console.WriteLine(_ftp.directoryListSimple("ImageData"));
                string[] ftpDir = _ftp.directoryListSimple("ImageData");
                for (int i = 0; i < ftpDir.Length; i++)
                {
                    Console.WriteLine(ftpDir[i].ToString());
                }

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FDownload();
        }

        private void frm_tprc_OrderPopUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            AllClear();
            foreach (Control pic in this.Controls)
            {
                if (pic is PictureBox)
                {
                    if (((PictureBox)(pic)).Image != null)
                    {
                        ((PictureBox)(pic)).Image = null;
                        ((PictureBox)(pic)).Image.Dispose();
                    }
                }
            }
            

            //AllClear();
            this.Dispose();
        }
        private void AllClear()
        {
            txtAcptDate.Text = string.Empty;
            txtArticle.Text = string.Empty;
            txtCustom.Text = string.Empty;
            txtDvlyDate.Text = string.Empty;
            txtDvlyPlace.Text = string.Empty;
            txtOrderID.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtOrderQty.Text = string.Empty;
            txtOrderSpec.Text = string.Empty;
            //txtPoNo.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtWorkID.Text = string.Empty;
            lblImageName_1.Text = string.Empty;
            lblImageName_2.Text = string.Empty;
            lblImageName_3.Text = string.Empty;
            //imgSajin_1.Image = null;
            imgSajin_1.Image = null;
            imgSajin_2.Image = null;
            imgSajin_3.Image = null;           
        }

        private void cmdOrderID_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_CMNumericKeypad numkeypad = new WizCommon.Popup.Frm_CMNumericKeypad(txtOrderID.Text, "관리번호");
            if (numkeypad.ShowDialog() == DialogResult.OK)
            {
                txtOrderID.Text = numkeypad.tbInputText.Text;
                //m_sOrderID = txtOrderID.Text;
                m_sInstID = txtOrderID.Text;
                //procQuery();
                procQuery();
                txtOrderID.Text = string.Empty;
                FDownload();
            }
            
        }

        private void imgSajin_1_DoubleClick(object sender, EventArgs e)
        {
            PopUp popup = new PopUp();
            popup.Picture = this.imgSajin_1.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
            popup.Show();
        }

        private void imgSajin_2_DoubleClick(object sender, EventArgs e)
        {
            PopUp popup = new PopUp();
            popup.Picture = this.imgSajin_2.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
            popup.Show();
        }

        private void imgSajin_3_DoubleClick(object sender, EventArgs e)
        {
            PopUp popup = new PopUp();
            popup.Picture = this.imgSajin_3.Image; //OrderPopUp의 이미지를 PopUp의 PictureBox에 할당. 
            popup.Show();
        }

        private void txtOrderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //m_sOrderID = txtOrderID.Text;
                m_sInstID = txtOrderID.Text;
                //procQuery();
                if (procQuery() == true)
                {
                    FDownload();
                }

                txtOrderID.Text = string.Empty;
             }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_tprc_OrderPopUp_Load(object sender, EventArgs e)
        {
            m_sOrderID = Frm_tins_Main.g_tBase.OrderID;
            m_sInstID = Frm_tins_Main.g_tBase.sInstID;

            if (procQuery() == true)
            {
                FDownload();

            }
            txtOrderID.Text = string.Empty;
            
        }
    }
}
