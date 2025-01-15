using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001007 RID: 4103
	public struct leafFExpr_leafAtom : IProgramNodeBuilder, IEquatable<leafFExpr_leafAtom>
	{
		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x060078D5 RID: 30933 RVA: 0x0019F8AE File Offset: 0x0019DAAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x0019F8B6 File Offset: 0x0019DAB6
		private leafFExpr_leafAtom(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x0019F8BF File Offset: 0x0019DABF
		public static leafFExpr_leafAtom CreateUnsafe(ProgramNode node)
		{
			return new leafFExpr_leafAtom(node);
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x0019F8C8 File Offset: 0x0019DAC8
		public static leafFExpr_leafAtom? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.leafFExpr_leafAtom)
			{
				return null;
			}
			return new leafFExpr_leafAtom?(leafFExpr_leafAtom.CreateUnsafe(node));
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x0019F8FD File Offset: 0x0019DAFD
		public leafFExpr_leafAtom(GrammarBuilders g, leafAtom value0)
		{
			this._node = g.UnnamedConversion.leafFExpr_leafAtom.BuildASTNode(value0.Node);
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x0019F91C File Offset: 0x0019DB1C
		public static implicit operator leafFExpr(leafFExpr_leafAtom arg)
		{
			return leafFExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x0019F92A File Offset: 0x0019DB2A
		public leafAtom leafAtom
		{
			get
			{
				return leafAtom.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060078DC RID: 30940 RVA: 0x0019F93E File Offset: 0x0019DB3E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x0019F954 File Offset: 0x0019DB54
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x0019F97E File Offset: 0x0019DB7E
		public bool Equals(leafFExpr_leafAtom other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003320 RID: 13088
		private ProgramNode _node;
	}
}
