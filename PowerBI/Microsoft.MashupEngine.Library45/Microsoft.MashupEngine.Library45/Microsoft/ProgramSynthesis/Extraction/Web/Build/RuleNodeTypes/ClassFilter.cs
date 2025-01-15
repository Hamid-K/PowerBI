using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200103F RID: 4159
	public struct ClassFilter : IProgramNodeBuilder, IEquatable<ClassFilter>
	{
		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06007B22 RID: 31522 RVA: 0x001A2D56 File Offset: 0x001A0F56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x001A2D5E File Offset: 0x001A0F5E
		private ClassFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x001A2D67 File Offset: 0x001A0F67
		public static ClassFilter CreateUnsafe(ProgramNode node)
		{
			return new ClassFilter(node);
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x001A2D70 File Offset: 0x001A0F70
		public static ClassFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ClassFilter)
			{
				return null;
			}
			return new ClassFilter?(ClassFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x001A2DA5 File Offset: 0x001A0FA5
		public ClassFilter(GrammarBuilders g, className value0, nodeCollection value1)
		{
			this._node = g.Rule.ClassFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x001A2DCB File Offset: 0x001A0FCB
		public static implicit operator nodeCollection(ClassFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06007B28 RID: 31528 RVA: 0x001A2DD9 File Offset: 0x001A0FD9
		public className className
		{
			get
			{
				return className.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06007B29 RID: 31529 RVA: 0x001A2DED File Offset: 0x001A0FED
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x001A2E01 File Offset: 0x001A1001
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x001A2E14 File Offset: 0x001A1014
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x001A2E3E File Offset: 0x001A103E
		public bool Equals(ClassFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003358 RID: 13144
		private ProgramNode _node;
	}
}
