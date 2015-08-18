using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Helper
{
    //使用枚举会好一点
    public class Position
    {
         public static readonly int President = 10001;//总裁
         public static readonly int Minister = 10002;//部长
         public static readonly int StudyLeader = 10003;//顾问团团长
         public static readonly int StudyMember = 10004;//顾问团团员
         public static readonly int PlanLeader = 10005;//策划组组长
         public static readonly int PlanMmember = 10006;//策划组成员
         public static readonly int Financial = 10007;//财务主管
         public static readonly int TechnicalGuide = 10009; //技术指导
    }

    public class Department
    {
        public static readonly int NET = 10001;//.NET
        public static readonly int JAVA = 10002;//JAVA
        public static readonly int HardWare = 10003;//硬件编程
        public static readonly int System = 10004;//系统编程部
        public static readonly int Design = 10005;//设计
        public static readonly int Scheme = 10007;//策划
    }

    public class TechnicalLevel
    {
        public static readonly int  TechBackbone = 10001;//技术骨干
        public static readonly int EliteProgram = 10002;//项目精英
        public static readonly int  FullMember = 10004;//正式成员
        public static readonly int Technician = 10005;//技术人员
        public static readonly int Student = 10003;//实习生
    }


    public enum PositionEx
    {
        President = 10001,//总裁
        Minister = 10002,//部长       
        StudyLeader = 10003,//顾问团团长
        StudyMember = 10004,//顾问团团员
        PlanLeader = 10005,//策划组组长
        PlanMmember = 10006,//策划组成员
        Financial = 10007,//财务主管
        TechnicalGuide=10008,//技术指导
        CommonMember = 10009, //普通成员
        System=10010,//系统管理员
        DepMember=10011//录入部门成员
    }
}
