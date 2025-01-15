using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DD RID: 221
	[LayoutRenderer("performancecounter")]
	public class PerformanceCounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00021687 File Offset: 0x0001F887
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0002168F File Offset: 0x0001F88F
		[RequiredParameter]
		public string Category { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00021698 File Offset: 0x0001F898
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x000216A0 File Offset: 0x0001F8A0
		[RequiredParameter]
		public string Counter { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x000216A9 File Offset: 0x0001F8A9
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x000216B1 File Offset: 0x0001F8B1
		public string Instance { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x000216BA File Offset: 0x0001F8BA
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x000216C2 File Offset: 0x0001F8C2
		public string MachineName { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x000216CB File Offset: 0x0001F8CB
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x000216D3 File Offset: 0x0001F8D3
		public string Format { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x000216DC File Offset: 0x0001F8DC
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x000216E4 File Offset: 0x0001F8E4
		public CultureInfo Culture { get; set; }

		// Token: 0x06000D25 RID: 3365 RVA: 0x000216F0 File Offset: 0x0001F8F0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			this._prevSample = CounterSample.Empty;
			this._nextSample = CounterSample.Empty;
			if (this.MachineName != null)
			{
				this._perfCounter = new PerformanceCounter(this.Category, this.Counter, this.Instance ?? string.Empty, this.MachineName);
				return;
			}
			string text = this.Instance;
			if (string.IsNullOrEmpty(text) && string.Equals(this.Category, "Process", StringComparison.OrdinalIgnoreCase))
			{
				text = PerformanceCounterLayoutRenderer.GetCurrentProcessInstanceName(this.Category);
			}
			this._perfCounter = new PerformanceCounter(this.Category, this.Counter, text ?? string.Empty, true);
			this.GetValue();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000217A8 File Offset: 0x0001F9A8
		private static string GetCurrentProcessInstanceName(string category)
		{
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					int id = currentProcess.Id;
					foreach (string text in new PerformanceCounterCategory(category).GetInstanceNames())
					{
						using (PerformanceCounter performanceCounter = new PerformanceCounter(category, "ID Process", text, true))
						{
							if ((int)performanceCounter.RawValue == id)
							{
								return text;
							}
						}
					}
					InternalLogger.Debug<int>("PerformanceCounter - Failed to auto detect current process instance. ProcessId={0}", id);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				InternalLogger.Warn(ex, "PerformanceCounter - Failed to auto detect current process instance.");
			}
			return string.Empty;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00021870 File Offset: 0x0001FA70
		protected override void CloseLayoutRenderer()
		{
			base.CloseLayoutRenderer();
			if (this._perfCounter != null)
			{
				this._perfCounter.Close();
				this._perfCounter = null;
			}
			this._prevSample = CounterSample.Empty;
			this._nextSample = CounterSample.Empty;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000218A8 File Offset: 0x0001FAA8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, this.Culture);
			builder.Append(this.GetValue().ToString(this.Format, formatProvider));
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000218E0 File Offset: 0x0001FAE0
		private float GetValue()
		{
			CounterSample counterSample = this._perfCounter.NextSample();
			if (counterSample.SystemFrequency != 0L)
			{
				float num = (float)(counterSample.TimeStamp - this._nextSample.TimeStamp) / (float)counterSample.SystemFrequency;
				if (num > 0.5f || num < -0.5f)
				{
					this._prevSample = this._nextSample;
					this._nextSample = counterSample;
					if (this._prevSample.Equals(CounterSample.Empty))
					{
						this._prevSample = counterSample;
					}
				}
			}
			else
			{
				this._prevSample = this._nextSample;
				this._nextSample = counterSample;
			}
			return CounterSample.Calculate(this._prevSample, counterSample);
		}

		// Token: 0x04000355 RID: 853
		private PerformanceCounter _perfCounter;

		// Token: 0x04000356 RID: 854
		private CounterSample _prevSample = CounterSample.Empty;

		// Token: 0x04000357 RID: 855
		private CounterSample _nextSample = CounterSample.Empty;
	}
}
