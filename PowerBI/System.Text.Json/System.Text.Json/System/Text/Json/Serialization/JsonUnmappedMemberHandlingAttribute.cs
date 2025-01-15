using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public class JsonUnmappedMemberHandlingAttribute : JsonAttribute
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00024B16 File Offset: 0x00022D16
		public JsonUnmappedMemberHandlingAttribute(JsonUnmappedMemberHandling unmappedMemberHandling)
		{
			this.UnmappedMemberHandling = unmappedMemberHandling;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00024B25 File Offset: 0x00022D25
		public JsonUnmappedMemberHandling UnmappedMemberHandling { get; }
	}
}
