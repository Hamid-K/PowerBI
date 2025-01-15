using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000009 RID: 9
	public interface IPrimitiveValueConverter
	{
		// Token: 0x06000024 RID: 36
		object ConvertToUnderlyingType(object value);

		// Token: 0x06000025 RID: 37
		object ConvertFromUnderlyingType(object value);
	}
}
