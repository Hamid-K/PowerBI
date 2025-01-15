using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000BF RID: 191
	public class CommandProcessorBlock : Block
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x00013F00 File Offset: 0x00012100
		public CommandProcessorBlock(string name)
			: base(name)
		{
			this.m_runThread = null;
			this.m_name = name;
			this.m_commands = new Dictionary<string, ProcessorCommand>();
			this.m_quitSignal = new ManualResetEvent(false);
			this.m_initialized = false;
			this.TimeCommands = false;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00013F3C File Offset: 0x0001213C
		protected override BlockInitializationStatus OnInitialize()
		{
			if (!this.m_initialized)
			{
				this.Add("?", new ProcessorCommand(new CommandFunction(this.HelpCommand), "Displays help", ""));
				this.Add("h", new ProcessorCommand(new CommandFunction(this.HelpCommand), "Displays help", ""));
				this.Add("help", new ProcessorCommand(new CommandFunction(this.HelpCommand), "Displays help", ""));
				this.Add("quit", new ProcessorCommand(new CommandFunction(this.QuitCommand), "Quits the console", ""));
				this.Add("exit", new ProcessorCommand(new CommandFunction(this.QuitCommand), "Quits the console", ""));
				this.Add("q", new ProcessorCommand(new CommandFunction(this.QuitCommand), "Quits the console", ""));
				this.Add("sleep", new ProcessorCommand(new CommandFunction(this.SleepCommand), "Sleep for a specified time", "sleep <time in seconds>"));
				this.m_runThread = new Thread(new ThreadStart(this.Run));
				this.m_initialized = true;
			}
			return BlockInitializationStatus.Done;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001407E File Offset: 0x0001227E
		protected override void OnStart()
		{
			CommandProcessorBlock.Out("** Starting", new object[0]);
			UtilsContext.Current.RunWithClearContext(delegate
			{
				this.m_runThread.Start();
			});
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000140A6 File Offset: 0x000122A6
		protected override void OnStop()
		{
			CommandProcessorBlock.Out("** Stopping", new object[0]);
			this.SetQuit();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000140BE File Offset: 0x000122BE
		protected override void OnWaitForStopToComplete()
		{
			CommandProcessorBlock.Out("** Wait for completion", new object[0]);
			this.m_runThread.Join();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000140DB File Offset: 0x000122DB
		protected override void OnShutdown()
		{
			this.m_quitSignal.Close();
			this.m_quitSignal = null;
			this.m_runThread = null;
			this.m_initialized = false;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000140FD File Offset: 0x000122FD
		protected void Add(string verb, ProcessorCommand command)
		{
			this.m_commands.Add(verb.ToLower(CultureInfo.CurrentCulture), command);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014116 File Offset: 0x00012316
		protected virtual string Prompt
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014120 File Offset: 0x00012320
		protected virtual void Run()
		{
			Thread.CurrentThread.Name = this.m_name;
			while (!this.IsQuitting)
			{
				try
				{
					Console.Write(this.Prompt);
					string text = Console.ReadLine();
					if (text == null)
					{
						break;
					}
					this.Parse(ExtendedText.Split(text, "\\t "));
				}
				catch (SyntaxErrorException ex)
				{
					this.OnSyntaxException(ex);
				}
				catch (Exception ex2)
				{
					this.OnException(ex2);
				}
			}
			base.BlockHost.RequestShutdown(this);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000141B0 File Offset: 0x000123B0
		protected virtual void OnSyntaxException(SyntaxErrorException exception)
		{
			CommandProcessorBlock.Out(ConsoleColor.Red, "Syntax error: {0}", new object[] { exception.Message });
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000141CD File Offset: 0x000123CD
		protected virtual void OnException(Exception exception)
		{
			CommandProcessorBlock.Out(ConsoleColor.Red, "Exception occured: {0}", new object[] { exception });
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000141E5 File Offset: 0x000123E5
		protected void SetQuit()
		{
			this.m_quitSignal.Set();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000141F3 File Offset: 0x000123F3
		protected bool IsQuitting
		{
			get
			{
				return this.m_quitSignal.WaitOne(0, false);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00014202 File Offset: 0x00012402
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0001420A File Offset: 0x0001240A
		protected bool TimeCommands { get; set; }

		// Token: 0x06000587 RID: 1415 RVA: 0x00014213 File Offset: 0x00012413
		[StringFormatMethod("format")]
		protected static void Out(ConsoleColor color, [NotNull] string format, params object[] args)
		{
			ExtendedConsole.WriteLine(color, format, args);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001421D File Offset: 0x0001241D
		[StringFormatMethod("format")]
		protected static void Out([NotNull] string format, params object[] args)
		{
			CommandProcessorBlock.Out(Console.ForegroundColor, format, args);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001422B File Offset: 0x0001242B
		protected static void Out(ConsoleColor color, object argument)
		{
			CommandProcessorBlock.Out(color, "{0}", new object[] { argument });
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014242 File Offset: 0x00012442
		protected static void Out(object argument)
		{
			CommandProcessorBlock.Out("{0}", new object[] { argument });
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014258 File Offset: 0x00012458
		protected void HelpCommand([NotNull] string[] args)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string[]>(args, "args");
			if (args.Length != 1)
			{
				throw new SyntaxErrorException();
			}
			this.DisplayCommands();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00014277 File Offset: 0x00012477
		protected void QuitCommand([NotNull] string[] args)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string[]>(args, "args");
			if (args.Length != 1)
			{
				throw new SyntaxErrorException();
			}
			this.SetQuit();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00014298 File Offset: 0x00012498
		protected void SleepCommand([NotNull] string[] args)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string[]>(args, "args");
			if (args.Length != 2 || string.IsNullOrEmpty(args[1]))
			{
				throw new SyntaxErrorException();
			}
			int num = int.Parse(args[1], NumberFormatInfo.InvariantInfo) * 1000;
			this.m_quitSignal.WaitOne(num, false);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000142E8 File Offset: 0x000124E8
		protected void DisplayCommands()
		{
			int num = this.m_commands.Keys.Aggregate(8, (int current, string command) => Math.Max(current, command.Length));
			string text = string.Format(CultureInfo.CurrentCulture, "{{0,-{0}}}{{1,-40}}{{2}}", new object[] { num + 1 });
			foreach (string text2 in this.m_commands.Keys)
			{
				CommandProcessorBlock.Out(text, new object[]
				{
					text2,
					this.m_commands[text2].Help,
					this.m_commands[text2].Usage
				});
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000143C4 File Offset: 0x000125C4
		private void Parse(string[] args)
		{
			if (args.Length == 0)
			{
				return;
			}
			ProcessorCommand processorCommand;
			if (this.m_commands.TryGetValue(args[0].ToLower(CultureInfo.CurrentCulture), out processorCommand))
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					processorCommand.Invoke(args);
				}
				finally
				{
					stopwatch.Stop();
					if (this.TimeCommands)
					{
						CommandProcessorBlock.Out(ConsoleColor.Blue, "Execution: {0}", new object[] { stopwatch.Elapsed });
					}
				}
				return;
			}
			throw new SyntaxErrorException(string.Format(CultureInfo.CurrentCulture, "Unrecognized command: '{0}'", new object[] { args[0] }));
		}

		// Token: 0x040001E3 RID: 483
		private Thread m_runThread;

		// Token: 0x040001E4 RID: 484
		private readonly string m_name;

		// Token: 0x040001E5 RID: 485
		private readonly Dictionary<string, ProcessorCommand> m_commands;

		// Token: 0x040001E6 RID: 486
		private ManualResetEvent m_quitSignal;

		// Token: 0x040001E7 RID: 487
		private bool m_initialized;
	}
}
