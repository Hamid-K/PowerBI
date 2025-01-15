using System;
using System.ComponentModel;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x0200002D RID: 45
	[NLogConfigurationItem]
	public class ConsoleRowHighlightingRule
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0000AF73 File Offset: 0x00009173
		public ConsoleRowHighlightingRule()
			: this(null, ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange)
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000AF80 File Offset: 0x00009180
		public ConsoleRowHighlightingRule(ConditionExpression condition, ConsoleOutputColor foregroundColor, ConsoleOutputColor backgroundColor)
		{
			this.Condition = condition;
			this.ForegroundColor = foregroundColor;
			this.BackgroundColor = backgroundColor;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000AF9D File Offset: 0x0000919D
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public static ConsoleRowHighlightingRule Default { get; private set; } = new ConsoleRowHighlightingRule(null, ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000AFAC File Offset: 0x000091AC
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000AFB4 File Offset: 0x000091B4
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000AFBD File Offset: 0x000091BD
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000AFC5 File Offset: 0x000091C5
		[DefaultValue("NoChange")]
		public ConsoleOutputColor ForegroundColor { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000AFCE File Offset: 0x000091CE
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000AFD6 File Offset: 0x000091D6
		[DefaultValue("NoChange")]
		public ConsoleOutputColor BackgroundColor { get; set; }

		// Token: 0x06000520 RID: 1312 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public bool CheckCondition(LogEventInfo logEvent)
		{
			return this.Condition == null || true.Equals(this.Condition.Evaluate(logEvent));
		}
	}
}
