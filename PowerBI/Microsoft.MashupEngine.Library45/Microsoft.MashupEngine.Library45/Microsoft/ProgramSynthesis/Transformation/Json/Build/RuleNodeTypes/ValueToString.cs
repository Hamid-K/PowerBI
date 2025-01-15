using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2E RID: 6702
	public struct ValueToString : IProgramNodeBuilder, IEquatable<ValueToString>
	{
		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x0600DC3D RID: 56381 RVA: 0x002EEA32 File Offset: 0x002ECC32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC3E RID: 56382 RVA: 0x002EEA3A File Offset: 0x002ECC3A
		private ValueToString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC3F RID: 56383 RVA: 0x002EEA43 File Offset: 0x002ECC43
		public static ValueToString CreateUnsafe(ProgramNode node)
		{
			return new ValueToString(node);
		}

		// Token: 0x0600DC40 RID: 56384 RVA: 0x002EEA4C File Offset: 0x002ECC4C
		public static ValueToString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ValueToString)
			{
				return null;
			}
			return new ValueToString?(ValueToString.CreateUnsafe(node));
		}

		// Token: 0x0600DC41 RID: 56385 RVA: 0x002EEA81 File Offset: 0x002ECC81
		public ValueToString(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.ValueToString.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC42 RID: 56386 RVA: 0x002EEAA7 File Offset: 0x002ECCA7
		public static implicit operator selectValue(ValueToString arg)
		{
			return selectValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x0600DC43 RID: 56387 RVA: 0x002EEAB5 File Offset: 0x002ECCB5
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x0600DC44 RID: 56388 RVA: 0x002EEAC9 File Offset: 0x002ECCC9
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC45 RID: 56389 RVA: 0x002EEADD File Offset: 0x002ECCDD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC46 RID: 56390 RVA: 0x002EEAF0 File Offset: 0x002ECCF0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC47 RID: 56391 RVA: 0x002EEB1A File Offset: 0x002ECD1A
		public bool Equals(ValueToString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541F RID: 21535
		private ProgramNode _node;
	}
}
