using System;

namespace dotless.Core.Loggers
{
	// Token: 0x020000B0 RID: 176
	public interface ILogger
	{
		// Token: 0x0600050B RID: 1291
		void Log(LogLevel level, string message);

		// Token: 0x0600050C RID: 1292
		void Info(string message);

		// Token: 0x0600050D RID: 1293
		void Info(string message, params object[] args);

		// Token: 0x0600050E RID: 1294
		void Debug(string message);

		// Token: 0x0600050F RID: 1295
		void Debug(string message, params object[] args);

		// Token: 0x06000510 RID: 1296
		void Warn(string message);

		// Token: 0x06000511 RID: 1297
		void Warn(string message, params object[] args);

		// Token: 0x06000512 RID: 1298
		void Error(string message);

		// Token: 0x06000513 RID: 1299
		void Error(string message, params object[] args);
	}
}
