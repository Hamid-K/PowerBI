using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000617 RID: 1559
	internal sealed class LiteralVertexPair<T_Identifier>
	{
		// Token: 0x06004BA5 RID: 19365 RVA: 0x0010AE3A File Offset: 0x0010903A
		internal LiteralVertexPair(Vertex vertex, Literal<T_Identifier> literal)
		{
			this.Vertex = vertex;
			this.Literal = literal;
		}

		// Token: 0x04001A73 RID: 6771
		internal readonly Vertex Vertex;

		// Token: 0x04001A74 RID: 6772
		internal readonly Literal<T_Identifier> Literal;
	}
}
