using System;

namespace Microsoft.OData.Edm.PrimitiveValueConverters
{
	// Token: 0x02000011 RID: 17
	internal class PassThroughPrimitiveValueConverter : IPrimitiveValueConverter
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000028FB File Offset: 0x00000AFB
		private PassThroughPrimitiveValueConverter()
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002903 File Offset: 0x00000B03
		public object ConvertToUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002906 File Offset: 0x00000B06
		public object ConvertFromUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x0400001D RID: 29
		internal static readonly IPrimitiveValueConverter Instance = new PassThroughPrimitiveValueConverter();
	}
}
