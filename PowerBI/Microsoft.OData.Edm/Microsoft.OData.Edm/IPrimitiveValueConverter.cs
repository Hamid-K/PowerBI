using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000032 RID: 50
	public interface IPrimitiveValueConverter
	{
		// Token: 0x060000E4 RID: 228
		object ConvertToUnderlyingType(object value);

		// Token: 0x060000E5 RID: 229
		object ConvertFromUnderlyingType(object value);
	}
}
