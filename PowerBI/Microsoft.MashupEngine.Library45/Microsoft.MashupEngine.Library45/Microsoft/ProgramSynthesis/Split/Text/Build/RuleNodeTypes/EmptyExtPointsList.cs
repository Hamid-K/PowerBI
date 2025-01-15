using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001344 RID: 4932
	public struct EmptyExtPointsList : IProgramNodeBuilder, IEquatable<EmptyExtPointsList>
	{
		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x060097FB RID: 38907 RVA: 0x002061CE File Offset: 0x002043CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097FC RID: 38908 RVA: 0x002061D6 File Offset: 0x002043D6
		private EmptyExtPointsList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097FD RID: 38909 RVA: 0x002061DF File Offset: 0x002043DF
		public static EmptyExtPointsList CreateUnsafe(ProgramNode node)
		{
			return new EmptyExtPointsList(node);
		}

		// Token: 0x060097FE RID: 38910 RVA: 0x002061E8 File Offset: 0x002043E8
		public static EmptyExtPointsList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EmptyExtPointsList)
			{
				return null;
			}
			return new EmptyExtPointsList?(EmptyExtPointsList.CreateUnsafe(node));
		}

		// Token: 0x060097FF RID: 38911 RVA: 0x0020621D File Offset: 0x0020441D
		public EmptyExtPointsList(GrammarBuilders g)
		{
			this._node = g.Rule.EmptyExtPointsList.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x0020623A File Offset: 0x0020443A
		public static implicit operator extractionPoints(EmptyExtPointsList arg)
		{
			return extractionPoints.CreateUnsafe(arg.Node);
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x00206248 File Offset: 0x00204448
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x0020625C File Offset: 0x0020445C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009803 RID: 38915 RVA: 0x00206286 File Offset: 0x00204486
		public bool Equals(EmptyExtPointsList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBB RID: 15803
		private ProgramNode _node;
	}
}
