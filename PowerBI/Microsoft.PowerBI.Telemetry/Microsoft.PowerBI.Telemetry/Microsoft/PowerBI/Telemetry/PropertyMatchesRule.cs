using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000013 RID: 19
	public class PropertyMatchesRule : IEventTransmissionRule
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B74 File Offset: 0x00000D74
		public PropertyMatchesRule(string name, HashSet<string> matches)
		{
			this.PropertyName = name;
			this.Matches = matches;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B8A File Offset: 0x00000D8A
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B92 File Offset: 0x00000D92
		private string PropertyName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B9B File Offset: 0x00000D9B
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002BA3 File Offset: 0x00000DA3
		private HashSet<string> Matches { get; set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002BAC File Offset: 0x00000DAC
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			string text = null;
			return telemetryEvent.Properties.TryGetValue(this.PropertyName, out text) && this.Matches.Contains(text);
		}
	}
}
