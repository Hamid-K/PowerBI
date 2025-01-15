using System;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200006D RID: 109
	public class LogEventDroppedEventArgs : EventArgs
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0001732E File Offset: 0x0001552E
		public LogEventDroppedEventArgs(LogEventInfo logEventInfo)
		{
			this.DroppedLogEventInfo = logEventInfo;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0001733D File Offset: 0x0001553D
		public LogEventInfo DroppedLogEventInfo { get; }
	}
}
