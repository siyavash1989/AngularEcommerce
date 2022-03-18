using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessage(int statusCode)
        {
            return StatusCode switch
            {
                400 => "خطا در درخواست ارسالی از سوی کاربر",
                401 => "عدم احراز هویت",
                404 => "اطلاعات درخواستی یافت نشد",
                500 => "خطا در سمت سرور رخ داده است",
                _ => "خطای شناسایی نشده"
            };
        }
    }
}