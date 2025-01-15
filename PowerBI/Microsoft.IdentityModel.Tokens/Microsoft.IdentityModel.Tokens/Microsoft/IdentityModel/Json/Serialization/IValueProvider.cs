using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000084 RID: 132
	[NullableContext(1)]
	internal interface IValueProvider
	{
		// Token: 0x0600068E RID: 1678
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x0600068F RID: 1679
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
