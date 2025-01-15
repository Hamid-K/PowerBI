using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AB5 RID: 6837
	public struct @out : IProgramNodeBuilder, IEquatable<@out>
	{
		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x0600E1E9 RID: 57833 RVA: 0x003007E2 File Offset: 0x002FE9E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1EA RID: 57834 RVA: 0x003007EA File Offset: 0x002FE9EA
		private @out(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1EB RID: 57835 RVA: 0x003007F3 File Offset: 0x002FE9F3
		public static @out CreateUnsafe(ProgramNode node)
		{
			return new @out(node);
		}

		// Token: 0x0600E1EC RID: 57836 RVA: 0x003007FC File Offset: 0x002FE9FC
		public static @out? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.@out)
			{
				return null;
			}
			return new @out?(@out.CreateUnsafe(node));
		}

		// Token: 0x0600E1ED RID: 57837 RVA: 0x00300836 File Offset: 0x002FEA36
		public static @out CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new @out(new Hole(g.Symbol.@out, holeId));
		}

		// Token: 0x0600E1EE RID: 57838 RVA: 0x0030084E File Offset: 0x002FEA4E
		public TTableProgram Cast_TTableProgram()
		{
			return TTableProgram.CreateUnsafe(this.Node);
		}

		// Token: 0x0600E1EF RID: 57839 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_TTableProgram(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600E1F0 RID: 57840 RVA: 0x0030085B File Offset: 0x002FEA5B
		public bool Is_TTableProgram(GrammarBuilders g, out TTableProgram value)
		{
			value = TTableProgram.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600E1F1 RID: 57841 RVA: 0x0030086F File Offset: 0x002FEA6F
		public TTableProgram? As_TTableProgram(GrammarBuilders g)
		{
			return new TTableProgram?(TTableProgram.CreateUnsafe(this.Node));
		}

		// Token: 0x0600E1F2 RID: 57842 RVA: 0x00300881 File Offset: 0x002FEA81
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1F3 RID: 57843 RVA: 0x00300894 File Offset: 0x002FEA94
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1F4 RID: 57844 RVA: 0x003008BE File Offset: 0x002FEABE
		public bool Equals(@out other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005574 RID: 21876
		private ProgramNode _node;
	}
}
