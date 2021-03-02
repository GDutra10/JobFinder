using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Extensions
{
    public static class ExtensionsMethods
    {

        public static bool IsNullThrowException(this object pObject)
        {
            if (pObject == null)
            {
                throw new Exception($"{pObject.ToString()} is null!");
            }

            return false;
        }

        public static string ToStringOrEmpty(this object pObject)
        {

            if (pObject == null)
            {
                return string.Empty;
            }
            else
            {
                return pObject.ToString();
            }
        }

        public static int ToIntZeroAble(this object pObject)
        {
            if (pObject == null)
                return 0;

            var isNumeric = int.TryParse(pObject.ToString(), out int number);

            if (isNumeric)
                return number;

            return 0;

        }

    }
}
