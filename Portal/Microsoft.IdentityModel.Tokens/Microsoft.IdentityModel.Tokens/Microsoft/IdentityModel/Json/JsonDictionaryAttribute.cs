using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	internal sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002FB4 File Offset: 0x000011B4
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002FBC File Offset: 0x000011BC
		public JsonDictionaryAttribute(string id)
			: base(id)
		{
		}
	}
}
