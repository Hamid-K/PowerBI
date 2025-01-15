using System;

namespace Microsoft.OData.Edm.PrimitiveValueConverters
{
	// Token: 0x02000010 RID: 16
	public interface IPrimitiveValueConverter
	{
		// Token: 0x0600004F RID: 79
		object ConvertToUnderlyingType(object value);

		// Token: 0x06000050 RID: 80
		object ConvertFromUnderlyingType(object value);
	}
}
