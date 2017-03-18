using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{
    public enum Response
    {
        LOGIN_SUCCESS,    //登录成功
        LOGIN_FAILED,     //登录失败
        UnConnection,  //无法连接

        INSERT_DEPARTMENT_SUCCESS,   //添加部门成功

        QUERY_DEPARTMENT_SUCCESS,       //查询部门成功
        DELETE_DEPARTMENT_SUCCESS,      //删除部门成功  
        QUERY_DEPARTMENT_SHORTCALL_SUCCESS,
        QUERY_DEPARTMENT_SHORTCALL_FAILED,

        QUERY_EMPLOYEE_SUCCESS,     //查询部门员工成功
        INSERT_EMPLOYEE_SUCCESS,    //添加部门员工成功
        INSERT_EMPLOYEE_FAILED,
        INSERT_EMPLOYEE_EXIST,
        DELETE_EMPLOYEE_SUCCESS,    //删除部门员工成功

        INSERT_CONTRACT_TEMPLATE_SUCCESS,       //添加模板成功
        QUERY_CONTRACT_TEMPLATE_SUCCESS,         //查询模板成功
        GET_CONTRACT_TEMPLATE_SUCCESS,          //获取特定模板成功
        DELETE_CONTRACT_TEMPLATE_SUCCESS,        //删除特定模板成功
        MODIFY_CONTRACT_TEMPLATE_SUCCESS,         //修改模板信息成功                 
        INSERT_HDJCONTRACT_SUCCESS,                //添加签单成功
        DELETE_HDJCONTRACT_SUCCESS,             // 删除会签单陈功
        
        QUERY_SIGN_PEND_SUCCESS,               //查询正在审批的方案
        QUERY_SIGN_AGREE_SUCCESS,              //查询已通过的方案
        QUERY_SIGN_REFUSE_SUCCESS,              //查询已拒绝的方案

        QUERY_UNSIGN_CONTRACT_SUCCESS,           //查询等待签单列表成功
        QUERY_SIGNED_CONTRACT_SUCCESS,            //查询已签字列表成功
        GET_HDJCONTRACT_SUCCESS,                  //获取会签单成功
        GET_HDJCONTRACT_WITH_WORKLOAD_SUCCESS,
        GET_HDJCONTRACT_WITH_WORKLOAD_FAILED,
        INSERT_SIGN_DETAIL_SUCCESS,                //签单签字成功
        UPLOAD_PICTURE_SUCCESS,                     //上传图片成功
        MODIFY_HDJCONTRACT_SUCCESS,                    //重新提交方案成功

        SEARCH_AGREE_HDJCONTRACT_SUCCESS,               //条件查询已通过方案成功
        SEARCH_SIGNED_HDJCONTRACT_SUCCESS,               //条件查询已办理方案成功

        MODIFY_EMP_PWD_SUCCESS,                          //  重置用户密码成功
        MODIFY_EMP_PWD_FAILED,                           //  重置用户密码失败

        MODIFY_EMPLOYEE_SUCCESS,                //  修改员工信息
        MODIFY_EMPLOYEE_FAILED,                //  修改员工信息

        MODIFY_DEPARTMENT_SUCCESS,
        MODIFY_DEPARTMENT_FAILED,

        QUERY_AGREE_UNDOWN_SUCCESS,
        QUERY_AGREE_UNDOWN_FAILED,

        DELETE_DEPARTMENT_EXIST_EMPLOYEE,              //该部门存在员工无法删除
        DELETE_EMPLOYEE_EXIST_CONTRACT,     //  待删除的员工存在会签单信息无法删除
        DELETE_EMPLOYEE_EXIST_CONTEMP,      //  待删除的员工在某个会签模版中，无法删除
        DELETE_CONTRACT_TEMPLATE_EXIST_CONTRACT,
        INSERT_HDJCONTRACT_EXIST,



        // Modify by gatieme at 2015-08-26 13:44
        QUERY_SDEPARTMENT_SUCCESS,
        QUERY_SDEPARTMENT_FAILED,

        // Modify by gatieme at 2015-09-07 10:51
        MODIFY_SDEPARTMENT_SUCCESS,
        MODIFY_SDEPARTMENT_FAILED,
        INSERT_SDEPARTMENT_SUCCESS,             //添加部门权限成功
        QUERY_SDEPARTMENT_CATEGORY_SUCCESS,
        QUERY_CATEGORY_PROJECT_SUCCESS,

        QUERY_SDEP_CON_CATEGORY_SUCCESS,
        QUERY_SDEP_CON_CATEGORY_FAILED,

        //  统计信息
        GET_CATEGORY_YEAR_CONTRACT_COUNT_SUCCESS,       // 获取当年已经签署的CATEGORY的会签单数目
        GET_CATEGORY_YEAR_CONTRACT_COUNT_FAILED,        // 获取当年已经签署的CATEGORY的会签单数目

        GET_DEP_CATE_YEAR_CON_COUNT_SUCCESS,        // [2015/12/17]编号最后两位的数目是当年部门department本年度year分类category下的会签单数目
        GET_DEP_CATE_YEAR_CON_COUNT_FAILED,        // [2015/12/17]编号最后两位的数目是当年部门department本年度year分类category下的会签单数目


        QUERY_PROJECT_ITEM_SUCCESS,
        QUERY_PROJECT_ITEM_BY_NAME_SUCCESS,
        
        INSERT_ITEM_SUCCESS,
        INSERT_ITEM_FAILED,
        DELETE_ITEM_SUCCESS,
        DELETE_ITEM_FAILED,
        MODIFY_ITEM_SUCCESS,
        MODIFY_ITEM_FAILED,
        


        INSERT_PROJECT_SUCCESS,
        INSERT_PROJECT_FAILED,
        DELETE_PROJECT_SUCCESS,
        DELETE_PROJECT_FAILED,
        MODIFY_PROJECT_SUCCESS,
        MODIFY_PROJECT_FAILED,

        STATISTIC_CATEGORY_SUCCESS,                     // 统计信息
        STATISTIC_CATEGORY_FAILED,                     // 统计信息
        ///  用户获取当前部门department当前工程的总申请额度  
        STATISTIC_DEP_YEAR_PRO_SUCCESS,
        STATISTIC_DEP_YEAR_PRO_FAILED,
        STATISTIC_DEP_YEAR_CATEGORY_SUCCESS,
        STATISTIC_DEP_YEAR_CATEGORY_FAILED,
    }
}
