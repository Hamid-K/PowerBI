using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E58 RID: 7768
	public struct match_pred : IProgramNodeBuilder, IEquatable<match_pred>
	{
		// Token: 0x17002B70 RID: 11120
		// (get) Token: 0x060105BD RID: 67005 RVA: 0x00388AF7 File Offset: 0x00386CF7
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105BE RID: 67006 RVA: 0x00388AFF File Offset: 0x00386CFF
		private match_pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105BF RID: 67007 RVA: 0x00388B08 File Offset: 0x00386D08
		public static match_pred CreateUnsafe(ProgramNode node)
		{
			return new match_pred(node);
		}

		// Token: 0x060105C0 RID: 67008 RVA: 0x00388B10 File Offset: 0x00386D10
		public static match_pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.match_pred)
			{
				return null;
			}
			return new match_pred?(match_pred.CreateUnsafe(node));
		}

		// Token: 0x060105C1 RID: 67009 RVA: 0x00388B45 File Offset: 0x00386D45
		public match_pred(GrammarBuilders g, pred value0)
		{
			this._node = g.UnnamedConversion.match_pred.BuildASTNode(value0.Node);
		}

		// Token: 0x060105C2 RID: 67010 RVA: 0x00388B64 File Offset: 0x00386D64
		public static implicit operator match(match_pred arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B71 RID: 11121
		// (get) Token: 0x060105C3 RID: 67011 RVA: 0x00388B72 File Offset: 0x00386D72
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105C4 RID: 67012 RVA: 0x00388B86 File Offset: 0x00386D86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105C5 RID: 67013 RVA: 0x00388B9C File Offset: 0x00386D9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105C6 RID: 67014 RVA: 0x00388BC6 File Offset: 0x00386DC6
		public bool Equals(match_pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04006297 RID: 25239
		private ProgramNode _node;
	}
}
