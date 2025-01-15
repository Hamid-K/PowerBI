using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D35 RID: 3381
	internal interface ICubeMeasureProperty : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06005AE2 RID: 23266
		ICubeMeasure Measure { get; }
	}
}
