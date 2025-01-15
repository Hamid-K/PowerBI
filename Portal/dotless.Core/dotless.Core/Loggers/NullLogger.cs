using System;

namespace dotless.Core.Loggers
{
	// Token: 0x020000B3 RID: 179
	public class NullLogger : Logger
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x00017417 File Offset: 0x00015617
		public NullLogger(LogLevel level)
			: base(level)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017420 File Offset: 0x00015620
		protected override void Log(string message)
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00017422 File Offset: 0x00015622
		public static NullLogger Instance
		{
			get
			{
				return NullLogger.instance;
			}
		}

		// Token: 0x040000FB RID: 251
		private static readonly NullLogger instance = new NullLogger(LogLevel.Warn);
	}
}
