using System;

namespace NLog.Conditions
{
	// Token: 0x020001AF RID: 431
	internal sealed class ConditionOrExpression : ConditionExpression
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00033E7B File Offset: 0x0003207B
		public ConditionOrExpression(ConditionExpression left, ConditionExpression right)
		{
			this.LeftExpression = left;
			this.RightExpression = right;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00033E91 File Offset: 0x00032091
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x00033E99 File Offset: 0x00032099
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00033EA2 File Offset: 0x000320A2
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x00033EAA File Offset: 0x000320AA
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x0600132C RID: 4908 RVA: 0x00033EB3 File Offset: 0x000320B3
		public override string ToString()
		{
			return string.Format("({0} or {1})", this.LeftExpression, this.RightExpression);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00033ECB File Offset: 0x000320CB
		protected override object EvaluateNode(LogEventInfo context)
		{
			if ((bool)this.LeftExpression.Evaluate(context))
			{
				return ConditionOrExpression.BoxedTrue;
			}
			if ((bool)this.RightExpression.Evaluate(context))
			{
				return ConditionOrExpression.BoxedTrue;
			}
			return ConditionOrExpression.BoxedFalse;
		}

		// Token: 0x0400051B RID: 1307
		private static readonly object BoxedFalse = false;

		// Token: 0x0400051C RID: 1308
		private static readonly object BoxedTrue = true;
	}
}
