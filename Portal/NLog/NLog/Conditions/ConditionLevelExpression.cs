using System;

namespace NLog.Conditions
{
	// Token: 0x020001A6 RID: 422
	internal sealed class ConditionLevelExpression : ConditionExpression
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x00033983 File Offset: 0x00031B83
		public override string ToString()
		{
			return "level";
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0003398A File Offset: 0x00031B8A
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.Level;
		}
	}
}
