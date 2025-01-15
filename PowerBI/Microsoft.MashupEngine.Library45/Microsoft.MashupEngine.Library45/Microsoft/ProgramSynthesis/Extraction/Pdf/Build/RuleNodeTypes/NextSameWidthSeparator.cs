using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BFB RID: 3067
	public struct NextSameWidthSeparator : IProgramNodeBuilder, IEquatable<NextSameWidthSeparator>
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x000F94A2 File Offset: 0x000F76A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x000F94AA File Offset: 0x000F76AA
		private NextSameWidthSeparator(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x000F94B3 File Offset: 0x000F76B3
		public static NextSameWidthSeparator CreateUnsafe(ProgramNode node)
		{
			return new NextSameWidthSeparator(node);
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x000F94BC File Offset: 0x000F76BC
		public static NextSameWidthSeparator? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NextSameWidthSeparator)
			{
				return null;
			}
			return new NextSameWidthSeparator?(NextSameWidthSeparator.CreateUnsafe(node));
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x000F94F4 File Offset: 0x000F76F4
		public NextSameWidthSeparator(GrammarBuilders g, before value0, dir value1, k value2, tolerance value3)
		{
			this._node = g.Rule.NextSameWidthSeparator.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x000F9545 File Offset: 0x000F7745
		public static implicit operator beforeRelativeBounds(NextSameWidthSeparator arg)
		{
			return beforeRelativeBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x000F9553 File Offset: 0x000F7753
		public before before
		{
			get
			{
				return before.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06004EDC RID: 20188 RVA: 0x000F9567 File Offset: 0x000F7767
		public dir dir
		{
			get
			{
				return dir.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x000F957B File Offset: 0x000F777B
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x000F958F File Offset: 0x000F778F
		public tolerance tolerance
		{
			get
			{
				return tolerance.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x000F95A3 File Offset: 0x000F77A3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x000F95B8 File Offset: 0x000F77B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x000F95E2 File Offset: 0x000F77E2
		public bool Equals(NextSameWidthSeparator other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002323 RID: 8995
		private ProgramNode _node;
	}
}
