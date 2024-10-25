using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using System.Reflection.Emit;


namespace OCOO.Models
{
    public class Department : Menu
    {
        public string? Pk_DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
      
        public string? Pk_UserId { get; set; }
        public DataSet GetMasterData()
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@OpCode", OpCode),
                new SqlParameter("@Value", Value)

            };
                DataSet ds = Connection.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ManageDepartment()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_DepartmentId",Pk_DepartmentId),
                    new SqlParameter("@Pk_UserId",Pk_UserId),
                    new SqlParameter("@DepartmentName",DepartmentName),
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),


                };
                ds = Connection.ExecuteQuery(ProcedureName.DepartmentMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }

    public class Degination : Menu
    {
        public string? Pk_DepartmentId { get; set; }
        public string? Pk_UserId { get; set; }
        public string? DepartmentName { get; set; }
        public string? Pk_DesignationId { get; set; }
        public string? Sequence { get; set; }
        public string? Approval { get; set; }        
        public bool IsSelectValue { get; set; }
        public bool IsSaveValue { get; set; }
        public bool IsDeleteValue { get; set; }
        public string? SelectedValue { get; set; }
        public DataTable? UserTypeDesignation { get; set; }

        public string? FormSave { get; set; }
        public string? FormView { get; set; }
        public string? FormUpdate { get; set; }
        public string? FormDelete { get; set; }

        public List<Degination> lstDegination = new List<Degination>();
        public DataSet ManageDegination()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@OpCode",OpCode),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@Pk_DesignationId",Pk_DesignationId),                 
                    new SqlParameter("@Pk_UserId",Pk_UserId),
                    new SqlParameter("@DesignationName",DesignationName),                   
                    new SqlParameter("@Sequence",Sequence),                   
                    new SqlParameter("@IsApproval",Approval=="0"?false:true),                   
                    new SqlParameter("@Page", Page),
                    new SqlParameter("@Size", Size),


                };
                ds = Connection.ExecuteQuery(ProcedureName.DeginationMaster, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet GetDegination()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {                                       
                    new SqlParameter("@FK_DeptId",Fk_DepartmentId),
                    
                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_GetDesignation, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet SaveDegination()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para =
                {
                      new SqlParameter("@FK_DeptId",Fk_DepartmentId),
                      new SqlParameter("@Deslist",UserTypeDesignation),                                            
                        new SqlParameter("@AddedBy", AddedBy)

                };
                ds = Connection.ExecuteQuery(ProcedureName.SP_Dept_Des_Link, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}

