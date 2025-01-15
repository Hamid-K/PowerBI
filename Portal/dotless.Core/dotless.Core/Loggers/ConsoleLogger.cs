using System;
using dotless.Core.configuration;

namespace dotless.Core.Loggers
{
	// Token: 0x020000AE RID: 174
	public class ConsoleLogger : Logger
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00017353 File Offset: 0x00015553
		public ConsoleLogger(LogLevel level)
			: base(level)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001735C File Offset: 0x0001555C
		public ConsoleLogger(DotlessConfiguration config)
			: this(config.LogLevel)
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001736A File Offset: 0x0001556A
		protected override void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
