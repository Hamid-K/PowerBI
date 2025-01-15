using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class JsonNumberHandlingAttribute : JsonAttribute
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00024A7B File Offset: 0x00022C7B
		public JsonNumberHandling Handling { get; }

		// Token: 0x06000816 RID: 2070 RVA: 0x00024A83 File Offset: 0x00022C83
		public JsonNumberHandlingAttribute(JsonNumberHandling handling)
		{
			if (!JsonSerializer.IsValidNumberHandlingValue(handling))
			{
				throw new ArgumentOutOfRangeException("handling");
			}
			this.Handling = handling;
		}
	}
}
