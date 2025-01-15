using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000003 RID: 3
	internal class PassThroughPrimitiveValueConverter : IPrimitiveValueConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private PassThroughPrimitiveValueConverter()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public object ConvertToUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public object ConvertFromUnderlyingType(object value)
		{
			return value;
		}

		// Token: 0x04000004 RID: 4
		internal static readonly IPrimitiveValueConverter Instance = new PassThroughPrimitiveValueConverter();
	}
}
