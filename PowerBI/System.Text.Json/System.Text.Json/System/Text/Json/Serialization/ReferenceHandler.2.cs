using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000098 RID: 152
	public sealed class ReferenceHandler<T> : ReferenceHandler where T : ReferenceResolver, new()
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x000273A4 File Offset: 0x000255A4
		[NullableContext(1)]
		public override ReferenceResolver CreateResolver()
		{
			return new T();
		}
	}
}
