using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001099 RID: 4249
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06008001 RID: 32769 RVA: 0x001ACD56 File Offset: 0x001AAF56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x001ACD5E File Offset: 0x001AAF5E
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x001ACD67 File Offset: 0x001AAF67
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x001ACD70 File Offset: 0x001AAF70
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x001ACDAA File Offset: 0x001AAFAA
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x06008006 RID: 32774 RVA: 0x001ACDC2 File Offset: 0x001AAFC2
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06008007 RID: 32775 RVA: 0x001ACDE0 File Offset: 0x001AAFE0
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x001ACDF7 File Offset: 0x001AAFF7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x001ACE0C File Offset: 0x001AB00C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x001ACE36 File Offset: 0x001AB036
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B2 RID: 13234
		private ProgramNode _node;
	}
}
