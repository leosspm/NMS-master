using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.MODEL.ViewModel
{
    public class ResponseViewModel
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
