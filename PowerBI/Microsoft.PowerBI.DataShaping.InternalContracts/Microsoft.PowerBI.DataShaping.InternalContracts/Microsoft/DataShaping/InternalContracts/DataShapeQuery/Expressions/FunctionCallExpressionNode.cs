using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D3 RID: 211
	internal sealed class FunctionCallExpressionNode : ExpressionNode
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0000BFE3 File Offset: 0x0000A1E3
		internal FunctionCallExpressionNode(FunctionDescriptor descriptor, FunctionUsageKind functionUsageKind, IList<ExpressionNode> arguments)
		{
			this.Descriptor = descriptor;
			this.UsageKind = functionUsageKind;
			this.Arguments = arguments.AsReadOnlyCollection<ExpressionNode>();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000C005 File Offset: 0x0000A205
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.FunctionCall;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000C009 File Offset: 0x0000A209
		public FunctionUsageKind UsageKind { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000C011 File Offset: 0x0000A211
		public FunctionDescriptor Descriptor { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000C019 File Offset: 0x0000A219
		public ReadOnlyCollection<ExpressionNode> Arguments { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000C021 File Offset: 0x0000A221
		public bool HasValidArgumentCount
		{
			get
			{
				return this.Descriptor.Arguments.Count == this.Arguments.Count;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000C040 File Offset: 0x0000A240
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			FunctionCallExpressionNode functionCallExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<FunctionCallExpressionNode>(this, other, out flag, out functionCallExpressionNode))
			{
				return flag;
			}
			return this.Descriptor.Equals(functionCallExpressionNode.Descriptor) && this.UsageKind.Equals(functionCallExpressionNode.UsageKind) && this.Arguments.SequenceEqual(functionCallExpressionNode.Arguments, ExpressionNode.Comparer);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.UsageKind.GetHashCode(), this.Descriptor.GetHashCode(), Hashing.CombineHash<ExpressionNode>(this.Arguments, null));
		}
	}
}
