using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001001 RID: 4097
	public struct selectionEnd_regionStartSiblings : IProgramNodeBuilder, IEquatable<selectionEnd_regionStartSiblings>
	{
		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06007899 RID: 30873 RVA: 0x0019F356 File Offset: 0x0019D556
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x0019F35E File Offset: 0x0019D55E
		private selectionEnd_regionStartSiblings(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x0019F367 File Offset: 0x0019D567
		public static selectionEnd_regionStartSiblings CreateUnsafe(ProgramNode node)
		{
			return new selectionEnd_regionStartSiblings(node);
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x0019F370 File Offset: 0x0019D570
		public static selectionEnd_regionStartSiblings? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selectionEnd_regionStartSiblings)
			{
				return null;
			}
			return new selectionEnd_regionStartSiblings?(selectionEnd_regionStartSiblings.CreateUnsafe(node));
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x0019F3A5 File Offset: 0x0019D5A5
		public selectionEnd_regionStartSiblings(GrammarBuilders g, regionStartSiblings value0)
		{
			this._node = g.UnnamedConversion.selectionEnd_regionStartSiblings.BuildASTNode(value0.Node);
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x0019F3C4 File Offset: 0x0019D5C4
		public static implicit operator selectionEnd(selectionEnd_regionStartSiblings arg)
		{
			return selectionEnd.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600789F RID: 30879 RVA: 0x0019F3D2 File Offset: 0x0019D5D2
		public regionStartSiblings regionStartSiblings
		{
			get
			{
				return regionStartSiblings.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x0019F3E6 File Offset: 0x0019D5E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x0019F3FC File Offset: 0x0019D5FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x0019F426 File Offset: 0x0019D626
		public bool Equals(selectionEnd_regionStartSiblings other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400331A RID: 13082
		private ProgramNode _node;
	}
}
