using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DgWebAPI
{
    public class ResultHandingMiddleware : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //根据实际需求进行具体实现
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = SetResponse(objectResult.Value);
            }
            else if (context.Result is NotFoundResult)
            {
                context.Result = new ObjectResult(new { code = 200, success = false, msg = "未找到数据" });
            }
            else if (context.Result is BadRequestResult)
            {
                context.Result = new ObjectResult(new { code = 200, success = false, msg = "入参错误" });
            }
            //else if (context.Result is EmptyResult)
            //{
            //    context.Result = new ObjectResult(new { code = 404, sub_msg = "未找到资源", msg = "" });
            //}
            //else if (context.Result is ContentResult)
            //{
            //    context.Result = new ObjectResult(new { code = 200, msg = "", result = (context.Result as ContentResult).Content });
            //}
            //else if (context.Result is StatusCodeResult)
            //{
            //    context.Result = new ObjectResult(new { code = (context.Result as StatusCodeResult).StatusCode, sub_msg = "", msg = "" });
            //}
        }

        private ObjectResult SetResponse(object data)
        {
            if (data != null)
            {
                if (data is ResultModel)
                {
                    var objectData = data as ResultModel;
                    return new ObjectResult(new { code = 200, success = objectData.Success, msg = objectData.Msg, data = ((ResultModel)data).Data });
                }
                else
                {
                    return new ObjectResult(new { code = 200, success = data.ToString() == "" ? true : false, msg = ((string)data).ToString() });
                }
            }
            else
            {
                return new ObjectResult(new { code = 200, success = true, msg = "成功" });
            }

        }
    }
}