using System;
using API.Classes;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static MemberSince CalculateDateSince(this DateTime d2)
        {
            var MemberSince = new MemberSince();
            var d1 = DateTime.Now;
            var span = d1 - d2;

            MemberSince.Months = (12 * (d1.Year - d2.Year)) + (d1.Month - d2.Month);

            //month may need to be decremented because the above calculates the ceiling of the months, not the floor.
            //to do so we increase d2 by the same number of months and compare.
            //(500ms fudge factor because datetimes are not precise enough to compare exactly)
            if (d1.CompareTo(d2.AddMonths(MemberSince.Months).AddMilliseconds(-500)) <= 0)
            {
                --MemberSince.Months;
            }

            MemberSince.Years = MemberSince.Months / 12;
            MemberSince.Months -= MemberSince.Years * 12;

            if (MemberSince.Months == 0 && MemberSince.Years == 0)
            {
                MemberSince.Days = span.Days;
            }
            else
            {
                var md1 = new DateTime(d1.Year, d1.Month, d1.Day);
                var md2 = new DateTime(d2.Year, d2.Month, d2.Day);
                var mDays = (int)(md1 - md2).TotalDays;

                if (mDays > span.Days)
                {
                    mDays = (int)(md1.AddMonths(-1) - md2).TotalDays;
                }

                MemberSince.Days = span.Days - mDays;
            }

            return MemberSince;
        }
    }
}