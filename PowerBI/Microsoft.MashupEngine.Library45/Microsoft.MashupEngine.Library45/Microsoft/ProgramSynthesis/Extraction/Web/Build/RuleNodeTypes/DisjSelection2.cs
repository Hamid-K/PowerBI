using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001019 RID: 4121
	public struct DisjSelection2 : IProgramNodeBuilder, IEquatable<DisjSelection2>
	{
		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x0600798D RID: 31117 RVA: 0x001A0916 File Offset: 0x0019EB16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x001A091E File Offset: 0x0019EB1E
		private DisjSelection2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x001A0927 File Offset: 0x0019EB27
		public static DisjSelection2 CreateUnsafe(ProgramNode node)
		{
			return new DisjSelection2(node);
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x001A0930 File Offset: 0x0019EB30
		public static DisjSelection2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSelection2)
			{
				return null;
			}
			return new DisjSelection2?(DisjSelection2.CreateUnsafe(node));
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x001A0965 File Offset: 0x0019EB65
		public DisjSelection2(GrammarBuilders g, selection3 value0, filterSelection2 value1)
		{
			this._node = g.Rule.DisjSelection2.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001A098B File Offset: 0x0019EB8B
		public static implicit operator selection3(DisjSelection2 arg)
		{
			return selection3.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06007993 RID: 31123 RVA: 0x001A0999 File Offset: 0x0019EB99
		public selection3 selection3
		{
			get
			{
				return selection3.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06007994 RID: 31124 RVA: 0x001A09AD File Offset: 0x0019EBAD
		public filterSelection2 filterSelection2
		{
			get
			{
				return filterSelection2.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x001A09C1 File Offset: 0x0019EBC1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007996 RID: 31126 RVA: 0x001A09D4 File Offset: 0x0019EBD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x001A09FE File Offset: 0x0019EBFE
		public bool Equals(DisjSelection2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003332 RID: 13106
		private ProgramNode _node;
	}
}
