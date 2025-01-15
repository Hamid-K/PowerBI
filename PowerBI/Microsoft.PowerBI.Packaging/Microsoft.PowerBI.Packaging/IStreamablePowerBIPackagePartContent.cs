using System;
using System.IO;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200001D RID: 29
	public interface IStreamablePowerBIPackagePartContent
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BC RID: 188
		string ContentType { get; }

		// Token: 0x060000BD RID: 189
		Stream GetStream();
	}
}
