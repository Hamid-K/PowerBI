using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001092 RID: 4242
	public struct propName : IProgramNodeBuilder, IEquatable<propName>
	{
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06007FBB RID: 32699 RVA: 0x001AC6BA File Offset: 0x001AA8BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x001AC6C2 File Offset: 0x001AA8C2
		private propName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x001AC6CB File Offset: 0x001AA8CB
		public static propName CreateUnsafe(ProgramNode node)
		{
			return new propName(node);
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x001AC6D4 File Offset: 0x001AA8D4
		public static propName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.propName)
			{
				return null;
			}
			return new propName?(propName.CreateUnsafe(node));
		}

		// Token: 0x06007FBF RID: 32703 RVA: 0x001AC70E File Offset: 0x001AA90E
		public static propName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new propName(new Hole(g.Symbol.propName, holeId));
		}

		// Token: 0x06007FC0 RID: 32704 RVA: 0x001AC726 File Offset: 0x001AA926
		public propName(GrammarBuilders g, string value)
		{
			this = new propName(new LiteralNode(g.Symbol.propName, value));
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06007FC1 RID: 32705 RVA: 0x001AC73F File Offset: 0x001AA93F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x001AC756 File Offset: 0x001AA956
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x001AC76C File Offset: 0x001AA96C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x001AC796 File Offset: 0x001AA996
		public bool Equals(propName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AB RID: 13227
		private ProgramNode _node;
	}
}
