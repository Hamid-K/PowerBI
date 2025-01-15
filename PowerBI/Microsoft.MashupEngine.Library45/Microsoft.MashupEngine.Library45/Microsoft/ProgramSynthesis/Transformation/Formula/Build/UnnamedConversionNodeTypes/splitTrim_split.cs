using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001544 RID: 5444
	public struct splitTrim_split : IProgramNodeBuilder, IEquatable<splitTrim_split>
	{
		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x0600B19F RID: 45471 RVA: 0x00270BFA File Offset: 0x0026EDFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1A0 RID: 45472 RVA: 0x00270C02 File Offset: 0x0026EE02
		private splitTrim_split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1A1 RID: 45473 RVA: 0x00270C0B File Offset: 0x0026EE0B
		public static splitTrim_split CreateUnsafe(ProgramNode node)
		{
			return new splitTrim_split(node);
		}

		// Token: 0x0600B1A2 RID: 45474 RVA: 0x00270C14 File Offset: 0x0026EE14
		public static splitTrim_split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.splitTrim_split)
			{
				return null;
			}
			return new splitTrim_split?(splitTrim_split.CreateUnsafe(node));
		}

		// Token: 0x0600B1A3 RID: 45475 RVA: 0x00270C49 File Offset: 0x0026EE49
		public splitTrim_split(GrammarBuilders g, split value0)
		{
			this._node = g.UnnamedConversion.splitTrim_split.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1A4 RID: 45476 RVA: 0x00270C68 File Offset: 0x0026EE68
		public static implicit operator splitTrim(splitTrim_split arg)
		{
			return splitTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x0600B1A5 RID: 45477 RVA: 0x00270C76 File Offset: 0x0026EE76
		public split split
		{
			get
			{
				return split.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1A6 RID: 45478 RVA: 0x00270C8A File Offset: 0x0026EE8A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1A7 RID: 45479 RVA: 0x00270CA0 File Offset: 0x0026EEA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1A8 RID: 45480 RVA: 0x00270CCA File Offset: 0x0026EECA
		public bool Equals(splitTrim_split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F2 RID: 17906
		private ProgramNode _node;
	}
}
