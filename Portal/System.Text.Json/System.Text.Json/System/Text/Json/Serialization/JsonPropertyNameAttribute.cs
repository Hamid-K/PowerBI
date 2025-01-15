using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000079 RID: 121
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class JsonPropertyNameAttribute : JsonAttribute
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00024AE0 File Offset: 0x00022CE0
		public JsonPropertyNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00024AEF File Offset: 0x00022CEF
		public string Name { get; }
	}
}
