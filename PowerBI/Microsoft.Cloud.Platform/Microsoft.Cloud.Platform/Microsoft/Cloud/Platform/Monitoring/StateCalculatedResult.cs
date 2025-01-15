using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000093 RID: 147
	internal class StateCalculatedResult
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000EF11 File Offset: 0x0000D111
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000EF19 File Offset: 0x0000D119
		internal State State { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000EF22 File Offset: 0x0000D122
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000EF2A File Offset: 0x0000D12A
		internal DateTime Time { get; private set; }

		// Token: 0x06000424 RID: 1060 RVA: 0x0000EF33 File Offset: 0x0000D133
		public StateCalculatedResult(DateTime time, State state)
		{
			this.State = state;
			this.Time = time;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000EF49 File Offset: 0x0000D149
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Time: {0}. State {1}.", new object[] { this.Time, this.State });
		}
	}
}
