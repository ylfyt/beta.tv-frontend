using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Dtos
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
