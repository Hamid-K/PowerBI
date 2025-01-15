using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FFA RID: 4090
	internal class SetFilter : Filter
	{
		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06006B3B RID: 27451 RVA: 0x00171578 File Offset: 0x0016F778
		public BooleanOperator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06006B3C RID: 27452 RVA: 0x00171580 File Offset: 0x0016F780
		public Filter[] Operands
		{
			get
			{
				return this.operands;
			}
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x00171588 File Offset: 0x0016F788
		public SetFilter(BooleanOperator op, params Filter[] operands)
		{
			this.op = op;
			this.operands = operands;
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x001715A0 File Offset: 0x0016F7A0
		public override void Write(StringBuilder builder)
		{
			builder.Append("(");
			this.AppendOperator(builder);
			Filter[] array = this.Operands;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Write(builder);
			}
			builder.Append(")");
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x001715EC File Offset: 0x0016F7EC
		public SetFilter AddOperand(Filter newOperand)
		{
			Filter[] array = new Filter[this.Operands.Length + 1];
			for (int i = 0; i < this.Operands.Length; i++)
			{
				Filter filter = this.Operands[i];
				if (filter.Equals(newOperand))
				{
					return this;
				}
				array[i] = filter;
			}
			array[this.Operands.Length] = newOperand;
			return new SetFilter(this.op, array);
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x0017164C File Offset: 0x0016F84C
		private void AppendOperator(StringBuilder builder)
		{
			BooleanOperator @operator = this.Operator;
			if (@operator == BooleanOperator.And)
			{
				builder.Append("&");
				return;
			}
			if (@operator != BooleanOperator.Or)
			{
				return;
			}
			builder.Append("|");
		}

		// Token: 0x04003BA5 RID: 15269
		private readonly BooleanOperator op;

		// Token: 0x04003BA6 RID: 15270
		private readonly Filter[] operands;
	}
}
