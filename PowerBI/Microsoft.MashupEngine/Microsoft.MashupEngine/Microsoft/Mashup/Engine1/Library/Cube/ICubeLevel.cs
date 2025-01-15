using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D32 RID: 3378
	internal interface ICubeLevel : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x06005ADF RID: 23263
		ICubeHierarchy Hierarchy { get; }

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06005AE0 RID: 23264
		int Number { get; }
	}
}
