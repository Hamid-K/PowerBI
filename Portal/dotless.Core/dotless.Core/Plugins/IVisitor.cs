using System;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Plugins
{
	// Token: 0x0200001C RID: 28
	public interface IVisitor
	{
		// Token: 0x060000A8 RID: 168
		Node Visit(Node node);
	}
}
