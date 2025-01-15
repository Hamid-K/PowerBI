using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039A RID: 922
	[DataContract]
	internal class RequestTrackerOnPrimary
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x00064002 File Offset: 0x00062202
		internal void Start()
		{
			this._primaryStartTime = Stopwatch.GetTimestamp();
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0006400F File Offset: 0x0006220F
		internal void Stop()
		{
			this.TimeSpentInPrimary = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(Stopwatch.GetTimestamp() - this._primaryStartTime));
		}

		// Token: 0x04001322 RID: 4898
		[DataMember]
		internal TimeSpan TimeSpentInPrimary;

		// Token: 0x04001323 RID: 4899
		[DataMember]
		internal string PrimaryId;

		// Token: 0x04001324 RID: 4900
		[DataMember]
		internal string DisplayFriendlyNodeId;

		// Token: 0x04001325 RID: 4901
		private long _primaryStartTime;
	}
}
