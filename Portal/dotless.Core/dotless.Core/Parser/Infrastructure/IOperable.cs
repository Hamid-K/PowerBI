using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000057 RID: 87
	public interface IOperable
	{
		// Token: 0x060003DA RID: 986
		Node Operate(Operation op, Node other);

		// Token: 0x060003DB RID: 987
		Color ToColor();
	}
}
