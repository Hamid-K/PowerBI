using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C27 RID: 7207
	public struct Atom : IProgramNodeBuilder, IEquatable<Atom>
	{
		// Token: 0x170028A1 RID: 10401
		// (get) Token: 0x0600F2B8 RID: 62136 RVA: 0x00341736 File Offset: 0x0033F936
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2B9 RID: 62137 RVA: 0x0034173E File Offset: 0x0033F93E
		private Atom(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2BA RID: 62138 RVA: 0x00341747 File Offset: 0x0033F947
		public static Atom CreateUnsafe(ProgramNode node)
		{
			return new Atom(node);
		}

		// Token: 0x0600F2BB RID: 62139 RVA: 0x00341750 File Offset: 0x0033F950
		public static Atom? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Atom)
			{
				return null;
			}
			return new Atom?(Atom.CreateUnsafe(node));
		}

		// Token: 0x0600F2BC RID: 62140 RVA: 0x00341785 File Offset: 0x0033F985
		public Atom(GrammarBuilders g, f value0)
		{
			this._node = g.Rule.Atom.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2BD RID: 62141 RVA: 0x003417A4 File Offset: 0x0033F9A4
		public static implicit operator e(Atom arg)
		{
			return e.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028A2 RID: 10402
		// (get) Token: 0x0600F2BE RID: 62142 RVA: 0x003417B2 File Offset: 0x0033F9B2
		public f f
		{
			get
			{
				return f.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2BF RID: 62143 RVA: 0x003417C6 File Offset: 0x0033F9C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2C0 RID: 62144 RVA: 0x003417DC File Offset: 0x0033F9DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2C1 RID: 62145 RVA: 0x00341806 File Offset: 0x0033FA06
		public bool Equals(Atom other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B16 RID: 23318
		private ProgramNode _node;
	}
}
