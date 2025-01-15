using System;

namespace NLog.Conditions
{
	// Token: 0x020001A8 RID: 424
	internal sealed class ConditionLoggerNameExpression : ConditionExpression
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x000339E2 File Offset: 0x00031BE2
		public override string ToString()
		{
			return "logger";
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000339E9 File Offset: 0x00031BE9
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.LoggerName;
		}
	}
}
