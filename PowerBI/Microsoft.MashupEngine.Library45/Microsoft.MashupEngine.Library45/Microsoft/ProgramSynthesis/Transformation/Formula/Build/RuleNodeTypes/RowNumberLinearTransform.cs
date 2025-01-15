using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001577 RID: 5495
	public struct RowNumberLinearTransform : IProgramNodeBuilder, IEquatable<RowNumberLinearTransform>
	{
		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x0600B3C3 RID: 46019 RVA: 0x00273D66 File Offset: 0x00271F66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3C4 RID: 46020 RVA: 0x00273D6E File Offset: 0x00271F6E
		private RowNumberLinearTransform(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3C5 RID: 46021 RVA: 0x00273D77 File Offset: 0x00271F77
		public static RowNumberLinearTransform CreateUnsafe(ProgramNode node)
		{
			return new RowNumberLinearTransform(node);
		}

		// Token: 0x0600B3C6 RID: 46022 RVA: 0x00273D80 File Offset: 0x00271F80
		public static RowNumberLinearTransform? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RowNumberLinearTransform)
			{
				return null;
			}
			return new RowNumberLinearTransform?(RowNumberLinearTransform.CreateUnsafe(node));
		}

		// Token: 0x0600B3C7 RID: 46023 RVA: 0x00273DB5 File Offset: 0x00271FB5
		public RowNumberLinearTransform(GrammarBuilders g, fromRowNumber value0, rowNumberLinearTransformDesc value1)
		{
			this._node = g.Rule.RowNumberLinearTransform.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x00273DDB File Offset: 0x00271FDB
		public static implicit operator rowNumberTransform(RowNumberLinearTransform arg)
		{
			return rowNumberTransform.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x0600B3C9 RID: 46025 RVA: 0x00273DE9 File Offset: 0x00271FE9
		public fromRowNumber fromRowNumber
		{
			get
			{
				return fromRowNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x0600B3CA RID: 46026 RVA: 0x00273DFD File Offset: 0x00271FFD
		public rowNumberLinearTransformDesc rowNumberLinearTransformDesc
		{
			get
			{
				return rowNumberLinearTransformDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3CB RID: 46027 RVA: 0x00273E11 File Offset: 0x00272011
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3CC RID: 46028 RVA: 0x00273E24 File Offset: 0x00272024
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3CD RID: 46029 RVA: 0x00273E4E File Offset: 0x0027204E
		public bool Equals(RowNumberLinearTransform other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004625 RID: 17957
		private ProgramNode _node;
	}
}
