using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizIns
{
    public class Ins_Label
    {
        public string InstID            { get; set; }
        public string MoveLabelID       { get; set; }
        public string InsLabelID        { get; set; }
        public string PrintQty          { get; set; }
        public string WorkQty           { get; set; }
        public string NowDate           { get; set; }
        public string OutQtyPerBox      { get; set; }
        public string Model             { get; set; }
        public string BuyerModelID      { get; set; }
        public string BuyerArticleNo    { get; set; }
        public string Article           { get; set; }
        public string ArticleID         { get; set; }
        public string WorkEndDate       { get; set; }
        public string sLastProdArticleID { get; set; }
        public Ins_Label()
        {
            InstID              = "";
            MoveLabelID         = "";
            InsLabelID          = "";
            PrintQty            = "";
            WorkQty             = "";
            NowDate             = "";
            OutQtyPerBox        = "";
            Model               = "";
            BuyerModelID        = "";
            BuyerArticleNo      = "";
            Article             = "";
            ArticleID           = "";
            WorkEndDate = "";
            sLastProdArticleID = "";
        }
    }
}
