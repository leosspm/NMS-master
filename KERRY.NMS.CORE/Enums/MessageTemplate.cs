using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.CORE.Enums
{
    public sealed class MessageTemplate
    {
        private MessageTemplate() { }

        public const string STAFF_ACCEPTED_PICKUP = "Đơn hàng {0} của quý khách đã được tiếp nhận. Nhân viên Kerry đang trên đường đến lấy đơn hàng. Quý khách vui lòng chuẩn bị hàng và để ý điện thoại. Xin cám ơn";
        public const string STAFF_CANCEL_PICKUP = "Nhân viên Kerry Express đã từ chối tiếp nhận đơn {0} với lý do: {1}. Mọi thắc mắc xin liên hệ hotline 19006663.";
        public const string STAFF_PICKUP_SUCCESSED = "Đơn hàng {0} của quý khách đã được nhân viên lấy thành công. Quý khách có thể theo dõi trạng thái đơn hàng tại đường dẫn: https://kerryexpress.com.vn/trang-thai-don-hang/?trackingid={0}. Mọi thắc mắc xin liên hệ hotline 19006663.";
        public const string STAFF_PICKUP_FAILED = "Đơn hàng {0} của quý khách đã nhận hàng không thành công với lý do: {1}. Mọi thắc mắc xin liên hệ hotline 19006663.";
        public const string STAFF_DELIVER_SUCCESSED = "Đơn hàng {0} của quý khách đã được giao thành công. Xin cảm ơn quý khách!";


    }
}
