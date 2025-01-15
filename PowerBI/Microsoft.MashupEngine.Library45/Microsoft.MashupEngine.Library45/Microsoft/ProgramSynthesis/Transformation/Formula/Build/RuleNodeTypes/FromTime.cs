using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001594 RID: 5524
	public struct FromTime : IProgramNodeBuilder, IEquatable<FromTime>
	{
		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x0600B504 RID: 46340 RVA: 0x00275A66 File Offset: 0x00273C66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B505 RID: 46341 RVA: 0x00275A6E File Offset: 0x00273C6E
		private FromTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B506 RID: 46342 RVA: 0x00275A77 File Offset: 0x00273C77
		public static FromTime CreateUnsafe(ProgramNode node)
		{
			return new FromTime(node);
		}

		// Token: 0x0600B507 RID: 46343 RVA: 0x00275A80 File Offset: 0x00273C80
		public static FromTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromTime)
			{
				return null;
			}
			return new FromTime?(FromTime.CreateUnsafe(node));
		}

		// Token: 0x0600B508 RID: 46344 RVA: 0x00275AB5 File Offset: 0x00273CB5
		public FromTime(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B509 RID: 46345 RVA: 0x00275ADB File Offset: 0x00273CDB
		public static implicit operator fromTime(FromTime arg)
		{
			return fromTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x0600B50A RID: 46346 RVA: 0x00275AE9 File Offset: 0x00273CE9
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x0600B50B RID: 46347 RVA: 0x00275AFD File Offset: 0x00273CFD
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B50C RID: 46348 RVA: 0x00275B11 File Offset: 0x00273D11
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B50D RID: 46349 RVA: 0x00275B24 File Offset: 0x00273D24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B50E RID: 46350 RVA: 0x00275B4E File Offset: 0x00273D4E
		public bool Equals(FromTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004642 RID: 17986
		private ProgramNode _node;
	}
}
