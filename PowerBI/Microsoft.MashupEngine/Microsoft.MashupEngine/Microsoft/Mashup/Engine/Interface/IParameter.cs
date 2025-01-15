using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B9 RID: 185
	public interface IParameter
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000304 RID: 772
		Identifier Identifier { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000305 RID: 773
		IExpression Type { get; }
	}
}
