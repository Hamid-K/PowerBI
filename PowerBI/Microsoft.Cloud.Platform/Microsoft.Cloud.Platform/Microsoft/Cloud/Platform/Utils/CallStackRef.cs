using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D3 RID: 467
	public sealed class CallStackRef : IDisposable, ITraceDumpable
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002A094 File Offset: 0x00028294
		public StackTrace StackTrace
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002A09C File Offset: 0x0002829C
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002A0A4 File Offset: 0x000282A4
		public DateTime Timestamp { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002A0AD File Offset: 0x000282AD
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x0002A0B5 File Offset: 0x000282B5
		public int ThreadId { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002A0BE File Offset: 0x000282BE
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x0002A0C6 File Offset: 0x000282C6
		public Activity Activity { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002A0CF File Offset: 0x000282CF
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x0002A0D7 File Offset: 0x000282D7
		public bool AssertOnLeaks { get; set; }

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002A0E0 File Offset: 0x000282E0
		public static CallStackRef Capture(int skipFrames, bool needStackInfo)
		{
			return new CallStackRef(new StackTrace(skipFrames + 1, needStackInfo));
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002A0F0 File Offset: 0x000282F0
		public static CallStackRef Capture(bool needStackInfo)
		{
			return CallStackRef.Capture(1, needStackInfo);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002A0F9 File Offset: 0x000282F9
		public void Dispose()
		{
			this.m_stackTrace = null;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002A104 File Offset: 0x00028304
		private CallStackRef(StackTrace stackTrace)
		{
			this.m_stackTrace = stackTrace;
			this.Timestamp = DateTime.UtcNow;
			this.ThreadId = Thread.CurrentThread.ManagedThreadId;
			this.Activity = UtilsContext.Current.Activity;
			this.AssertOnLeaks = true;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A150 File Offset: 0x00028350
		public void Dump([NotNull] TraceDump dumper)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TraceDump>(dumper, "dump");
			dumper.Add(this.ToString());
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A16C File Offset: 0x0002836C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, string.Concat(new string[]
			{
				"Creation time: {0:o}",
				Environment.NewLine,
				"Thread: {1}",
				Environment.NewLine,
				"ActivityId: {2}",
				Environment.NewLine,
				"ActivityType: {3}",
				Environment.NewLine,
				"Call stack: {4}"
			}), new object[]
			{
				this.Timestamp,
				this.ThreadId,
				this.Activity.ActivityId,
				this.Activity.ActivityType,
				this.StackTrace
			});
		}

		// Token: 0x04000499 RID: 1177
		private StackTrace m_stackTrace;
	}
}
