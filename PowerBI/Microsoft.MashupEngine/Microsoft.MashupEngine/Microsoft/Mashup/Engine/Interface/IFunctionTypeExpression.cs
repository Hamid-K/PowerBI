using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B8 RID: 184
	public interface IFunctionTypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000301 RID: 769
		IExpression ReturnType { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000302 RID: 770
		IList<IParameter> Parameters { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000303 RID: 771
		int Min { get; }
	}
}
