using System;

namespace NLog.Conditions
{
	// Token: 0x020001A9 RID: 425
	internal sealed class ConditionMessageExpression : ConditionExpression
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x000339F9 File Offset: 0x00031BF9
		public override string ToString()
		{
			return "message";
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00033A00 File Offset: 0x00031C00
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.FormattedMessage;
		}
	}
}
