using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D30 RID: 3376
	internal interface ICubeObject2 : ICubeObject, IEquatable<ICubeObject>
	{
		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x06005ADA RID: 23258
		IdentifierCubeExpression Identifier { get; }

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x06005ADB RID: 23259
		string Caption { get; }

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x06005ADC RID: 23260
		TypeValue Type { get; }
	}
}
