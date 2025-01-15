using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000004 RID: 4
	internal class PassThroughPrimitiveValueConverter : IPrimitiveValueConverter
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		private PassThroughPrimitiveValueConverter()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002090 File Offset: 0x00000290
		public object ConvertToUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002090 File Offset: 0x00000290
		public object ConvertFromUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x04000006 RID: 6
		internal static readonly IPrimitiveValueConverter Instance = new PassThroughPrimitiveValueConverter();
	}
}
