using System;
using System.ComponentModel;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000072 RID: 114
	[Target("RepeatingWrapper", IsWrapper = true)]
	public class RepeatingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x00017678 File Offset: 0x00015878
		public RepeatingTargetWrapper()
			: this(null, 3)
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00017682 File Offset: 0x00015882
		public RepeatingTargetWrapper(string name, Target wrappedTarget, int repeatCount)
			: this(wrappedTarget, repeatCount)
		{
			base.Name = name;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00017693 File Offset: 0x00015893
		public RepeatingTargetWrapper(Target wrappedTarget, int repeatCount)
		{
			base.WrappedTarget = wrappedTarget;
			this.RepeatCount = repeatCount;
			base.OptimizeBufferReuse = base.GetType() == typeof(RepeatingTargetWrapper);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000176C4 File Offset: 0x000158C4
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000176CC File Offset: 0x000158CC
		[DefaultValue(3)]
		public int RepeatCount { get; set; }

		// Token: 0x06000917 RID: 2327 RVA: 0x000176D8 File Offset: 0x000158D8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.Repeat(this.RepeatCount, logEvent.Continuation, delegate(AsyncContinuation cont)
			{
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}
	}
}
