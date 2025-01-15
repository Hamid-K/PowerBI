using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200007B RID: 123
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class JsonPropertyOrderAttribute : JsonAttribute
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00024AFF File Offset: 0x00022CFF
		public JsonPropertyOrderAttribute(int order)
		{
			this.Order = order;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00024B0E File Offset: 0x00022D0E
		public int Order { get; }
	}
}
