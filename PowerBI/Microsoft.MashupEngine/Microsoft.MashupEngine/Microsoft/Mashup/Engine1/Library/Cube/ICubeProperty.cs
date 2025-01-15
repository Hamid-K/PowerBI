using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D31 RID: 3377
	internal interface ICubeProperty : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x06005ADD RID: 23261
		CubePropertyKind PropertyKind { get; }

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x06005ADE RID: 23262
		ICubeLevel Level { get; }
	}
}
