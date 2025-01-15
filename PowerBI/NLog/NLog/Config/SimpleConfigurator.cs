using System;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x0200019D RID: 413
	public static class SimpleConfigurator
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x00032FB1 File Offset: 0x000311B1
		public static void ConfigureForConsoleLogging()
		{
			SimpleConfigurator.ConfigureForConsoleLogging(LogLevel.Info);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00032FC0 File Offset: 0x000311C0
		public static void ConfigureForConsoleLogging(LogLevel minLevel)
		{
			ConsoleTarget consoleTarget = new ConsoleTarget();
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			loggingConfiguration.AddRule(minLevel, LogLevel.MaxLevel, consoleTarget, "*");
			LogManager.Configuration = loggingConfiguration;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00032FEF File Offset: 0x000311EF
		public static void ConfigureForTargetLogging(Target target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Info);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003300A File Offset: 0x0003120A
		public static void ConfigureForTargetLogging(Target target, LogLevel minLevel)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			loggingConfiguration.AddRule(minLevel, LogLevel.MaxLevel, target, "*");
			LogManager.Configuration = loggingConfiguration;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00033036 File Offset: 0x00031236
		public static void ConfigureForFileLogging(string fileName)
		{
			SimpleConfigurator.ConfigureForFileLogging(fileName, LogLevel.Info);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00033043 File Offset: 0x00031243
		public static void ConfigureForFileLogging(string fileName, LogLevel minLevel)
		{
			SimpleConfigurator.ConfigureForTargetLogging(new FileTarget
			{
				FileName = fileName
			}, minLevel);
		}
	}
}
