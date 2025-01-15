using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D33 RID: 3379
	internal interface ICubeHierarchy : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x06005AE1 RID: 23265
		ICubeLevel GetLevel(int number);
	}
}
