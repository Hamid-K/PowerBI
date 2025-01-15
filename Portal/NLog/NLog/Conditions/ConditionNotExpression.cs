using System;

namespace NLog.Conditions
{
	// Token: 0x020001AE RID: 430
	internal sealed class ConditionNotExpression : ConditionExpression
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x00033E2E File Offset: 0x0003202E
		public ConditionNotExpression(ConditionExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00033E3D File Offset: 0x0003203D
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x00033E45 File Offset: 0x00032045
		public ConditionExpression Expression { get; private set; }

		// Token: 0x06001325 RID: 4901 RVA: 0x00033E4E File Offset: 0x0003204E
		public override string ToString()
		{
			return string.Format("(not {0})", this.Expression);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00033E60 File Offset: 0x00032060
		protected override object EvaluateNode(LogEventInfo context)
		{
			return !(bool)this.Expression.Evaluate(context);
		}
	}
}
