using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D3 RID: 211
	public interface IRecordExpression : IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600032D RID: 813
		Identifier Identifier { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600032E RID: 814
		IList<VariableInitializer> Members { get; }
	}
}
