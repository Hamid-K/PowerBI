using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABA RID: 6842
	public struct sourceColumnName : IProgramNodeBuilder, IEquatable<sourceColumnName>
	{
		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x0600E24B RID: 57931 RVA: 0x0030174A File Offset: 0x002FF94A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E24C RID: 57932 RVA: 0x00301752 File Offset: 0x002FF952
		private sourceColumnName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E24D RID: 57933 RVA: 0x0030175B File Offset: 0x002FF95B
		public static sourceColumnName CreateUnsafe(ProgramNode node)
		{
			return new sourceColumnName(node);
		}

		// Token: 0x0600E24E RID: 57934 RVA: 0x00301764 File Offset: 0x002FF964
		public static sourceColumnName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sourceColumnName)
			{
				return null;
			}
			return new sourceColumnName?(sourceColumnName.CreateUnsafe(node));
		}

		// Token: 0x0600E24F RID: 57935 RVA: 0x0030179E File Offset: 0x002FF99E
		public static sourceColumnName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sourceColumnName(new Hole(g.Symbol.sourceColumnName, holeId));
		}

		// Token: 0x0600E250 RID: 57936 RVA: 0x003017B6 File Offset: 0x002FF9B6
		public sourceColumnName(GrammarBuilders g, string value)
		{
			this = new sourceColumnName(new LiteralNode(g.Symbol.sourceColumnName, value));
		}

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x0600E251 RID: 57937 RVA: 0x003017CF File Offset: 0x002FF9CF
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E252 RID: 57938 RVA: 0x003017E6 File Offset: 0x002FF9E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E253 RID: 57939 RVA: 0x003017FC File Offset: 0x002FF9FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E254 RID: 57940 RVA: 0x00301826 File Offset: 0x002FFA26
		public bool Equals(sourceColumnName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005579 RID: 21881
		private ProgramNode _node;
	}
}
