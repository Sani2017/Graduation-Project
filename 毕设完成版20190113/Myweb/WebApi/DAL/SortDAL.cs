using Common;
using Models;
using Models.ModelTemplate;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAl.Models;

namespace DAL
{
    public class SortDAL
    {
        SqlSugarClient db;
        public SortDAL() {
            db = SqlSugarClientHelper.SqlSugarDB();
        }
        /// <summary>
        /// 查询所有类别表内容
        /// </summary>
        /// <returns></returns>
        public object GetSort() {
            var getAll = db.Queryable<Sort>().ToList();
            return getAll;
        }
        /// <summary>
        /// 增加作品类型
        /// </summary>
        /// <param name="model">类型表传参model类</param>
        /// <returns></returns>
        public TMessage<Sort> AddSort(string SortName)
        {
            TMessage<Sort> mes= new TMessage<Sort>();
            if (SeleSortInfoBySortName(SortName))//true，已存在不作为添加
            {
                mes.suc = false;
                mes.mes = ConstHelper.SORT_NAME_EXISTED + "," + ConstHelper.INSERT_MODEL_ERROR;
            }
            else { 
                Sort sort = new Sort();
                sort.SortName = SortName;
                var AddSortInfo = db.Insertable(sort)
                    .InsertColumns(it => new { it.SortName })
                    .ExecuteCommand(); //.ToSql();
                mes.suc = true;
                mes.mes = ConstHelper.INSERT_MODEL_SUCCESS + AddSortInfo + "条";
            }
            return mes;
        }
        /// <summary>
        /// 根据Id删除作品类型
        /// </summary>
        /// <param name="Id">作品类型Id</param>
        /// <returns></returns>
        public TMessage<Sort> DelSortById(int Id) {
            TMessage<Sort> mes = new TMessage<Sort>();

            var getInfo = db.Queryable<Works>().Where(wk => wk.Sort == Id
                &&wk.AllowShow == (int)EnumType.StateResolution.OneType
                 && wk.IsDeleted == (int)Common.EnumType.StateResolution.ZeroType).ToList();
            if (getInfo.Count >= 1)
            {
                mes.suc = false;
                mes.mes = "该类型下已存在作品，无法删除";
            }
            else { 

                var DelInfoById =  db.Deleteable<Sort>(Id).ExecuteCommand();//.ToSql();//
                if (DelInfoById < 1)
                {
                    mes.suc = false;
                    mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
                }
                else {
                    mes.suc = true;
                    mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + DelInfoById + "条";
                }
            }
            return mes;
        }
        /// <summary>
        /// 根据id修改类型内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TMessage<Sort> UpadteSortInfo(SortModel model) {
            TMessage<Sort> mes = new TMessage<Sort>();
            if (SeleSortInfoBySortName(model.SortName))
            {
                mes.suc = false;
                mes.mes = ConstHelper.SORT_NAME_EXISTED + "," + ConstHelper.UPDATE_MODEL_ERROR;
            }
            else {
                Sort sort = new Sort();
                sort.SortName = model.SortName.Trim();
                sort.Id = model.Id;
                var UpdateInfo = db.Updateable(sort).WhereColumns(it => new { it.Id }).ExecuteCommand();
                mes.suc = true;
                mes.mes = ConstHelper.MODIFY_SUCCESS + UpdateInfo + "条";
            }
            return mes;
        }
        /// <summary>
        /// 批量删除作品类型
        /// </summary>
        /// <param name="DeleBatchById"></param>
        /// <returns></returns>
        public TMessage<Sort> DeleBatchActivityInfo(int[] DeleBatchById) {
            TMessage<Sort> mes = new TMessage<Sort>();
            var t4 = db.Deleteable<Sort>().Where(it => DeleBatchById.Contains(it.Id)).ExecuteCommand();//.ToSql();//.In(new int[] { 1,2,3}).ToSql();//.ExecuteCommand();
            if (t4 >= 1)
            {
                mes.suc = true;
                mes.mes = ConstHelper.DELETE_MODEL_SUCCESS + t4 + "条";
            }
            else
            {
                mes.suc = false;
                mes.mes = ConstHelper.GET_NOTHING + "," + ConstHelper.DELETE_MODEL_ERROR;
            }
            return mes;
        }
        /// <summary>
        /// 判断类型名称是否重复
        /// </summary>
        /// <param name="SortName">查询用的类型名称</param>
        /// <returns></returns>
        public bool SeleSortInfoBySortName(string SortName)
        {
            var isAny = db.Queryable<Sort>().Where(it => it.SortName == SortName).Any();
            return isAny;
        }
    }
}
