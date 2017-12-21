using System;
using System.Net;

namespace DgWebAPI
{
    public class ResultModel
    {
        public ResultModel(bool success, object data, string msg = "")
        {
            this.Success = success;
            this.Data = data;
            this.Msg = msg;
        }

        public bool Success { get; set; }
        public object Data { get; set; }
        public string Msg { get; set; }
    }
}
