using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000609 RID: 1545
	internal abstract class ConversionContext<T_Identifier>
	{
		// Token: 0x06004B5C RID: 19292
		internal abstract Vertex TranslateTermToVertex(TermExpr<T_Identifier> term);

		// Token: 0x06004B5D RID: 19293
		internal abstract IEnumerable<LiteralVertexPair<T_Identifier>> GetSuccessors(Vertex vertex);

		// Token: 0x04001A54 RID: 6740
		internal readonly Solver Solver = new Solver();
	}
}
