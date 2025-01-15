using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200008C RID: 140
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TelemetryModules
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00013755 File Offset: 0x00011955
		protected TelemetryModules()
		{
			this.Modules = new SnapshottingList<ITelemetryModule>();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00013768 File Offset: 0x00011968
		public static TelemetryModules Instance
		{
			get
			{
				return LazyInitializer.EnsureInitialized<TelemetryModules>(ref TelemetryModules.instance, () => new TelemetryModules());
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00013793 File Offset: 0x00011993
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001379B File Offset: 0x0001199B
		public IList<ITelemetryModule> Modules { get; private set; }

		// Token: 0x040001C0 RID: 448
		private static TelemetryModules instance;
	}
}
