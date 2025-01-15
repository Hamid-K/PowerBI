using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class JsonIgnoreAttribute : JsonAttribute
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00024A53 File Offset: 0x00022C53
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00024A5B File Offset: 0x00022C5B
		public JsonIgnoreCondition Condition { get; set; } = JsonIgnoreCondition.Always;
	}
}
