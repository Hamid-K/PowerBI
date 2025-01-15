using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x0200001E RID: 30
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct ActivityContext : IEquatable<ActivityContext>
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004EB2 File Offset: 0x000030B2
		public ActivityContext(ActivityTraceId traceId, ActivitySpanId spanId, ActivityTraceFlags traceFlags, string traceState = null, bool isRemote = false)
		{
			this.TraceId = traceId;
			this.SpanId = spanId;
			this.TraceFlags = traceFlags;
			this.TraceState = traceState;
			this.IsRemote = isRemote;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004ED9 File Offset: 0x000030D9
		public ActivityTraceId TraceId { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004EE1 File Offset: 0x000030E1
		public ActivitySpanId SpanId { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004EE9 File Offset: 0x000030E9
		public ActivityTraceFlags TraceFlags { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004EF1 File Offset: 0x000030F1
		public string TraceState { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004EF9 File Offset: 0x000030F9
		public bool IsRemote { get; }

		// Token: 0x06000112 RID: 274 RVA: 0x00004F01 File Offset: 0x00003101
		public static bool TryParse(string traceParent, string traceState, out ActivityContext context)
		{
			if (traceParent == null)
			{
				context = default(ActivityContext);
				return false;
			}
			return Activity.TryConvertIdToContext(traceParent, traceState, out context);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004F18 File Offset: 0x00003118
		[NullableContext(1)]
		public static ActivityContext Parse(string traceParent, [Nullable(2)] string traceState)
		{
			if (traceParent == null)
			{
				throw new ArgumentNullException("traceParent");
			}
			ActivityContext activityContext;
			if (!Activity.TryConvertIdToContext(traceParent, traceState, out activityContext))
			{
				throw new ArgumentException(SR.InvalidTraceParent);
			}
			return activityContext;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004F4C File Offset: 0x0000314C
		public bool Equals(ActivityContext value)
		{
			return this.SpanId.Equals(value.SpanId) && this.TraceId.Equals(value.TraceId) && this.TraceFlags == value.TraceFlags && this.TraceState == value.TraceState && this.IsRemote == value.IsRemote;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004FBC File Offset: 0x000031BC
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is ActivityContext)
			{
				ActivityContext activityContext = (ActivityContext)obj;
				return this.Equals(activityContext);
			}
			return false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004FE3 File Offset: 0x000031E3
		public static bool operator ==(ActivityContext left, ActivityContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004FED File Offset: 0x000031ED
		public static bool operator !=(ActivityContext left, ActivityContext right)
		{
			return !(left == right);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004FFC File Offset: 0x000031FC
		public override int GetHashCode()
		{
			if (this == default(ActivityContext))
			{
				return 0;
			}
			int num = 5381;
			num = (num << 5) + num + this.TraceId.GetHashCode();
			num = (num << 5) + num + this.SpanId.GetHashCode();
			num = (int)((num << 5) + num + this.TraceFlags);
			return (num << 5) + num + ((this.TraceState == null) ? 0 : this.TraceState.GetHashCode());
		}
	}
}
