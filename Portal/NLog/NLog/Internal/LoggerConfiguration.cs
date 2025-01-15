using System;

namespace NLog.Internal
{
	// Token: 0x02000127 RID: 295
	internal class LoggerConfiguration
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00024DF0 File Offset: 0x00022FF0
		public LoggerConfiguration(TargetWithFilterChain[] targetsByLevel, bool exceptionLoggingOldStyle)
		{
			this._targetsByLevel = targetsByLevel;
			this.ExceptionLoggingOldStyle = exceptionLoggingOldStyle;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00024E06 File Offset: 0x00023006
		[Obsolete("This property marked obsolete before v4.3.11 and it will be removed in NLog 5.")]
		public bool ExceptionLoggingOldStyle { get; }

		// Token: 0x06000EEA RID: 3818 RVA: 0x00024E0E File Offset: 0x0002300E
		public TargetWithFilterChain GetTargetsForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return null;
			}
			return this._targetsByLevel[level.Ordinal];
		}

		// Token: 0x040003F9 RID: 1017
		private readonly TargetWithFilterChain[] _targetsByLevel;
	}
}
