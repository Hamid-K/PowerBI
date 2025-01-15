using System;
using System.Data.Entity.Core.Common.Utils;
using System.Diagnostics;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000566 RID: 1382
	internal sealed class ConfigViewGenerator : InternalBase
	{
		// Token: 0x06004356 RID: 17238 RVA: 0x000E92F4 File Offset: 0x000E74F4
		internal ConfigViewGenerator()
		{
			this.m_watch = new Stopwatch();
			this.m_singleWatch = new Stopwatch();
			int num = Enum.GetNames(typeof(PerfType)).Length;
			this.m_breakdownTimes = new TimeSpan[num];
			this.m_traceLevel = ViewGenTraceLevel.None;
			this.m_generateUpdateViews = false;
			this.StartWatch();
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x000E935D File Offset: 0x000E755D
		// (set) Token: 0x06004358 RID: 17240 RVA: 0x000E9365 File Offset: 0x000E7565
		internal bool GenerateEsql { get; set; }

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x000E936E File Offset: 0x000E756E
		internal TimeSpan[] BreakdownTimes
		{
			get
			{
				return this.m_breakdownTimes;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x000E9376 File Offset: 0x000E7576
		// (set) Token: 0x0600435B RID: 17243 RVA: 0x000E937E File Offset: 0x000E757E
		internal ViewGenTraceLevel TraceLevel
		{
			get
			{
				return this.m_traceLevel;
			}
			set
			{
				this.m_traceLevel = value;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x000E9387 File Offset: 0x000E7587
		// (set) Token: 0x0600435D RID: 17245 RVA: 0x000E938F File Offset: 0x000E758F
		internal bool IsValidationEnabled
		{
			get
			{
				return this.m_enableValidation;
			}
			set
			{
				this.m_enableValidation = value;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000E9398 File Offset: 0x000E7598
		// (set) Token: 0x0600435F RID: 17247 RVA: 0x000E93A0 File Offset: 0x000E75A0
		internal bool GenerateUpdateViews
		{
			get
			{
				return this.m_generateUpdateViews;
			}
			set
			{
				this.m_generateUpdateViews = value;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000E93A9 File Offset: 0x000E75A9
		// (set) Token: 0x06004361 RID: 17249 RVA: 0x000E93B1 File Offset: 0x000E75B1
		internal bool GenerateViewsForEachType { get; set; }

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x000E93BA File Offset: 0x000E75BA
		internal bool IsViewTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.ViewsOnly);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x000E93C3 File Offset: 0x000E75C3
		internal bool IsNormalTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Normal);
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x000E93CC File Offset: 0x000E75CC
		internal bool IsVerboseTracing
		{
			get
			{
				return this.IsTraceAllowed(ViewGenTraceLevel.Verbose);
			}
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000E93D5 File Offset: 0x000E75D5
		private void StartWatch()
		{
			this.m_watch.Start();
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000E93E2 File Offset: 0x000E75E2
		internal void StartSingleWatch(PerfType perfType)
		{
			this.m_singleWatch.Start();
			this.m_singlePerfOp = perfType;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000E93F8 File Offset: 0x000E75F8
		internal void StopSingleWatch(PerfType perfType)
		{
			TimeSpan elapsed = this.m_singleWatch.Elapsed;
			this.m_singleWatch.Stop();
			this.m_singleWatch.Reset();
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000E9448 File Offset: 0x000E7648
		internal void SetTimeForFinishedActivity(PerfType perfType)
		{
			TimeSpan elapsed = this.m_watch.Elapsed;
			this.BreakdownTimes[(int)perfType] = this.BreakdownTimes[(int)perfType].Add(elapsed);
			this.m_watch.Reset();
			this.m_watch.Start();
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000E9497 File Offset: 0x000E7697
		internal bool IsTraceAllowed(ViewGenTraceLevel traceLevel)
		{
			return this.TraceLevel >= traceLevel;
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x000E94A5 File Offset: 0x000E76A5
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.FormatStringBuilder(builder, "Trace Switch: {0}", new object[] { this.m_traceLevel });
		}

		// Token: 0x04001809 RID: 6153
		private ViewGenTraceLevel m_traceLevel;

		// Token: 0x0400180A RID: 6154
		private readonly TimeSpan[] m_breakdownTimes;

		// Token: 0x0400180B RID: 6155
		private readonly Stopwatch m_watch;

		// Token: 0x0400180C RID: 6156
		private readonly Stopwatch m_singleWatch;

		// Token: 0x0400180D RID: 6157
		private PerfType m_singlePerfOp;

		// Token: 0x0400180E RID: 6158
		private bool m_enableValidation = true;

		// Token: 0x0400180F RID: 6159
		private bool m_generateUpdateViews = true;
	}
}
