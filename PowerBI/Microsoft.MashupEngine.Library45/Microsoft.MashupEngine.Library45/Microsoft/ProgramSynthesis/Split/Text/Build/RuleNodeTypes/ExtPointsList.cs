using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001343 RID: 4931
	public struct ExtPointsList : IProgramNodeBuilder, IEquatable<ExtPointsList>
	{
		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x060097F0 RID: 38896 RVA: 0x002060D2 File Offset: 0x002042D2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097F1 RID: 38897 RVA: 0x002060DA File Offset: 0x002042DA
		private ExtPointsList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097F2 RID: 38898 RVA: 0x002060E3 File Offset: 0x002042E3
		public static ExtPointsList CreateUnsafe(ProgramNode node)
		{
			return new ExtPointsList(node);
		}

		// Token: 0x060097F3 RID: 38899 RVA: 0x002060EC File Offset: 0x002042EC
		public static ExtPointsList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ExtPointsList)
			{
				return null;
			}
			return new ExtPointsList?(ExtPointsList.CreateUnsafe(node));
		}

		// Token: 0x060097F4 RID: 38900 RVA: 0x00206121 File Offset: 0x00204321
		public ExtPointsList(GrammarBuilders g, extractionPoints value0, cndExtPoint value1)
		{
			this._node = g.Rule.ExtPointsList.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060097F5 RID: 38901 RVA: 0x00206147 File Offset: 0x00204347
		public static implicit operator extractionPoints(ExtPointsList arg)
		{
			return extractionPoints.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x060097F6 RID: 38902 RVA: 0x00206155 File Offset: 0x00204355
		public extractionPoints extractionPoints
		{
			get
			{
				return extractionPoints.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x060097F7 RID: 38903 RVA: 0x00206169 File Offset: 0x00204369
		public cndExtPoint cndExtPoint
		{
			get
			{
				return cndExtPoint.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060097F8 RID: 38904 RVA: 0x0020617D File Offset: 0x0020437D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097F9 RID: 38905 RVA: 0x00206190 File Offset: 0x00204390
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097FA RID: 38906 RVA: 0x002061BA File Offset: 0x002043BA
		public bool Equals(ExtPointsList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DBA RID: 15802
		private ProgramNode _node;
	}
}
