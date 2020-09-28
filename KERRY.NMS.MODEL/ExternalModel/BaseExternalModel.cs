using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.MODEL.ExternalModel
{
    public class BaseExternalModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}
