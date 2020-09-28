namespace KERRY.NMS.CORE.Utilities
{
    public static class Utils
    {
        //public static string ValueFilter(string value, FilterType filterType)
        //{
        //    switch (filterType)
        //    {
        //        case FilterType.Currency:
        //            var currency = string.Empty;
        //            if (value.Split('.')[0].Length > 9)
        //                currency = string.Format("{0:#.0}{1}", Convert.ToDecimal(value) / 1000000000, "tỷ");
        //            else if (value.Split('.')[0].Length > 6)
        //                currency = string.Format("{0:#.0}{1}", Convert.ToDecimal(value) / 1000000, "triệu");
        //            else
        //                currency = string.Format("{0}{1}", value, "VND");
        //            return currency;
        //        case FilterType.Length:
        //            var length = string.Format("{0}{1}", value, "m");
        //            return length;
        //        case FilterType.Square:
        //            var square = string.Format("{0}{1}", value, "m²");
        //            return square;
        //        case FilterType.Date:
        //            var date = DateTime.Parse(value);
        //            return date.ToString("dd-MM-yyyy");
        //        default:
        //            return value.ToString();
        //    }
        //}
    }
}
