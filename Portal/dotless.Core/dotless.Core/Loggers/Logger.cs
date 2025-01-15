using System;

namespace dotless.Core.Loggers
{
	// Token: 0x020000B2 RID: 178
	public abstract class Logger : ILogger
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001737D File Offset: 0x0001557D
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00017385 File Offset: 0x00015585
		public LogLevel Level { get; set; }

		// Token: 0x06000516 RID: 1302 RVA: 0x0001738E File Offset: 0x0001558E
		protected Logger(LogLevel level)
		{
			this.Level = level;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001739D File Offset: 0x0001559D
		public void Log(LogLevel level, string message)
		{
			if (this.Level <= level)
			{
				this.Log(message);
			}
		}

		// Token: 0x06000518 RID: 1304
		protected abstract void Log(string message);

		// Token: 0x06000519 RID: 1305 RVA: 0x000173AF File Offset: 0x000155AF
		public void Info(string message)
		{
			this.Log(LogLevel.Info, message);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000173B9 File Offset: 0x000155B9
		public void Debug(string message)
		{
			this.Log(LogLevel.Debug, message);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000173C3 File Offset: 0x000155C3
		public void Warn(string message)
		{
			this.Log(LogLevel.Warn, message);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000173CD File Offset: 0x000155CD
		public void Error(string message)
		{
			this.Log(LogLevel.Error, message);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000173D7 File Offset: 0x000155D7
		public void Info(string message, params object[] args)
		{
			this.Log(LogLevel.Info, string.Format(message, args));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000173E7 File Offset: 0x000155E7
		public void Debug(string message, params object[] args)
		{
			this.Log(LogLevel.Debug, string.Format(message, args));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000173F7 File Offset: 0x000155F7
		public void Warn(string message, params object[] args)
		{
			this.Log(LogLevel.Warn, string.Format(message, args));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017407 File Offset: 0x00015607
		public void Error(string message, params object[] args)
		{
			this.Log(LogLevel.Error, string.Format(message, args));
		}
	}
}
