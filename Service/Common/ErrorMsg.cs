using System;
namespace DgWebAPI.Service
{
    public static class ErrorMsg
    {
        public static string FailMsg(){
            return "操作失败";
        }

        public static string AddFailMsg(){
            return "添加失败";
        }

        public static string DeleteFailMsg()
        {
            return "删除失败";
        }

        public static string UpdateFailMsg()
        {
            return "更新失败";
        }
    }
}
