using System;
using System.Configuration;
using System.IO;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D1 RID: 209
	public class UtilityApplicationRoot<T> : ApplicationRoot where T : UtilityBlock, new()
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00014D4C File Offset: 0x00012F4C
		public UtilityApplicationRoot()
			: base("UtilityApplicationRoot")
		{
			this.m_utilityBlock = new T();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014D6C File Offset: 0x00012F6C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			try
			{
				base.AddBlocks(Loader.LoadConfiguredBlocks());
			}
			catch (ConfigurationErrorsException)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Configuration section 'applicationRoot' missing -- blocks will not be loaded from configuration");
			}
			base.AddBlock(this.m_utilityBlock);
			base.AddBlocks(this.m_utilityBlock.GetBlocksToAdd());
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014DCC File Offset: 0x00012FCC
		protected override void OnPostStart()
		{
			base.OnPostStart();
			int num = this.m_utilityBlock.Run();
			base.RequestShutdown(this.m_utilityBlock, num);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected override void ValidateRuntimeRequirements()
		{
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00014DF8 File Offset: 0x00012FF8
		protected override void OnCrash(object sender, CrashEventArgs args)
		{
			int num = 111;
			Exception exceptionObject = args.ExceptionObject;
			if (exceptionObject != null)
			{
				if (exceptionObject is UnauthorizedAccessException)
				{
					num = 5;
				}
				else if (exceptionObject is FileNotFoundException)
				{
					num = 2;
				}
				else if (exceptionObject is NotSupportedException)
				{
					num = 6;
				}
			}
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "Process terminated due to an unhandled exception (exitCode={0})", new object[] { num });
			TraceDump traceDump = new TraceDump((args.ExceptionObject != null) ? args.ExceptionObject.ToString() : "");
			ExtendedConsole.WriteLineToError(string.Concat(new object[]
			{
				"Process ",
				CurrentProcess.Name,
				" terminated due to an unhandled exception (exitCode=",
				num,
				")"
			}));
			if (args.ExceptionObject != null && string.IsNullOrEmpty(args.ExceptionObject.StackTrace))
			{
				traceDump.Add(ExtendedEnvironment.CrashStackTrace);
			}
			foreach (string text in traceDump.Lines)
			{
				ExtendedConsole.WriteLineToError(text);
			}
			Environment.Exit(num);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014F14 File Offset: 0x00013114
		protected override void OnRequestShutdown(IBlock requestor, int returnCode)
		{
			if (requestor == null && this.m_utilityBlock.ExitOnControlC)
			{
				Environment.Exit(0);
				return;
			}
			base.OnRequestShutdown(requestor, returnCode);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014F35 File Offset: 0x00013135
		public static int DebuggableRun(string[] args)
		{
			return UtilityApplicationRoot<T>.DebuggableRun<UtilityApplicationRoot<T>>(args);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00014F40 File Offset: 0x00013140
		public static int DebuggableRun<Q>(string[] args) where Q : UtilityApplicationRoot<T>, new()
		{
			int result = 0;
			ExceptionFilters.TryFilterCatch(delegate
			{
				result = WinStarter.Run<Q>(args);
			}, delegate(Exception ex)
			{
				ExtendedDiagnostics.AlertDebuggerIfAttached();
				return ExceptionDisposition.ExecuteHandler;
			}, delegate(Exception ex)
			{
				ExtendedConsole.WriteLineToError(ConsoleColor.Red, "Error: {0}", new object[] { ex.Message });
				ExtendedConsole.WriteLineToError(ConsoleColor.White, "Details: {0}", new object[] { ex.ToString() });
				ExtendedDiagnostics.AlertDebuggerIfAttached();
				result = 100;
			});
			return result;
		}

		// Token: 0x0400020B RID: 523
		private UtilityBlock m_utilityBlock;
	}
}
