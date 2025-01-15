using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000983 RID: 2435
	public struct record : IProgramNodeBuilder, IEquatable<record>
	{
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000B3466 File Offset: 0x000B1666
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000B346E File Offset: 0x000B166E
		private record(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000B3477 File Offset: 0x000B1677
		public static record CreateUnsafe(ProgramNode node)
		{
			return new record(node);
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000B3480 File Offset: 0x000B1680
		public static record? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.record)
			{
				return null;
			}
			return new record?(record.CreateUnsafe(node));
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000B34BA File Offset: 0x000B16BA
		public static record CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new record(new Hole(g.Symbol.record, holeId));
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000B34D2 File Offset: 0x000B16D2
		public record(GrammarBuilders g)
		{
			this = new record(new VariableNode(g.Symbol.record));
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x000B34EA File Offset: 0x000B16EA
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000B34F7 File Offset: 0x000B16F7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000B350C File Offset: 0x000B170C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000B3536 File Offset: 0x000B1736
		public bool Equals(record other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001AA3 RID: 6819
		private ProgramNode _node;
	}
}
