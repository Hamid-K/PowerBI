using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200008D RID: 141
	public interface IResource
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020F RID: 527
		string Kind { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000210 RID: 528
		string Path { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000211 RID: 529
		string NonNormalizedPath { get; }
	}
}
