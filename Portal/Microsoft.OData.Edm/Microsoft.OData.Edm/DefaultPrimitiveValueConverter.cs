using System;
using System.Globalization;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000004 RID: 4
	internal class DefaultPrimitiveValueConverter : IPrimitiveValueConverter
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		private DefaultPrimitiveValueConverter()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002068 File Offset: 0x00000268
		public object ConvertToUnderlyingType(object value)
		{
			if (value is ushort)
			{
				return Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
			if (value is uint)
			{
				return Convert.ToInt64(value, CultureInfo.InvariantCulture);
			}
			if (value is ulong)
			{
				return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
			}
			return value;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		public object ConvertFromUnderlyingType(object value)
		{
			if (value is int)
			{
				return Convert.ToUInt16(value, CultureInfo.InvariantCulture);
			}
			if (value is long)
			{
				return Convert.ToUInt32(value, CultureInfo.InvariantCulture);
			}
			if (value is decimal)
			{
				return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			}
			return value;
		}

		// Token: 0x04000005 RID: 5
		internal static readonly IPrimitiveValueConverter Instance = new DefaultPrimitiveValueConverter();
	}
}
