using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200009E RID: 158
	public class StateCalculatedEventArgs : EventArgs
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001066F File Offset: 0x0000E86F
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00010677 File Offset: 0x0000E877
		public object Context { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00010680 File Offset: 0x0000E880
		public IList<string> Differentiators
		{
			get
			{
				return this.m_differentiators;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010688 File Offset: 0x0000E888
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00010690 File Offset: 0x0000E890
		public string StreamId { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010699 File Offset: 0x0000E899
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000106A1 File Offset: 0x0000E8A1
		public State StateOfDifferentiatorsSet { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000106AA File Offset: 0x0000E8AA
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000106B2 File Offset: 0x0000E8B2
		public DateTime TimeOfNotification { get; private set; }

		// Token: 0x06000469 RID: 1129 RVA: 0x000106BC File Offset: 0x0000E8BC
		internal StateCalculatedEventArgs(DateTime timeOfNotification, string streamId, string[] differentiators, State state, object context)
		{
			this.TimeOfNotification = timeOfNotification;
			this.Context = context;
			this.StreamId = streamId;
			this.m_differentiators = new string[differentiators.Length];
			differentiators.CopyTo(this.m_differentiators, 0);
			this.StateOfDifferentiatorsSet = state;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010708 File Offset: 0x0000E908
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Stream id: {0}. State of differentiators set: {1}. Differentiators: {2}. Context: {3}. Time of notification {4}", new object[]
			{
				this.StreamId,
				this.StateOfDifferentiatorsSet,
				this.Differentiators,
				this.Context ?? "No Context",
				this.TimeOfNotification
			});
		}

		// Token: 0x04000185 RID: 389
		private string[] m_differentiators;
	}
}
