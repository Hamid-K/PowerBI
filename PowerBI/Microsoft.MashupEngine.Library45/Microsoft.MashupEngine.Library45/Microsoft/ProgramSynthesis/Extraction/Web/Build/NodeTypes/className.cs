using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108F RID: 4239
	public struct className : IProgramNodeBuilder, IEquatable<className>
	{
		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06007F9D RID: 32669 RVA: 0x001AC3EA File Offset: 0x001AA5EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x001AC3F2 File Offset: 0x001AA5F2
		private className(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x001AC3FB File Offset: 0x001AA5FB
		public static className CreateUnsafe(ProgramNode node)
		{
			return new className(node);
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x001AC404 File Offset: 0x001AA604
		public static className? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.className)
			{
				return null;
			}
			return new className?(className.CreateUnsafe(node));
		}

		// Token: 0x06007FA1 RID: 32673 RVA: 0x001AC43E File Offset: 0x001AA63E
		public static className CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new className(new Hole(g.Symbol.className, holeId));
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x001AC456 File Offset: 0x001AA656
		public className(GrammarBuilders g, string value)
		{
			this = new className(new LiteralNode(g.Symbol.className, value));
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06007FA3 RID: 32675 RVA: 0x001AC46F File Offset: 0x001AA66F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x001AC486 File Offset: 0x001AA686
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x001AC49C File Offset: 0x001AA69C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x001AC4C6 File Offset: 0x001AA6C6
		public bool Equals(className other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A8 RID: 13224
		private ProgramNode _node;
	}
}
