using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Defaults
{
	// Token: 0x02000039 RID: 57
	[ImmutableObject(true)]
	public sealed class DefaultActivityMonitor : IActivityMonitor<string>
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000827B File Offset: 0x0000647B
		private DefaultActivityMonitor()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008283 File Offset: 0x00006483
		public void RunActivitySync(string activityType, Action action)
		{
			action();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000828B File Offset: 0x0000648B
		public TValue RunActivitySync<TValue>(string activityType, Func<TValue> action)
		{
			return action();
		}

		// Token: 0x04000095 RID: 149
		public static readonly IActivityMonitor<string> Instance = new DefaultActivityMonitor();
	}
}
