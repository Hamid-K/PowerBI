using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001E RID: 30
	public interface ICustomAttributeMetadata
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000089 RID: 137
		string Name { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008A RID: 138
		Type Type { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008B RID: 139
		object DefaultValue { get; }
	}
}
