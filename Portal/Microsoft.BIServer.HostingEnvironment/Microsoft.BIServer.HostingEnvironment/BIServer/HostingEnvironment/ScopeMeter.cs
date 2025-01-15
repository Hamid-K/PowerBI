using System;
using System.Diagnostics;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000015 RID: 21
	public sealed class ScopeMeter : IDisposable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000039E4 File Offset: 0x00001BE4
		public static IDisposable Use(string name)
		{
			if (!Logger.MetricsEnabled)
			{
				return new ScopeMeter.NoOpPathMeter();
			}
			if (Environment.UserInteractive)
			{
				return new ScopeMeter.ConsolePathMeter();
			}
			return new ScopeMeter(name);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A06 File Offset: 0x00001C06
		public static IDisposable Use(params string[] names)
		{
			if (!Logger.MetricsEnabled)
			{
				return new ScopeMeter.NoOpPathMeter();
			}
			return ScopeMeter.Use(string.Join(".", names));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003A25 File Offset: 0x00001C25
		public ScopeMeter(string name)
		{
			this._name = name;
			this._stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A40 File Offset: 0x00001C40
		public void Dispose()
		{
			try
			{
				this._stopwatch.Stop();
				long elapsedMilliseconds = this._stopwatch.ElapsedMilliseconds;
				Logger.Meter(this._name, elapsedMilliseconds);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0400006D RID: 109
		private readonly Stopwatch _stopwatch;

		// Token: 0x0400006E RID: 110
		private readonly string _name;

		// Token: 0x0200005C RID: 92
		private sealed class ConsolePathMeter : IDisposable
		{
			// Token: 0x060001FC RID: 508 RVA: 0x00003749 File Offset: 0x00001949
			public void Dispose()
			{
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00006C4C File Offset: 0x00004E4C
			private void Meter(TraceLevel traceLevel, string format, params object[] args)
			{
				object consoleLockObj = ScopeMeter.ConsolePathMeter.ConsoleLockObj;
				lock (consoleLockObj)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("==| {0:T} - {1}", DateTimeOffset.Now, string.Format(format, args));
					Console.ResetColor();
				}
			}

			// Token: 0x04000147 RID: 327
			private static readonly object ConsoleLockObj = new object();
		}

		// Token: 0x0200005D RID: 93
		private sealed class NoOpPathMeter : IDisposable
		{
			// Token: 0x06000200 RID: 512 RVA: 0x00003749 File Offset: 0x00001949
			public void Dispose()
			{
			}
		}
	}
}
