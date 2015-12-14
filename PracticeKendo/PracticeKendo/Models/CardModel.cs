using System.Collections.Generic;
using System.Web;

namespace PracticeKendo.Models
{
    public class CardModel
    {
        public double Id { set; get; }
        public HtmlString CardInfo { set; get; }
        public IList<TransactionModel> Transactions { get; set; }
    }
}