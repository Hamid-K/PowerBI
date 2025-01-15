using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D2F RID: 3375
	internal interface ICubeObject : IEquatable<ICubeObject>
	{
		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x06005AD9 RID: 23257
		CubeObjectKind Kind { get; }
	}
}
