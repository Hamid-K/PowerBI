using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	internal interface IValueProvider
	{
		// Token: 0x06000684 RID: 1668
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x06000685 RID: 1669
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
