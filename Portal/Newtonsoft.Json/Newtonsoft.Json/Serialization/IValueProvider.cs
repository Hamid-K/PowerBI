using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x0600068D RID: 1677
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x0600068E RID: 1678
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
