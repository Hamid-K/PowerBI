using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000070 RID: 112
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectCreationHandlingAttribute : JsonAttribute
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002499E File Offset: 0x00022B9E
		public JsonObjectCreationHandling Handling { get; }

		// Token: 0x06000804 RID: 2052 RVA: 0x000249A6 File Offset: 0x00022BA6
		public JsonObjectCreationHandlingAttribute(JsonObjectCreationHandling handling)
		{
			if (!JsonSerializer.IsValidCreationHandlingValue(handling))
			{
				throw new ArgumentOutOfRangeException("handling");
			}
			this.Handling = handling;
		}
	}
}
