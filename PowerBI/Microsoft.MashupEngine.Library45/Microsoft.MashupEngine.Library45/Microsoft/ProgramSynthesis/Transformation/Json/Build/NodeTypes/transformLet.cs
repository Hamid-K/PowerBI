using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A45 RID: 6725
	public struct transformLet : IProgramNodeBuilder, IEquatable<transformLet>
	{
		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x0600DD90 RID: 56720 RVA: 0x002F1B2A File Offset: 0x002EFD2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD91 RID: 56721 RVA: 0x002F1B32 File Offset: 0x002EFD32
		private transformLet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD92 RID: 56722 RVA: 0x002F1B3B File Offset: 0x002EFD3B
		public static transformLet CreateUnsafe(ProgramNode node)
		{
			return new transformLet(node);
		}

		// Token: 0x0600DD93 RID: 56723 RVA: 0x002F1B44 File Offset: 0x002EFD44
		public static transformLet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.transformLet)
			{
				return null;
			}
			return new transformLet?(transformLet.CreateUnsafe(node));
		}

		// Token: 0x0600DD94 RID: 56724 RVA: 0x002F1B7E File Offset: 0x002EFD7E
		public static transformLet CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new transformLet(new Hole(g.Symbol.transformLet, holeId));
		}

		// Token: 0x0600DD95 RID: 56725 RVA: 0x002F1B96 File Offset: 0x002EFD96
		public TransformLet Cast_TransformLet()
		{
			return TransformLet.CreateUnsafe(this.Node);
		}

		// Token: 0x0600DD96 RID: 56726 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_TransformLet(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600DD97 RID: 56727 RVA: 0x002F1BA3 File Offset: 0x002EFDA3
		public bool Is_TransformLet(GrammarBuilders g, out TransformLet value)
		{
			value = TransformLet.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600DD98 RID: 56728 RVA: 0x002F1BB7 File Offset: 0x002EFDB7
		public TransformLet? As_TransformLet(GrammarBuilders g)
		{
			return new TransformLet?(TransformLet.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD99 RID: 56729 RVA: 0x002F1BC9 File Offset: 0x002EFDC9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD9A RID: 56730 RVA: 0x002F1BDC File Offset: 0x002EFDDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD9B RID: 56731 RVA: 0x002F1C06 File Offset: 0x002EFE06
		public bool Equals(transformLet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005436 RID: 21558
		private ProgramNode _node;
	}
}
