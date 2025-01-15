using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using NLog.Common;

namespace NLog.Targets
{
	// Token: 0x0200002E RID: 46
	[Target("Console")]
	public sealed class ConsoleTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000B00C File Offset: 0x0000920C
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000B014 File Offset: 0x00009214
		[DefaultValue(false)]
		public bool Error { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000B01D File Offset: 0x0000921D
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000B036 File Offset: 0x00009236
		public Encoding Encoding
		{
			get
			{
				return ConsoleTargetHelper.GetConsoleOutputEncoding(this._encoding, base.IsInitialized, this._pauseLogging);
			}
			set
			{
				if (ConsoleTargetHelper.SetConsoleOutputEncoding(value, base.IsInitialized, this._pauseLogging))
				{
					this._encoding = value;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000B053 File Offset: 0x00009253
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000B05B File Offset: 0x0000925B
		[DefaultValue(false)]
		public bool DetectConsoleAvailable { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000B064 File Offset: 0x00009264
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000B06C File Offset: 0x0000926C
		[DefaultValue(false)]
		public bool AutoFlush { get; set; }

		// Token: 0x06000529 RID: 1321 RVA: 0x0000B075 File Offset: 0x00009275
		public ConsoleTarget()
		{
			this._pauseLogging = false;
			this.DetectConsoleAvailable = false;
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000B092 File Offset: 0x00009292
		public ConsoleTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000B0A4 File Offset: 0x000092A4
		protected override void InitializeTarget()
		{
			this._pauseLogging = false;
			if (this.DetectConsoleAvailable)
			{
				string text;
				this._pauseLogging = !ConsoleTargetHelper.IsConsoleAvailable(out text);
				if (this._pauseLogging)
				{
					InternalLogger.Info<string>("Console has been detected as turned off. Disable DetectConsoleAvailable to skip detection. Reason: {0}", text);
				}
			}
			if (this._encoding != null)
			{
				ConsoleTargetHelper.SetConsoleOutputEncoding(this._encoding, true, this._pauseLogging);
			}
			base.InitializeTarget();
			if (base.Header != null)
			{
				this.WriteToOutput(base.RenderLogEvent(base.Header, LogEventInfo.CreateNullEvent()));
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000B123 File Offset: 0x00009323
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				this.WriteToOutput(base.RenderLogEvent(base.Footer, LogEventInfo.CreateNullEvent()));
			}
			this.ExplicitConsoleFlush();
			base.CloseTarget();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000B150 File Offset: 0x00009350
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				this.ExplicitConsoleFlush();
				base.FlushAsync(asyncContinuation);
			}
			catch (Exception ex)
			{
				asyncContinuation(ex);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000B188 File Offset: 0x00009388
		private void ExplicitConsoleFlush()
		{
			if (!this._pauseLogging && !this.AutoFlush)
			{
				this.GetOutput().Flush();
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000B1A5 File Offset: 0x000093A5
		protected override void Write(LogEventInfo logEvent)
		{
			if (this._pauseLogging)
			{
				return;
			}
			this.WriteToOutput(base.RenderLogEvent(this.Layout, logEvent));
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private void WriteToOutput(string textLine)
		{
			if (this._pauseLogging)
			{
				return;
			}
			TextWriter output = this.GetOutput();
			try
			{
				output.WriteLine(textLine);
				if (this.AutoFlush)
				{
					output.Flush();
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				this._pauseLogging = true;
				InternalLogger.Warn(ex, "An IndexOutOfRangeException has been thrown and this is probably due to a race condition.Logging to the console will be paused. Enable by reloading the config or re-initialize the targets");
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				this._pauseLogging = true;
				InternalLogger.Warn(ex2, "An ArgumentOutOfRangeException has been thrown and this is probably due to a race condition.Logging to the console will be paused. Enable by reloading the config or re-initialize the targets");
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000B23C File Offset: 0x0000943C
		private TextWriter GetOutput()
		{
			if (!this.Error)
			{
				return Console.Out;
			}
			return Console.Error;
		}

		// Token: 0x04000088 RID: 136
		private bool _pauseLogging;

		// Token: 0x0400008A RID: 138
		private Encoding _encoding;
	}
}
