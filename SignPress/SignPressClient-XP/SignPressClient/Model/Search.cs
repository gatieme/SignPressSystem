using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignPressClient.Model
{
    public class Search
    {
        public int EmployeeId { get; set; }
        public string ConId { get; set; }
        public string ProjectName { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public int Downloadable { get; set; }

        //  会签单统计信息
        // 用户查询表中，{ 当前类别CategoryShortCall | 当前部门签署的SDepartmentShortlCall | 当前年份Year的} 会签单信息或者数目 
        public String CategoryShortCall { get; set; }
        public String CategoryName { get; set; }
        public String SDepartmentShortlCall { get; set; }
        public int ItemId { get; set; }
        public int ProjectId { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
    }
}
