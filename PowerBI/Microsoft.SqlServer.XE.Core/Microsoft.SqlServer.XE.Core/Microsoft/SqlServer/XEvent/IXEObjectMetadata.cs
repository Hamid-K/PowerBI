using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200001F RID: 31
	public interface IXEObjectMetadata
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008C RID: 140
		string Name { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008D RID: 141
		IPackage Package { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008E RID: 142
		string Description { get; }
	}
}
