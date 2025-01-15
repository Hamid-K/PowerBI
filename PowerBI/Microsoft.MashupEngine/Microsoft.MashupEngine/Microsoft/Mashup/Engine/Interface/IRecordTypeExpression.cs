using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B4 RID: 180
	public interface IRecordTypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002FC RID: 764
		IList<IFieldType> Fields { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002FD RID: 765
		bool Wildcard { get; }
	}
}
