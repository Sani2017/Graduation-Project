using Common;
using DAL;
using Models;
using Models.ModelTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SortBLL
    {
        SortDAL dt = new SortDAL();
        public object AllSort() {
            return dt.GetSort();
        }
        public TMessage<Sort> AddSort(string SortName)
        {
            return dt.AddSort(SortName);
        }
        public TMessage<Sort> DelSortById(int Id){
            return dt.DelSortById(Id);
        }
        public TMessage<Sort> UpadteSortInFo(SortModel model) {
            return dt.UpadteSortInfo(model);
        }
        public object DeleBatchSort(int[] DeleBatchById) {
            return dt.DeleBatchActivityInfo(DeleBatchById);
        }
    }
}
