using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D2D RID: 3373
	internal interface ICube
	{
		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06005AD4 RID: 23252
		IdentifierCubeExpression Identifier { get; }

		// Token: 0x06005AD5 RID: 23253
		bool TryGetObject(IdentifierCubeExpression identifier, out ICubeObject obj);
	}
}
