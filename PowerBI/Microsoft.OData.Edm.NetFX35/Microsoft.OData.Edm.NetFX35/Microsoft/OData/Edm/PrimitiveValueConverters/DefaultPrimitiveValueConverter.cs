using System;
using System.Globalization;

namespace Microsoft.OData.Edm.PrimitiveValueConverters
{
	// Token: 0x020000D8 RID: 216
	internal class DefaultPrimitiveValueConverter : IPrimitiveValueConverter
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000A830 File Offset: 0x00008A30
		private DefaultPrimitiveValueConverter()
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000A838 File Offset: 0x00008A38
		public object ConvertToUnderlyingType(object value)
		{
			Type type = value.GetType();
			switch (PlatformHelper.GetTypeCode(type))
			{
			case 8:
				return Convert.ToInt32(value, CultureInfo.InvariantCulture);
			case 10:
				return Convert.ToInt64(value, CultureInfo.InvariantCulture);
			case 12:
				return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
			}
			return value;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		public object ConvertFromUnderlyingType(object value)
		{
			Type type = value.GetType();
			TypeCode typeCode = PlatformHelper.GetTypeCode(type);
			TypeCode typeCode2 = typeCode;
			switch (typeCode2)
			{
			case 9:
				return Convert.ToUInt16(value, CultureInfo.InvariantCulture);
			case 10:
				break;
			case 11:
				return Convert.ToUInt32(value, CultureInfo.InvariantCulture);
			default:
				if (typeCode2 == 15)
				{
					return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
				}
				break;
			}
			return value;
		}

		// Token: 0x040001A6 RID: 422
		internal static readonly IPrimitiveValueConverter Instance = new DefaultPrimitiveValueConverter();
	}
}
