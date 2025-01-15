using System;

namespace NLog.Conditions
{
	// Token: 0x020001A2 RID: 418
	internal sealed class ConditionAndExpression : ConditionExpression
	{
		// Token: 0x060012EB RID: 4843 RVA: 0x00033826 File Offset: 0x00031A26
		public ConditionAndExpression(ConditionExpression left, ConditionExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0003383C File Offset: 0x00031A3C
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x00033844 File Offset: 0x00031A44
		public ConditionExpression Left { get; private set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0003384D File Offset: 0x00031A4D
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00033855 File Offset: 0x00031A55
		public ConditionExpression Right { get; private set; }

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003385E File Offset: 0x00031A5E
		public override string ToString()
		{
			return string.Format("({0} and {1})", this.Left, this.Right);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00033876 File Offset: 0x00031A76
		protected override object EvaluateNode(LogEventInfo context)
		{
			if (!(bool)this.Left.Evaluate(context))
			{
				return ConditionAndExpression.BoxedFalse;
			}
			if (!(bool)this.Right.Evaluate(context))
			{
				return ConditionAndExpression.BoxedFalse;
			}
			return ConditionAndExpression.BoxedTrue;
		}

		// Token: 0x0400050E RID: 1294
		private static readonly object BoxedFalse = false;

		// Token: 0x0400050F RID: 1295
		private static readonly object BoxedTrue = true;
	}
}
