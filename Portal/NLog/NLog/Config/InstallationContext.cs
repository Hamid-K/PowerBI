using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace NLog.Config
{
	// Token: 0x0200018A RID: 394
	public sealed class InstallationContext : IDisposable
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0002E3B2 File Offset: 0x0002C5B2
		public InstallationContext()
			: this(TextWriter.Null)
		{
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0002E3BF File Offset: 0x0002C5BF
		public InstallationContext(TextWriter logOutput)
		{
			this.LogOutput = logOutput;
			this.Parameters = new Dictionary<string, string>();
			this.LogLevel = LogLevel.Info;
			this.ThrowExceptions = false;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0002E3EB File Offset: 0x0002C5EB
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x0002E3F3 File Offset: 0x0002C5F3
		public LogLevel LogLevel { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0002E3FC File Offset: 0x0002C5FC
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x0002E404 File Offset: 0x0002C604
		public bool IgnoreFailures { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0002E40D File Offset: 0x0002C60D
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0002E415 File Offset: 0x0002C615
		public bool ThrowExceptions { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0002E41E File Offset: 0x0002C61E
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x0002E426 File Offset: 0x0002C626
		public IDictionary<string, string> Parameters { get; private set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0002E42F File Offset: 0x0002C62F
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x0002E437 File Offset: 0x0002C637
		public TextWriter LogOutput { get; set; }

		// Token: 0x060011D9 RID: 4569 RVA: 0x0002E440 File Offset: 0x0002C640
		public void Trace([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Trace, message, arguments);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0002E44F File Offset: 0x0002C64F
		public void Debug([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Debug, message, arguments);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0002E45E File Offset: 0x0002C65E
		public void Info([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Info, message, arguments);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0002E46D File Offset: 0x0002C66D
		public void Warning([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Warn, message, arguments);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0002E47C File Offset: 0x0002C67C
		public void Error([Localizable(false)] string message, params object[] arguments)
		{
			this.Log(LogLevel.Error, message, arguments);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0002E48B File Offset: 0x0002C68B
		public void Dispose()
		{
			if (this.LogOutput != null)
			{
				this.LogOutput.Close();
				this.LogOutput = null;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		public LogEventInfo CreateLogEvent()
		{
			LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
			foreach (KeyValuePair<string, string> keyValuePair in this.Parameters)
			{
				logEventInfo.Properties.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return logEventInfo;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0002E510 File Offset: 0x0002C710
		private void Log(LogLevel logLevel, [Localizable(false)] string message, object[] arguments)
		{
			if (logLevel >= this.LogLevel)
			{
				if (arguments != null && arguments.Length != 0)
				{
					message = string.Format(CultureInfo.InvariantCulture, message, arguments);
				}
				ConsoleColor foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = InstallationContext.LogLevel2ConsoleColor[logLevel];
				try
				{
					this.LogOutput.WriteLine(message);
				}
				finally
				{
					Console.ForegroundColor = foregroundColor;
				}
			}
		}

		// Token: 0x040004D9 RID: 1241
		private static readonly Dictionary<LogLevel, ConsoleColor> LogLevel2ConsoleColor = new Dictionary<LogLevel, ConsoleColor>
		{
			{
				LogLevel.Trace,
				ConsoleColor.DarkGray
			},
			{
				LogLevel.Debug,
				ConsoleColor.Gray
			},
			{
				LogLevel.Info,
				ConsoleColor.White
			},
			{
				LogLevel.Warn,
				ConsoleColor.Yellow
			},
			{
				LogLevel.Error,
				ConsoleColor.Red
			},
			{
				LogLevel.Fatal,
				ConsoleColor.DarkRed
			}
		};
	}
}
