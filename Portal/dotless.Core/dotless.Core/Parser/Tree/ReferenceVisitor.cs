using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003A RID: 58
	public class ReferenceVisitor : IVisitor
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000AC98 File Offset: 0x00008E98
		public ReferenceVisitor(bool isReference)
		{
			this.isReference = isReference;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public Node Visit(Node node)
		{
			Ruleset ruleset = node as Ruleset;
			if (ruleset != null)
			{
				if (ruleset.Selectors != null)
				{
					ruleset.Selectors.Accept(this);
					ruleset.Selectors.IsReference = this.isReference;
				}
				if (ruleset.Rules != null)
				{
					ruleset.Rules.Accept(this);
					ruleset.Rules.IsReference = this.isReference;
				}
			}
			Media media = node as Media;
			if (media != null)
			{
				media.Ruleset.Accept(this);
				media.Ruleset.IsReference = this.isReference;
			}
			NodeList nodeList = node as NodeList;
			if (nodeList != null)
			{
				nodeList.Accept(this);
			}
			node.IsReference = this.isReference;
			return node;
		}

		// Token: 0x0400007D RID: 125
		private readonly bool isReference;
	}
}
