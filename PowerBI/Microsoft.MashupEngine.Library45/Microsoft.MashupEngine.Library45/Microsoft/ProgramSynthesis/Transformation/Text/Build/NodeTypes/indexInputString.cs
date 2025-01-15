using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3F RID: 7231
	public struct indexInputString : IProgramNodeBuilder, IEquatable<indexInputString>
	{
		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x0600F3E2 RID: 62434 RVA: 0x00343992 File Offset: 0x00341B92
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F3E3 RID: 62435 RVA: 0x0034399A File Offset: 0x00341B9A
		private indexInputString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F3E4 RID: 62436 RVA: 0x003439A3 File Offset: 0x00341BA3
		public static indexInputString CreateUnsafe(ProgramNode node)
		{
			return new indexInputString(node);
		}

		// Token: 0x0600F3E5 RID: 62437 RVA: 0x003439AC File Offset: 0x00341BAC
		public static indexInputString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.indexInputString)
			{
				return null;
			}
			return new indexInputString?(indexInputString.CreateUnsafe(node));
		}

		// Token: 0x0600F3E6 RID: 62438 RVA: 0x003439E6 File Offset: 0x00341BE6
		public static indexInputString CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new indexInputString(new Hole(g.Symbol.indexInputString, holeId));
		}

		// Token: 0x0600F3E7 RID: 62439 RVA: 0x003439FE File Offset: 0x00341BFE
		public IndexInputString Cast_IndexInputString()
		{
			return IndexInputString.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F3E8 RID: 62440 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_IndexInputString(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F3E9 RID: 62441 RVA: 0x00343A0B File Offset: 0x00341C0B
		public bool Is_IndexInputString(GrammarBuilders g, out IndexInputString value)
		{
			value = IndexInputString.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F3EA RID: 62442 RVA: 0x00343A1F File Offset: 0x00341C1F
		public IndexInputString? As_IndexInputString(GrammarBuilders g)
		{
			return new IndexInputString?(IndexInputString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3EB RID: 62443 RVA: 0x00343A31 File Offset: 0x00341C31
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3EC RID: 62444 RVA: 0x00343A44 File Offset: 0x00341C44
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3ED RID: 62445 RVA: 0x00343A6E File Offset: 0x00341C6E
		public bool Equals(indexInputString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2E RID: 23342
		private ProgramNode _node;
	}
}
