using System;
using System.Globalization;

namespace Microsoft.Data.Common
{
	// Token: 0x02000177 RID: 375
	internal static class ActivityCorrelator
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00071A50 File Offset: 0x0006FC50
		internal static ActivityCorrelator.ActivityId Current
		{
			get
			{
				ActivityCorrelator.ActivityId activityId;
				if ((activityId = ActivityCorrelator.t_tlsActivity) == null)
				{
					activityId = (ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId(null, 1U));
				}
				return activityId;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00071A7C File Offset: 0x0006FC7C
		internal static ActivityCorrelator.ActivityId Next()
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.t_tlsActivity;
			Guid? guid = ((activityId != null) ? new Guid?(activityId.Id) : null);
			ActivityCorrelator.ActivityId activityId2 = ActivityCorrelator.t_tlsActivity;
			return ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId(guid, ((activityId2 != null) ? activityId2.Sequence : 0U) + 1U);
		}

		// Token: 0x04000B59 RID: 2905
		[ThreadStatic]
		private static ActivityCorrelator.ActivityId t_tlsActivity;

		// Token: 0x02000277 RID: 631
		internal sealed class ActivityId
		{
			// Token: 0x06001F47 RID: 8007 RVA: 0x0007FC50 File Offset: 0x0007DE50
			internal ActivityId(Guid? currentActivityId, uint sequence = 1U)
			{
				this.Id = currentActivityId ?? Guid.NewGuid();
				this.Sequence = sequence;
			}

			// Token: 0x06001F48 RID: 8008 RVA: 0x0007FC89 File Offset: 0x0007DE89
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Id, this.Sequence);
			}

			// Token: 0x0400176B RID: 5995
			internal readonly Guid Id;

			// Token: 0x0400176C RID: 5996
			internal readonly uint Sequence;
		}
	}
}
