using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Scheduler
{
	// Token: 0x0200043E RID: 1086
	[Serializable]
	public sealed class ScheduledTaskSettings : ConfigurationClass
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0007D1C2 File Offset: 0x0007B3C2
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0007D1CA File Offset: 0x0007B3CA
		[ConfigurationProperty]
		public string PolicyName { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0007D1D3 File Offset: 0x0007B3D3
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x0007D1DB File Offset: 0x0007B3DB
		[ConfigurationProperty]
		public ConfigurationCollection<string> RequiredResources { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0007D1E4 File Offset: 0x0007B3E4
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0007D1EC File Offset: 0x0007B3EC
		[ConfigurationProperty]
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0007D1F5 File Offset: 0x0007B3F5
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0007D1FD File Offset: 0x0007B3FD
		[ConfigurationProperty]
		public bool IsCrashAllowedOnTimeout { get; set; } = true;

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0007D206 File Offset: 0x0007B406
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0007D20E File Offset: 0x0007B40E
		[ConfigurationProperty]
		public int TaskPriority
		{
			get
			{
				return this.m_TaskPriority;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 0.0);
				base.ValidateLessOrEqual((double)value, 10.0);
				this.m_TaskPriority = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0007D239 File Offset: 0x0007B439
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x0007D241 File Offset: 0x0007B441
		[ConfigurationProperty]
		public SchedulingFrequency ExecutionsFrequency { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0007D24A File Offset: 0x0007B44A
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0007D252 File Offset: 0x0007B452
		[ConfigurationProperty]
		public ConfigurationCollection<string> ExecutionFrequencyOffsets { get; set; }

		// Token: 0x04000B97 RID: 2967
		private int m_TaskPriority;
	}
}
