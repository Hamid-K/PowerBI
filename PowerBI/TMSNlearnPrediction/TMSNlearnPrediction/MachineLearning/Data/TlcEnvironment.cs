using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002B5 RID: 693
	public sealed class TlcEnvironment : HostEnvironmentBase
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x000578F0 File Offset: 0x00055AF0
		public TlcEnvironment(int? seed = null, bool verbose = false, int conc = 0, TextWriter outWriter = null, TextWriter errWriter = null)
			: this(RandomUtils.Create(seed), verbose, conc, outWriter, errWriter)
		{
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00057904 File Offset: 0x00055B04
		public TlcEnvironment(IRandom rand, bool verbose = false, int conc = 0, TextWriter outWriter = null, TextWriter errWriter = null)
			: base(rand, verbose, conc, null, null)
		{
			this._componentCreationHistory = new BlockingCollection<string>();
			this._consoleWriter = new TlcEnvironment.ConsoleWriter(this, outWriter ?? Console.Out, errWriter ?? Console.Error);
			base.AddListener<ChannelMessage>(new Action<HostEnvironmentBase.IMessageSource, ChannelMessage>(this.PrintMessage));
			this._root = this;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00057964 File Offset: 0x00055B64
		private TlcEnvironment(TlcEnvironment source, IRandom rand, bool verbose, int conc)
			: base(source, rand, verbose, conc, null, null)
		{
			this._componentCreationHistory = source._componentCreationHistory;
			this._root = ((source._master == null) ? source : (source._master as TlcEnvironment));
			this._consoleWriter = null;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000579A4 File Offset: 0x00055BA4
		public void PrintProgress()
		{
			this._root._consoleWriter.GetAndPrintAllProgress(this._progressTracker);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000579BE File Offset: 0x00055BBE
		private void PrintMessage(HostEnvironmentBase.IMessageSource src, ChannelMessage msg)
		{
			this._root._consoleWriter.PrintMessage(src, msg);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000579D4 File Offset: 0x00055BD4
		protected override IHostEnvironment ForkCore(IRandom rand, bool? verbose = null, int? conc = null)
		{
			return new TlcEnvironment(this, rand, verbose ?? base.Verbose, conc ?? this._conc);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00057A1C File Offset: 0x00055C1C
		protected override IFileHandle CreateTempFileCore(IHostEnvironment env, string suffix = null, string prefix = null)
		{
			return base.CreateTempFileCore(env, suffix, "TLC_" + prefix);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00057A31 File Offset: 0x00055C31
		private void NewComponentRegistered(string fullName)
		{
			this._componentCreationHistory.Add(fullName);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00057A40 File Offset: 0x00055C40
		protected override IHost RegisterCore(IRandom rand, string parentFullName, string shortName, bool? verbose, int? conc)
		{
			HostEnvironmentBase.Host host = new HostEnvironmentBase.Host(this, shortName, parentFullName, RandomUtils.Create(rand), verbose, conc);
			this.NewComponentRegistered(host.FullName);
			return host;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00057A6D File Offset: 0x00055C6D
		protected override IChannel CreateCommChannel(ChannelProviderBase parent, string name)
		{
			return new TlcEnvironment.Channel(this, parent, name, base.GetDispatchDelegate<ChannelMessage>());
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00057A7D File Offset: 0x00055C7D
		protected override IPipe<TMessage> CreatePipe<TMessage>(ChannelProviderBase parent, string name)
		{
			return new HostEnvironmentBase.Pipe<TMessage>(this, parent, name, base.GetDispatchDelegate<TMessage>());
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00057A8D File Offset: 0x00055C8D
		protected override void DecorateException(Exception ex)
		{
			ex.Data["ComponentHistory"] = this._componentCreationHistory.ToArray();
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00057AAA File Offset: 0x00055CAA
		public override Exception Process(Exception ex)
		{
			ex = base.Process(ex);
			this.DecorateException(ex);
			return ex;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00057ABD File Offset: 0x00055CBD
		internal IDisposable RedirectChannelOutput(TextWriter newOutWriter, TextWriter newErrWriter)
		{
			Contracts.CheckValue<TextWriter>(newOutWriter, "newOutWriter");
			Contracts.CheckValue<TextWriter>(newErrWriter, "newErrWriter");
			return new TlcEnvironment.OutputRedirector(this, newOutWriter, newErrWriter);
		}

		// Token: 0x040008D0 RID: 2256
		public const string ComponentHistoryKey = "ComponentHistory";

		// Token: 0x040008D1 RID: 2257
		private volatile TlcEnvironment.ConsoleWriter _consoleWriter;

		// Token: 0x040008D2 RID: 2258
		private readonly TlcEnvironment _root;

		// Token: 0x040008D3 RID: 2259
		private readonly BlockingCollection<string> _componentCreationHistory;

		// Token: 0x020002B6 RID: 694
		private sealed class ConsoleWriter
		{
			// Token: 0x06000FE3 RID: 4067 RVA: 0x00057ADD File Offset: 0x00055CDD
			public ConsoleWriter(TlcEnvironment parent, TextWriter outWriter, TextWriter errWriter)
			{
				this._lock = new object();
				this._parent = parent;
				this._out = outWriter;
				this._err = errWriter;
			}

			// Token: 0x06000FE4 RID: 4068 RVA: 0x00057B08 File Offset: 0x00055D08
			public void PrintMessage(HostEnvironmentBase.IMessageSource sender, ChannelMessage msg)
			{
				bool flag;
				switch (msg.Kind)
				{
				case 0:
					if (!sender.Verbose)
					{
						return;
					}
					flag = false;
					break;
				case 1:
					flag = false;
					break;
				case 2:
					flag = true;
					break;
				default:
					flag = true;
					break;
				}
				lock (this._lock)
				{
					this.EnsureNewLine(flag);
					TextWriter textWriter = (flag ? this._err : this._out);
					HostEnvironmentBase.PipeBase<ChannelMessage> pipeBase = sender as HostEnvironmentBase.PipeBase<ChannelMessage>;
					if (pipeBase != null && pipeBase.Verbose)
					{
						this.WriteHeader(textWriter, pipeBase);
					}
					if (msg.Kind == 2)
					{
						textWriter.Write("Warning: ");
					}
					this._parent.PrintMessageNormalized(textWriter, msg.Message, true);
				}
			}

			// Token: 0x06000FE5 RID: 4069 RVA: 0x00057BD8 File Offset: 0x00055DD8
			private void WriteHeader(TextWriter wr, HostEnvironmentBase.PipeBase<ChannelMessage> commChannel)
			{
				wr.Write(new string(' ', commChannel.Depth * 2));
				this.WriteName(wr, commChannel);
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x00057BF8 File Offset: 0x00055DF8
			private void WriteName(TextWriter wr, ChannelProviderBase provider)
			{
				TlcEnvironment.Channel channel = provider as TlcEnvironment.Channel;
				if (channel != null)
				{
					this.WriteName(wr, channel.Parent);
				}
				wr.Write("{0}: ", provider.ShortName);
			}

			// Token: 0x06000FE7 RID: 4071 RVA: 0x00057C30 File Offset: 0x00055E30
			public void ChannelStarted(TlcEnvironment.Channel channel)
			{
				if (!channel.Verbose)
				{
					return;
				}
				lock (this._lock)
				{
					this.EnsureNewLine(false);
					this.WriteHeader(this._out, channel);
					this._out.WriteLine("Started.");
				}
			}

			// Token: 0x06000FE8 RID: 4072 RVA: 0x00057C98 File Offset: 0x00055E98
			public void ChannelFinished(TlcEnvironment.Channel channel)
			{
				if (!channel.Verbose)
				{
					return;
				}
				lock (this._lock)
				{
					this.EnsureNewLine(false);
					this.WriteHeader(this._out, channel);
					this._out.WriteLine("Finished.");
				}
			}

			// Token: 0x06000FE9 RID: 4073 RVA: 0x00057D00 File Offset: 0x00055F00
			public void ChannelDisposed(TlcEnvironment.Channel channel, bool active)
			{
				if (!channel.Verbose)
				{
					return;
				}
				lock (this._lock)
				{
					this.EnsureNewLine(false);
					if (active)
					{
						this.PrintMessage(channel, new ChannelMessage(3, "The channel was not properly closed."));
					}
					this.WriteHeader(this._out, channel);
					this._out.WriteLine("Elapsed {0:c}.", channel.Watch.Elapsed);
				}
			}

			// Token: 0x06000FEA RID: 4074 RVA: 0x00057DA4 File Offset: 0x00055FA4
			public void GetAndPrintAllProgress(ProgressReporting.ProgressTracker progressTracker)
			{
				List<ProgressReporting.ProgressEvent> allProgress = progressTracker.GetAllProgress();
				if (allProgress.Count == 0)
				{
					return;
				}
				IEnumerable<ProgressReporting.ProgressEvent> enumerable = allProgress.Where((ProgressReporting.ProgressEvent x) => x.Kind != ProgressReporting.ProgressEvent.EventKind.Progress || x.ProgressEntry.IsCheckpoint);
				lock (this._lock)
				{
					bool flag2 = false;
					foreach (ProgressReporting.ProgressEvent progressEvent in enumerable)
					{
						flag2 = true;
						this.EnsureNewLine(false);
						switch (progressEvent.Kind)
						{
						case ProgressReporting.ProgressEvent.EventKind.Start:
							TlcEnvironment.ConsoleWriter.PrintOperationStart(this._out, progressEvent);
							break;
						case ProgressReporting.ProgressEvent.EventKind.Progress:
							this._out.Write("[{0}] ", progressEvent.Index);
							this.PrintProgressLine(this._out, progressEvent);
							break;
						case ProgressReporting.ProgressEvent.EventKind.Stop:
							TlcEnvironment.ConsoleWriter.PrintOperationStop(this._out, progressEvent);
							break;
						}
					}
					if (!flag2)
					{
						if (this.PrintDot())
						{
							bool flag3 = allProgress.Count > 1;
							foreach (ProgressReporting.ProgressEvent progressEvent2 in allProgress)
							{
								if (flag3)
								{
									this.EnsureNewLine(false);
									this._out.Write("[{0}] ", progressEvent2.Index);
								}
								else
								{
									this._dots = 0;
								}
								this.PrintProgressLine(this._out, progressEvent2);
							}
						}
					}
				}
			}

			// Token: 0x06000FEB RID: 4075 RVA: 0x00057F6C File Offset: 0x0005616C
			private static void PrintOperationStart(TextWriter writer, ProgressReporting.ProgressEvent ev)
			{
				writer.WriteLine("[{0}] '{1}' started.", ev.Index, ev.Name);
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x00057F8A File Offset: 0x0005618A
			private static void PrintOperationStop(TextWriter writer, ProgressReporting.ProgressEvent ev)
			{
				writer.WriteLine("[{0}] '{1}' finished in {2}.", ev.Index, ev.Name, ev.EventTime - ev.StartTime);
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x00057FC0 File Offset: 0x000561C0
			private void PrintProgressLine(TextWriter writer, ProgressReporting.ProgressEvent ev)
			{
				TimeSpan timeSpan = ev.EventTime - ev.StartTime;
				if (timeSpan.TotalMinutes < 1.0)
				{
					writer.Write("(00:{0:00.00})", timeSpan.TotalSeconds);
				}
				else if (timeSpan.TotalHours < 1.0)
				{
					writer.Write("({0:00}:{1:00.0})", timeSpan.Minutes, timeSpan.TotalSeconds - (double)(60 * timeSpan.Minutes));
				}
				else
				{
					writer.Write("({0:00}:{1:00}:{2:00})", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				}
				bool flag = true;
				for (int i = 0; i < ev.ProgressEntry.Header.UnitNames.Length; i++)
				{
					if (ev.ProgressEntry.Progress[i] != null)
					{
						writer.Write(flag ? "\t" : ", ");
						flag = false;
						writer.Write("{0}", ev.ProgressEntry.Progress[i]);
						if (ev.ProgressEntry.ProgressLim[i] != null)
						{
							writer.Write("/{0}", ev.ProgressEntry.ProgressLim[i].Value);
						}
						writer.Write(" {0}", ev.ProgressEntry.Header.UnitNames[i]);
					}
				}
				for (int j = 0; j < ev.ProgressEntry.Header.MetricNames.Length; j++)
				{
					if (ev.ProgressEntry.Metrics[j] != null)
					{
						writer.Write("\t{0}: {1}", ev.ProgressEntry.Header.MetricNames[j], ev.ProgressEntry.Metrics[j].Value);
					}
				}
				writer.WriteLine();
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x000581C8 File Offset: 0x000563C8
			private void EnsureNewLine(bool isError = false)
			{
				if (this._dots == 0)
				{
					return;
				}
				if (isError && this._err != this._out && (this._out != Console.Out || this._err != Console.Error))
				{
					return;
				}
				this._out.WriteLine();
				this._dots = 0;
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x0005821C File Offset: 0x0005641C
			private bool PrintDot()
			{
				this._out.Write(".");
				this._dots++;
				return this._dots == 50;
			}

			// Token: 0x040008D4 RID: 2260
			private const int _maxDots = 50;

			// Token: 0x040008D5 RID: 2261
			private readonly object _lock;

			// Token: 0x040008D6 RID: 2262
			private readonly TlcEnvironment _parent;

			// Token: 0x040008D7 RID: 2263
			private readonly TextWriter _out;

			// Token: 0x040008D8 RID: 2264
			private readonly TextWriter _err;

			// Token: 0x040008D9 RID: 2265
			private int _dots;
		}

		// Token: 0x020002B7 RID: 695
		private sealed class Channel : HostEnvironmentBase.ChannelBase
		{
			// Token: 0x06000FF1 RID: 4081 RVA: 0x00058246 File Offset: 0x00056446
			public Channel(TlcEnvironment master, ChannelProviderBase parent, string shortName, Action<HostEnvironmentBase.IMessageSource, ChannelMessage> dispatch)
				: base(master, parent, shortName, dispatch)
			{
				this._master = master._root;
				this.Watch = Stopwatch.StartNew();
				this._master._consoleWriter.ChannelStarted(this);
			}

			// Token: 0x06000FF2 RID: 4082 RVA: 0x0005827D File Offset: 0x0005647D
			public override void Done()
			{
				this.Watch.Stop();
				this._master._consoleWriter.ChannelFinished(this);
				base.Done();
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x000582A3 File Offset: 0x000564A3
			protected override void DisposeCore()
			{
				if (base.IsActive)
				{
					this.Watch.Stop();
				}
				this._master._consoleWriter.ChannelDisposed(this, base.IsActive);
				base.DisposeCore();
			}

			// Token: 0x040008DB RID: 2267
			private readonly TlcEnvironment _master;

			// Token: 0x040008DC RID: 2268
			public readonly Stopwatch Watch;
		}

		// Token: 0x020002B8 RID: 696
		private sealed class OutputRedirector : IDisposable
		{
			// Token: 0x06000FF4 RID: 4084 RVA: 0x000582D8 File Offset: 0x000564D8
			public OutputRedirector(TlcEnvironment env, TextWriter newOutWriter, TextWriter newErrWriter)
			{
				this._root = env._root;
				this._newConsoleWriter = new TlcEnvironment.ConsoleWriter(this._root, newOutWriter, newErrWriter);
				this._oldConsoleWriter = Interlocked.Exchange<TlcEnvironment.ConsoleWriter>(ref this._root._consoleWriter, this._newConsoleWriter);
			}

			// Token: 0x06000FF5 RID: 4085 RVA: 0x00058326 File Offset: 0x00056526
			public void Dispose()
			{
				if (this._oldConsoleWriter == null)
				{
					return;
				}
				this._root._consoleWriter = this._oldConsoleWriter;
				this._oldConsoleWriter = null;
			}

			// Token: 0x040008DD RID: 2269
			private readonly TlcEnvironment _root;

			// Token: 0x040008DE RID: 2270
			private TlcEnvironment.ConsoleWriter _oldConsoleWriter;

			// Token: 0x040008DF RID: 2271
			private readonly TlcEnvironment.ConsoleWriter _newConsoleWriter;
		}
	}
}
