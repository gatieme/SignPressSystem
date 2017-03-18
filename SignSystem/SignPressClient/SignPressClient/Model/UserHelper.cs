using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{
    public class UserHelper
    {
        //public UserHelper()
        //{ 
            
        //}
        public const string DEFAULT_SIGNATURE_PATH = "./signature/";
 
        public static Employee UserInfo { get; set; }

        public static List<Department> DepList { get; set; }
        public static List<SDepartment> SDepList { get; set; }
        public static List<MDepartment> MDepList { get; set; } 

        public static List<Employee> EmpList { get; set; }
        public static List<sEmployee> sEmpList { get; set; }

        public static List<Templete> TempList { get; set; }

        public static Templete SelectedTemp { get; set; }
        //对应增加内河的模版集合
        public static List<Templete_Inside> TempInsideList { get; set; }
        public static Templete_Inside SelectedTempInside { get; set; }
        //

        //对应增加不同类型的模版集合
        public static List<Templete_All> TempAllList { get; set; }
        public static Templete_All SelectedTempAll { get; set; }


        public static List<SHDJContract> ToDoList { get; set; }


        public static List<SHDJContract> DoneList { get; set; }

        public static List<SHDJContract> PenddingList { get; set; }

        public static List<SHDJContract> RefuseList { get; set; }

        public static List<SHDJContract> AgreeList { get; set; }
        public static List<SHDJContract> AgreeUndownList { get; set; }

        public static List<String> DepartmentShortCallList{ get; set;}          ///  存放部门简称

        public static List<ContractCategory> ContractCategoryList { get; set; }
        public static List<ContractProject> ContractProjectList { get; set; }
        public static List<ContractItem> ContractItemList { get; set; }
    }

}
