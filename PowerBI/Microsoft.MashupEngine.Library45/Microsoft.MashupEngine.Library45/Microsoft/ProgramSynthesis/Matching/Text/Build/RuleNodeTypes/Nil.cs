using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E8 RID: 4584
	public struct Nil : IProgramNodeBuilder, IEquatable<Nil>
	{
		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x060089BE RID: 35262 RVA: 0x001CF96A File Offset: 0x001CDB6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089BF RID: 35263 RVA: 0x001CF972 File Offset: 0x001CDB72
		private Nil(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089C0 RID: 35264 RVA: 0x001CF97B File Offset: 0x001CDB7B
		public static Nil CreateUnsafe(ProgramNode node)
		{
			return new Nil(node);
		}

		// Token: 0x060089C1 RID: 35265 RVA: 0x001CF984 File Offset: 0x001CDB84
		public static Nil? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Nil)
			{
				return null;
			}
			return new Nil?(Nil.CreateUnsafe(node));
		}

		// Token: 0x060089C2 RID: 35266 RVA: 0x001CF9B9 File Offset: 0x001CDBB9
		public Nil(GrammarBuilders g, sRegions value0)
		{
			this._node = g.Rule.Nil.BuildASTNode(value0.Node);
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x001CF9D8 File Offset: 0x001CDBD8
		public static implicit operator multi_result_matches(Nil arg)
		{
			return multi_result_matches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x060089C4 RID: 35268 RVA: 0x001CF9E6 File Offset: 0x001CDBE6
		public sRegions sRegions
		{
			get
			{
				return sRegions.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060089C5 RID: 35269 RVA: 0x001CF9FA File Offset: 0x001CDBFA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089C6 RID: 35270 RVA: 0x001CFA10 File Offset: 0x001CDC10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089C7 RID: 35271 RVA: 0x001CFA3A File Offset: 0x001CDC3A
		public bool Equals(Nil other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389C RID: 14492
		private ProgramNode _node;
	}
}
