using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000031 RID: 49
	public abstract class Notification
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006E RID: 110
		public abstract Report Report { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006F RID: 111
		public abstract string Owner { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112
		public abstract Setting[] UserData { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000071 RID: 113
		public abstract int Attempt { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000072 RID: 114
		public abstract int MaxNumberOfRetries { get; }

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x06000073 RID: 115
		public abstract string Status { set; }

		// Token: 0x06000074 RID: 116
		public abstract void Save();

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		public abstract bool Retry { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000020AB File Offset: 0x000002AB
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000020B2 File Offset: 0x000002B2
		public virtual bool IsDataDriven
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
